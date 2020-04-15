using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace GCloud
{
    delegate void UnitySendMessageDelegate(IntPtr obj, IntPtr method, IntPtr msg);
    delegate void LogDelegate(LogPriority pri, IntPtr msg);

	class GCloud : IGCloud
    {
        [MonoPInvokeCallback(typeof(LogDelegate))]
        static void OnApolloLogDelegate(LogPriority pri, IntPtr msg)
        {
            GCloud instance = GCloud.Instance as GCloud;

			if (instance.logEvent != null)
            {
				instance.logEvent(pri, Marshal.PtrToStringAnsi(msg));
            }
        }
        
        private event LogHandler logEvent;

		public override void SetLogger(LogPriority pri, LogHandler callback)
		{
			switch(pri)
			{
			case LogPriority.None:
				ADebug.Level = ADebug.LogPriority.None;
				break;
			case LogPriority.Event:
				ADebug.Level = ADebug.LogPriority.Event;
				break;
			case LogPriority.Error:
				ADebug.Level = ADebug.LogPriority.Error;
				break;
			default:
				ADebug.Level = ADebug.LogPriority.Info;
				break;
			}
			logEvent = callback;
			gcloud_setApolloLogger(pri, OnApolloLogDelegate);
		}

        public GCloud()
        {
            Tx.Instance.Initialize();

#if UNITY_EDITOR_OSX
            setEnginePluginPath();
#endif
        }

        ~GCloud()
        {
        }
        
        #region Service
		public override Result Initialize(InitializeInfo initInfo)
        {
			GCloudCommon.InitializeInfo = initInfo;
			if(initInfo == null)
            {
                throw new Exception("ApolloInfo could not be null!!");
            }
            

			byte[] buffer;

			if(!initInfo.Encode(out buffer))
			{
				return new Result(ErrorCode.InnerError);
			}
			
			return new Result((ErrorCode)gcloud_init(buffer, buffer.Length));
        }

		
		public override bool SwitchPlugin(string pluginName)
		{
			return gcloud_switchplugin(pluginName);
		}
		
		
		public override IConnector CreateConnector(ConnectorType type, bool manualUpdate, bool autoReconnect, bool autoLogin)
		{
			ADebug.Log("CreateApolloConnection");

			
			if (GCloudCommon.InitializeInfo == null)
			{
				throw new Exception("IGCloud.Instance.Initialize must be called first!");
			}

			GCloudConnector connector = new GCloudConnector(type, manualUpdate, autoReconnect, autoLogin);
			return connector;
		}
		
		public override void DestroyConnector(IConnector connector)
		{
			ADebug.Log("DestroyConnector");
			if (connector == null) {
				return ;
			}
			
			GCloudConnector con = connector as GCloudConnector;
			if (con != null)
			{
				con.Destroy();
			}
		}

		public override void SetUserInfo (UserInfo userInfo)
		{

			if(userInfo == null)
			{
                ADebug.Log("userInfo is null!");
                return;
			}
			
			
			byte[] buffer;
			
			if(!userInfo.Encode(out buffer))
			{
				ADebug.Log("userInfo enCode failed!");
				return;
			}
			gcloud_setUserInfo (buffer,buffer.Length);
		}


        private void setEnginePluginPath()
        {
#if UNITY_EDITOR_OSX
            string path = Application.dataPath;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(path);
            if (null != data)
            {
                gcloud_set_engine_plugin_path(data, data.Length);
            }
#endif
        }
        #endregion

        #region Dllimport
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloud_setLogLevel(LogPriority pri);
		
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern int gcloud_init( byte[] data, int len);
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool gcloud_switchplugin([MarshalAs(UnmanagedType.LPStr)] string pluginName);
        
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloud_setApolloLogger(LogPriority pri, [MarshalAs(UnmanagedType.FunctionPtr)] LogDelegate callback);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_setUserInfo(byte[] data, int len);

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloud_set_engine_plugin_path(byte[] path, int len);

        #endregion
    }
}
