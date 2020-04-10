using System;
using System.Collections.Generic;
using UnityEngine;


namespace HonorSdk
{

    public class SDKManager : IBaseApi
    {
        private IBaseApi instanceImpl;

        private static SDKManager instance;

        protected SDKManager()
        {

        }

        public static SDKManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SDKManager();
                instance.handler = new MessageHandler(instance);

            }

#if UNITY_IOS && !UNITY_EDITOR
              instance.instanceImpl = new IosSdkImpl();
#elif UNITY_ANDROID && !UNITY_EDITOR
              instance.instanceImpl = new AndroidSdkImpl();
#endif
            return instance;
        }


        protected MessageHandler handler;

        private string mConfigs;

        /// <summary>
        /// 初始化，此接口必须最先调用
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="initListener">返回初始化结果<see cref="ResultInit"/></param>
        public virtual void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            if (configs == null)
            {
                configs = new Dictionary<string, string>();
            }
            configs.Add("gameResVersion", gameResVersion);
            gameObject.SetOnReceiveListener(new OnReceiveMsg(handler.OnReceive));
            handler.RegisterCallback(Api.INIT, initListener);
            instanceImpl.Init(gameObject, initListener, gameResVersion, configs);
        }

        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="appInfoListener">返回应用信息<see cref="AppInfo"/></param>
        public virtual void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            handler.RegisterCallback(Api.GET_APP_INFO, appInfoListener);
            instanceImpl.GetAppInfo(appInfoListener);
        }


        private NotchScreenInfo notchInfo;

        /// <summary>
        /// 注册刘海屏监听,<see cref="GetNotchScreenInfo"/>  获取刘海屏宽高 </param>
        /// </summary>
        /// <param name="getNotchInfoListener">获取刘海屏成功回调<see cref="NotchScreenInfo"/></param>
        public virtual void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
            handler.RegisterCallback(Api.REGISTER_NOTCH_SCREEN_INFO, (NotchScreenInfo info) =>
            {
                notchInfo = info;
                if (getNotchInfoListener != null)
                {
                    getNotchInfoListener();
                    getNotchInfoListener = null;
                }
            });
            instanceImpl.RegisterNotchScreenInfoListener(getNotchInfoListener);
        }
        /// <summary>
        ///  获取刘海屏宽高 
        /// </summary>

        public virtual NotchScreenInfo GetNotchScreenInfo()
        {
            if (notchInfo != null)
                return notchInfo;
            else
                return new NotchScreenInfo();
        }

        /// <summary>
        /// 获取国家码 
        /// </summary>
        /// <param name="getCountryAndLanguageListener">返回国家码例:中国CN，美国US</param>
        public virtual void GetCountryAndLanguage(OnFinish<Locale> getCountryAndLanguageListener)
        {
            handler.RegisterCallback(Api.GET_LOCALE, getCountryAndLanguageListener);
            instanceImpl.GetCountryAndLanguage(getCountryAndLanguageListener);
        }

        /// <summary>
        /// 上报异常日志
        /// </summary>
        /// <param name="errorMsg">异常日志内容</param>
        /// <param name="type">异常日志分类,不传递时默认分类为default</param>
        public virtual void ReportError(string errorMsg, string type = "")
        {
            instanceImpl.ReportError(errorMsg, type);
        }

        /// <summary>
        /// 设置剪切板
        /// </summary>
        /// <param name="content">设置剪切板内容</param>
        public virtual void SetClipboard(string content)
        {
            instanceImpl.SetClipboard(content);
        }
        /// <summary>
        /// 获取手机内存信息
        /// </summary>
        /// <param name="getMemroyInfoListener">返回手机内存信息<see cref="MemoryInfo"/></param>
        public virtual void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
            handler.RegisterCallback(Api.GET_MEMORY, getMemroyInfoListener);
            instanceImpl.GetMemory(getMemroyInfoListener);
        }

        /// <summary>
        /// 获取手机电量信息
        /// </summary>
        /// <param name="getBatteryInfoListener">返回手机电量信息<see cref="BatteryInfo"/></param>
        public virtual void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
            handler.RegisterCallback(Api.GET_BATTERY, getBatteryInfoListener);
            instanceImpl.GetBattery(getBatteryInfoListener);
        }

        /// <summary>
        /// 本地通知
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="delay">延迟时间推送 单位秒</param>
        /// <param name="id">本地通知唯一标识,清除推送时用到</param>
        public virtual void PushNotification(string content, int delay, int id)
        {

            instanceImpl.PushNotification(content, delay, id);
        }
        /// <summary>
        /// 清除本地通知
        /// </summary>
        /// <param name="id">本地通知唯一标识，和PushNotification方法参数id对应</param>
        public virtual void CleanNotification(int id)
        {

            instanceImpl.CleanNotification(id);
        }
        /// <summary>
        /// 清除全部本地通知
        /// </summary>
        public virtual void CleanAllNotification()
        {
            instanceImpl.CleanAllNotification();
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="loginListener">返回账号登录结果<see cref="UserInfo"/></param>
        public virtual void Login(OnFinish<UserInfo> loginListener)
        {
            handler.RegisterCallback(Api.LOGIN, loginListener);
            instanceImpl.Login(loginListener);
        }

        /// <summary>
        ///不管之前是否有游客账号都从新生成新的游客账号
        /// </summary>
        /// <param name="startNewGameListener"></param>
        public virtual void StartNewGame(OnFinish<UserInfo> startNewGameListener)
        {
            handler.RegisterCallback(Api.START_NEW_GAME, startNewGameListener);
            instanceImpl.StartNewGame(startNewGameListener);
        }
        /// <summary>
        /// 账号切换
        /// </summary>

        public virtual void SwitchAccount()
        {
            instanceImpl.SwitchAccount();
        }
        /// <summary>
        /// 注册监听
        /// </summary>
        /// <param name="switchAccountListener">返回账号切换结果<see cref="UserInfo"/></param>
        public virtual void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
        {
            handler.RegisterCallback(Api.REGISTER_SWITCH_ACCOUNT, switchAccountListener);
            instanceImpl.RegisterSwitchAccountListener(switchAccountListener);
        }

        /// <summary>
        /// 账号绑定
        /// </summary>
        /// <param name="startBindListener">返回账号绑定结果<see cref="ResultBind"/></param>
        public virtual void StartBind(OnFinish<ResultBind> startBindListener)
        {
            handler.RegisterCallback(Api.BIND, startBindListener);
            instanceImpl.StartBind(startBindListener);
        }

        /// <summary>
        /// 账号注销
        /// </summary>
        /// <param name="logoutListener">返回账号注销结果<see cref="Result"/></param>
        public virtual void Logout(OnFinish<Result> logoutListener)
        {
            handler.RegisterCallback(Api.LOGOUT, logoutListener);
            instanceImpl.Logout(logoutListener);
        }
        /// <summary>
        /// 渠道是否有退出框
        /// </summary>
        /// <returns>true表示渠道有退出框，false表示渠道没有退出框此时游戏需要创建退出框</returns>
        public virtual bool HasExitDialog()
        {
            return instanceImpl.HasExitDialog();
        }

        /// <summary>
        /// 上报角色信息
        /// </summary>
        /// <param name="gameRoleInfo">角色信息</param>
        /// <returns></returns>
        public virtual void UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            instanceImpl.UploadGameRoleInfo(gameRoleInfo);
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="payListener">返回支付结果<see cref="ResultPay"/></param>
        /// <returns></returns>
        public virtual void Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
            handler.RegisterCallback(Api.PAY, payListener);
            instanceImpl.Pay(orderInfo, payListener);
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        /// <param name="exitListener">返回退出结果<see cref="Result"/></param>
        public virtual void Exit(OnFinish<Result> exitListener)
        {
            handler.RegisterCallback(Api.EXIT, exitListener);
            instanceImpl.Exit(exitListener);

        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="serverId">服务器id(可选)</param>
        /// <param name="language">公告语言类型(可选) 例:中文zh,英语en</param>
        /// <param name="type">公告类型(可选) (0普通,1活动,2强更,3热更,4拦截,5跑马灯,6登录,7登出)</param>
        /// <param name="getNoticeListListener"></param>
        public virtual void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId = "", string language = "", string country = "", string type = "")
        {
            handler.RegisterCallback(Api.GET_NOTICE_LIST, getNoticeListListener);
            instanceImpl.GetNoticeList(getNoticeListListener, serverId, language, country, type);
        }


        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="getServerListListener">返回服务器列表<see cref="ServerList"/></param>
        public virtual void GetServerList(OnFinish<ServerList> getServerListListener)
        {
            handler.RegisterCallback(Api.GET_SERVER_LIST, getServerListListener);
            instanceImpl.GetServerList(getServerListListener);
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="serverId">服务器id</param>
        /// <param name="category">商品类型(可选)，逗号分隔，留空或不传表示所有类型)</param>
        /// <param name="currency">币种过滤(可选)（将只返回符合币种条件的商品）</param>
        /// <param name="getGoodsListListener">返回商品列表<see cref="GoodsList"/></param>
        public virtual void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category = "", string currency = "")
        {
            handler.RegisterCallback(Api.GET_GOODS_LIST, getGoodsListListener);
            instanceImpl.GetGoodsList(getGoodsListListener, serverId, category, currency);
        }

        /// <summary>
        /// 获取渠道是否支持接口
        /// </summary>
        /// <param name="api">接口类型</param>
        /// <returns>返回true标识支持，false标识不支持</returns>
        public virtual bool IsSupportApi(Api api)
        {
            return instanceImpl.IsSupportApi(api);
        }

        /// <summary>
        /// 扩展方法 需要与Android联调
        /// </summary>
        /// <param name="functionName">扩展方法名称</param>
        /// <param name="jsonParameter">扩展方法所需参数(可选)</param>
        /// <param name="headName">处理标识确保(可选),不与已存在的HeadName冲突</param>
        /// <param name="expandFunctionListener">返回处理结果(可选) <see cref="ResultExpand"/></param>
        public virtual void ExpandFunction(string functionName, string jsonParameter = "", string headName = "", OnFinish<ResultExpand> expandFunctionListener = null)
        {
            handler.RegisterCallback(Api.EXPAND_FUNCTION, expandFunctionListener, headName);
            instanceImpl.ExpandFunction(functionName, jsonParameter, headName, expandFunctionListener);
        }

        /// <summary>
        /// 设置SDK应用语言和国家
        /// </summary>
        /// <param name="language"></param>
        /// <param name="country"></param>
        public virtual void SetApplicationLocale(string language, string country = "")
        {
            instanceImpl.SetApplicationLocale(language, country);
        }

        /// <summary>
        /// 新手引导完调用
        /// </summary>
        public virtual void SendGuideFinish()
        {

            instanceImpl.SendGuideFinish();
        }

        /// <summary>
        /// 获取网络状态
        /// </summary>
        /// <param name="getNetStateInfoListener"></param>
        public virtual void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
            handler.RegisterCallback(Api.GET_NET_STATE_INFO, getNetStateInfoListener);
            instanceImpl.GetNetStateInfo(getNetStateInfoListener);
        }

        public virtual void RestartApp()
        {
            instanceImpl.RestartApp();
        }

        public DiskInfo GetDiskInfo()
        {
            return instanceImpl.GetDiskInfo();
        }

        public void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            handler.RegisterCallback(Api.DOWNLOAD_TEXT, downloadTextListener);
            instanceImpl.DownloadText(url, retry, timeout, downloadTextListener);
        }

        public void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<HeadsetStateInfo> getHeadsetStateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
    }
}
