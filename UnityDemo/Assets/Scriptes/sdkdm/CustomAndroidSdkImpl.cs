using System;
using System.Collections.Generic;
using UnityEngine;

namespace Honor
{

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class CustomAndroidSdkImpl : HonorSDKImpl {
  
        AndroidJavaObject currentActivity;

        public CustomAndroidSdkImpl() {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        protected override void Init(string configs) {
            Debug.Log("CustomAndroidSdkImpl.init");
            currentActivity.Call("init",configs);
        }


        protected override void SetGameObjectName(string gameObjectName) {
            Debug.Log("CustomAndroidSdkImpl.SetGameObjectName");
            currentActivity.Call("setGameObjectName", gameObjectName);
        }

        public override void Login(OnFinish<UserInfo> loginListener, string channel = "", Dictionary<string, string> extra = null)
        {
            base.Login(loginListener, channel, extra);
            currentActivity.Call("login");
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener) {
            base.GetAppInfo(appInfoListener);
            currentActivity.Call("getAppInfo");
        }

        public override void GameStepInfo(string step, string type)
        {
            base.GameStepInfo(step, type);
            currentActivity.Call("gameStepInfo", step, type);
        }
       

        public override void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener) {
            base.RegisterNotchScreenInfoListener(getNotchInfoListener);
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
        public override void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener) {
            base.TranslateContent(srcContent, targetLan, id,translateContentListener);
            currentActivity.Call("translateContent", srcContent, targetLan, id);
        }
        public override void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener) {
            base.GetHardwareInfo(getHardwareListener);
            currentActivity.Call("getHardwareInfo");
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


        public override void SwitchAccount()
        {
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

        public override void Logout(OnFinish<Result> logoutListener, Dictionary<string, string> extra = null) {
            base.Logout(logoutListener, extra);
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

        public override void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener) {
            base.GetDynamicUpdate(rootDir, getDynamicUpdateListener);
            currentActivity.Call("getDynamicUpdate", rootDir);
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
            currentActivity.Call("setClipboard", content);
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

        public override void StartNewGame(OnFinish<UserInfo> startNewGameListener) {
            base.StartNewGame(startNewGameListener);
            currentActivity.Call("startNewGame");
        }

        public override DiskInfo GetDiskInfo() {
            base.GetDiskInfo();
            string result = currentActivity.Call<string>("getDiskInfo");
            DiskInfo info = new DiskInfo();
            JSONNode node = JSONNode.Parse(result);
            info.totalSize = node["totalSize"].AsLong;
            info.availSize = node["availSize"].AsLong;
            return info;
        }

        public override string GetResFilePath() {
            base.GetResFilePath();
            return currentActivity.Call<string>("getResFilePath")+"/";
        }



        public override string GetGameResUrl() {
            base.GetGameResUrl();
            return currentActivity.Call<string>("getGameResUrl")+"/";
        }

        public override string GetAuthInfo() {
            base.GetAuthInfo();
            return currentActivity.Call<string>("getAuthInfo");
        }

     

        public override void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            base.DownloadText(url, retry,timeout, downloadTextListener);
            currentActivity.Call("downloadText", url,retry,timeout);
        }


        public override void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
            base.GetNetStateInfo(getNetStateInfoListener);
            currentActivity.Call<string>("getNetStateInfo",true);
        }

        public override NetStateInfo GetNetStateInfo()
        {
            base.GetNetStateInfo();
            string result = currentActivity.Call<string>("getNetStateInfo",false);
            JSONNode node = JSONNode.Parse(result);
            NetStateInfo info = new NetStateInfo();
            info.wifi = node["wifi"].AsBool;
            info.networkConnect = node["networkConnect"].AsBool;
            return info;
        }

        public override void SetApplicationLocale(string language, string country = "")
        {
            base.SetApplicationLocale(language, country);
            currentActivity.Call("setApplicationLocale", language, country);
        }

        public override void RestartApp()
        {
            base.RestartApp();
            currentActivity.Call("restartApp");
        }
    }
}
