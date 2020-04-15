//------------------------------------------------------------------------------
//
// File: MSDKWebView
// Module: MSDK
// Date: 2020-03-31
// Hash: 8926e9d718efee0761be9349f7d68752
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace GCloud.MSDK
{
    #region MSDKWebVeiwRet
    public class MSDKWebViewRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private int msgType;

        private string msgJsonData;

        private float embedProgress;

        private string embedUrl;

        /// <summary>
		/// 100：关闭, 101:js 调用Native, 102：js 调用share 接口、103：js 调用sendMessage
        /// </summary>
        /// <value>The type of the message.</value>
        [JsonProp("msgType")]
        public int MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        /// <summary>
        /// JS 传回的json信息
        /// </summary>
        /// <value>The message json data.</value>
        [JsonProp("msgJsonData")]
        public string MsgJsonData
        {
            get { return msgJsonData; }
            set { msgJsonData = value; }
        }
        /// <summary>
        /// 嵌入式浏览器的加载进度
        /// </summary>
        /// <value>The progress of loading embedwebview.</value>
        [JsonProp("embedProgress")]
        public float EmbedProgress
        {
            get { return embedProgress; }
            set { embedProgress = value; }
        }
        /// <summary>
        /// 嵌入式浏览器加载进度对应的 URL
        /// </summary>
        /// <value>The url of embedwebview.</value>
        [JsonProp("embedUrl")]
        public string EmbedUrl
        {
            get { return embedUrl; }
            set { embedUrl = value; }
        }


        public MSDKWebViewRet()
        {
        }

        public MSDKWebViewRet(string param) : base(param)
        {
        }

        public MSDKWebViewRet(object json) : base(json)
        {
        }
#endif
    }
    #endregion

	public enum MSDKWebViewOrientation
	{
		Auto = 1,
		Portrait = 2, 
		Landscape = 3,
	}

    #region MSDKWebView

    public class MSDKWebView
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void openUrlAdapter([MarshalAs(UnmanagedType.LPStr)] string url,
            int screenType,
            bool isFullScreen,
            bool isUseURLEcode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson,
            bool isBrowser);
        
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
    	private static extern void openAmsCenterAdapter([MarshalAs(UnmanagedType.LPStr)] string gameName,
                                                        [MarshalAs(UnmanagedType.LPStr)] string actChannelId, 
                                                        [MarshalAs(UnmanagedType.LPStr)] string zoneId,
                                                        [MarshalAs(UnmanagedType.LPStr)] string platformId,
                                                        [MarshalAs(UnmanagedType.LPStr)] string partitionId,
                                                        [MarshalAs(UnmanagedType.LPStr)] string roleId , 
                                                        int screenType, 
                                                        [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern string getEncodeUrlAdapter([MarshalAs(UnmanagedType.LPStr)] string url);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void callJSAdapter([MarshalAs(UnmanagedType.LPStr)] string jsonJsPara);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void closeAdapter();

		/// <summary>
		/// webview的回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKWebViewRet> WebViewRetEvent;

        /// <summary>
        /// 打开指定网页
        /// </summary>
        /// <param name="url">网络链接</param>
		/// <param name="screenType">1 默认 2 竖屏 3 横屏 </param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="isUseURLEcode">使用 URL 编码方式，处理非 UTF-8 的字符</param>
        /// <param name="extraJson">扩展字段</param>
        /// <param name="isBrowser">使用系统自带的浏览器打开链接</param>
		public static void OpenUrl(string url, MSDKWebViewOrientation screenType = MSDKWebViewOrientation.Auto, bool isFullScreen = false,
		                           bool isUseURLEcode = true, string extraJson = "", bool isBrowser = false)
        {
            try
			{
				MSDKLog.Log ("OpenUrl url=" + url + " screenType=" + screenType+ " isFullScreen=" + isFullScreen+ " isUseURLEcode=" + isUseURLEcode
				             + " extraJson=" + extraJson+ " extraJson=" + extraJson+ " isBrowser=" + isBrowser);
#if UNITY_EDITOR

#else
				openUrlAdapter(url, (int)screenType, isFullScreen, isUseURLEcode, extraJson, isBrowser);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("OpenUrl with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

                /// <summary>
        /// 打开 ams 活动中心
        /// </summary>
        /// <param name="gameName"> 业务代码缩写，每个业务不同，各业务在接入AMS平台时由系统分配。 </param>
		/// <param name="actChannelId">活动渠道ID，由活动管理端后台分配，各业务登录[互娱AMS]管理端自行获取。</param>
        /// <param name="zoneId">用户区服信息，大区 1手Q，2微信</param>
        /// <param name="platformId">平台ID：iOS（0），安卓（1）</param>
        /// <param name="partitionId">小区ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="screenType">屏幕方向：1 默认 2 竖屏 3 横屏</param>
        /// <param name="extraJson">扩展字段，默认为空</param>

		public static void OpenAmsCenter(string gameName, 
                                   string actChannelId,
                                   string zoneId,
                                   string platformId,
                                   string partitionId,
                                   string roleId,
                                   MSDKWebViewOrientation screenType = MSDKWebViewOrientation.Auto,
		                           string extraJson = "")
        {
            try
			{
				MSDKLog.Log ("OpenAmsCenter gameName=" + gameName + " actChannelId=" + actChannelId+ " zoneId=" + zoneId+ " platformId=" + platformId
				             + " roleId=" + roleId+ " screenType=" + screenType+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				openAmsCenterAdapter(gameName, actChannelId, zoneId, platformId, partitionId, roleId ,(int)screenType, extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("OpenAmsCenter with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        

        /// <summary>
        /// 获取用 URL 编码之后的链接
        /// </summary>
        /// <param name="url">链接</param>
        /// <returns>返回 URL 编码之后的链接</returns>
        public static string GetEncodeUrl(string url)
        {
            try
			{
				MSDKLog.Log ("GetEncodeUrl url=" + url);
#if UNITY_EDITOR

#else
				return getEncodeUrlAdapter(url);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("GetEncodeUrl with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// javascript 调用
        /// </summary>
		/// <param name="jsonJsParam"> javascript json 格式数据入参</param>
        public static void CallJS(string jsonJsParam)
        {
            try
			{
				MSDKLog.Log ("CallJS jsonJsParam=" + jsonJsParam);
#if UNITY_EDITOR

#else
				callJSAdapter(jsonJsParam);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("CallJS with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 关闭嵌入式浏览器，暂不支持iOS和内置浏览器
        /// </summary>
        public static void Close()
        {
            try
            {
                MSDKLog.Log ("Close");
#if UNITY_EDITOR

#else
                closeAdapter();
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("CallJS with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        ///-------callback-----------
        internal static void OnWebViewRet(string json)
		{
			MSDKLog.Log ("OnWebViewRet  json= " + json);
			if (WebViewRetEvent != null)
			{
				var ret = new MSDKWebViewRet (json);
				try {
					WebViewRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			} else{
				MSDKLog.LogError ("No callback for WebViewRetEvent !");
			}
        }
        
#endif
    }
    #endregion
}