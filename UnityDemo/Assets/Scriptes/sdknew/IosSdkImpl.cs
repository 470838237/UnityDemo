//#define IosSDK
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：游戏调用IOS的接口
    /// </summary>
    class IosSdkImpl : IBaseApi
    {
        public IosSdkImpl()
        {

        }
#if IosSDK
       
        [DllImport("__Internal")]
        private static extern void startNewGame();
        [DllImport("__Internal")]
        private static extern void initConfig(string configs);
        [DllImport("__Internal")]
        private static extern void login();
        [DllImport("__Internal")]
        private static extern void switchAccount();
        [DllImport("__Internal")]
        private static extern void logout();
        [DllImport("__Internal")]
        private static extern void startBind();
        [DllImport("__Internal")]
        private static extern void pay(string jsonParameter);
        [DllImport("__Internal")]
        private static extern void uploadGameRoleInfo(string gameRoleInfo);
        [DllImport("__Internal")]
        private static extern void getServerList();
        [DllImport("__Internal")]
        private static extern void getGoodsList(string serverId, string category, string currency);
        [DllImport("__Internal")]
        private static extern void getNoticeList(string serverId, string language, string country, string type);
        [DllImport("__Internal")]
        private static extern void monitorGameStep(string step, string type);
        [DllImport("__Internal")]
        private static extern void uploadExceptionLog();//暂未实现
        [DllImport("__Internal")]
        private static extern void getBattery();
        [DllImport("__Internal")]
        private static extern void getMemory();
        [DllImport("__Internal")]
        private static extern void getCountryAndLanguage();
        [DllImport("__Internal")]
        private static extern void reportError(string errorMsg, string type);
        [DllImport("__Internal")]
        private static extern void getLanguage();//暂未实现
        [DllImport("__Internal")]
        private static extern void getNotchScreenInfo();
        [DllImport("__Internal")]
        private static extern void getAppInfo();
        [DllImport("__Internal")]
        private static extern void pushNotification(string content, int after, int noticeId);
        [DllImport("__Internal")]
        private static extern bool cleanNotification(int noticeId);
        [DllImport("__Internal")]
        private static extern void cleanAllNotification();
        [DllImport("__Internal")]
        private static extern void setGameObjectName(string gameObjectName);
        [DllImport("__Internal")]
        private static extern void expandFunction(string functionName, string jsonParameter);
        [DllImport("__Internal")]
        private static extern void sendGuideFinish();
#endif
        public void CleanAllNotification()
        {
           
        }

        public void CleanNotification(int id)
        {
            
        }

        public void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            
        }

        public void Exit(OnFinish<Result> exitListener)
        {
           
        }

        public void ExpandFunction(string functionName, string jsonParameter = "", string headName = "", OnFinish<ResultExpand> expandFunctionListener = null)
        {
            
        }

        public void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
           
        }

        public void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
           
        }

        public void GetCountryAndLanguage(OnFinish<Locale> getCountryAndLanguageListener)
        {
           
        }

        public DiskInfo GetDiskInfo()
        {
            return null;
        }

        public void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category = "", string currency = "")
        {
          
        }

        public void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<HeadsetStateInfo> getHeadsetStateListener)
        {
           
        }

        public void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
           
        }

        public void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
           
        }

        public NotchScreenInfo GetNotchScreenInfo()
        {
            return null;
        }

        public void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId = "", string language = "", string country = "", string type = "")
        {
            
        }

        public void GetServerList(OnFinish<ServerList> getServerListListener)
        {
          
        }

        public bool HasExitDialog()
        {
            return false;
        }

        public void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
           
        }

        public bool IsSupportApi(Api api)
        {
            return false;
        }

        public void Login(OnFinish<UserInfo> loginListener)
        {
           
        }

        public void Logout(OnFinish<Result> logoutListener)
        {
           
        }

        public void Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
           
        }

        public void PushNotification(string content, int delay, int id)
        {
            
        }

        public void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
           
        }

        public void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
        {
           
        }

        public void ReportError(string errorMsg, string type = "")
        {
           
        }

        public void RestartApp()
        {
            
        }

        public void SendGuideFinish()
        {
           
        }

        public void SetApplicationLocale(string language, string country = "")
        {
            
        }

        public void SetClipboard(string content)
        {
            
        }

        public void StartBind(OnFinish<ResultBind> startBindListener)
        {
           
        }

        public void StartNewGame(OnFinish<UserInfo> startNewGameListener)
        {
           
        }

        public void SwitchAccount()
        {
            
        }

        public void UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            
        }

        void IBaseApi.UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            throw new NotImplementedException();
        }
    }
}

