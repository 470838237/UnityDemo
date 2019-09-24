using honorsdk.SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace honorsdk
{
    class IosSDKImpl : HonorSDK
    {

        public IosSDKImpl()
        {
            
        }
        protected override void Init()
        {       
            initConfig();
        }


        protected override void SetGameObjectName(string gameObjectName)
        {
            setGameObjectName(gameObjectName);
        }

        public override void Login(OnFinish<UserInfo> loginListener)
        {
            base.Login(loginListener);
            login();
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            base.GetAppInfo(appInfoListener);
            getAppInfo();
        }
        public override void GameStepInfo(string step, string type)
        {
            base.GameStepInfo(step, type);
            monitorGameStep(step, type);
        }

        public override void GetNotchScreenInfo(OnFinish<NotchScreenInfo> getNotchInfoListener)
        {
            base.GetNotchScreenInfo(getNotchInfoListener);
            getNotchScreenInfo();
        }

        public override void GetCountryCode(OnFinish<string> getCountryCodeListener)
        {
            base.GetCountryCode(getCountryCodeListener);
            getCountryCode();
        }

        public override void ReportError(string errorMsg, string type)
        {
            base.ReportError(errorMsg);
            reportError(errorMsg, type);
        }

        public override void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
            base.GetMemory(getMemroyInfoListener);
            getMemory();
        }
        public override void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
            base.GetBattery(getBatteryInfoListener);
            getBattery();
        }
        public override void TranslateContent(string srcContent, string targetLan, OnFinish<string> translateContentListener)
        {
            base.TranslateContent(srcContent, targetLan, translateContentListener);
            translateContent(srcContent, targetLan);
        }
        public override void GetCpuAndGpu(OnFinish<CpuGpuInfo> getCpuAndGpuListener)
        {
            base.GetCpuAndGpu(getCpuAndGpuListener);            
        }
        public override void PushNotification(string content, int delay, int id)
        {
            base.PushNotification(content, delay, id);
            pushNotification(content, delay, id);
        }

        public override void CleanNotification(int id)
        {
            base.CleanNotification(id);
            cleanNotification(id);
        }
        public override void CleanAllNotification()
        {
            base.CleanAllNotification();
            cleanAllNotification();
        }

        public override void UdpPush(string ip, string port, string gameRoleId)
        {
            base.UdpPush(ip, port, gameRoleId);          
        }

        public override void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
            base.StartRecordVideo(serverURL, bit, recordMaxTime);
            startRecordVideo(serverURL, bit, recordMaxTime);
        }
        public override void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            base.StopRecordVideo(stopRecordVideoListener);
            stopRecordVideo();
        }
        public override void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            base.PlayVideo(videoUrl, playVideoListener);
            playVideo(videoUrl);
        }

        public override void SwitchAccount(OnFinish<UserInfo> switchAccountListener)
        {
            base.SwitchAccount(switchAccountListener);
            switchAccount();
        }

        public override void StartBind(OnFinish<ResultBind> startBindListener)
        {
            base.StartBind(startBindListener);
            startBind();
        }

        public override void Logout(OnFinish<Result> logoutListener)
        {
            base.Logout(logoutListener);
            logout();
        }

        public override bool HasExitDialog()
        {            
            return false;
        }

        public override string UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            string gameRoleInfoStr = base.UploadGameRoleInfo(gameRoleInfo);
            uploadGameRoleInfo(gameRoleInfoStr);
            return gameRoleInfoStr;
        }

        public override string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
            string orderInfoStr = base.Pay(orderInfo, payListener);
            pay(orderInfoStr);
            return orderInfoStr;
        }

        public override void Exit(OnFinish<Result> exitListener)
        {
            base.Exit(exitListener);          
        }

        public override void GetNoticeList(string serverId, string language, String type, OnFinish<NoticeList> getNoticeListListener)
        {
            base.GetNoticeList(serverId, language, type, getNoticeListListener);
            getNoticeList(serverId, language, type);
        }

        public override void GetServerList(OnFinish<ServerList> getServerListListener)
        {
            base.GetServerList(getServerListListener);
            getServerList();
        }

        public override void GetGoodsList(string serverId, string category, string currency, OnFinish<GoodsList> getGoodsListListener)
        {
            base.GetGoodsList(serverId, category, currency,getGoodsListListener);
            getGoodsList(serverId, category, currency);
        }

        public override void GetDynamicUpdate(string type, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            base.GetDynamicUpdate(type, getDynamicUpdateListener);
            getDynamicUpdate(type);
        }

        public override void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            base.DownDynamicUpdate(downDynamicUpdateListener);
            downDynamicUpdate();
        }

        public override void RepairUpdateRes()
        {
            base.RepairUpdateRes();
            repairUpdateRes();
        }
        public override void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            base.GetForceUpdate(getForceUpdateListener);
            getForceUpdate();
        }

        public override void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            base.DownForceUpdate(downForceUpdateListener);
            downForceUpdate();
        }

        public override bool HasObbUpdate()
        {
            base.HasObbUpdate();
            return false;
        }
        public override void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            base.DownObbUpdate(downObbUpdateListener);           
        }

        public override void ContinueUpdateObb()
        {
            base.ContinueUpdateObb();            
        }

        public override void ReloadObb()
        {
            base.ReloadObb();           
        }

        public override void SetClipboard(string content)
        {
            base.SetClipboard(content);          
        }
        public override bool IsSupportApi(Api api)
        {
           base.IsSupportApi(api);
           return true;
        }
        public override void ExpandFunction(string functionName, string jsonParameter, string headName , OnFinish<ResultExpand> expandFunctionListener)
        {
            base.ExpandFunction(functionName,jsonParameter, headName, expandFunctionListener);
            expandFunction(functionName, jsonParameter);
        }
        public override void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<ResultGetHeadsetState> getHeadsetStateListener)
        {
            base.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            getHeadsetState(notifyWhenHeadsetChanged);
        }

        public override void SendGuideFinish()
        {
            base.SendGuideFinish();
            sendGuideFinish();
        }

        public override void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
            base.GetABTestVer(getABTestVerListener);
            getABTestVer();
        }
        public override void GetMobileAdapter(OnFinish<ResultGetMobileAdapter> getMobileAdapterListener)
        {
            base.GetMobileAdapter(getMobileAdapterListener);
            
        }


        [DllImport("__Internal")]
        private static extern void initConfig();
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
        private static extern void getNoticeList(string serverId, string language, string type);
        [DllImport("__Internal")]
        private static extern void translateContent(string srcContent, string targetLan);
        [DllImport("__Internal")]
        private static extern void monitorGameStep(string step, string type);
        [DllImport("__Internal")]
        private static extern void uploadExceptionLog();//暂未实现
        [DllImport("__Internal")]
        private static extern void getForceUpdate();
        [DllImport("__Internal")]
        private static extern void downForceUpdate();
        [DllImport("__Internal")]
        private static extern void getDynamicUpdate(string type);
        [DllImport("__Internal")]
        private static extern void downDynamicUpdate();
        [DllImport("__Internal")]
        private static extern void repairUpdateRes();//暂未实现
        [DllImport("__Internal")]
        private static extern void getBattery();
        [DllImport("__Internal")]
        private static extern void getMemory();
        [DllImport("__Internal")]
        private static extern void getCountryCode();
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
        private static extern void startRecordVideo(string videoUrl, string quality, long maxTime);
        [DllImport("__Internal")]
        private static extern void stopRecordVideo();
        [DllImport("__Internal")]
        private static extern void playVideo(string videoUrl);
        [DllImport("__Internal")]
        private static extern void setGameObjectName(string gameObjectName);
        [DllImport("__Internal")]
        private static extern void expandFunction(string functionName, string jsonParameter);
        [DllImport("__Internal")]
        private static extern void getHeadsetState(bool notifyWhenHeadsetChanged);
        [DllImport("__Internal")]
        private static extern void sendGuideFinish();
        [DllImport("__Internal")]
        private static extern void getABTestVer();


    }
}

