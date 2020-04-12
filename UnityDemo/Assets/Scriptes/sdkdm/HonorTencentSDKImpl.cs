using System;
using GCloud.MSDK;
using UnityEngine;
using System.Collections.Generic;

namespace Honor
{
    public class RemoteNotification
    {
        //true为前台消息，false为后台消息
        public bool foreground
        {
            set; get;
        }
        public string notification
        {
            set; get;
        }
}
    public interface Callback<T>
    {
        void OnSuccess(T ret);
        void OnFailure(int retCode, string msg);
    }
    public interface LoginCallback
    {
        /// <summary>
        /// 回调该接口用户登录成功
        /// onlyRefreshData为false需要走一遍登录流程
        /// onlyRefreshData为true，游戏只需要刷新MSDKLoginRet信息就
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="onlyRefreshData"></param>
        void OnLoginSuccess(MSDKLoginRet ret, bool onlyRefreshData = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retCode"><see cref="MSDKError"/></param>
        /// <param name="msg"></param>
        void OnLoginFailure(int retCode, string msg);
        /// <summary>
        /// 回调该接口游戏需弹出询问玩家是否切换账号对话框，玩家响应后调用SwitchUser接口
        /// </summary>
        void OnSwitchUser();
        /// <summary>
        /// 回调该接口游戏需要返回登录界面，让玩家选择渠道登录，例如QQ，微信
        /// </summary>
        void OnAutoLoginFailed();
        /// <summary>
        /// 注销成功后游戏应返回登录界面，让玩家选择渠道登录，例如QQ，微信
        /// </summary>
        void OnLogoutSuccess();
    }

    public class HonorTencentSDKImpl : HonorSDKImpl
    {
        private static HonorTencentSDKImpl instance;
        protected HonorSDKImpl parentImpl;
        protected HonorTencentSDKImpl()
        {
        }

        new public static void CreateInstance(ePlat plant)
        {
            if (instance == null)
            {
                if (plant == ePlat.Android)
                {
                    instance = new TencentAndroidImpl();
                    instance.parentImpl = new CustomAndroidSdkImpl();
                }

            }
        }

        internal void RegisterLoginCallback(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        new public static HonorTencentSDKImpl GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Login.html"/>
        /// </summary>
        /// <param name="channel"><see cref="MSDKChannel"/>不传递channel表示自动登录</param>
        /// <param name="permissions"></param>
        /// <param name="subChannel"></param>
        /// <param name="extraJson"></param>
        public virtual void Login(string channel = "", string permissions = "", string subChannel = "", string extraJson = "")
        {

        }

        public virtual void RegisterLoginCallback(LoginCallback LoginCallback)
        {

        }

        /// <summary>
        /// </summary>
        /// <param name="confirm">true表示玩家同意切换账号,false表示玩家不同意切换账号</param>
        public virtual void SwitchUser(bool confirm)
        {

        }
        /// <summary>
		/// 加载公告信息
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Notice.html"/>
		/// </summary>
		/// <param name="noticeGroup">公告分组，后台填好公告并且分好组就可以对应拉取公告信息</param>
		/// <param name="language">语种</param>
		/// <param name="region">地区，北美、亚太、南美等等</param>
		/// <param name="partition">游戏大区</param>
		/// <param name="extraJson">扩展字段</param>
        public virtual void LoadNoticeData(OnFinish<MSDKNoticeRet> callback,string noticeGroup, string language, int region, string partition, string extra)
        {

        }

        /// <summary>
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Push.html"/>
        /// </summary>
        /// <param name="channel">三方渠道专有名词，比如 "XG", "Firebase",默认为XG</param>
        /// <param name="tag">标签，不能为 null 或包含空格</param>
        public virtual void SetTag(string tag, string channel="") {

        }
        public virtual void DeleteTag(string tag, string channel = "") {

        }
        /// <summary>
        ///       /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Push.html"/>
        /// </summary>
        /// <param name="callback">接受远程推送回调</param>
        /// <param name="channel">三方渠道专有名词，比如 "XG", "Firebase",默认为XG </param>
        /// <param name="account">绑定的账号，绑定后可以针对账号发送推送消息，account不能为单个字符如“2”，“a”,msdk的openid</param>

        public virtual void RegisterPush(OnFinish<RemoteNotification> callback =null,string account = "", string channel = "") {
        }

    }
}
