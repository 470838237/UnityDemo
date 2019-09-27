using System;
using UnityEngine;

namespace HonorSDK {

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class AndroidSdkImpl : HonorSDKImpl {

        AndroidJavaObject currentActivity;

        public AndroidSdkImpl() {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        protected override void Init() {
            currentActivity.Call("init");
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
        public override void TranslateContent(string srcContent, string targetLan, OnFinish<ResultTranslate> translateContentListener) {
            base.TranslateContent(srcContent, targetLan, translateContentListener);
            currentActivity.Call("translateContent", srcContent, targetLan);
        }
        public override void GetCpuAndGpu(OnFinish<CpuGpuInfo> getCpuAndGpuListener) {
            base.GetCpuAndGpu(getCpuAndGpuListener);
            currentActivity.Call("getCpuAndGpu");
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

        public override void UdpPush(string ip, string port, string gameRoleId) {
            base.UdpPush(ip, port, gameRoleId);
            currentActivity.Call("udpPush", ip, port, gameRoleId);
        }

        public override void StartRecordVideo(string serverURL, string bit, long recordMaxTime) {
            base.StartRecordVideo(serverURL, bit, recordMaxTime);
            currentActivity.Call("startRecordVideo", serverURL, bit, recordMaxTime);
        }
        public override void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener) {
            base.StopRecordVideo(stopRecordVideoListener);
            currentActivity.Call("stopRecordVideo");
        }
        public override void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener) {
            base.PlayVideo(videoUrl, playVideoListener);
            currentActivity.Call("playVideo", videoUrl);
        }

        public override void SwitchAccount(OnFinish<UserInfo> switchAccountListener) {
            base.SwitchAccount(switchAccountListener);
            currentActivity.Call("switchAccount");
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

        public override void GetDynamicUpdate(string type, OnFinish<ResultGetDynamic> getDynamicUpdateListener) {
            base.GetDynamicUpdate(type, getDynamicUpdateListener);
            currentActivity.Call("getDynamicUpdate", type);
        }

        public override void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener) {
            base.DownDynamicUpdate(downDynamicUpdateListener);
            currentActivity.Call("downDynamicUpdate");
        }

        public override void RepairUpdateRes() {
            base.RepairUpdateRes();
            currentActivity.Call("repairUpdateRes");
        }
        public override void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener) {
            base.GetForceUpdate(getForceUpdateListener);
            currentActivity.Call("getForceUpdate");
        }

        public override void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener) {
            base.DownForceUpdate(downForceUpdateListener);
            currentActivity.Call("downForceUpdate");
        }

        public override bool HasObbUpdate() {
            base.HasObbUpdate();
            return currentActivity.Call<bool>("hasObbUpdate");
        }
        public override void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener) {
            base.DownObbUpdate(downObbUpdateListener);
            currentActivity.Call("downObbUpdate");
        }

        public override void ContinueUpdateObb() {
            base.ContinueUpdateObb();
            currentActivity.Call("continueUpdateObb");
        }

        public override void ReloadObb() {
            base.ReloadObb();
            currentActivity.Call("reloadObb");
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
        public override void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<ResultGetHeadsetState> getHeadsetStateListener) {
            base.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            currentActivity.Call("getHeadsetState", notifyWhenHeadsetChanged ? "true" : "false");
        }

        public override void SendGuideFinish() {
            base.SendGuideFinish();
            currentActivity.Call("sendGuideFinish");
        }

        public override void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener) {
            base.GetABTestVer(getABTestVerListener);
            currentActivity.Call("getABTestVer");
        }
        public override void GetMobileAdapter(OnFinish<ResultGetMobileAdapter> getMobileAdapterListener) {
            base.GetMobileAdapter(getMobileAdapterListener);
            currentActivity.Call("getMobileAdapter");
        }
    }
}
