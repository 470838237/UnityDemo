//------------------------------------------------------------------------------
//
// File: MSDKFriend
// Module: MSDK
// Date: 2020-03-31
// Hash: edc7ed8bfa815c01583b12dfdc47322f
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GCloud.MSDK
{
    public class MSDKExtendRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
        //标记是哪个渠道的回调
        private string channel;
        //标记是Extend插件中哪个方法回调回来的
        private string extendMethodName;

		/// <summary>
		/// Extend渠道
		/// </summary>
		/// <value>The channel of Extend.</value>
        [JsonProp("channel")]
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        /// <summary>
        /// Extend方法名
        /// </summary>
        /// <value>The method name of Extend.</value>
        [JsonProp("extend_method_name")]
        public string ExtendMethodName
        {
            get { return extendMethodName; }
            set { extendMethodName = value; }
        }

        public MSDKExtendRet()
        {
        }

        public MSDKExtendRet(string param) : base(param)
        {
        }

        public MSDKExtendRet(object json) : base(json)
        {
        }
#endif
    }

    #region Extend

    public class MSDKExtend
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern string extendInvokeAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string extendMethodName, [MarshalAs(UnmanagedType.LPStr)] string paramsJson);

		/// <summary>
		/// Extend回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKExtendRet> ExtendRetEvent;

        /// <summary>
        /// Extend 调用
        /// </summary>
		/// <param name="info">消息结构体，包含需要分享的内容</param>
        /// <param name="channel">发送消息的渠道，比如 weChat、QQ</param>
		public static string Invoke(string channel, string extendMethodName, string paramsJson)
        {
            string result = "";
            try
            {
				MSDKLog.Log("Extend Invoke channel=" + channel + " extendMethodName=" + extendMethodName + " paramsJson=" + paramsJson);
#if UNITY_EDITOR

#else
				result = extendInvokeAdapter(channel, extendMethodName, paramsJson);
#endif

            }
            catch (Exception ex)
            {
				MSDKLog.LogError("MSDKExtend Invoke with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
            return result;
        }

        ///-------callback-----------
		internal static void OnExtendRet(string json)
		{
			MSDKLog.Log ("OnExtendRet json= " + json);
			if (ExtendRetEvent != null)
			{
				var ret = new MSDKExtendRet (json);
				try{
                    ExtendRetEvent(ret);
				}
				catch(Exception e)
				{
					MSDKLog.LogError (e.StackTrace);
				}
            } else
			{
				MSDKLog.LogError ("No callback for ExtendRetEvent !");
			}
        }
#endif
    }

    #endregion
}