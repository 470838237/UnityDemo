//#define IosSDK
using System.Runtime.InteropServices;

namespace Honor
{

    /// <summary>
    /// 类说明：游戏调用IOS的接口
    /// </summary>
    class IosSdkImpl : SDKManager {

#if IosSDK
        public IosSdkImpl() {

        }
        protected override void Init(string configs) {
            initConfig(configs);
        }


        protected override void SetGameObjectName(string gameObjectName) {
            setGameObjectName(gameObjectName);
        }

        public override void Login(OnFinish<UserInfo> loginListener) {
            base.Login(loginListener);
            login();
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener) {
            base.GetAppInfo(appInfoListener);
            getAppInfo();
        }
        public override void GameStepInfo(string step, string type) {
            base.GameStepInfo(step, type);
            monitorGameStep(step, type);
        }

        public override void GetNotchScreenInfo(OnFinish<NotchScreenInfo> getNotchInfoListener) {
            base.GetNotchScreenInfo(getNotchInfoListener);
            getNotchScreenInfo();
        }

        public override void GetCountryAndLanguage(OnFinish<Locale> getCountryCodeListener) {
            base.GetCountryAndLanguage(getCountryCodeListener);
            getCountryAndLanguage();
        }

        public override void ReportError(string errorMsg, string type) {
            base.ReportError(errorMsg);
            reportError(errorMsg, type);
        }

        public override void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener) {
            base.GetMemory(getMemroyInfoListener);
            getMemory();
        }
        public override void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener) {
            base.GetBattery(getBatteryInfoListener);
            getBattery();
        }
        public override void PushNotification(string content, int delay, int id) {
            base.PushNotification(content, delay, id);
            pushNotification(content, delay, id);
        }

        public override void CleanNotification(int id) {
            base.CleanNotification(id);
            cleanNotification(id);
        }
        public override void CleanAllNotification() {
            base.CleanAllNotification();
            cleanAllNotification();
        }
   
        public override void SwitchAccount(OnFinish<UserInfo> switchAccountListener) {
            base.SwitchAccount(switchAccountListener);
            switchAccount();
        }

        public override void StartBind(OnFinish<ResultBind> startBindListener) {
            base.StartBind(startBindListener);
            startBind();
        }

        public override void Logout(OnFinish<Result> logoutListener) {
            base.Logout(logoutListener);
            logout();
        }

        public override bool HasExitDialog() {
            return false;
        }

        public override string UploadGameRoleInfo(GameRoleInfo gameRoleInfo) {
            string gameRoleInfoStr = base.UploadGameRoleInfo(gameRoleInfo);
            uploadGameRoleInfo(gameRoleInfoStr);
            return gameRoleInfoStr;
        }

        public override string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener) {
            string orderInfoStr = base.Pay(orderInfo, payListener);
            pay(orderInfoStr);
            return orderInfoStr;
        }

        public override void Exit(OnFinish<Result> exitListener) {
            base.Exit(exitListener);
        }

        public override void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId, string language, string country, string type) {
            base.GetNoticeList(getNoticeListListener, serverId, language, country, type);
            getNoticeList(serverId, language, country, type);
        }

        public override void GetServerList(OnFinish<ServerList> getServerListListener) {
            base.GetServerList(getServerListListener);
            getServerList();
        }

        public override void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category, string currency) {
            base.GetGoodsList(getGoodsListListener, serverId, category, currency);
            getGoodsList(serverId, category, currency);
        }
        public override void SetClipboard(string content) {
            base.SetClipboard(content);
        }
        public override bool IsSupportApi(Api api) {
            base.IsSupportApi(api);
            return true;
        }
        public override void ExpandFunction(string functionName, string jsonParameter, string headName, OnFinish<ResultExpand> expandFunctionListener) {
            base.ExpandFunction(functionName, jsonParameter, headName, expandFunctionListener);
            expandFunction(functionName, jsonParameter);
        }

        public override void SendGuideFinish() {
            base.SendGuideFinish();
            sendGuideFinish();
        }


        public override void StartNewGame(OnFinish<UserInfo> startNewGameListener) {
            base.StartNewGame(startNewGameListener);
            startNewGame();
        }


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
    }
}

