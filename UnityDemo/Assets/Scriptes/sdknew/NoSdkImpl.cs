using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：方便PC测试用
    /// </summary>
    public class NoSdkImpl : IHonorApi
    {
        public void CleanAllNotification()
        {
        }

        public void CleanNotification(int id)
        {
        }

        public void ContinueUpdateObb()
        {
        }

        public void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
        }

        public void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
        }

        public void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
        }

        public void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
        }

        public void Exit(OnFinish<Result> exitListener)
        {
        }

        public void ExpandFunction(string functionName, string jsonParameter = "", string headName = "", OnFinish<ResultExpand> expandFunctionListener = null)
        {
        }

        public void GameStepInfo(string step, string type = "")
        {
        }

        public void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
        }

        public void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
        }

        public string GetAuthInfo()
        {
            return null;
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

        public void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category = "", string currency = "")
        {
        }

        public void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
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

        public string GetResFilePath()
        {
            return null;
        }

        public void GetServerList(OnFinish<ServerList> getServerListListener)
        {
        }

        public bool HasExitDialog()
        {
            return false;
        }

        public bool HasObbUpdate()
        {
            return false;
        }

        public void Init(HonorSDKGameObject gameObject, OnFinish<HonorResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
           
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

        public void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
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

        public void ReloadObb()
        {
        }

        public void RepairUpdateRes()
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

        public void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
        }

        public void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
        }

        public void SwitchAccount()
        {
        }

        public void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
        }

        public void UdpPush(string ip, string port, string gameRoleId)
        {
        }

        public void UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
        }
    }
}
