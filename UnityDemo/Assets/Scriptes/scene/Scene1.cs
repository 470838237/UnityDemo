using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using honorsdk;
using UnityEngine.SceneManagement;

public class Scene1 : BaseScene
{
    private Button loginButton;
    // Use this for initialization
    void OnEnable()
    {
        Init();
        GameObject gameObject = transform.Find("LoginButton").gameObject;
        loginButton = gameObject.GetComponent<Button>();
        loginButton.enabled = false;
        loginButton.onClick.AddListener(delegate()
        {
            Login();
        });
    }

    private void Init()
    {
        HonorSDK.GetInstance().Init(LoadTest.Instance.sdkObj, delegate (Result initResult)
        {
            Debug.Log("HonorSDK:Init.success = " + initResult.success
                + ",message =" + initResult.message
                );
            InitFinish();
        });
    }

    private void InitFinish()
    {
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

    private void GetAppInfo()
    {
        HonorSDK.GetInstance().GetAppInfo(delegate (AppInfo appInfo)
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
        HonorSDK.GetInstance().GetBattery(delegate (BatteryInfo batteryInfo)
        {
            Debug.Log("HonorSDK:GetBattery.scale = " + batteryInfo.scale
               + ",level =" + batteryInfo.level
               );
        });
    }

    private void GameStepInfo()
    {
        HonorSDK.GetInstance().GameStepInfo("-10086","");
    }

    private void GetCountryCode()
    {
        HonorSDK.GetInstance().GetCountryCode(delegate (string countryCode)
        {
            Debug.Log("HonorSDK:GetCountryCode.countryCode = " + countryCode
               );
        });
    }


    private void GetCpuAndGpu()
    {
        HonorSDK.GetInstance().GetCpuAndGpu(delegate (CpuGpuInfo cpuGpuInfo)
        {
            Debug.Log("HonorSDK:GetCpuAndGpu.cpu = " + cpuGpuInfo.cpu
                + ",cpuFreq =" + cpuGpuInfo.cpuFreq
                + ",cpuNum =" + cpuGpuInfo.cpuNum
                 + ",gpu =" + cpuGpuInfo.gpu
               );
        });
    }

    private void GetMemory()
    {
        HonorSDK.GetInstance().GetMemory(delegate (MemoryInfo memoryInfo)
        {
            Debug.Log("HonorSDK:GetMemory.availMem = " + memoryInfo.availMem
               + ",totalMem =" + memoryInfo.totalMem
              );
        });
    }

    private void ReportError()
    {
        HonorSDK.GetInstance().ReportError("HonorSDK:Test ReportError");
    }

    private void PushNotification()
    {
        HonorSDK.GetInstance().PushNotification("HonorSDK:Test PushNotification", 10, 10086);
    }

    private void GetNoticeList()
    {
        HonorSDK.GetInstance().GetNoticeList("", "", "", delegate (NoticeList result)
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
        });
    }


   

    private void GetForceUpdate()
    {

        HonorSDK.GetInstance().GetForceUpdate(delegate(ResultGetForce result)
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
        HonorSDK.GetInstance().DownForceUpdate(delegate (ResultDownload result)
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
        bool hasObbUpdtae = HonorSDK.GetInstance().HasObbUpdate();
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
        HonorSDK.GetInstance().DownObbUpdate(delegate(ResultObbDownload result)
        {
            Debug.Log("HonorSDK:DownObbUpdate.stateChanged = " + result.stateChanged);
            if (result.stateChanged)
            {
                Debug.Log("HonorSDK:DownObbUpdate.state = " + result.state);
                if (ResultObbDownload.STATE_COMPLETED.Equals(result.state))
                {
                    HonorSDK.GetInstance().ReloadObb();
                }
                else
                {
                    HonorSDK.GetInstance().ContinueUpdateObb();
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
        HonorSDK.GetInstance().GetDynamicUpdate("GameRes", delegate (ResultGetDynamic result)
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
                    loginButton.enabled = true;
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
        HonorSDK.GetInstance().DownDynamicUpdate(delegate(ResultDownload result)
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


    void Awake()
    {
        Debug.Log("awake = " + Time.time);
    }


    public void Login()
    {
        
        HonorSDK.GetInstance().Login(delegate (UserInfo userInfo)
        {
            Debug.Log("HonorSDK:Login.success = " + userInfo.success);
            if (userInfo.success)
            {
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
