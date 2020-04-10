

namespace HonorSdk
{
    public class HonorMessageHandler : MessageHandler
    {
        //翻译成功
        const string TRANSLATE_SUCCESS = "translate_success";
        //翻译失败
        const string TRANSLATE_FAILED = "translate_failed";
        //获取手机硬件信息
        const string GET_HARDWARE_INFO = "get_hardware_info";
        //录音成功
        const string RECORD_VIDEO_SUCCESS = "record_video_success";
        //录音失败
        const string RECORD_VIDEO_FAILED = "record_video_failed";
        //播放录音
        const string START_PLAY_VIDEO = "play_video";
        //获取热更信息成功
        const string GET_DYNAMIC_UPDATE_SUCCESS = "get_dynamic_update_success";
        //获取热更信息事变
        const string GET_DYNAMIC_UPDATE_FAILED = "get_dynamic_update_failed";
        //下载热更成功
        const string DOWN_DYNAMIC_UPDATE_SUCCESS = "down_dynamic_update_success";
        //下载热更失败
        const string DOWN_DYNAMIC_UPDATE_FAILED = "down_dynamic_update_failed";
        //获取强更信息成功
        const string GET_FORCE_UPDATE_SUCCESS = "get_force_update_success";
        //获取强更信息失败
        const string GET_FORCE_UPDATE_FAILED = "get_force_update_failed";
        //下载强更信息成功
        const string DOWN_FORCE_UPDATE_SUCCESS = "down_force_update_success";
        //下载强更信息失败
        const string DOWN_FORCE_UPDATE_FAILED = "down_force_update_failed";
        //Obb下载成功
        const string DOWN_OBB_UPDATE_SUCCESS = "down_obb_update_success";
        //obb下载状态发送变化
        const string DOWN_OBB_STATE_CHANGED = "down_obb_state_changed";
        //获取ABTest成功
        const string GET_AB_TEST_FINISH = "get_ab_test_ver_finish";

        private OnFinish<ResultTranslate> translateContentListener;
        private OnFinish<HardwareInfo> getHardwareListener;
        private OnFinish<ResultVideoRecord> stopRecordVideoListener;
        private OnFinish<Result> playVideoListener;
        private OnFinish<ResultGetDynamic> getDynamicUpdateListener;
        private OnFinish<ResultDownload> downDynamicUpdateListener;
        private OnFinish<ResultGetForce> getForceUpdateListener;
        private OnFinish<ResultDownload> downForceUpdateListener;
        private OnFinish<ResultObbDownload> downObbUpdateListener;
        private OnFinish<ResultGetABTestVer> getABTestVerListener;

        public void RegisterCallback<T>(HonorApi function, OnFinish<T> callback)
        {
            
            switch (function)
            {
                case HonorApi.CONTINUE_UPDATE_OBB:break;

            }
        }

