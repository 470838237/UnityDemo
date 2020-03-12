#define AndroidSDK
using System;
using UnityEngine;

namespace HonorSDK {

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class AndroidSdkImpl : SDKManager {

#if AndroidSDK
        AndroidJavaObject currentActivity;

        public AndroidSdkImpl() {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        protected override void Init(string configs) {
            currentActivity.Call("init", configs);
        }
        protected override void SetGameObjectName(string gameObjectName) {
            currentActivity.Call("setGameObjectName", gameObjectName);
        }
        public override void Login(OnFinish<UserInfo> loginListener) {
            base.Login(loginListener);
            currentActivity.Call("login");
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener) {
            base.GetAppInfo(appInfoListener);
            currentActivity.Call("getAppInfo");
        }
        public override void GameStepInfo(string step, string type) {
            base.GameStepInfo(step, type);
            currentActivity.Call("gameStepInfo", step, type);
        }

        public override void GetNotchScreenInfo(OnFinish<NotchScreenInfo> getNotchInfoListener) {
            base.GetNotchScreenInfo(getNotchInfoListener);
            currentActivity.Call("getNotchScreenInfo");
        }

        public override void GetCountryAndLanguage(OnFinish<Locale> getCountryCodeListener) {
            base.GetCountryAndLanguage(getCountryCodeListener);
            currentActivity.Call("getCountryAndLanguage");
        }

        public override void ReportError(string errorMsg, string type) {
            base.ReportError(errorMsg, type);
            currentActivity.Call("reportError", errorMsg, type);
        }

        public override void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener) {
            base.GetMemory(getMemroyInfoListener);
            currentActivity.Call("getMemory");
        }


        public override void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener) {
            base.GetBattery(getBatteryInfoListener);
            currentActivity.Call("getBattery");
        }

        public override void PushNotification(string content, int delay, int id) {
            base.PushNotification(content, delay, id);
            currentActivity.Call("pushNotification", content, delay, id);
        }

        public override void CleanNotification(int id) {
            base.CleanNotification(id);
            currentActivity.Call("cleanNotification", id);
        }
        public override void CleanAllNotification() {
            base.CleanAllNotification();
            currentActivity.Call("cleanAllNotification");
        }

        public override void SwitchAccount() {
            currentActivity.Call("switchAccount");
        }

        public override void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
        {
            base.RegisterSwitchAccountListener(switchAccountListener);
            currentActivity.Call("registerSwitchAccountListener");
        }

        public override void StartBind(OnFinish<ResultBind> startBindListener) {
            base.StartBind(startBindListener);
            currentActivity.Call("startBind");
        }

        public override void Logout(OnFinish<Result> logoutListener) {
            base.Logout(logoutListener);
            currentActivity.Call("logout");
        }

        public override bool HasExitDialog() {
            return currentActivity.Call<bool>("hasExitDialog");
        }

        public override string UploadGameRoleInfo(GameRoleInfo gameRoleInfo) {
            string gameRoleInfoStr = base.UploadGameRoleInfo(gameRoleInfo);
            currentActivity.Call("uploadGameRoleInfo", gameRoleInfoStr);
            return gameRoleInfoStr;
        }

        public override string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener) {
            string orderInfoStr = base.Pay(orderInfo, payListener);
            currentActivity.Call("pay", orderInfoStr);
            return orderInfoStr;
        }

        public override void Exit(OnFinish<Result> exitListener) {
            base.Exit(exitListener);
            currentActivity.Call("exit");
        }

        public override void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId, string language, string country, string type) {
            base.GetNoticeList(getNoticeListListener, serverId, language, country, type);
            currentActivity.Call("getNoticeList", serverId, language, country, type);
        }

        public override void GetServerList(OnFinish<ServerList> getServerListListener) {
            base.GetServerList(getServerListListener);
            currentActivity.Call("getServerList");
        }

        public override void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category, string currency) {
            base.GetGoodsList(getGoodsListListener, serverId, category, currency);
            currentActivity.Call("getGoodsList", serverId, category, currency);
        }
        public override void SetClipboard(string content) {
            base.SetClipboard(content);
            currentActivity.Call("setClipboard");
        }
        public override bool IsSupportApi(Api api) {
            base.IsSupportApi(api);
            return currentActivity.Call<bool>("isSupportApi", Convert.ToInt32(api));
        }

        public override void ExpandFunction(string functionName, string jsonParameter, string headName, OnFinish<ResultExpand> expandFunctionListener) {
            base.ExpandFunction(functionName, jsonParameter, headName, expandFunctionListener);
            currentActivity.Call("expandFunction", functionName, jsonParameter, headName);
        }

        public override void SendGuideFinish() {
            base.SendGuideFinish();
            currentActivity.Call("sendGuideFinish");
        }

        public override void StartNewGame(OnFinish<UserInfo> startNewGameListener) {
            base.StartNewGame(startNewGameListener);
            currentActivity.Call("startNewGame");
        }


        public override void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
            base.GetNetStateInfo(getNetStateInfoListener);
            currentActivity.Call("getNetStateInfo");
        }

        public override void SetApplicationLocale(string language, string country = "")
        {
            base.SetApplicationLocale(language, country);
            currentActivity.Call("setApplicationLocale", language, country);
        }

       


#endif
    }
}
