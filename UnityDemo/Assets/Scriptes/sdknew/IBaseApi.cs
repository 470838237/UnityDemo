
using System.Collections.Generic;

namespace HonorSdk
{
    public enum Api
    {
        SET_GAME_OBJECT = 0,
        INIT,
        GET_APP_INFO,
        REGISTER_NOTCH_SCREEN_INFO,
        GET_NOTCH_INFO,
        //语言国家
        GET_LOCALE,
        REPORT_ERROR,
        SetClipboard,
        SET_CLIPBOARD,
        GET_MEMORY,
        GET_BATTERY,
        PUSH_NOTIFICATION,
        CLEAN_NOTIFICATION,
        CLEAN_ALL_NOTIFICATION,
        LOGIN,
        //开始新游戏
        START_NEW_GAME,
        REGISTER_SWITCH_ACCOUNT,
        //切换账号
        SWITCH_ACCOUNT,
        //注销
        LOGOUT,
        //绑定
        BIND,
        HAS_EXIT_DIALOG,
        UPLOAD_GAME_ROLE_INFO,
        PAY,
        EXIT,
        GET_NOTICE_LIST,
        GET_SERVER_LIST,
        GET_GOODS_LIST,
        IS_SUPPORT_API,
        EXPAND_FUNCTION,
        SET_APPLICATION_LOCALE,
        SEND_GUIDE_FINISH,
        GET_NET_STATE_INFO,
        DOWNLOAD_TEXT,
        GET_HEADSET_STATE
          


    }
    /// <summary>
    /// 基础功能接口
    /// </summary>
    interface IBaseApi
    {

        void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null);
        void GetAppInfo(OnFinish<AppInfo> appInfoListener);
        void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener);
        NotchScreenInfo GetNotchScreenInfo();
        void GetCountryAndLanguage(OnFinish<Locale> getCountryAndLanguageListener);
        void ReportError(string errorMsg, string type = "");
        void SetClipboard(string content);
        void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener);
        void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener);
        void PushNotification(string content, int delay, int id);
        void CleanNotification(int id);
        void CleanAllNotification();
        void Login(OnFinish<UserInfo> loginListener);
        void StartNewGame(OnFinish<UserInfo> startNewGameListener);
        void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener);
        void SwitchAccount();
        void StartBind(OnFinish<ResultBind> startBindListener);
        void Logout(OnFinish<Result> logoutListener);
        bool HasExitDialog();
        void UploadGameRoleInfo(GameRoleInfo gameRoleInfo);
        void Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener);
        void Exit(OnFinish<Result> exitListener);
        void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId = "", string language = "", string country = "", string type = "");
        void GetServerList(OnFinish<ServerList> getServerListListener);
        void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category = "", string currency = "");
        bool IsSupportApi(Api api);
        void ExpandFunction(string functionName, string jsonParameter = "", string headName = "", OnFinish<ResultExpand> expandFunctionListener = null);
        void SetApplicationLocale(string language, string country = "");
        void SendGuideFinish();
        void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener);
        void RestartApp();
        DiskInfo GetDiskInfo();
        void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener);
        void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<HeadsetStateInfo> getHeadsetStateListener);
    }


}
