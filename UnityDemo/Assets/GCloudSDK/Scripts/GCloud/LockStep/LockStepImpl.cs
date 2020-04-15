using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace GCloud.LockStep
{
	public class LockStepImpl : LockSteper, ILockStep {
		LockStepInitializeInfo initInfo;
		IBufferStrategy bufferStrategy;
		
		public override event StateChangedEventHandler StateChangedEvent;
		public override event BroadcastEventHandler BroadcastEvent;
		
		public override UInt32 PlayerID
		{
			get
			{
				return gcloud_lockstep_get_playerID();
			}
		}
		
		public override bool HasLogined
		{
			get
			{
				return gcloud_lockstep_has_logined();
			}
		}
		public override bool HasStarted
		{
			get
			{
				return gcloud_lockstep_has_started();
			}
		}

        public override LockStepStatus GetStatus()
        {
            return gcloud_lockstep_get_status();
        }

        public override bool Initialize(LockStepInitializeInfo initInfo)
		{
			base.Initialize(false, true);
			return Initialize(initInfo, null);
		}
		
		public bool Initialize(LockStepInitializeInfo initInfo, IBufferStrategy bufferStrategy)
		{
			// just want to initialize Gcloud
			IGCloud gcloud = IGCloud.Instance;
			ADebug.Log("Initialize" + (gcloud == null));
			
			if(initInfo != null)
			{
				this.initInfo = initInfo;
				
				byte[] buffer;
				if(initInfo.Encode(out buffer) && buffer != null)
				{
					bool ret = gcloud_lockstep_init(buffer, buffer.Length);
					if(!ret)
					{
						return false;
					}
				}
				else
				{
					ADebug.LogError("LockStep Initialize Encode error");
					return false;
				}
			}
			else
			{
				ADebug.LogError("LockStep Initialize is null");
				return true;
			}
			
			if(bufferStrategy != null)
			{
				this.bufferStrategy = bufferStrategy;
			}
			else
			{
				this.bufferStrategy = new DefaultBufferStrategy();
			}
			
			if(this.bufferStrategy != null)
			{
				this.bufferStrategy.Steper = this;
			}
			
			return true;
		}
		
		public override void Login(byte[] accessInfo, LoginEventHandler callback)
		{
			loginEventHandler = callback;
			gcloud_lockstep_login(accessInfo, accessInfo == null ? 0 : accessInfo.Length);
		}
		
		// disconnect from server
		public override void Logout(LogoutEventHandler callback)
		{
			logoutEventHandler  = callback;
			gcloud_lockstep_logout();
		}
		
		// Send operation data to server
		public override bool SendBroadcast(byte[] data, int len = -1, bool rawUdp = true, LockStepBroadcastFlag flag = LockStepBroadcastFlag.None)
		{
			if(len == -1)
			{
				len = data == null ? 0:data.Length;
			}
			return gcloud_lockstep_send_broadcast(data, len, rawUdp, (int)flag);
		}
		
		// Send operation data to server
		public bool SendToServer(byte[] data, int len = -1, bool rawUdp = true)
		{
			if(len == -1)
			{
				len = data == null ? 0:data.Length;
			}
			return gcloud_lockstep_send_to_server(data, len, rawUdp);
		}
		
		// Send operation data to server
		public override bool Input(byte[] data, bool rawUdp = true, LockStepInputFlag flag = LockStepInputFlag.None)
		{
			return Input(data, -1, rawUdp, flag);
		}
		
		public override bool Input(byte[] data, int len, bool rawUdp = true, LockStepInputFlag flag = LockStepInputFlag.None)
		{
			if(bufferStrategy != null)
			{
				return bufferStrategy.OnInput(data, len, rawUdp, flag);
			}
			else
			{
				ADebug.LogError("Input bufferStrategy is null");
				return false;
			}
		}
		
		// Send operation data to server
		public bool Write(byte[] data, int len, bool rawUdp = true, LockStepInputFlag flag = LockStepInputFlag.None)
		{
			if(data != null)
			{
				if(len == -1 || len > data.Length)
				{
					len = data.Length;
				}
				return gcloud_lockstep_input(data, len, rawUdp, (int)flag);
			}
			return false;
		}
		public override  FrameInfo PopFrame()
		{	
			if(!hasReady)
			{
				return null;
			}
			
			if(bufferStrategy != null)
			{
				return bufferStrategy.OnPopFrame();
			}
			else
			{
				ADebug.LogError("Input bufferStrategy is null");
				return null;
			}
		}
		byte[] recvBuffer;
		
		ApolloBufferReader reader;
		// return the next frame data from the cache
        public override bool ReadFrame(FrameInfo frame)
		{
			if(frame == null)
			{
				return false;
			}
			
			if(recvBuffer == null)
			{
				recvBuffer = new byte[initInfo.MaxBufferSize];
				reader = new ApolloBufferReader(recvBuffer);
			}
			
			int length = recvBuffer.Length;
			if(gcloud_lockstep_popframe(recvBuffer, ref length))
			{
				reader.ResetPosition();
				
				if (!frame.Decode(reader))
				{
					return false;
				}
				
				return true;
			}
			
			return false;
		}
		
		bool hasReady = false;
		public override void Ready(ReadyEventHandler callback)
		{
			readyEventHandler = callback;
			hasReady = false;
			gcloud_lockstep_ready();
		}

        public override bool ReconnectManually()
        {
            return gcloud_lockstep_reconnect_manually();
        }

        public void Seek(int start, int count, int frameIntervalMs)
		{
			gcloud_lockstep_seek(start, count, frameIntervalMs);
		}
		
		protected override void OnUpdate(float deltaTime)
		{
			gcloud_lockstep_update((int)(deltaTime*10000));
			
			if(bufferStrategy != null)
			{
				bufferStrategy.Update();
			}
		}
		
		public override int GetCurrentMaxFrameId()
		{
			return gcloud_lockstep_get_current_max_frameId();
		}
		
		public override void SetStartFrame(int frameId)
		{
			gcloud_lockstep_set_start_point(frameId);
		}

		public override void GetSpeed(ref int udp, ref int tcp)
		{
			gcloud_lockstep_get_speed(ref udp, ref tcp);
		}

        public override bool SetUserData(string key, string value)
        {
            return gcloud_set_user_data(key, value);
        }

        public override void NotifyLoadingBegin()
        {
            gcloud_notify_loading_begin();
        }

        public override void NotifyLoadingEnd()
        {
            gcloud_notify_loading_end();
        }

        #region Callback

		LoginEventHandler loginEventHandler;
		void OnLoginProc(byte[] data)
		{
			ADebug.Log("OnLoginProc");
			
			if(data != null)
			{
				LockStepResult result = new LockStepResult();
				if(result.Decode(data))
				{
					if(loginEventHandler != null)
					{
						loginEventHandler(result);
					}
					
				}
				else
				{
					ADebug.LogError("OnLoginProc Decoce Error");
				}
			}
			else
			{
				ADebug.LogError("OnLoginProc data is null");
			}
		}
		
		LogoutEventHandler logoutEventHandler;
		void OnLogoutProc(byte[] data)
		{
			ADebug.Log("OnLogoutProc");
			
			if(data != null)
			{
				LockStepResult result = new LockStepResult();
				if(result.Decode(data))
				{
					if(logoutEventHandler != null)
					{
						logoutEventHandler(result);
					}
					
				}
				else
				{
					ADebug.LogError("OnLogoutProc Decoce Error");
				}
			}
			else
			{
				ADebug.LogError("OnLogoutProc data is null");
			}
		}
		
		ReadyEventHandler readyEventHandler;
		void OnReadyProc(byte[] data)
		{
			ADebug.Log("OnReadyProc");
			
			if(data != null)
			{
				LockStepResult result = new LockStepResult();
				if(result.Decode(data))
				{
					if(readyEventHandler != null)
					{
						readyEventHandler(result);
					}
					
					if(result.IsSuccess())
					{
						hasReady = true;
					}
				}
				else
				{
					ADebug.LogError("OnReadyProc Decoce Error");
				}
			}
			else
			{
				ADebug.LogError("OnReadyProc data is null");
			}
		}
		
		void OnBroadcastProc(byte[] data)
		{
			//ADebug.Log("OnBroadcastProc");
			
			if(data != null)
			{
				FrameCollection frames = new FrameCollection();
				if(frames.Decode(data))
				{
					if(BroadcastEvent != null)
					{
						BroadcastEvent(frames);
					}
				}
				else
				{
					ADebug.LogError("OnBroadcastProc Decoce Error");
				}
			}
			else
			{
				ADebug.LogError("OnBroadcastProc data is null");
			}
		}
		
		void OnRecvedFrameProc(int numberOfReceivedFrames)
		{
			ADebug.Log("OnRecvedFrameProc:" + numberOfReceivedFrames);
		}
		
		void OnStateChangedProc(int state, byte[] data)
		{
			ADebug.Log("OnStateChangedProc:" + state);
			if(data != null)
			{
				LockStepResult result = new LockStepResult();
				if(result.Decode(data))
				{
					if(StateChangedEvent != null)
					{
						StateChangedEvent((LockStepState)state, result);
					}
				}
				else
				{
					ADebug.LogError("OnStateChangedProc Decoce Error");
				}
			}
			else
			{
				ADebug.LogError("OnStateChangedProc data is null");
			}
		}
		#endregion
		
		#region Dllimport
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_init( byte[] data, int len);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_update(int fps);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_ready();

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool gcloud_lockstep_reconnect_manually();

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_set_start_point(int startPoint);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_seek(int start, int count, int frameIntervalMs);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern LockStepStatus gcloud_lockstep_get_status();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_has_started();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_has_logined();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern int gcloud_lockstep_get_current_max_frameId();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern UInt32 gcloud_lockstep_get_playerID();
		
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_getresult(byte[] buf, ref int size);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_login(byte[] receipt, int len);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_logout();
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_input( byte[] data, int len, bool rawUdp, int flag);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_send_broadcast( byte[] data, int len, bool rawUdp, int flag);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_send_to_server( byte[] data, int len, bool rawUdp);
		
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_popframe(byte[] buf, ref int size);
		
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_lockstep_get_speed(ref int udp, ref int tcp);

        [DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool gcloud_set_user_data(string key, string value);

        [DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloud_notify_loading_begin();

        [DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloud_notify_loading_end();
        #endregion
    }
}