using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSDK
{

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class NoSdkImpl : HonorSDKImpl
    {

        AndroidJavaObject currentActivity;

        public NoSdkImpl()
        {
            
        }
        public override void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null) {
            Debug.Log("Init:gameObject=" + gameObject.name + ";gameResVersion=" + gameResVersion);
            base.Init(gameObject, initListener, gameResVersion, configs);
            InitFinish(true, "{\"abtest\":\"\",\"honor_log_debug\":\"true\",\"dynamic_setting_file_path\":\"\"}");
            GetNotchInfoFinish(true, "{\"height\":0,\"width\":0}");
        }

        protected override void Init(string configs)
        {
          
        }


        protected override void SetGameObjectName(string gameObjectName)
        {
            
        }

        public override void Login(OnFinish<UserInfo> loginListener)
        {
            base.Login(loginListener);
            currentActivity.Call("login");
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            base.GetAppInfo(appInfoListener);
            currentActivity.Call("getAppInfo");
        }

        public override void GameStepInfo(string step, string type)
        {
            Debug.Log("GameStepInfo:step=" + step+";type="+ type);
        }


        public override void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
            base.RegisterNotchScreenInfoListener(getNotchInfoListener);
            GetNotchInfoFinish(true,"{\"height\":0,\"width\":0}");
        }

        public override void GetCountryAndLanguage(OnFinish<Locale> getCountryCodeListener)
        {
           
        }

        public override void ReportError(string errorMsg, string type)
        {
           
        }

        public override void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
           
        }


        public override void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
           
        }
        public override void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
            
        }
        public override void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
        {
           
        }
        public override void PushNotification(string content, int delay, int id)
        {
           
        }

        public override void CleanNotification(int id)
        {
          
        }
        public override void CleanAllNotification()
        {
           
        }

        public override void UdpPush(string ip, string port, string gameRoleId)
        {
          
        }

        public override void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
         
        }
        public override void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {

        }
        public override void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
         
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
           
        }

        public override bool HasExitDialog()
        {
            return false;
        }

        public override string UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
          
            return "";
        }

        public override string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
         
            return null;
        }

        public override void Exit(OnFinish<Result> exitListener)
        {
          
        }

        public override void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId, string language, string country, string type)
        {
           
        }

        public override void GetServerList(OnFinish<ServerList> getServerListListener)
        {
          
        }

        public override void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category, string currency)
        {
            
        }

        public override void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
           
        }

        public override void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
           
        }

        public override void RepairUpdateRes()
        {
          
        }
        public override void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            
        }

        public override void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
          
        }

        public override bool HasObbUpdate()
        {
            return false;
        }
        public override void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
           
        }

        public override void ContinueUpdateObb()
        {
          
        }

        public override void ReloadObb()
        {
           
        }

        public override void SetClipboard(string content)
        {
          
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
           
            return null;
        }

        public override string GetResFilePath()
        {
         
            return null;
        }



        public override string GetGameResUrl()
        {
            return "";
        }

        public override string GetAuthInfo()
        {
          
            return "";
        }



        public override void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            
        }


        public override void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
      
        }

        public override NetStateInfo GetNetStateInfo()
        {

            return null;
        }

        public override void SetApplicationLocale(string language, string country = "")
        {
           
        }

        public override void RestartApp()
        {
       
        }
    }
}
