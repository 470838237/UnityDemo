using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TDM
{
	class TDataMaster : ITDataMaster
	{
		public TDataMaster()
		{ }

		private TBufferWriter mWriter = new TBufferWriter();
        private Object mLocker = new Object();

        //-----------------------  TDataMaster  API  ----------------------- 
        #region TDataMaster API

        public override bool Initialize(string appId, string appChannel,bool isTest=false)
		{
			TLog.TDebug("TDataMaster::Initialize");

#if UNITY_IOS || UNITY_ANDROID
			TLog.TWarning("Android/iOS don not need to call u3d initialize.");
			return true;
#else
			return tdm_initialize(appId, appChannel, isTest);
#endif
		}

		public override void EnableReport(bool enable)
		{
			TLog.TDebug("TDataMaster::EnableReport");
			tdm_enable_report(enable);
		}
		public override void ReleaseInstance()
		{
			TLog.TDebug("TDataMaster::ReleaseInstance");
			tdm_release_instance ();
		}
		public override string GetTDMUID()
		{
			TLog.TDebug("TDataMaster::GetTDMUID");
			string str = Marshal.PtrToStringAnsi(tdm_get_uid());
			return str;
		}

		public override void SetLogLevel(TLogPriority level)
		{
			TLog.TDebug("TDataMaster::SetLogLevel, level" + level);
			TLog.Level = level;
			tdm_set_log((int)level);
		}

		public override void ReportEvent(int srcId, string eventName, CustomInfo eventInfo)
		{
			TLog.TDebug("TDataMaster::ReportEvent");
			if (eventInfo != null)
			{
                lock(mLocker)
                {
                    mWriter.Reset();
                    if (eventInfo.Encode(mWriter))
                    {
                        byte[] buffer = mWriter.GetBufferData();
                        if (buffer != null && buffer.Length > 0)
                        {
                            tdm_report_event(srcId, eventName, buffer, buffer.Length, false);
                        }
                        else
                        {
                            TLog.TError("ReportEvent, Get Encode Buffer Error");
                        }
                    }
                    else
                    {
                        TLog.TError("ReportEvent, EventInfo Encode Error");
                    }
                }
			}
            else{
                TLog.TError("ReportEvent, EventInfo is Null");
            }
		}

		public override void ReportBinary(int srcId, string eventName, byte[] data, int len)
		{
			TLog.TDebug("TDataMaster::ReportBinary");
			if (data != null)
			{
				tdm_report_event(srcId, eventName, data, len, true);
			}
		}

        public override string GetSessionID()
        {
            TLog.TDebug("TDataMaster::GetSessionID");
            string str = Marshal.PtrToStringAnsi(tdm_get_session_id());
            return str;
        }

		public override void SetRouterAddress(bool isTest, string url)
        {
            TLog.TDebug("TDataMaster::SetRouterAddress");
            tdm_set_router_address(isTest, url);
        }

        #endregion


        #region DllImport

#if (UNITY_IOS || UNITY_XBOX360 || UNITY_SWITCH) && !UNITY_EDITOR
		internal const string LibName = "__Internal";
#else
        internal const string LibName = "TDataMaster";
#endif

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool tdm_initialize([MarshalAs(UnmanagedType.LPStr)]string appID, [MarshalAs(UnmanagedType.LPStr)]string appChannel, bool test);

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void tdm_enable_report(bool enable);

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr tdm_get_uid();

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void tdm_set_log(int level);

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void tdm_report_event(int srcId, [MarshalAs(UnmanagedType.LPStr)]string eventName, byte[] data, int len, bool isBinary);

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void tdm_release_instance();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr tdm_get_session_id();

		[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void tdm_set_router_address(bool isTest, [MarshalAs(UnmanagedType.LPStr)]string url);

        #endregion
    };
}
