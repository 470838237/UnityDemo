//------------------------------------------------------------------------------
//
// File: MSDKPush
// Module: MSDK
// Date: 2020-03-20
// Hash: f3495ac6c19db6a0334b313a238c4dd4
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace GCloud.MSDK
{
    #if UNITY_IOS
    public class MSDKLocalNotification : JsonSerializable
    {
        private int repeatType;

        private long fireTime;

        private int badge;

        private string alertBody;

        private string alertAction;
        /** 自定义参数，可以用来标识推送和增加附加信息 */
        private Dictionary<string,string>userInfo;

        [JsonProp ("repeatType")]
        public int RepeatType
        {
            get { return repeatType; }
            set { repeatType = value; }
        }

        [JsonProp ("fireTime")]
        public long FireTime {
            get { return fireTime; }
            set { fireTime = value; }
        }

        [JsonProp ("badge")]
        public int Badge {
            get { return badge; }
            set { badge = value; }
        }
        [JsonProp ("alertBody")]
        public string AlertBody {
            get { return alertBody; }
            set { alertBody = value; }
        }

        [JsonProp ("alertAction")]
        public string AlertAction {
            get { return alertAction; }
            set { alertAction = value; }
        }
        [JsonProp ("userInfo")]
        public Dictionary<string, string> UserInfo {
            get { return userInfo; }
            set { userInfo = value; }
        }

        public MSDKLocalNotification () { }
        public MSDKLocalNotification (string param) : base (param) { }
        public MSDKLocalNotification (object json) : base (json) { }
    }

#else
    public class MSDKLocalNotification : JsonSerializable
    {
        private int type;

        private int actionType;

        private int iconType;

        private int lights;

        private int ring;

        private int vibrate;

        private int styleID;

        private long builderID;

        private string content;

        private string customContent;

        private string activity;

        private string packageDownloadUrl;

        private string packageName;

        private string iconRes;

        private string date;

        private string hour;

        private string intent;

        private string min;

        private string title;

        private string url;

        private string ringRaw;

        private string smallIcon;

        [JsonProp("type")]
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        [JsonProp("actionType")]
        public int ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        [JsonProp("iconType")]
        public int IconType
        {
            get { return iconType; }
            set { iconType = value; }
        }

        [JsonProp("lights")]
        public int Lights
        {
            get { return lights; }
            set { lights = value; }
        }

        [JsonProp("ring")]
        public int Ring
        {
            get { return ring; }
            set { ring = value; }
        }

        [JsonProp("vibrate")]
        public int Vibrate
        {
            get { return vibrate; }
            set { vibrate = value; }
        }

        [JsonProp("styleID")]
        public int StyleId
        {
            get { return styleID; }
            set { styleID = value; }
        }

        [JsonProp("builderID")]
        public long BuilderId
        {
            get { return builderID; }
            set { builderID = value; }
        }

        [JsonProp("content")]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        [JsonProp("customContent")]
        public string CustomContent
        {
            get { return customContent; }
            set { customContent = value; }
        }

        [JsonProp("activity")]
        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        [JsonProp("packageDownloadUrl")]
        public string PackageDownloadUrl
        {
            get { return packageDownloadUrl; }
            set { packageDownloadUrl = value; }
        }

        [JsonProp("packageName")]
        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        [JsonProp("iconRes")]
        public string IconRes
        {
            get { return iconRes; }
            set { iconRes = value; }
        }

        [JsonProp("date")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        [JsonProp("hour")]
        public string Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        [JsonProp("intent")]
        public string Intent
        {
            get { return intent; }
            set { intent = value; }
        }

        [JsonProp("min")]
        public string Min
        {
            get { return min; }
            set { min = value; }
        }

        [JsonProp("title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [JsonProp("url")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        [JsonProp("ringRaw")]
        public string RingRaw
        {
            get { return ringRaw; }
            set { ringRaw = value; }
        }

        [JsonProp("smallIcon")]
        public string SmallIcon
        {
            get { return smallIcon; }
            set { smallIcon = value; }
        }

        public MSDKLocalNotification()
        {
        }

        public MSDKLocalNotification(string param) : base(param)
        {
        }

        public MSDKLocalNotification(object json) : base(json)
        {
        }
    }
#endif
    public class MSDKPushRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private int type;

        private string notification;

        /// <summary>
		/// 收到消息类型 0：点击进入-通知， 1：前台时收到- 通知 ，2:点击进入-本地通知 3:前台收到 - 本地通知
        /// </summary>
        /// <value>The type.</value>
        [JsonProp("type")]
        public int Type
        {
            get { return type; }
            set { type = value; }
        }


        /// <summary>
		/// 收到消息内容
        /// </summary>
        /// <value>The notification.</value>
        [JsonProp("notification")]
        public string Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        public MSDKPushRet()
        {
        }

        public MSDKPushRet(string param) : base(param)
        {
        }

        public MSDKPushRet(object json) : base(json)
        {
        }
#endif
    }

    public class MSDKPush
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void registerPushAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void unregisterPushAdapter([MarshalAs(UnmanagedType.LPStr)] string channel);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void setTagAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string tag);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void setAccountAdapter([MarshalAs(UnmanagedType.LPStr)] string channel, [MarshalAs(UnmanagedType.LPStr)] string account);


		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void deleteAccountAdapter([MarshalAs(UnmanagedType.LPStr)] string channel, [MarshalAs(UnmanagedType.LPStr)] string account);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void deleteTagAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string tag);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void addLocalNotificationAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string localNotificationJson);


        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void clearLocalNotificationsAdapter([MarshalAs(UnmanagedType.LPStr)] string channel);


