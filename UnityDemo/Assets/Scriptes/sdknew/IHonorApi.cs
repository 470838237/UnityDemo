

namespace HonorSdk
{
    /// <summary>
    /// Honor扩展接口
    /// </summary>
    /// 
    enum HonorApi {
        TRANSLATE = 1000,
        GET_HARDWARE_INFO,
        UDP_PUSH,
        START_RECORD_VIDEO,
        STOP_RECORD_VIDEO,
        PLAY_VIDEO,
        GET_DYNAMIC_UPDATE,
        DOWN_DYNAMIC_UPDATE,
        REPARE_UPDATE_RES,
        GET_FORCE_UPDATE,
        DOWN_FORCE_UPDATE,
        HAS_OBB_UPDATE,
        DOWN_OBB_UPDATE,
        CONTINUE_UPDATE_OBB,
        RELOAD_OBB,
        GET_ABTEST_VER,
        GET_RES_FILE_PATH,
        GET_GAME_RES_URL,
        GET_AUTH_INFO,
        REGISTER_NETWORK_STATE
    }


    interface IHonorApi : IBaseApi
    {
        void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener);
        void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareListener);
        void UdpPush(string ip, string port, string gameRoleId);
        void StartRecordVideo(string serverURL, string bit, long recordMaxTime);
        void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener);
        void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener);
        void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener);
        void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener);
        void RepairUpdateRes();
        void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener);
        void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener);
        bool HasObbUpdate();
        void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener);
        void ContinueUpdateObb();
        void ReloadObb();
        void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener);
        string GetResFilePath();
        string GetGameResUrl();
        string GetAuthInfo();
        void GameStepInfo(string step, string type = "");

    }
}
