

using System;
using System.Collections.Generic;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class CustomAndroidSdkImpl : AndroidSdkImpl, IHonorApi
    {

        public CustomAndroidSdkImpl() : base()
        {

        }

        public virtual void GameStepInfo(string step, string type)
        {

            currentActivity.Call("gameStepInfo", step, type);
        }

        public void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
            currentActivity.Call("translateContent", srcContent, targetLan, id);
        }
        public void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
        {
            currentActivity.Call("getHardwareInfo");
        }

        public void UdpPush(string ip, string port, string gameRoleId)
        {
            currentActivity.Call("udpPush", ip, port, gameRoleId);
        }

        public void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
            currentActivity.Call("startRecordVideo", serverURL, bit, recordMaxTime);
        }
        public void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            currentActivity.Call("stopRecordVideo");
        }
        public void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            currentActivity.Call("playVideo", videoUrl);
        }

        public void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            currentActivity.Call("getDynamicUpdate", rootDir);
        }

        public void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            currentActivity.Call("downDynamicUpdate");
        }

        public void RepairUpdateRes()
        {
            currentActivity.Call("repairUpdateRes");
        }
        public void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            currentActivity.Call("getForceUpdate");
        }

        public void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            currentActivity.Call("downForceUpdate");
        }

        public bool HasObbUpdate()
        {
            return currentActivity.Call<bool>("hasObbUpdate");
        }
        public void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            currentActivity.Call("downObbUpdate");
        }

        public void ContinueUpdateObb()
        {
            currentActivity.Call("continueUpdateObb");
        }

        public void ReloadObb()
        {
            currentActivity.Call("reloadObb");
        }

        public void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
            currentActivity.Call("getABTestVer");
        }

        public virtual string GetResFilePath()
        {
            return currentActivity.Call<string>("getResFilePath") + "/";
        }

        public string GetGameResUrl()
        {
            return currentActivity.Call<string>("getGameResUrl") + "/";
        }

        public string GetAuthInfo()
        {
            return currentActivity.Call<string>("getAuthInfo");
        }

        public void Init(HonorSDKGameObject gameObject, OnFinish<HonorResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            base.Init(gameObject, null, gameResVersion, configs);
        }
    }
}
