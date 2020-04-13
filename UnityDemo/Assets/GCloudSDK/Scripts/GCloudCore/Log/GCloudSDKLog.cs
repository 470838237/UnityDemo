using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Reflection;
namespace GCloud
{
    public class GCloudSDKLog : IGCloudSDKLog
    {

		public static readonly GCloudSDKLog Instance = new GCloudSDKLog();

		private GCloudSDKLog()
        {
        }

        public void SetLogLevel(string sdkName, ALogPriority logLevel)
        {
            GCloudSDKLog_setLogLevel(sdkName, (int)logLevel);
        }

        public void SetAllLogLevel(ALogPriority logLevel)
        {
            GCloudSDKLog_setAllLogLevel((int)logLevel);
        }

        #region Dllimport
        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GCloudSDKLog_setLogLevel(string sdkName, int logLevel);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GCloudSDKLog_setAllLogLevel(int logLevel);
        #endregion
    }


}
