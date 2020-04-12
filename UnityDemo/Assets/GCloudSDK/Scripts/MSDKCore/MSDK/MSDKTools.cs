//------------------------------------------------------------------------------
//
// File: MSDKTools
// Module: MSDK
// Date: 2020-03-20
// Hash: 594206c090beb5d21e05c86355ff780e
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GCloud.MSDK
{
	public class MSDKToolsFreeFlowInfo : JsonSerializable
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private string ipAll;

		private string ipConnect;

		private	int isFree;

		private	int ccType;

		[JsonProp ("ip_all")]
		public string IPAll
		{
			get { return ipAll; }
			set { ipAll = value; }
		}

		[JsonProp ("ipConnect")]
		public string IPConnect
		{
			get { return ipConnect; }
			set { ipConnect = value; }
		}

		[JsonProp ("is_free")]
		public int ISFree
		{
			get { return isFree; }
			set { isFree = value; }
		}

		[JsonProp ("cc_type")]
		public int CCType
		{
			get { return ccType; }
			set { ccType = value; }
		}

		
		public MSDKToolsFreeFlowInfo () { }
		public MSDKToolsFreeFlowInfo (string param) : base (param) { }
		public MSDKToolsFreeFlowInfo (object json) : base (json) { }
#endif
	}

	public class MSDKToolsRet : MSDKBaseRet
	{	
#if GCLOUD_MSDK_WINDOWS
#else
		private string link;
		
		/// <summary>
		/// 返回的 Url
		/// </summary>
		/// <value>The link.</value>
		[JsonProp("link")]
		public string Link
		{
			get { return link; }
			set { link = value; }
		}

		public MSDKToolsRet () { }
		public MSDKToolsRet (string param) : base (param) { }
		public MSDKToolsRet (object json) : base (json) { }
#endif
	}

	public class MSDKToolsFreeFlowRet : MSDKBaseRet
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private MSDKToolsFreeFlowInfo freeFlowInfo;

		[JsonProp ("freeflow_info")]
		public MSDKToolsFreeFlowInfo FreeFlowInfo
		{
			get { return freeFlowInfo; }
			set { freeFlowInfo = value; }
		}
		public MSDKToolsFreeFlowRet () { }
		public MSDKToolsFreeFlowRet (string param) : base (param) { }
		public MSDKToolsFreeFlowRet (object json) : base (json) { }
