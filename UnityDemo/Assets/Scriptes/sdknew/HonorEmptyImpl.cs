

using System;

namespace HonorSdk
{
    class HonorEmptyImpl : EmptyImpl, IHonorApi
    {


        public void ContinueUpdateObb()
        {
            throw new NotImplementedException();
        }

        public void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            throw new NotImplementedException();
        }

        public void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            throw new NotImplementedException();
        }

        public void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            throw new NotImplementedException();
        }

        public void GameStepInfo(string step, string type = "")
        {
            throw new NotImplementedException();
        }

        public void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
            throw new NotImplementedException();
        }

        public string GetAuthInfo()
        {
            throw new NotImplementedException();
        }

        public void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            throw new NotImplementedException();
        }

        public void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            throw new NotImplementedException();
        }

        public string GetGameResUrl()
        {
            throw new NotImplementedException();
        }

        public void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener)
        {
            throw new NotImplementedException();
        }

        public string GetResFilePath()
        {
            throw new NotImplementedException();
        }

        public bool HasObbUpdate()
        {
            throw new NotImplementedException();
        }

        public void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            throw new NotImplementedException();
        }

        public void ReloadObb()
        {
            throw new NotImplementedException();
        }

        public void RepairUpdateRes()
        {
            throw new NotImplementedException();
        }

        public void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
            throw new NotImplementedException();
        }

        public void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            throw new NotImplementedException();
        }

        public void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
            throw new NotImplementedException();
        }

        public void UdpPush(string ip, string port, string gameRoleId)
        {
            throw new NotImplementedException();
        }
    }
}
