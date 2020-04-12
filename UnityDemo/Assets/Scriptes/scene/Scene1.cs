using HonorSDK;
using System.Diagnostics;

public class Scene1 : BaseScene
{
    private Button loginButton;
    // Use this for initialization
    void Awake()
    {
        Debug.Log("awake = " + Time.time);
        Init();
        GameObject gameObject = transform.Find("LoginButton").gameObject;
        loginButton = gameObject.GetComponent<Button>();
        Debug.LogError("000000000000");
        loginButton.enabled = false;
        loginButton.onClick.AddListener(delegate()
        {
            Debug.LogError("1111111111111111");
            Login();
        });
    }


    private void Init()
    {

        Debug.Log("unity.Init");
        HonorSDKImpl.CreateInstance(HonorSDKImpl.ePlat.Android);
        HonorSDKImpl.GetInstance().Init(LoadTest.Instance.sdkObj, delegate (ResultInit initResult)
        {       
            string url = initResult.getCustomParameter("mobileAdapterKey");
            string privacyUrl = initResult.getCustomParameter("privacy");
            string termsUrl = initResult.getCustomParameter("terms");
            Debug.Log("HonorSDK:Init.success = " + initResult.success
             + ",message =" + initResult.message
             );
            if (initResult.success)
            {
                loginButton.enabled = true;
            }
            InitFinish();


        }, "1");
    }
    int identify ;

    private void InitFinish()
    {
        //  HonorSDKImpl.GetInstance().RequestDownload(new int[] { 1,2},new string[] { "1","2"},new int[] { 1, 2 }, new int[] { 1, 2 }, new long[] { 1, 2 }, new int[] { 1, 2 });

        string OPEN_GOOGLE_PLAY_COMMENTS = "open_google_play_comments";
        HonorSDKImpl.GetInstance().ExpandFunction(OPEN_GOOGLE_PLAY_COMMENTS);


        HonorSDKImpl.GetInstance().RegisterIdentifyListener(delegate(IdentifyInfo info) {

            identify = info.identify;

        });

        GetAppInfo();
        GetBattery();
        GameStepInfo();
        GetCountryCode();
        GetCpuAndGpu();
        GetMemory();
        ReportError();
        PushNotification();
        GetDynamicUpdate();
        GetNoticeList();
    }
    private void GetABTest()
    {
        HonorSDKImpl.GetInstance().GetABTestVer(delegate(ResultGetABTestVer result) {
        });
    }
    private void GetAppInfo()
    {
        HonorSDKImpl.GetInstance().GetAppInfo(delegate (AppInfo appInfo)
        {
            Debug.Log("HonorSDK:GetAppInfo.deviceId = " + appInfo.deviceId
               + ",appName =" + appInfo.appName
               + ",version =" + appInfo.version
               + ",platform =" + appInfo.platform
               + ",appId =" + appInfo.appId
               );
        });
    }
    private void GetBattery()
    {

     

        




        HonorSDKImpl.GetInstance().GetBattery(delegate (BatteryInfo batteryInfo)
        {
            Debug.Log("HonorSDK:GetBattery.scale = " + batteryInfo.scale
               + ",level =" + batteryInfo.level
               );
        });
    }

    private void GameStepInfo()
    {
        HonorSDKImpl.GetInstance().GameStepInfo("-10086","");
    }

    private void GetCountryCode()
    {
        HonorSDKImpl.GetInstance().GetCountryAndLanguage(delegate (Locale countryCode)
        {
            Debug.Log("HonorSDK:GetCountryCode.countryCode = " + countryCode
               );
        });
    }


    private void GetCpuAndGpu()
    {
     
    }

    private void GetMemory()
    {
        HonorSDKImpl.GetInstance().GetMemory(delegate (MemoryInfo memoryInfo)
        {
            Debug.Log("HonorSDK:GetMemory.availMem = " + memoryInfo.availMem
               + ",totalMem =" + memoryInfo.totalMem
              );
        });
    }

    private void ReportError()
    {
        HonorSDKImpl.GetInstance().ReportError("HonorSDK:Test ReportError");
    }

    private void PushNotification()
    {
        HonorSDKImpl.GetInstance().PushNotification("HonorSDK:Test PushNotification", 10, 10086);
    }

    private void GetNoticeList()
    {
        HonorSDKImpl.GetInstance().GetNoticeList(delegate (NoticeList result)
        {
            Debug.Log("HonorSDK:GetNoticeList.success = " + result.success);
            if (result.success)
            {
                List<NoticeInfo> notices = result.notices;
                foreach (NoticeInfo notice in notices)
                {
                    Debug.Log("HonorSDK:GetNoticeList.notices.item.content =" + notice.content
                            + ",endTime =" + notice.endTime
                            + ",image =" + notice.image
                            + ",important =" + notice.important
                            + ",link =" + notice.link
                            + ",mode =" + notice.mode
                            + ",sort =" + notice.sort
                            + ",startTime =" + notice.startTime
                            + ",status =" + notice.status
                            + ",title =" + notice.title
                            + ",type =" + notice.type
                        );
                }

            }
            else
            {
                Debug.Log("HonorSDK:GetNoticeList.message = " + result.message);
            }
        },"", "", "", "");
    }


   