#endif
	}


    public class MSDKTools
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool isAppInstalledAdapter([MarshalAs(UnmanagedType.LPStr)] string channels,[MarshalAs (UnmanagedType.LPStr)] string extra);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void openDeepLinkAdapter([MarshalAs(UnmanagedType.LPStr)] string link);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool convertShortUrlAdapter([MarshalAs(UnmanagedType.LPStr)] string url,
                                                          [MarshalAs(UnmanagedType.LPStr)] string typeMark);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool getFreeFlowInfoAdapter([MarshalAs(UnmanagedType.LPStr)] string key,
		                                           [MarshalAs(UnmanagedType.LPStr)] string extra);
         
		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void openPrajnaWebViewAdapter([MarshalAs(UnmanagedType.LPStr)] string jsonStr);
        
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void reportPrajnaAdapter([MarshalAs(UnmanagedType.LPStr)] string serialNumber);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void showRatingAlertAdapter();

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern String getConfigChannelAdapter();

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern string getConfigAdapter([MarshalAs(UnmanagedType.LPStr)] string key);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool updateConfigAdapter([MarshalAs(UnmanagedType.LPStr)] string configs);

		/// <summary>
		/// Tools的回调接口
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKToolsRet>  ToolsRetEvent;

        /// <summary>
        /// Notice的回调接口
        /// </summary>
        public static event OnMSDKRetEventHandler<MSDKToolsFreeFlowRet>  ToolsFreeFlowRetEvent;
        /// <summary>
		/// 
        /// 检查app 是否安装。安卓传packageName,
        /// iOS可以传URLScheme，或者传渠道名称比如"WeChat"，"QQ"
        /// </summary>
        /// <returns><c>true</c>, if app installed was installed, <c>false</c> otherwise.</returns>
        /// <param name="channel">Channel.</param>
        public static bool IsAppInstalled(string channel,string extra="")
        {
            try
            {
                MSDKLog.Log("IsAppInstalled channel=" + channel);
#if UNITY_EDITOR

#else
				return isAppInstalledAdapter(channel,extra);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("IsAppInstalled with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
            return false;
        }

        /// <summary>
        /// 根据当前登录态，打开对应应用deeplink (deeplink功能的开通和配置请联系各平台)
        /// 具体跳转deeplink。以微信为例，可填写为：
        /// INDEX：跳转微信游戏中心首页
        /// DETAIL：跳转微信游戏中心详情页
        /// LIBRARY：跳转微信游戏中心游戏库
        /// </summary>
        /// <param name="link">具体跳转的link （需要在微信侧先配置好此link）</param>
        public static void OpenDeepLink(string link)
        {
            try
            {
                MSDKLog.Log("OpenDeepLink link=" + link);
#if UNITY_EDITOR

#else
                openDeepLinkAdapter(link);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("OpenDeepLink with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        ///  转短链
        /// </summary>
        /// <returns><c>true</c>, if short URL was converted, <c>false</c> otherwise.</returns>
        /// <param name="url">目标 URL</param>
        /// <param name="typeMark">url类型标记，支持A-Z中26个大写英文字母，与open管理后台相对应。获取短链跳转时，通过此参数获取正确的404跳转地址。默认值：A</param>
        public static bool ConvertShortUrl(string url, string typeMark="A")
        {
            try
            {
                MSDKLog.Log("ConvertShortUrl url=" + url + " typeMark=" + typeMark);
#if UNITY_EDITOR

#else
                return convertShortUrlAdapter(url, typeMark);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("ConvertShortUrl with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
            return false;
        }

		/// <summary>
		///  
		/// </summary>
		/// <returns><c>true</c> 调用成功 <c>false</c> 调用成功 调用失败，参数错误或没有登录态</returns>
		/// <param name="key">目标 URL</param>
		/// <param name="extra">默认参数，暂未使用</param>
		public static bool GetFreeFlowInfo(string key, string extra="")
		{
			try
			{
				MSDKLog.Log("getFreeFlowInfo key=" + key);
#if UNITY_EDITOR
                return false;
#else
				return getFreeFlowInfoAdapter(key, extra);
#endif
            }
            catch (Exception ex)
			{
				MSDKLog.LogError("getFreeFlowInfo with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
			return false;
		}
        /// <summary>
        /// 中控相关，打开中控WebView，目前支持国内渠道，海外渠道暂不支持。
        /// </summary>
        /// <param name="jsonStr">中控下发的json string</param>
        public static void OpenPrajnaWebView(string jsonStr)
        {
            try {
                MSDKLog.Log ("OpenPrajnaWebView");
#if UNITY_EDITOR
                
#else
                openPrajnaWebViewAdapter(jsonStr);
#endif
            } catch (Exception ex) {
                MSDKLog.LogError ("OpenPrajnaWebView with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 中控相关，上报Prajna serinal number，目前支持国内渠道，海外渠道暂不支持。
        /// </summary>
        /// <param name="serialNumber">中控下发的serialNumber</param>
        public static void ReportPrajna(string serialNumber)
        {
            try {
                MSDKLog.Log ("ReportPrajna");
#if UNITY_EDITOR
                
#else
                reportPrajnaAdapter(serialNumber);
#endif
            } catch (Exception ex) {
                MSDKLog.LogError ("OpenPrajna with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
		/// <summary>
		/// 显示应用内评分，支持iOS
		/// </summary>
		public static void ShowRatingAlert()
		{
			try {
				MSDKLog.Log ("ShowRatingAlert");
#if UNITY_EDITOR

#else
				showRatingAlertAdapter();
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("OpenPrajna with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}
        /// <summary>
        ///  获取渠道包打包信息
        /// </summary>
        /// <returns> </returns>
        public static string GetConfigChannel()
        {
            try
            {
#if UNITY_ANDROID
                string channel = getConfigChannelAdapter();
                MSDKLog.Log("GetConfigChannel =" + channel);
                return channel;

#else
                return "";
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("GetConfigChannel with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
            return "";
        }

        
        /// <summary>
        /// 获取游戏配置。支持int、bool、string、long类型
        /// </summary>
        /// <returns>The configs.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defValue">Def value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetConfig<T>(string key, T defValue)
        {
            UnityEngine.Debug.Log("GetConfig key=" + key + " defValue=" + defValue + " typeof(T)=" + typeof(T));
            string val = string.Empty;
#if UNITY_EDITOR
            val = UnityEditorData.GetConfig(key);
#else
            val = getConfigAdapter(key);
#endif
            UnityEngine.Debug.Log("GetConfig string val=" + val);
            if(string.IsNullOrEmpty(val))
            {
                return defValue;
            }
            if (typeof(T) == typeof(bool))
            {
                bool boolVal = val.Equals("1");
                return (T)System.Convert.ChangeType(boolVal, typeof(bool));
            }
            return (T)System.Convert.ChangeType(val, typeof(T)); 
        }

        /// <summary>
        /// 更新游戏配置
        /// </summary>
        /// <param name="configsDic">配置信息</param>
        public static bool UpdateConfig(Dictionary<string, string> configsDic)
        {
            try
            {
                var config = MiniJSON.Json.Serialize(configsDic);
                MSDKLog.Log("UpdateConfig config = " + config);
#if UNITY_EDITOR
                return true;
#else
                return updateConfigAdapter (config);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("UpdateConfig with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        ///-------callback-----------
		internal static void OnToolsRet (string json)
		{
			MSDKLog.Log ("OnToolsRet  json= " + json);
			if (ToolsRetEvent != null) {
				var ret = new MSDKToolsRet (json);
				try {
					ToolsRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for OnToolsRet !");
			}
		}

        internal static void OnToolsFreeFlowRet (string json)
		{
			MSDKLog.Log ("OnToolsFreeFlowRet  json= " + json);
			if (ToolsFreeFlowRetEvent != null) {
				var ret = new MSDKToolsFreeFlowRet (json);
				try {
					ToolsFreeFlowRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for OnToolsFreeFlowRet !");
			}
		}
#endif
    }
}