//------------------------------------------------------------------------------
//
// File: MSDKReport
// Module: MSDK
// Date: 2020-03-31
// Hash: 321f00b3bff0908f354730944cc00758
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GCloud.MSDK
{
#if GCLOUD_MSDK_WINDOWS

#else
	public class MSDKReport
	{
		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool reportInitAdapter ([MarshalAs (UnmanagedType.LPStr)] string channels);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void reportEventAdapter ([MarshalAs (UnmanagedType.LPStr)] string eventName,
			[MarshalAs (UnmanagedType.LPStr)] string paramsJson,
			[MarshalAs (UnmanagedType.LPStr)] string eventNspChannelsame,
			bool isRealTime);
		
		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void reportSetPushTokenAdapter ([MarshalAs (UnmanagedType.LPStr)] string pushToken);

		/// <summary>
		/// 上报初始化接口
		/// </summary>
        /// <param name="channel">初始化渠道列表，以 "," 分割</param>
		/// <returns>返回初始化状态</returns>
		public static bool Init (string channel)
		{
			try {
                MSDKLog.Log ("Init channel=" + channel);
#if UNITY_EDITOR

#else
                return reportInitAdapter (channel);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("Init with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}

			return false;
		}

		/// <summary>
		/// 上报事件
		/// </summary>
		/// <param name="eventName">事件名称</param>
		/// <param name="paramsDic">参数</param>
		/// <param name="spChannels">指定渠道，若无可填空字符串</param>
		/// <param name="isRealTime">是否实时上报</param>
		public static void ReportEvent (string eventName, Dictionary<string, string> paramsDic,
		                                string spChannels="", bool isRealTime=true)
		{
			try {
				string paramsJson = MiniJSON.Json.Serialize (paramsDic);
				MSDKLog.Log ("ReportEvent eventName=" + eventName + " paramsJson="+paramsJson + " spChannels=" + spChannels + " isRealTime=" + isRealTime);
#if UNITY_EDITOR

#else
				reportEventAdapter (eventName, paramsJson, spChannels, isRealTime);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("ReportEvent with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 设置PushToken，可选接口，一般向Ajust、AppsFlyer插件设置firebase token，用于卸载追踪
		/// </summary>
		/// <param name="pushToken">push token</param>
		public static void SetPushToken (string pushToken)
		{
			try {
				MSDKLog.Log ("SetPushToken pushToken=" + pushToken);
#if UNITY_EDITOR

#else
				reportSetPushTokenAdapter (pushToken);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("SetPushToken with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}
	}
#endif
}
