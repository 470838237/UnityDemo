
using GCloud;
using GCloud.MSDK;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSDK
{
    class TencentAndroidImpl : HonorTencentSDKImpl
    {
        ITdir tdir = TdirFactory.CreateInstance();
        bool inited;
        AndroidJavaObject currentActivity;
        public TencentAndroidImpl()
        {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        private OnFinish<ResultInit> initListener;

        public override void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            this.initListener = initListener;
            parentImpl.Init(gameObject, initListener, gameResVersion, configs);

        }
        public override void InitGCloud(InitializeInfo cloudInfo, TdirInitInfo tdirInfo)
        {
            MSDKLogin.LoginRetEvent += OnLoginRetEvent;
            MSDKLogin.LoginBaseRetEvent += OnLoginBaseRetEvent;
            MSDKNotice.NoticeRetEvent += OnNoticeRetEvent;
            MSDKPush.PushBaseRetEvent += OnPushBaseRetEvent;
            MSDKPush.PushNotificationEvent += OnPushNotificationEvent;
            MSDKCrash.CrashBaseRetEvent += OnCrashBaseRetEvent;
            IGCloud.Instance.Initialize(cloudInfo);
            tdir.Initialize(tdirInfo);
            RegisterDirCallback();
            inited = true;
            MSDKPush.RegisterPush("XG");
            MSDKReport.Init("Beacon,TDM");
            MSDK.Init();
        }

        private void RegisterDirCallback()
        {
            tdir.QueryTreeEvent += (result, tree) =>
            {
                // showMessage("QueryTree result: " + result, false);
                if (QueryTreeCallback != null)
                {
                    Wrapper<TreeInfo> wrapper = new Wrapper<TreeInfo>();
                    wrapper.success = result.IsSuccess();
                    wrapper.obj = tree;
                    QueryTreeCallback(wrapper);
                }


                //if (result.IsSuccess() && tree != null)
                //{
                //    for (int j = 0; j < tree.NodeList.Count; j++)
                //    {
                //        NodeWrapper nodeWrapper = tree.NodeList[j];
                //        TreeNodeBase node = nodeWrapper.GetNode();

                //        //  printNode(node);
                //    }
                //}
            };

            tdir.QueryLeafEvent += (result, node) =>
            {
                // showMessage("QueryLeaf result: " + result, false);
                if (queryLeafCallback != null)
                {
                    Wrapper<NodeWrapper> wrapper = new Wrapper<NodeWrapper>();
                    wrapper.success = result.IsSuccess();
                    wrapper.obj = node;
                    queryLeafCallback(wrapper);
                }
                //if (result.IsSuccess() && node != null)
                //{
                //    TreeNodeBase node = node.GetNode();
                //    printNode(node);
                //}
            };

        }
        private OnFinish<Wrapper<TreeInfo>> QueryTreeCallback;
        public override int QueryTree(int treeId, OnFinish<Wrapper<TreeInfo>> callback)
        {
            this.QueryTreeCallback = callback;
            if (tdir != null)
            {
                return tdir.QueryTree(treeId);
            }
            return -1;
        }

        private OnFinish<Wrapper<NodeWrapper>> queryLeafCallback;
        public override int QueryLeaf(int treeId, int leafId, OnFinish<Wrapper<NodeWrapper>> callback)
        {
            this.queryLeafCallback = callback;
            if (tdir != null)
            {
                return tdir.QueryLeaf(treeId, leafId);
            }
            return -1;
        }


        public override void Update()
        {
            if (tdir != null && inited)
            {
                tdir.Update();
            }
        }

        //private void InitFailure(string message)
        //{
        //    Dictionary<string, string> extra = new Dictionary<string, string>();
        //    ResultInit result = new ResultInit(extra);
        //    result.success = false;
        //    result.message = message;
        //    if (initListener != null)
        //        initListener(result);
        //}

        protected override void SetGameObjectName(string gameObjectName)
        {
            currentActivity.Call("setGameObjectName", gameObjectName);
        }
        protected override void Init(string configsJson)
        {
            currentActivity.Call("init", configsJson);
        }
    
        public override void Exit(OnFinish<Result> exitListener)
        {

            MSDKLogin.LoginRetEvent -= OnLoginRetEvent;
            MSDKLogin.LoginBaseRetEvent -= OnLoginBaseRetEvent;
            MSDKNotice.NoticeRetEvent -= OnNoticeRetEvent;
            MSDKPush.PushBaseRetEvent -= OnPushBaseRetEvent;
            MSDKPush.PushNotificationEvent -= OnPushNotificationEvent;
            MSDKCrash.CrashBaseRetEvent -= OnCrashBaseRetEvent;
            MSDKPush.UnregisterPush("XG");
        }

        private CrashCallback crashCallback;
        public override void SetCrashCallback(CrashCallback crashCallback)
        {
            this.crashCallback = crashCallback;
            MSDKCrash.InitCrash();
            MSDKCrash.SetCrashCallback();

        }

        public override void LogInfo(MSDKCrashLevel level, string tag, string log)
        {
            MSDKCrash.LogInfo(level, tag, log);
        }

        public override void SetUserValue(string key, string value)
        {
            MSDKCrash.SetUserValue(key, value);
        }
        public override void SetUserId(string userId)
        {
            MSDKCrash.SetUserId(userId);
        }

        public string OnCrashBaseRetEvent(MSDKBaseRet baseRet)
        {
            if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_DATA)
            {
                // 这里是非unity进程，注意不要做unity相关的操作
                if (crashCallback != null)
                    return crashCallback.GetExtraData();
                return "this is extra data.";
            }
            else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_MESSAGE)
            {
                // 这里是非unity进程，注意不要做unity相关的操作
                if (crashCallback != null)
                    return crashCallback.GetExtraMessage();
                return "this is extra message.";
            }
            return "";
        }

        private void OnLoginRetEvent(MSDKLoginRet loginRet)
        {
            // Debug.Log("OnLoginRetNotify in Ligin");
            string methodTag = "";
            if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGIN)
            {
                methodTag = "Login";
                LoginFinish(ActionLogin.CHANNEL_LOGIN, loginRet);
            }
            else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_BIND)
            {
                methodTag = "Bind";
            }
            else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_AUTOLOGIN)
            {
                methodTag = "AutoLogin";
                LoginFinish(ActionLogin.AUTO_LOGIN, loginRet);
            }
            else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_QUERYUSERINFO)
            {
                methodTag = "QueryUserInfo";
                //MSDKLogin.QueryUserInfo
            }
            // GetLoginRet 为同步接口，不需要在回调中处理
            //      else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT) {
            //	methodTag = "GetLoginResult";
            //}
            else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE)
            {
                methodTag = "LoginWithConfirmCode";
            }
            // SampleInstance.showRetDialog(methodTag, loginRet);
        }

        enum ActionLogin
        {
            AUTO_LOGIN,
            CHANNEL_LOGIN,
            SWITCH_USER,
            Logout,
            OnlyRefreshLoginData
        }
        // private bool LoginWithConfirmCode = true;

        private void LoginFinish(ActionLogin action, MSDKLoginRet loginRet, MSDKBaseRet baseRet = null)
        {
            if (LoginCallback == null)
                return;
            switch (action)
            {
                case ActionLogin.OnlyRefreshLoginData:
                    LoginCallback.OnLoginSuccess(loginRet, true);
                    break;
                case ActionLogin.CHANNEL_LOGIN:
                    if (loginRet.RetCode == MSDKError.SUCCESS)
                    {
                        LoginCallback.OnLoginSuccess(loginRet);
                    }
                    else
                    {
                        LoginCallback.OnLoginFailure(loginRet.RetCode, loginRet.RetMsg);
                    }
                    break;
                case ActionLogin.AUTO_LOGIN:
                    if (loginRet.RetCode == MSDKError.SUCCESS)
                    {
                        LoginCallback.OnLoginSuccess(loginRet);
                    }
                    else
                    {
                        LoginCallback.OnAutoLoginFailed();
                    }
                    break;

                case ActionLogin.SWITCH_USER:
                    LoginCallback.OnSwitchUser();
                    break;
                case ActionLogin.Logout:
                    LoginCallback.OnLogoutSuccess();
                    break;

            }
        }

        private void OnLoginBaseRetEvent(MSDKBaseRet baseRet)
        {
            // Debug.Log("OnBaseRetNotify in Login");

            if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_WAKEUP)
            {
                handleDiifAccount(baseRet);
            }
            else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGOUT)
            {
                string methodTag = "Logout";
                //  SampleInstance.showRetDialog(methodTag, baseRet);
                LoginFinish(ActionLogin.Logout, MSDKLogin.GetLoginRet(), baseRet);
            }
        }

        // 处理异账号的逻辑
        private void handleDiifAccount(MSDKBaseRet baseRet)
        {
            string methodTag = "异账号";
            switch (baseRet.RetCode)
            {
                case MSDKError.SUCCESS:
                    { // 本地原有票据有效，使用原有票据登录
                      // SampleInstance.showRetDialog(methodTag, "使用原有票据登录，游戏无需处理");
                        LoginCallback.OnLoginSuccess(MSDKLogin.GetLoginRet(), true);
                        break;
                    }
                case MSDKError.LOGIN_ACCOUNT_REFRESH:
                    { // 新旧 openid 相同，票据不同。刷新登录票据
                      // SampleInstance.showRetDialog(methodTag, "新旧 openid 相同，票据不同。刷新登录票据，游戏无需处理");
                        LoginCallback.OnLoginSuccess(MSDKLogin.GetLoginRet(), true);
                        break;
                    }
                case MSDKError.LOGIN_URL_USER_LOGIN:
                    {// 本地无openid，拉起有票据，使用新票据登录
                        //SampleInstance.showRetDialog(methodTag, "本地无openid，拉起有票据，使用新票据登录，将自动触发切换游戏账号逻辑（SwitchUser），游戏需监控登录的回调结果");
                        break;
                    }
                case MSDKError.LOGIN_NEED_SELECT_ACCOUNT:
                    {
                        // SampleInstance.ShowSwithUserDialog();
                        LoginCallback.OnSwitchUser();
                        break;
                    }
                case MSDKError.LOGIN_NEED_LOGIN:
                    {
                        //  SampleInstance.showRetDialog(methodTag, "票据均无效，进入登录页面");
                        MSDKLoginRet ret = MSDKLogin.GetLoginRet();
                        LoginCallback.OnLoginFailure(ret.RetCode, ret.RetMsg);
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnNoticeRetEvent(MSDKNoticeRet noticeRet)
        {
            string methodTag = "";
            if (noticeRet.MethodNameId == (int)MSDKMethodNameID.MSDK_NOTICE_LOAD_DATA)
            {
                methodTag = "LoadNotice";
                if (loadNoticeDataCallback != null)
                    loadNoticeDataCallback(noticeRet);

            }
            //  SampleInstance.showRetDialog(methodTag, noticeRet);
        }

        public void OnPushBaseRetEvent(MSDKBaseRet baseRet)
        {
            string methodTag = "";

            if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_REGISTER_PUSH)
            {
                methodTag = "RegisterPush";
            }
            else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_UNREGISTER_PUSH)
            {
                methodTag = "UnregisterPush";
            }
            else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_SET_TAG)
            {
                methodTag = "SetTag";
            }
            else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_DELETE_TAG)
            {
                methodTag = "DeleteTag";
            }


            //  SampleInstance.showRetDialog(methodTag, baseRet);
        }

        public void OnPushNotificationEvent(MSDKPushRet pushRet)
        {
            string methodTag = "";

            if (pushRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_ADD_LOCAL_NOTIFICATION)
            {
                methodTag = "AddLocalNotification";
            }
            else if (pushRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_CLEAR_LOCAL_NOTIFICATION)
            {
                methodTag = "ClearLocalNotification";
            }
            else if (pushRet.MethodNameId == (int)MSDKMethodNameID.MSDK_PUSH_NOTIFICAITON_CALLBACK)
            {
                methodTag = "NotificationCallback";
                if (pushRet.Type == 0)
                {
                    RemoteNotification notification = new RemoteNotification();
                    notification.notification = pushRet.Notification;
                    notification.foreground = true;
                    if (pushCallback != null)
                        pushCallback(notification);
                }
                else if (pushRet.Type == 1)
                {
                    RemoteNotification notification = new RemoteNotification();
                    notification.notification = pushRet.Notification;
                    notification.foreground = false;
                    if (pushCallback != null)
                        pushCallback(notification);
                    else
                        PushNotification(pushRet.Notification, 0, 0);
                }
            }
            // SampleInstance.showRetDialog(methodTag, pushRet);
        }


        public override void SetTag(string tag, string channel = "")
        {

            if (channel == "" || channel == null)
            {
                MSDKPush.SetTag("XG", tag);
            }
            else
            {
                MSDKPush.SetTag(channel, tag);
            }
        }
        public override void DeleteTag(string tag, string channel = "")
        {

            if (channel == "" || channel == null)
            {
                MSDKPush.DeleteTag("XG", tag);
            }
            else
            {
                MSDKPush.DeleteTag(channel, tag);
            }
        }
        private OnFinish<RemoteNotification> pushCallback;
        public override void RegisterPush(OnFinish<RemoteNotification> callback = null, string account = "", string channel = "")
        {
            this.pushCallback = callback;
            if (channel == "" || channel == null)
            {
                MSDKPush.RegisterPush("XG", account);
            }
            else
            {
                MSDKPush.RegisterPush(channel, account);
            }
        }

        public override void ReportEvent(string eventName, Dictionary<string, string> paramsDic, string spChannels = "", bool isRealTime = true)
        {
            MSDKReport.ReportEvent(eventName, paramsDic, spChannels, isRealTime);
        }

        private OnFinish<MSDKNoticeRet> loadNoticeDataCallback;
        public override string LoadNoticeData(OnFinish<MSDKNoticeRet> callback, string noticeGroup, string language, int region, string partition, string extra)
        {
            this.loadNoticeDataCallback = callback;
            return MSDKNotice.LoadNoticeData(noticeGroup, language, region, partition, extra);
        }
        public override void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId, string language, string country, string type)
        {

        }

        /// <summary>
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Login.html"/>
        /// </summary>
        /// <param name="loginListener"></param>
        /// <param name="channel"><see cref="MSDKChannel"/></param>
        /// <param name="extra">[permissions:登录授权的权限列表，国内渠道默认传空，海外渠道若存在多个权限，可用逗号分隔。例如：user_info,inapp_friends
        /// ,subChannel:子渠道名字，大小写敏感。比如 Garena 包含 Facebook 子渠道
        /// ,extraJson:扩展字段，具体含义参考各渠道说明]</param>
        public override void Login(OnFinish<UserInfo> loginListener)
        {
            //无效
        }

        private LoginCallback LoginCallback;

        private string channel;

        private string permissions;

        private string subChannel;

        private string extraJson;

        public override void RegisterLoginCallback(LoginCallback LoginCallback)
        {
            this.LoginCallback = LoginCallback;
        }

        public override void Login(string channel = "", string permissions = "", string subChannel = "", string extraJson = "")
        {
            this.channel = channel;
            this.permissions = permissions;
            this.subChannel = subChannel;
            this.extraJson = extraJson;

            if (channel == "" || channel == null)
            {
                MSDKLogin.AutoLogin();
            }
            else
            {
                MSDKLogin.Login(channel, permissions, subChannel, extraJson);
            }
        }


        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            parentImpl.GetAppInfo(appInfoListener);
        }

        public override void GameStepInfo(string step, string type)
        {
            Dictionary<string, string> paramsDic = new Dictionary<string, string>();
            paramsDic.Add("step", step);
            paramsDic.Add("type", type);
            MSDKReport.ReportEvent("GameStepInfo", paramsDic, "", true);
        }


        public override void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
            parentImpl.RegisterNotchScreenInfoListener(getNotchInfoListener);
        }

        public override void GetCountryAndLanguage(OnFinish<Locale> getCountryCodeListener)
        {
            parentImpl.GetCountryAndLanguage(getCountryCodeListener);
        }

        public override void ReportError(string errorMsg, string type)
        {
            MSDKCrash.LogInfo(MSDKCrashLevel.BuglyLogLevelError, type, errorMsg);
        }

        public override void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
            parentImpl.GetMemory(getMemroyInfoListener);
        }


        public override void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
            parentImpl.GetBattery(getBatteryInfoListener);
        }
        public override void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {

        }
        public override void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
        {
            parentImpl.GetHardwareInfo(getHardwareListener);
        }
        public override void PushNotification(string content, int delay, int id)
        {
            MSDKLocalNotification message = new MSDKLocalNotification();
            message.ActionType = 1;
            message.Type = 1;
            message.Content = content;
            message.Title = Application.productName;
            // 马上通知，本地推送会有五分钟的弹性时间，如果小于当前时间就是马上通知
            message.Date = DateTime.Now.ToString("yyyyMMdd");
            message.Hour = DateTime.Now.Hour.ToString();
            message.Min = (DateTime.Now.Minute + delay).ToString();
            MSDKPush.AddLocalNotification("XG", message);
        }

        //不支持清除指定的消息
        public override void CleanNotification(int id)
        {
            MSDKPush.ClearLocalNotifications("XG");
        }
        public override void CleanAllNotification()
        {
            MSDKPush.ClearLocalNotifications("XG");
        }

        public override void UdpPush(string ip, string port, string gameRoleId)
        {

        }

        public override void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
            parentImpl.StartRecordVideo(serverURL, bit, recordMaxTime);
        }
        public override void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            parentImpl.StopRecordVideo(stopRecordVideoListener);
        }
        public override void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            parentImpl.PlayVideo(videoUrl, playVideoListener);
        }


        public override void SwitchAccount()
        {

        }

        public override void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
        {

        }

        public override void StartBind(OnFinish<ResultBind> startBindListener)
        {

        }

        public override void Logout(OnFinish<Result> logoutListener)
        {
            MSDKLogin.Logout();
        }

        public override bool HasExitDialog()
        {
            return false;
        }

        public override string UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            return null;
        }

        public override string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {

            return null;
        }

        public override void GetServerList(OnFinish<ServerList> getServerListListener)
        {

        }

        public override void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category, string currency)
        {

        }

        public override void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            parentImpl.GetDynamicUpdate(rootDir, getDynamicUpdateListener);
        }

        public override void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            parentImpl.DownDynamicUpdate(downDynamicUpdateListener);
        }

        public override void RepairUpdateRes()
        {
            parentImpl.RepairUpdateRes();
        }
        public override void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            parentImpl.GetForceUpdate(getForceUpdateListener);
        }

        public override void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            parentImpl.DownForceUpdate(downForceUpdateListener);
        }

        public override bool HasObbUpdate()
        {
            return parentImpl.HasObbUpdate(); ;
        }
        public override void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            parentImpl.DownObbUpdate(downObbUpdateListener);
        }

        public override void ContinueUpdateObb()
        {
            parentImpl.ContinueUpdateObb();
        }

        public override void ReloadObb()
        {
            parentImpl.ReloadObb();
        }

        public override void SetClipboard(string content)
        {
            parentImpl.SetClipboard(content);
        }
        public override bool IsSupportApi(Api api)
        {
            return false;
        }

        public override void ExpandFunction(string functionName, string jsonParameter, string headName, OnFinish<ResultExpand> expandFunctionListener)
        {

        }
        public override void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<ResultGetHeadsetState> getHeadsetStateListener)
        {
            parentImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }

        public override void SendGuideFinish()
        {

        }

        public override void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {

        }

        public override void StartNewGame(OnFinish<UserInfo> startNewGameListener)
        {

        }

        public override DiskInfo GetDiskInfo()
        {

            return parentImpl.GetDiskInfo();
        }

        public override string GetResFilePath()
        {
            return parentImpl.GetResFilePath();
        }

        public override string GetGameResUrl()
        {

            return parentImpl.GetGameResUrl();
        }

        public override string GetAuthInfo()
        {

            return null;
        }

        public override void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            parentImpl.DownloadText(url, retry, timeout, downloadTextListener);
        }


        public override void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
            parentImpl.GetNetStateInfo(getNetStateInfoListener);
        }

        public override NetStateInfo GetNetStateInfo()
        {

            return parentImpl.GetNetStateInfo(); ;
        }

        public override void SetApplicationLocale(string language, string country = "")
        {
            parentImpl.SetApplicationLocale(language, country);
        }

        public override void RestartApp()
        {
            parentImpl.RestartApp();
        }
    }
}
