//------------------------------------------------------------------------------
//
// File: MSDKAccount
// Module: MSDK
// Date: 2020-03-20
// Hash: 37be8bb48cd2884649396d53dd7f6f47
// Author: qingcuilu@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GCloud.MSDK
{
#if GCLOUD_MSDK_WINDOWS

#else

   public class MSDKAccountRet : MSDKBaseRet
    {
        private int channelID;

        private string channel;
        
        private string seqID;
        
        private string username;
        
        private string uid;
        
        private string token;
        
        private string expiretime;
        
        private int isRegister;
        
        private int isSetPassword;

		private int isReceiveEmail;

        private int verifyCodeExpireTime;
        
        
        // 渠道ID
        [JsonProp("channelID")]
        public int ChannelID
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

        // 后台 seq 字段
        [JsonProp("seq")]
        public string SeqID
        {
            get { return seqID; }
            set { seqID = value; }
        }

        [JsonProp("user_name")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [JsonProp("uid")]
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        [JsonProp("token")]
        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        [JsonProp("expiretime")]
        public string Expiretime
        {
            get { return expiretime; }
            set { expiretime = value; }
        }

        [JsonProp("is_register")]
        public int IsRegister
        {
            get { return isRegister; }
            set { isRegister = value; }
        }

        [JsonProp("isset_pwd")]
        public int IsSetPassword
        {
            get { return isSetPassword; }
            set { isSetPassword = value; }
        }

		[JsonProp("is_receive_email")]
        public int IsReceiveEmail
        {
            get { return isReceiveEmail; }
            set { isReceiveEmail = value; }
        }

        [JsonProp("expire_time")]
        public int VerifyCodeExpireTime
        {
            get { return verifyCodeExpireTime; }
            set { verifyCodeExpireTime = value; }
        }
    
        public MSDKAccountRet()
        {
        }

        public MSDKAccountRet(string param) : base(param)
        {
        }

        public MSDKAccountRet(object json) : base(json)
        {
        }
    }
    
    
    // Account接口类
    public class MSDKAccount
    {
        /// <summary>
        /// 发验证码，重置密码回调
        /// </summary>
        public static event OnMSDKRetEventHandler<MSDKAccountRet> AccountEvent;

        public virtual void onAccountEvent(MSDKAccountRet ret)
        {
            AccountEvent(ret);
        }

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void requestVerifyCodeAdapter(
            [MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account,
            int codeType,
            int accountType,
            [MarshalAs(UnmanagedType.LPStr)] string langType,
            [MarshalAs(UnmanagedType.LPStr)] string areaCode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson);
        
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void resetPasswordAdapter(
            [MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account,
            [MarshalAs(UnmanagedType.LPStr)] string password,
            int accountType,
            int verifyCode,
            [MarshalAs(UnmanagedType.LPStr)] string langType,
            [MarshalAs(UnmanagedType.LPStr)] string areaCode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void modifyAdapter(
            [MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account,
            int accountType,
            int verifyCode,
            int verifyCodeModify,
            [MarshalAs(UnmanagedType.LPStr)] string accountModify,
            int accountTypeModify,
            [MarshalAs(UnmanagedType.LPStr)] string areaCodeModify,
            [MarshalAs(UnmanagedType.LPStr)] string langType,
            [MarshalAs(UnmanagedType.LPStr)] string areaCode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void getRegisterStatusAdapter(
            [MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account,
            int accountType,
            [MarshalAs(UnmanagedType.LPStr)] string langType,
            [MarshalAs(UnmanagedType.LPStr)] string areaCode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson);


        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void getVerifyCodeStatusAdapter(
            [MarshalAs(UnmanagedType.LPStr)] string channel,
            [MarshalAs(UnmanagedType.LPStr)] string account,
            int accountType,
            int verifyCode,
            int codeType,
            [MarshalAs(UnmanagedType.LPStr)] string langType,
            [MarshalAs(UnmanagedType.LPStr)] string areaCode,
            [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        
        
        public static void GetVerifyCodeStatus(string channel, string account, int accountType, int verifyCode, int codeType, string langType, string areaCode, string extraJson)
        {
            try
            {
                MSDKLog.Log("GetVerifyCodeStatusAdapter channel= " + channel);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				getVerifyCodeStatusAdapter(channel, account, accountType, verifyCode, codeType, langType, areaCode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("GetVerifyCodeStatusAdapter with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        
        
        /// <summary>
        /// 用户账号是否注册查询接口
        /// <param name="channel">           【必填】支持"Email"，"SMS"
        /// <param name="account">           【必填】注册的帐号
        /// <param name="accountType">       【必填】1- email（5到50个字节）,2 - phone
        /// <param name="langType">          【选填】指定发送给用户的验证码短信或邮件所有语言
        /// <param name="areaCode">          【选填】手机登录时为必填参数， 这里填写的是手机区号字段
        /// <param name="extraJson">         【选填】扩展字段，默认为空，会透传到后台
        public static void GetRegisterStatus(string channel, string account, int accountType, string langType, string areaCode, string extraJson)
        {
            try
            {
                MSDKLog.Log("GetRegisterStatus channel= " + channel);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				getRegisterStatusAdapter(channel, account, accountType, langType, areaCode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("GetRegisterStatus with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        
        /// <summary>
        /// 修改账号信息接口（verifyCode校验码）
        /// <param name="channel">           【必填】支持"Email"，"SMS"
        /// <param name="account">           【必填】当前存在的账号
        /// <param name="accountType">       【必填】1- email（5到50个字节）,2 - phone
        /// <param name="verifyCode">        【必填】验证码
        /// <param name="verifyCodeModify">  【必填】要修改的账号接收到的验证码
        /// <param name="accountModify">     【必填】要修改的帐号
        /// <param name="accountTypeModify"> 【必填】要修改的账号类似1email,2phone
        /// <param name="areaCodeModify">    【选填】要修改的账号是手机时为必填参数， 这里填写的是手机区号字段
        /// <param name="langType">          【选填】指定发送给用户的验证码短信或邮件所有语言
        /// <param name="areaCode">          【选填】手机登录时为必填参数， 这里填写的是手机区号字段
        /// <param name="extraJson">         【选填】扩展字段，默认为空，会透传到后台
        public static void Modify(string channel, string account, int accountType, int verifyCode, int verifyCodeModify, string accountModify, int accountTypeModify, string areaCodeModify, string langType, string areaCode, string extraJson)
        {
            try
            {
                MSDKLog.Log("Modify channel= " + channel);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				modifyAdapter(channel, account, accountType, verifyCode, verifyCodeModify, accountModify, accountTypeModify, areaCodeModify, langType, areaCode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("Modify with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 发送校验(注册)码接口（校验码有效期3分钟）
        /// <param name="channel">       【必填】支持"Email"，"SMS"
        /// <param name="account">       【必填】注册的帐号
        /// <param name="codeType">      【必填】生成的验证码类型，0注册,1修改密码
        /// <param name="accountType">       【必填】1- email（5到50个字节）,2 - phone
        /// <param name="langType">      【选填】指定发送给用户的验证码短信或邮件所有语言
        /// <param name="areaCode">      【选填】手机渠道时为国际区号，邮箱渠道可直接填""
        /// <param name="extraJson">     【选填】扩展字段，默认为空，会透传到后台
        public static void RequestVerifyCode(string channel, string account, int codeType, int accountType, string langType, string areaCode = "86", string extraJson = "")
        {
            try
            {
                MSDKLog.Log("RequestVerifyCode channel= " + channel);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				requestVerifyCodeAdapter(channel, account, codeType, accountType, langType, areaCode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("RequestVerifyCode with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 用户注册接口（生成帐号及token）
        /// <param name="channel">         【必填】支持"Email"，"SMS"
        /// <param name="account">         【必填】注册的帐号
        /// <param name="password">        【必填】登录密码
        /// <param name="accountType">       【必填】1- email（5到50个字节）,2 - phone
        /// <param name="verifyCode">      【选填】验证码
        /// <param name="langType">      【选填】指定发送给用户的验证码短信或邮件所有语言
        /// <param name="areaCode">        【选填】手机渠道时为国际区号，邮箱渠道可直接填""
        /// <param name="extraJson">       【选填】扩展字段，默认为空，会透传到后台
        public static void ResetPassword(string channel, string account, string password, int accountType, int verifyCode, string langType, string areaCode = "86", string extraJson = "")
        {
            try
            {
                MSDKLog.Log("ResetPassword channel= " + channel);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                //UnityEditorLogin (MSDKMethodNameID.MSDK_LOGIN_LOGIN, channel, subChannel);
#else
				resetPasswordAdapter(channel, account, password, accountType, verifyCode, langType, areaCode, extraJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("ResetPassword with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        internal static void OnAccountRet(string json)
        {
            MSDKLog.Log("OnAccountRet json= " + json);
            if (AccountEvent != null)
            {
                var ret = new MSDKAccountRet(json);
                try
                {
                    AccountEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for AccountEvent!");
            }
        }

    }
#endif
}