    private void GetForceUpdate()
    {

        HonorSDKImpl.GetInstance().GetForceUpdate(delegate(ResultGetForce result)
        {
            Debug.Log("HonorSDK:GetForceUpdate.success = " + result.success);
            if (result.success)
            {
                Debug.Log("HonorSDK:GetForceUpdate.totalSize = " + result.totalSize);
                if (result.totalSize > 0)
                {               
                    DownForceUpdate();
                }
                else
                {
                    GetObbUpdate();
                }
            }
            else
            {
                Debug.Log("HonorSDK:GetForceUpdate.message = " + result.message);
            }
        });

    }

    private void DownForceUpdate()
    {
        HonorSDKImpl.GetInstance().DownForceUpdate(delegate (ResultDownload result)
        {
            Debug.Log("HonorSDK:DownForceUpdate.success = " + result.success);
            if (result.success)
            {
                Debug.Log("HonorSDK:DownForceUpdate.currentSize = " + result.currentSize
                    + ",totalSize =" + result.totalSize
                    );
            }
            else
            {
                Debug.Log("HonorSDK:DownForceUpdate.message = " + result.message);
            }

        });
    }

    private void GetObbUpdate()
    {
        bool hasObbUpdtae = HonorSDKImpl.GetInstance().HasObbUpdate();
        Debug.Log("HonorSDK:HasObbUpdate.hasObbUpdtae = " + hasObbUpdtae);
        if (hasObbUpdtae)
        {
            DownObbUpdate();
        }
        else
        {
            GetDynamicUpdate();
        }
    }

    private void DownObbUpdate()
    {
        HonorSDKImpl.GetInstance().DownObbUpdate(delegate(ResultObbDownload result)
        {
            Debug.Log("HonorSDK:DownObbUpdate.stateChanged = " + result.stateChanged);
            if (result.stateChanged)
            {
                Debug.Log("HonorSDK:DownObbUpdate.state = " + result.state);
                if (ResultObbDownload.STATE_COMPLETED.Equals(result.state))
                {
                    HonorSDKImpl.GetInstance().ReloadObb();
                }
                else
                {
                    HonorSDKImpl.GetInstance().ContinueUpdateObb();
                }
            }
            else
            {
                Debug.Log("HonorSDK:DownObbUpdate.currentSize = " + result.currentSize
                     + ",totalSize =" + result.totalSize
                    );
            }
        });
    }

  

    private void GetDynamicUpdate()
    {
        HonorSDKImpl.GetInstance().GetDynamicUpdate("GameRes", delegate (ResultGetDynamic result)
        {
            Debug.Log("HonorSDK:GetDynamicUpdate.success = " + result.success);
            if (result.success)
            {
                Debug.Log("HonorSDK:GetDynamicUpdate.totalSize = " + result.totalSize
                     + ",dynamicResPath =" + result.dynamicResPath
                );
                if (result.totalSize > 0)
                {
                    DownDynamicUpdate();
                }
                {
                    Debug.LogError("22222222222");              
                }
               
            }
            else
            {
                Debug.Log("HonorSDK:GetDynamicUpdate.message = " + result.message);
            }
        });

    }

    private void DownDynamicUpdate()
    {
        HonorSDKImpl.GetInstance().DownDynamicUpdate(delegate(ResultDownload result)
        {
            Debug.Log("HonorSDK:DownDynamicUpdate.success = " + result.success);
            if (result.success)
            {
                Debug.Log("HonorSDK:DownDynamicUpdate.currentSize = " + result.currentSize
                    + ",totalSize=" + result.totalSize
                    );
            }
            else
            {
                Debug.Log("HonorSDK:DownDynamicUpdate.message = " + result.message);
            }
        });
    }




    public void Login()
    {
        int identify = -1;
        HonorSDKImpl.GetInstance().RegisterIdentifyListener(delegate (IdentifyInfo identifyInfo) {
            identify = identifyInfo.identify;

        });
        HonorSDKImpl.GetInstance().Login(delegate (UserInfo userInfo)
        {
            Debug.Log("HonorSDK:Login.success = " + userInfo.success);
            if (userInfo.success)
            {

                string playTime = userInfo.getExtra(UserInfo.EXTRA_PLAY_TIME);

                string authInfo =   HonorSDKImpl.GetInstance().GetAuthInfo();
                authInfo += "&playtime="+ authInfo;
                authInfo+= "&identify=" + identify;









                Debug.Log("HonorSDK:Login.accessToken = " + userInfo.accessToken
               + ",nickName =" + userInfo.nickName
               + ",uid =" + userInfo.uid
              );
                //SceneManager.LoadScene("scene2");

                //SceneManager.LoadScene("Scene2");
                LoadTest.Instance.ChangeScene(2);
            }
            else
            {
                Debug.Log("HonorSDK:Login.message = " + userInfo.message);
            }
            

        });


    }
    

}
