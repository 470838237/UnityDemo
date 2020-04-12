#if UNITY_ANDROID || UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
namespace GCloud
{
    public class GTraceWrapper : IGTraceWrapper
    {

        public static readonly GTraceWrapper Instance = new GTraceWrapper();

        private GTraceWrapper()
        {
        }

        public String GetTraceId()
        {
#if UNITY_ANDROID || UNITY_IOS
            StringBuilder strBuilder = new StringBuilder(128);
            Boolean result = gcloudcore_gtrace_GetTraceId(strBuilder, 128);
            string str = strBuilder.ToString();
            if (result == true && str.Length > 0)
            {
                return str;
            }
            return null;
#else
            return null;
#endif

        }

        public String CreateContext(String parentContext, String privateType)
        {
#if UNITY_ANDROID || UNITY_IOS
            StringBuilder strBuilder = new StringBuilder(128);
            Boolean result = gcloudcore_gtrace_CreateContext(parentContext,privateType,strBuilder, 128);
            string str = strBuilder.ToString();
            if (result == true && str.Length > 0)
            {
                return str;
            }
            return null;
#else
            return null;
#endif
        }

        public void SpanStart(String context, String name, String caller, String callee)
        {
#if UNITY_ANDROID || UNITY_IOS
            gcloudcore_gtrace_SpanStart(context, name, caller, callee);
#endif
        }

        public void SpanFlush(String context, String key, String value)
        {
#if UNITY_ANDROID || UNITY_IOS
            gcloudcore_gtrace_SpanFlush(context, key, value);
#endif
        }

        public void SpanFinish(String context, String errCode, String errMsg)
        {
#if UNITY_ANDROID || UNITY_IOS
            gcloudcore_gtrace_SpanFinish(context, errCode, errMsg);
#endif

        }




#if UNITY_ANDROID || UNITY_IOS

#region Dllimport
        [DllImport(GCloudCoreCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Boolean gcloudcore_gtrace_GetTraceId([MarshalAs(UnmanagedType.LPStr)]StringBuilder value, int len);

        [DllImport(GCloudCoreCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Boolean gcloudcore_gtrace_CreateContext(string parentContext, string privateType,[MarshalAs(UnmanagedType.LPStr)]StringBuilder value, int len);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloudcore_gtrace_SpanStart(string context, string name, string caller, string callee);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloudcore_gtrace_SpanFlush(string context, string key, string value);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void gcloudcore_gtrace_SpanFinish(string context, string errCode, string errMsg);
#endregion

#endif

    }

}
#endif
