//------------------------------------------------------------------------------
//
// File: MSDKLogin
// Module: MSDK
// Date: 2020-03-20
// Hash: da438a765fe58470082c279d743ca7a9
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GCloud.MSDK
{
#if GCLOUD_MSDK_WINDOWS
	public class MSDKQrCodeRet : MSDKBaseRet
	{
		//渠道ID
		private int channelID;

		//渠道名
		private string channel;

		//二维码链接
		private string qrCodeUrl;

		/// <summary>
		/// 渠道ID
		/// </summary>
		/// <value>The channel id.</value>
		[JsonProp("channelID")]
		public int ChannelID
		{
			get { return channelID; }
			set { channelID = value; }
		}

		/// <summary>
		/// 渠道名
		/// </summary>
		/// <value>The channel.</value>
		[JsonProp("channel")]
		public string Channel
		{
			get { return channel; }
			set { channel = value; }
		}

		/// <summary>
		/// 二维码链接
		/// </summary>
		/// <value>The qrCode url.</value>
		[JsonProp("qrCodeUrl")]
		public string QrCodeUrl
		{
			get { return qrCodeUrl; }
			set { qrCodeUrl = value; }
		}

		public MSDKQrCodeRet()
		{
		}

		public MSDKQrCodeRet(string param) : base(param)
		{
		}

		public MSDKQrCodeRet(object json) : base(json)
		{
		}
	}
#endif

    public class MSDKLoginRet : MSDKBaseRet
    {
        // 用户 ID
        private string openID;

        private string token;

        private long tokenExpire;

        private int firstLogin;

        private string regChannelDis;

        private string userName;

        private int gender;

        private string birthdate;

        private string pictureUrl;

        private string pf;

        private string pfKey;

        private bool realNameAuth;

        private int channelID;

        private string channel;

        private string channelInfo;

        private string confirmCode;

        private long confirmCodeExpireTime;

        private string bindList;

        /// <summary>
        /// 用户 ID
        /// </summary>
        /// <value>The open identifier.</value>
        [JsonProp("openid")]
        public string OpenId
        {
            get { return openID; }
            set { openID = value; }
        }

        /// <summary>
        /// 用户 凭证
        /// </summary>
        /// <value>The token.</value>
        [JsonProp("token")]
        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        /// <value>The token expire.</value>
        [JsonProp("token_expire_time")]
        public long TokenExpire
        {
            get { return tokenExpire; }
            set { tokenExpire = value; }
        }

        /// <summary>
        /// 是否首次登陆，未知-1，否0，是1
        /// </summary>
        /// <value>The first login.</value>
        [JsonProp("first")]
        public int FirstLogin
        {
            get { return firstLogin; }
            set { firstLogin = value; }
        }

        /// <summary>
        /// 首次注册的分发渠道
        /// </summary>
        /// <value>The reg channel dis.</value>
        [JsonProp("reg_channel_dis")]
        public string RegChannelDis
        {
            get { return regChannelDis; }
            set { regChannelDis = value; }
        }

        /// <summary>
        ///  昵称
        /// </summary>
        /// <value>The name of the user.</value>
        [JsonProp("user_name")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 性别(int) 0未定义,1男, 2女
        /// </summary>
        /// <value>The gender.</value>
        [JsonProp("gender")]
        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// 出生日期(1987-2-23 11:33:33)
        /// </summary>
        /// <value>The birthdate.</value>
        [JsonProp("birthdate")]
        public string Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }


        /// <summary>
        //// 头像链接
        /// </summary>
        /// <value>The picture URL.</value>
        [JsonProp("picture_url")]
        public string PictureUrl
        {
            get { return pictureUrl; }
            set { pictureUrl = value; }
        }

        /// <summary>
        /// pf 值
        /// </summary>
        /// <value>The pf.</value>
        [JsonProp("pf")]
        public string Pf
        {
            get { return pf; }
            set { pf = value; }
        }

        // pf key
        [JsonProp("pfKey")]
        public string PfKey
        {
            get { return pfKey; }
            set { pfKey = value; }
        }

        // 是否需要实名认证
        [JsonProp("need_name_auth")]
        public bool RealNameAuth
        {
            get { return realNameAuth; }
            set { realNameAuth = value; }
        }

        // 渠道ID
        [JsonProp("channelid")]
        public int ChannelId
        {
            get { return channelID; }
            set { channelID = value; }
        }

        // 渠道名
        [JsonProp("channel")]
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        // 第三方渠道登录信息
        [JsonProp("channel_info")]
        public string ChannelInfo
        {
            get { return channelInfo; }
            set { channelInfo = value; }
        }

        /// <summary>
        /// 确认码，绑定失败后返回
        /// </summary>
        /// <value>The Confirm Code.</value>
        [JsonProp("confirm_code")]
        public string ConfirmCode
        {
            get { return confirmCode; }
            set { confirmCode = value; }
        }

        /// <summary>
        /// 确认码过期时间戳
        /// </summary>
        /// <value>The Confirm Code Expire Time.</value>
        [JsonProp("confirm_code_expire_time")]
        public long ConfirmCodeExpireTime
        {
            get { return confirmCodeExpireTime; }
            set { confirmCodeExpireTime = value; }
        }

        /// <summary>
        /// 绑定信息(JSON 数据，数组类型)
        /// </summary>
        /// <value>The Bind List.</value>
        [JsonProp("bind_list")]
        public string BindList
        {
            get { return bindList; }
            set { bindList = value; }
        }

        public MSDKLoginRet()
        {
        }

        public MSDKLoginRet(string param) : base(param)
        {
        }

        public MSDKLoginRet(object json) : base(json)
        {
        }
    }

    #region Login

    // 登录接口类
    public class MSDKLogin
    {
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void loginAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string permissions,
            [MarshalAs(UnmanagedType.LPStr)] string subChannel,
             [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void logoutAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
                                                  [MarshalAs(UnmanagedType.LPStr)] string subChannel,
                                                  bool channelOnly);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern string getLoginRetAdapter();

        [DllImport(MSDK.LibName)]
        private static extern void autoLoginAdapter();
		
#if GCLOUD_MSDK_WINDOWS
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void unInstallAdapter();
#else

        [DllImport(MSDK.LibName)]
        private static extern void bindAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
                                                [MarshalAs(UnmanagedType.LPStr)] string permissions,
                                                [MarshalAs(UnmanagedType.LPStr)] string subChannel,
                                                [MarshalAs(UnmanagedType.LPStr)] string extraJson);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void connectAdapter([MarshalAs(UnmanagedType.LPStr)] string channel,
			[MarshalAs(UnmanagedType.LPStr)] string permissions,
			[MarshalAs(UnmanagedType.LPStr)] string subChannel,
			[MarshalAs(UnmanagedType.LPStr)] string extraJson);

		[DllImport(MSDK.LibName)]
		private static extern void unconnectAdapter([MarshalAs(UnmanagedType.LPStr)] string channel);

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern string getConnectRetAdapter();

        [DllImport(MSDK.LibName)]
        private static extern bool switchUserAdapter(bool useLaunchUser);

        [DllImport(MSDK.LibName)]
        private static extern void queryUserInfoAdapter();

        [DllImport(MSDK.LibName)]
        private static extern void loginWithConfirmcodeAdapter(int actionType, [MarshalAs(UnmanagedType.LPStr)] string confirmcode, [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName)]
        private static extern void resetGuestAdapter();

        
        [DllImport(MSDK.LibName)]
        private static extern void bindUIAdapter([MarshalAs(UnmanagedType.LPStr)] string extraJson);
        
         [DllImport(MSDK.LibName)]
        private static extern void loginUIAdapter([MarshalAs(UnmanagedType.LPStr)] string extraJson);

#endif

        /// <summary>
        /// 登出回调、应用唤醒回调
        /// </summary>
        public static event OnMSDKRetEventHandler<MSDKBaseRet> LoginBaseRetEvent;

        /// <summary>
        //// 登录回调，包括 login、bind、autologin、switchuser 等
        /// </summary>
        public static event OnMSDKRetEventHandler<MSDKLoginRet> LoginRetEvent;

		/// <summary>
		//// 关联（Connect）回调，暂时只有Kwai渠道
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKLoginRet> ConnectRetEvent;

        public virtual void onLoginBaseRetEvent(MSDKBaseRet ret)
        {
            LoginBaseRetEvent(ret);
        }

        public virtual void onLoginRetEvent(MSDKLoginRet ret)
        {
            LoginRetEvent(ret);
        }

		public virtual void onConnectRetEvent(MSDKLoginRet ret)
		{
			ConnectRetEvent(ret);
		}

#if GCLOUD_MSDK_WINDOWS
        /// <summary>
        /// PC端登录二维码回调
        /// </summary>
        public static event OnMSDKRetEventHandler<MSDKQrCodeRet> QrCodeRetEvent;

        public virtual void onQrCodeRetEvent(MSDKQrCodeRet ret)
        {
            QrCodeRetEvent(ret);
        }
#endif
        /// <summary>
        /// 登录指定渠道
        /// </summary>
        /// <param name="channel">渠道信息，比如“WeChat”、“QQ”、“Facebook”.</param>
        /// <param name="permissions">登录授权权限列表，多个权限用逗号分隔，比如 user_info,inapp_friends.</param>
        /// <param name="subChannel">子渠道.</param>
        /// <param name="extraJson">Extra json.</param>
        public static void Login(string channel, string permissions = "", string subChannel = "", string extraJson = "")
        {
            try
            {
                MSDKLog.Log("login channel= " + channel + " permissions=" + permissions + " subChannel=" + subChannel + " extraJson=" + extraJson);
#if GCLOUD_MSDK_WINDOWS
                loginAdapter(channel, permissions, subChannel, extraJson);
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				loginAdapter(channel, permissions, subChannel,extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("Login with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        public static void Logout(string channel = "", string subChannel = "", bool channelOnly = false)
        {
            try
            {
                MSDKLog.Log("Logout channel=" + channel + " subChannel=" + subChannel + " channelOnly=" + channelOnly);
#if GCLOUD_MSDK_WINDOWS
                logoutAdapter(channel, subChannel, channelOnly);
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                UnityEditorLogout ();
#else
				logoutAdapter(channel,subChannel,channelOnly);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("Logout with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定关联指定渠道
        /// </summary>
        /// <param name="channel">渠道信息，比如“WeChat”、“QQ”、“Facebook”.</param>
        /// <param name="permissions">登录授权权限列表.</param>
        /// <param name="subChannel">子渠道.</param>
        /// <param name="extraJson">Extra json.</param>
        public static void Bind(string channel, string permissions = "", string subChannel = "", string extraJson = "")
        {
            try
            {
                MSDKLog.Log("Bind channel=" + channel + " permissions=" + permissions + " subChannel=" + subChannel + " extraJson=" + extraJson);
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN

#else
                bindAdapter (channel, permissions, subChannel, extraJson);
#endif

            }
            catch (Exception ex)
            {
                MSDKLog.LogError("Bind with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// Connect到指定渠道
		/// </summary>
		/// <param name="channel">渠道信息，比如目前只有Kwai渠道.</param>
		/// <param name="permissions">登录授权权限列表，多个权限用逗号分隔，比如 user_info,inapp_friends.</param>
		/// <param name="subChannel">子渠道.</param>
		/// <param name="extraJson">Extra json.</param>
		public static void Connect(string channel, string permissions = "", string subChannel = "", string extraJson = "")
		{
			try
			{
				MSDKLog.Log("connect channel= " + channel + " permissions=" + permissions + " subChannel=" + subChannel + " extraJson=" + extraJson);
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
				UnityEditorConnect (MSDKMethodNameID.MSDK_LOGIN_CONNECT, channel, subChannel);
#else
				connectAdapter(channel, permissions, subChannel,extraJson);
#endif
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("Connect with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 取消关联（connect）
		/// </summary>
		/// <param name="channel">渠道信息，比如目前只有Kwai渠道</param>
		public static  void Unconnect(string channel)
		{
			try
			{
				MSDKLog.Log("unconnect channel= " + channel);
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN

#else
				unconnectAdapter(channel);
#endif
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("unconnect with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 获取本地Connect的状态。同步调用
		/// </summary>
		public static MSDKLoginRet GetConnectRet()
		{
			try
			{
#if GCLOUD_MSDK_WINDOWS
				string retJson = "";
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
				string retJson = UnityEditorData.GetLoginData ((int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT);
#else
				string retJson = getConnectRetAdapter ();
#endif
				MSDKLog.Log("GetConnectRet retJson= " + retJson);
				if (!string.IsNullOrEmpty(retJson))
					return new MSDKLoginRet(retJson);
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("GetConnectRet with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}

			return null;
		}


        /// <summary>
        /// 获取本地登录态。同步调用
        /// </summary>
        public static MSDKLoginRet GetLoginRet()
        {
            try
            {
#if GCLOUD_MSDK_WINDOWS
                string retJson = getLoginRetAdapter();
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                string retJson = UnityEditorData.GetLoginData ((int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT);
#else
				string retJson = getLoginRetAdapter ();
#endif
                MSDKLog.Log("GetLoginRet retJson= " + retJson);
                if (!string.IsNullOrEmpty(retJson))
                    return new MSDKLoginRet(retJson);
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("GetLoginRet with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }

            return null;
        }
        
#if GCLOUD_MSDK_WINDOWS
        /// <summary>
        /// 卸载MSDK
        /// </summary>
        public static void UnInstall()
        {
            try
            {
                MSDKLog.Log("UnInstall");
                unInstallAdapter();
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("UnInstall with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
#endif
        /// <summary>
        /// 自动登录&刷新票据
        /// </summary>
        public static void AutoLogin()
        {
            try
            {
                MSDKLog.Log("AutoLogin");
#if GCLOUD_MSDK_WINDOWS
                autoLoginAdapter();
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                UnityEditorLogin(MSDKMethodNameID.MSDK_LOGIN_AUTOLOGIN);
#else
				autoLoginAdapter ();
#endif

            }
            catch (Exception ex)
            {
                MSDKLog.LogError("AutoLogin with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 出现异账号情况下，选择是否切换异账号登录
        /// </summary>
        /// <returns><c> 操作成功标志；不符合异账号切换的情况下会返回 false</returns>
        /// <param name="useLaunchUser">是否切换账号登录</param>
        public static bool SwitchUser(bool useLaunchUser)
        {
            try
            {
                MSDKLog.Log("SwitchUser useLaunchUser=" + useLaunchUser);
#if GCLOUD_MSDK_WINDOWS
#else
                return switchUserAdapter(useLaunchUser);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("SwitchUser with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }

            return false;
        }
        /// <summary>
        /// 查询个人账号信息
        /// </summary>
        public static void QueryUserInfo()
        {
            try
            {
                MSDKLog.Log("QueryUserInfo");
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                UnityEditorLogin(MSDKMethodNameID.MSDK_LOGIN_QUERYUSERINFO);
#else
				queryUserInfoAdapter ();
#endif

            }
            catch (Exception ex)
            {
                MSDKLog.LogError("QueryUserInfo with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 登录或者绑定失败后，通过确认码再次尝试登录
        /// </summary>
        /// <param name="actionType">操作类型：0 一般登录（暂不支持）；1 恢复渠道账号绑定的游客，并登录到游客.</param>
        /// <param name="confirmcode">确认码；绑定失败的情况下，由后台返回。缺省值为上次绑定失败时 后台的返回值.</param>
        /// <param name="extraJson">确认码；绑定失败的情况下，由后台返回。缺省值为上次绑定失败时 后台的返回值.</param>
		public static void LoginWithConfirmCode(int actionType = 0, string confirmcode = "", string extraJson = "")
        {
            try
            {
                MSDKLog.Log("LoginWithConfirmCode confirmcode= " + confirmcode + " actionType" + actionType + " extraJson" + extraJson);
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE);
#else
				loginWithConfirmcodeAdapter (actionType, confirmcode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("LoginWithConfirmCode with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 重置游客
        /// </summary>
        public static void ResetGuest()
        {
            try
            {
                MSDKLog.Log("ResetGuest Account");
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE);
#else
				resetGuestAdapter();
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("LoginWithConfirmCode with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// UI 登录
        /// </summary>
        public static void LoginUI(string extraJson = "")
        {
            try
            {
                MSDKLog.Log("LoginUI");
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE);
#else
				loginUIAdapter(extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("LoginWithConfirmCode with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// UI 绑定
        /// </summary>
        public static void BindUI(string extraJson = "")
        {
	        try
	        {
		        MSDKLog.Log("BindUI");
#if UNITY_EDITOR
		        //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE);
#else
				bindUIAdapter(extraJson);
#endif
	        }
	        catch (Exception ex)
	        {
		        MSDKLog.LogError("LoginWithConfirmCode with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
	        }
        }
        
        #region Simulator data
        private static void UnityEditorLogin(MSDKMethodNameID methodId, string channel = "", string subChannel = "")
        {
#if GCLOUD_MSDK_WINDOWS

#else
            string retJson = UnityEditorData.GetLoginData((int)methodId, channel, subChannel);
            if (!string.IsNullOrEmpty(retJson))
            {
                MSDKMessageCenter.OnMSDKRet((int)methodId, retJson);
            }
            else MSDKLog.Log("Simulator data is empty. channel=" + channel);
#endif
        }

		private static void UnityEditorConnect(MSDKMethodNameID methodId, string channel = "", string subChannel = "")
		{
#if GCLOUD_MSDK_WINDOWS

#else
			string retJson = UnityEditorData.GetLoginData((int)methodId, channel, subChannel);
			if (!string.IsNullOrEmpty(retJson))
			{
				MSDKMessageCenter.OnMSDKRet((int)methodId, retJson);
			}
			else MSDKLog.Log("Simulator data is empty. channel=" + channel);
#endif
		}


        private static void UnityEditorLogout()
        {
#if GCLOUD_MSDK_WINDOWS
#else
            string retJson = UnityEditorData.GetLogoutData();
            if (!string.IsNullOrEmpty(retJson))
                MSDKMessageCenter.OnMSDKRet((int)MSDKMethodNameID.MSDK_LOGIN_LOGOUT, retJson);
            else MSDKLog.Log("Simulator Logout data is empty.");
#endif
        }
        #endregion Simulator data

        #region callback
        internal static void OnLoginBaseRet(string json)
        {
            MSDKLog.Log("OnLoginBaseRet json= " + json);
            if (LoginBaseRetEvent != null)
            {
                var ret = new MSDKBaseRet(json);
                try
                {
                    LoginBaseRetEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for LoginBaseRetEvent !");
            }
        }

        internal static void OnLoginRet(string json)
        {
            MSDKLog.Log("OnLoginRet json= " + json);
            if (LoginRetEvent != null)
            {
                var ret = new MSDKLoginRet(json);
                try
                {
                    LoginRetEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for LoginRetEvent !");
            }
        }

		internal static void OnConnectRet(string json)
		{
			MSDKLog.Log("OnConnectRet json= " + json);
			if (ConnectRetEvent != null)
			{
				var ret = new MSDKLoginRet(json);
				try
				{
					ConnectRetEvent(ret);
				}
				catch (Exception e)
				{
					MSDKLog.LogError(e.StackTrace);
				}
			}
			else
			{
				MSDKLog.LogError("No callback for ConnectRetEvent !");
			}
		}

#if GCLOUD_MSDK_WINDOWS
        internal static void OnQrCodeRet(string json)
        {
            MSDKLog.Log("OnQrcodeRet json= " + json);
            if (QrCodeRetEvent != null)
            {
                var ret = new MSDKQrCodeRet(json);
                try
                {
                    QrCodeRetEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for QrCodeRetEvent !");
            }
        }
#endif

        #endregion callback

        // wakeup callback
        public static MSDKLoginRet GetWakeUpLoginRet()
        {
#if GCLOUD_MSDK_WINDOWS
			return null;
#else
            return MSDKMessageCenter.Instance.GetWakeUpLoginRet();
#endif
        }
        public static void ClearWakeUpLoginRet()
        {
#if GCLOUD_MSDK_WINDOWS
#else
            MSDKMessageCenter.Instance.ClearWakeUpLoginRet();
#endif
        }
    }

    #endregion Login
}