#if UNITY_IPHONE
		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void deleteLocalNotificationsAdapter ([MarshalAs (UnmanagedType.LPStr)] string channel,
		                                                              [MarshalAs (UnmanagedType.LPStr)] string key);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void addLocalNotificationAtFrontAdapter ([MarshalAs (UnmanagedType.LPStr)] string channel,
		                                                                 [MarshalAs (UnmanagedType.LPStr)] string localNotificationJson);
#endif

		/// <summary>
		/// Push的基本回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKBaseRet> PushBaseRetEvent;

		/// <summary>
		/// Push通知的回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKPushRet> PushNotificationEvent;


        /// <summary>
        /// 注册推送，完成之后会返回设备的 token，用于后台推送标识设备唯一性
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
		/// <param name="account"> 用户的帐号（别名）。上报用户的帐号（别名），以便支持按帐号（别名）推送。如果填 NULL，使用登录成功的openID 作为账号（别名） </param>
        public static void RegisterPush(string channel, string account="")
        {
            try
			{
				MSDKLog.Log ("RegisterPush channel=" + channel + " account=" +account);
#if UNITY_EDITOR

#else
				registerPushAdapter(channel, account);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("RegisterPush with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
		/// 注销设备,此台设备不接收推送
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
        public static void UnregisterPush(string channel)
        {
            try
			{
				MSDKLog.Log ("UnregisterPush channel=" + channel );
#if UNITY_EDITOR

#else
				unregisterPushAdapter(channel);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("UnregisterPush with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
		/// 设置标签，个性化推送使用，针对不同用户设置不同标签
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
        /// <param name="tag">标签</param>
        public static void SetTag(string channel, string tag)
        {
            try
			{
				MSDKLog.Log ("SetTag channel=" + channel + " tag=" + tag);
#if UNITY_EDITOR

#else
                setTagAdapter(channel, tag);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("SetTag with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        
        /// <summary>
		/// 删除标签，删除设置过的标签
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
        /// <param name="tag">标签</param>
        public static void DeleteTag(string channel, string tag)
        {
            try
			{
				MSDKLog.Log ("DeleteTag channel=" + channel + " tag=" + tag);
#if UNITY_EDITOR

#else
				deleteTagAdapter(channel, tag);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("DeleteTag with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 设置账号
		/// </summary>
		/// <param name="channel">渠道信息，比如 XG，Firebase</param>
		/// <param name="tag">账号</param>
		public static void SetAccount(string channel, string account)
		{
			try
			{
				MSDKLog.Log ("SetAccount channel=" + channel + " account=" + account);
				#if UNITY_EDITOR
				
				#else
				setAccountAdapter(channel, account);
				#endif
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("SetAccount with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 删除账号
		/// </summary>
		/// <param name="channel">渠道信息，比如 XG，Firebase</param>
		/// <param name="tag">账号</param>
		public static void DeleteAccount(string channel, string account)
		{
			try
			{
				MSDKLog.Log ("DeleteAccount channel=" + channel + " account=" + account);
#if UNITY_EDITOR
				
#else
				deleteAccountAdapter(channel, account);
#endif
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("DeleteAccount with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

        /// <summary>
		/// 新增本地推送
		/// 本地通知的数量是有限制的，最近的本地通知最多只能有64个，超过将被系统忽略
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
        /// <param name="localNotification">本地推送消息结构体</param>
        public static void AddLocalNotification(string channel, MSDKLocalNotification localNotification)
        {
            try
            {
				var localNotificationJson = localNotification.ToString();
				MSDKLog.Log("AddLocalNotification channel="+ channel + " localNotificationJson=" + localNotificationJson);
#if UNITY_EDITOR

#else
				addLocalNotificationAdapter(channel, localNotificationJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("AddLocalNotification with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
		/// 清空全部本地推送
        /// </summary>
        /// <param name="channel">渠道信息，比如 XG，Firebase</param>
        public static void ClearLocalNotifications(string channel)
        {
            try
			{
				MSDKLog.Log ("ClearLocalNotifications channel=" + channel);
#if UNITY_EDITOR

#else
                clearLocalNotificationsAdapter(channel);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("ClearLocalNotifications with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

#if UNITY_IPHONE || UNITY_IOS
		/// <summary>
		/// 删除指定本地推送
		/// </summary>
        /// <param name="localNotification">本地推送消息结构体</param>
		public static void DeleteLocalNotifications (string key)
		{
			try {
				MSDKLog.Log ("DeleteLocalNotification:" + key);
#if UNITY_EDITOR

#else
				deleteLocalNotificationsAdapter("", key);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("DeleteLocalNotifications with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 添加前台可显示的本地推送
		/// </summary>
		/// <param name="key">设置通知参数时userInfo指定的key</param>
		public static void AddLocalNotificationAtFront (MSDKLocalNotification localNotification)
		{
			try {
				string localNotificationJson = MiniJSON.Json.Serialize (localNotification);
				MSDKLog.Log ("AddLocalNotificationAtFront channel=" + "" + " localNotificationJso= " + localNotificationJson);
#if UNITY_EDITOR

#else
				addLocalNotificationAtFrontAdapter ("", localNotificationJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("AddLocalNotificationAtFront with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

#endif
		///-------callback-----------
        internal static void OnPushBaseRet(string json)
		{
			MSDKLog.Log ("OnPushBaseRet json= " + json);
            var ret = new MSDKBaseRet(json);
			if (PushBaseRetEvent != null)
            {
				try {
					PushBaseRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for PushBaseRetEvent !");
			}

        }

        internal static void OnPushRet (string json)
		{
			MSDKLog.Log ("OnPushRet json= " + json);
			if (PushNotificationEvent != null) {
				MSDKPushRet ret = new MSDKPushRet (json);
				try {
					PushNotificationEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for PushNotificationEvent !");
			}
		}
#endif
    }
    
}