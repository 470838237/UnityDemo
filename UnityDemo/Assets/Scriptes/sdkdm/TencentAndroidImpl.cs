
namespace Honor
{
    class TencentAndroidImpl : HonorSDKImpl
    {
        public TencentAndroidImpl()
        {
        }
        protected override void Init(string configs)
        {
           
        }


        protected override void SetGameObjectName(string gameObjectName)
        {
            
        }

        public override void Login(OnFinish<UserInfo> loginListener)
        {
         
        }

        public override void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
        }

        public override void GameStepInfo(string step, string type)
        {
           
        }


        public override void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
           
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
            return null;
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
          
            return null;
        }

        public override string GetAuthInfo()
        {
            
            return null;
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
