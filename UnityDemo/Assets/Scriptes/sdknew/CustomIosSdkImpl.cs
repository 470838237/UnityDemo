using System;
using System.Runtime.InteropServices;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：游戏调用IOS的接口
    /// </summary>
    class CustomIosSdkImpl : IosSdkImpl, IHonorApi
    {
#if IosSDK

        [DllImport("__Internal")]
        private static extern void startNewGame();
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
        private static extern void getNoticeList(string serverId, string language, string country, string type);
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
#endif
        public void ContinueUpdateObb()
        {
            
        }

        public void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            
        }

        public void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            
        }

        public void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            
        }

        public void GameStepInfo(string step, string type = "")
        {
          
        }

        public void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
          
        }

        public string GetAuthInfo()
        {
            return null;
        }

        public void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            
        }

        public void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
          
        }

        public string GetGameResUrl()
        {
            return null;

        }

        public void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
        {
          
        }

        public string GetResFilePath()
        {
            return null;
        }

        public bool HasObbUpdate()
        {
            return false;
        }

        public void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
           
        }

        public void ReloadObb()
        {
            
        }

        public void RepairUpdateRes()
        {
            
        }

        public void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
           
        }

        public void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
           
        }

        public void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
           
        }

        public void UdpPush(string ip, string port, string gameRoleId)
        {
           
        }
    }
}