        public HonorMessageHandler(HonorSDKImpl manager) : base(manager)
        {
            this.manager = manager;
        }
        public override void OnReceive(string head, string body)
        {
            base.OnReceive(head, body);
            switch (head)
            {
                case TRANSLATE_FAILED:
                    TranslateContentFinish(false, body);
                    break;
                case TRANSLATE_SUCCESS:
                    TranslateContentFinish(true, body);
                    break;
                case GET_HARDWARE_INFO:
                    GetHardwareFinish(true, body);
                    break;
                case RECORD_VIDEO_SUCCESS:
                    StopRecordVideoFinish(true, body);
                    break;
                case RECORD_VIDEO_FAILED:
                    StopRecordVideoFinish(false, body);
                    break;
                case START_PLAY_VIDEO:
                    PlayVideoFinish(true, body);
                    break;
                case GET_DYNAMIC_UPDATE_SUCCESS:
                    GetDynamicUpdateFinish(true, body);
                    break;
                case GET_DYNAMIC_UPDATE_FAILED:
                    GetDynamicUpdateFinish(false, body);
                    break;
                case DOWN_DYNAMIC_UPDATE_SUCCESS:
                    DownDynamicUpdateFinish(true, body);
                    break;
                case DOWN_DYNAMIC_UPDATE_FAILED:
                    DownDynamicUpdateFinish(false, body);
                    break;
                case GET_FORCE_UPDATE_SUCCESS:
                    GetForceUpdateFinish(true, body);
                    break;
                case GET_FORCE_UPDATE_FAILED:
                    GetForceUpdateFinish(false, body);
                    break;
                case DOWN_FORCE_UPDATE_SUCCESS:
                    DownForceUpdateFinish(true, body);
                    break;
                case DOWN_FORCE_UPDATE_FAILED:
                    DownForceUpdateFinish(false, body);
                    break;
                case DOWN_OBB_UPDATE_SUCCESS:
                    DownObbUpdateFinish(true, body);
                    break;
                case DOWN_OBB_STATE_CHANGED:
                    DownObbUpdateFinish(false, body);
                    break;
                case GET_AB_TEST_FINISH:
                    GetABTestVerFinish(true, body);
                    break;

                default: break;
            }
        }


        private void GetABTestVerFinish(bool success, string body)
        {
            ResultGetABTestVer result = new ResultGetABTestVer();
            result.success = success;
            int plan;
            bool parseResult = int.TryParse(body, out plan);
            if (parseResult)
            {
                result.plan = plan;
            }
            getABTestVerListener(result);
        }


        private void DownObbUpdateFinish(bool success, string body)
        {
            ResultObbDownload result = new ResultObbDownload();
            result.stateChanged = !success;
            if (result.stateChanged)
            {
                result.state = body;
            }
            else
            {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            downObbUpdateListener(result);
        }

        private void DownForceUpdateFinish(bool success, string body)
        {
            ResultDownload result = new ResultDownload();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            else
            {
                result.message = body;
            }
            downForceUpdateListener(result);
        }

        private void GetForceUpdateFinish(bool success, string body)
        {
            ResultGetForce result = new ResultGetForce();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
            }
            else
            {
                result.message = body;
            }
            getForceUpdateListener(result);
        }

        private void DownDynamicUpdateFinish(bool success, string body)
        {
            ResultDownload result = new ResultDownload();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            else
            {
                result.message = body;
            }
            downDynamicUpdateListener(result);
        }
        private void GetDynamicUpdateFinish(bool success, string body)
        {
            ResultGetDynamic result = new ResultGetDynamic();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.dynamicResPath = node["dynamicResPath"].Value;
            }
            else
            {
                result.message = body;
            }
            getDynamicUpdateListener(result);
        }
        private void StopRecordVideoFinish(bool success, string body)
        {
            ResultVideoRecord result = new ResultVideoRecord();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.time = node["time"].AsInt;
                result.url = node["url"].Value;
            }
            else
            {
                result.message = body;
            }
            stopRecordVideoListener(result);
        }
        private void PlayVideoFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            playVideoListener(result);
        }
        private void GetHardwareFinish(bool success, string body)
        {
            HardwareInfo info = new HardwareInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                //cpu 是json 字符串数组
                info.cpu = node["cpu"].Value;
                info.gpu = node["gpu"].Value;
                info.cpuFreq = node["cpuFreq"].AsInt;
                info.cpuNum = node["cpuNum"].AsInt;
                info.totalMemory = node["totalMemory"].AsInt;
                info.freeMemory = node["freeMemory"].AsInt;
                info.deviceModel = node["deviceModel"].Value;
            }
            else
            {
                info.message = body;
            }
            getHardwareListener(info);
        }
        private void TranslateContentFinish(bool success, string body)
        {
            ResultTranslate result = new ResultTranslate();
            result.success = success;
            JSONNode node = JSONNode.Parse(body);
            result.id = node["id"].AsInt;
            result.translateReult = node["translatedText"].Value;
            translateContentListener(result);
        }


    }
}
