using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using GCloud;
using GCloud.LockStep;

namespace GCloud.LockStep
{
	
	public class LockStepTest {
		
		public void Initialize(string url, LockStepCreateRoomInitInfo initInfo)
		{
			byte[] data;
			if(!initInfo.Encode(out data))
			{
				return;
			}
			gcloud_lockstep_test_init_create_room(url, data, data.Length);
		}

        public bool IsCreatingRoom()
		{
			return gcloud_lockstep_test_is_creating_room();
		}

        public bool IsFinishedCreated()
		{
			return gcloud_lockstep_test_finished_created_room();
		}

        public LockStepRoomUserCollection GetRoomUserCollection()
		{
			byte[] buf = new byte[102400];
			int used = buf.Length;
			if(gcloud_lockstep_test_get_room_detail_info(buf, ref used))
			{
				LockStepRoomUserCollection co = new LockStepRoomUserCollection();
				if(co.Decode(buf))
				{
					return co;
				}
				else
				{
					ADebug.LogError("GetRoomUserCollection decode error");
				}
			}
			else
			{
				ADebug.LogError("GetRoomUserCollection gcloud_lockstep_test_get_room_detail_info return false");
			}
			return null;
		}
		
		// Update is called once per frame
        public void Update()
        {
			
			gcloud_loackstep_test_update();
		}
		
		
		#region DllImplort
		
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern  void gcloud_lockstep_test_init_create_room(string url, byte[] buf, int len);
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern  bool gcloud_lockstep_test_is_creating_room();
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern  bool gcloud_lockstep_test_finished_created_room();
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_lockstep_test_get_room_detail_info(byte[] buf, ref int size);
		[DllImport(GCloudCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		private static extern  void gcloud_loackstep_test_update();
		#endregion
	}

	public class LockStepCreateRoomInitInfo : ApolloBufferBase
	{
		public int UserCount;
		public int FrameIntervalTimeMs = 66;
		public int WaitTimeMs;
		public bool AutoStartFrame;
		public string BusinessId;
		public string BusinessKey;

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(UserCount);
			writer.Write(FrameIntervalTimeMs);
			writer.Write(WaitTimeMs);
			writer.Write(AutoStartFrame);
			writer.Write(BusinessId);
			writer.Write(BusinessKey);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref UserCount);
			reader.Read(ref FrameIntervalTimeMs);
			reader.Read(ref WaitTimeMs);
			reader.Read(ref AutoStartFrame);
			reader.Read(ref BusinessId);
			reader.Read(ref BusinessKey);
		}
	};


	public class LockStepRoomUserCollection : ApolloBufferBase
	{
		public List<string> RoomUserCollection;
		
		public void Clear()
		{
			RoomUserCollection.Clear();
		}

		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(RoomUserCollection);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref RoomUserCollection);
		}
	};
}
