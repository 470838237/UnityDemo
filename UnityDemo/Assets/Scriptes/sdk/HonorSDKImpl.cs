using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSDK {


    public class IdentifyInfo
    {

        //-1:游客 0:未认证 1:未满8岁 2:未满16岁 3:未满18岁 4:成年
        public int identify
        {
            set; get;
        }

    }
    public class HardwareInfo : Result {
        //Gpu型号
        public string gpu {
            set; get;
        }
        //cpu型号 JSONArray类型字符串,数组成员为字符串类型  
        //由于各个手机获取cpu型号的接口不一致,该接口返回了常用的接口,开发者根据业务逻辑筛选
        public string cpu {
            set; get;
        }
        //Cpu核心数量
        public int cpuNum {
            set; get;
        }
        //Cpu最大频率
        public int cpuFreq {
            set; get;
        }
        //总内存
        public int totalMemory {
            set; get;
        }
        //空闲内存
        public int freeMemory {
            set; get;
        }
        //机型
        public string deviceModel;
        //cpu温度
        public double temperature;
    }

    public class ResultVideoRecord : Result {
        //语音存放链接
        public string url {
            set; get;
        }
        //语音时长 单位秒
        public long time {
            set; get;
        }
    }
    public class ResultTranslate : Result {
        public string translateReult {
            set; get;
        }
    }

    public class ResultGetDynamic : Result {
        //热更总大小 单位Byte
        public long totalSize {
            set; get;
        }
        //下载热更文件存储路径
        public string dynamicResPath {
            set; get;
        }
    }
    public class ResultDownload : Result {
        //下载总大小 单位Byte
        public long totalSize {
            set; get;
        }
        //已下载总大小 单位Byte
        public long currentSize {
            set; get;
        }
    }
    public class ResultGetForce : Result {
        //跳转应用商店返回强更大小
        public const int JUMP_APP_STORE = 1;
        //强更大小 单位Byte
        public long totalSize {
            set; get;
        }
    }
    public class ResultObbDownload {
        //需要下载总大小 单位Byte
        public long totalSize {
            set; get;
        }
        //已下载大小 单位Byte
        public long currentSize {
            set; get;
        }
        //obb下载过程返回的状态信息
        public string state {
            set; get;
        }
        //true表示状态发送变化返回状态信息,false表示没有状态变化时返回下载信息
        public bool stateChanged {
            set; get;
        }
        public const string STATE_COMPLETED = "state_completed";
        public const string STATE_IDLE = "state_idle";
        public const string STATE_FETCHING_URL = "state_fetching_url";
        public const string STATE_CONNECTING = "state_connecting";
        public const string STATE_DOWNLOADING = "state_downloading";
        public const string STATE_PAUSED_NETWORK_UNAVAILABLE = "state_paused_network_unavailable";
        public const string STATE_PAUSED_BY_REQUEST = "state_paused_by_request";
        public const string STATE_PAUSED_NEED_CELLULAR_PERMISSION = "state_paused_wifi_unavailable";
        public const string STATE_PAUSED_WIFI_DISABLED = "state_paused_wifi_disabled";
        public const string STATE_PAUSED_ROAMING = "state_paused_roaming";
        public const string STATE_PAUSED_NETWORK_SETUP_FAILURE = "state_paused_network_setup_failure";
        public const string STATE_PAUSED_SDCARD_UNAVAILABLE = "state_paused_sdcard_unavailable";
        public const string STATE_FAILED_UNLICENSED = "state_failed_unlicensed";
        public const string STATE_FAILED_FETCHING_URL = "state_failed_fetching_url";
        public const string STATE_FAILED_SDCARD_FULL = "state_failed_sdcard_full";
        public const string STATE_FAILED_CANCELED = "state_failed_cancelled";
        public const string STATE_UNKNOWM = "state_unknown";
    }

    public class ResultGetABTestVer : Result {
        //ABTest方案
        public int plan {
            set; get;
        }
    }

    public class DownloadInfo : Result {

        public const int CODE_CONNECTION_SUCCEED = 0, // 连接成功
              CODE_DOWNLOAD_NONE = 1, // 下载响应正常
              CODE_DOWNLOAD_TIMEOUT = -101, // 下载超时
              CODE_DISK_FULL = -102,// 磁盘满------TODO提前预估磁盘情况
              CODE_FILE_NOT_EXIST = -103, // 服务器没有该文件
              CODE_DOWNLOAD_UNCOMPLETET = -106, // 网络波动导致文件未完整下载
              DOWNLOAD_QUEUE_FULL = -107,// SDK下载队列已满
              CODE_WRITE_FILE_FAILED = -150; // 写文件失败等一些列未知异常

        //请求id
        public int seqId { set; get; }
        //文件下载进度 单位Byte
        public int downloadSize { set; get; }
    }

    public class ResultDownloadText : Result {
        public string content {
            set; get;
        }
    }


    public class HonorSDKImpl : SDKManager {

        public enum ePlat {
            none,
            Android,
            Ios,
        }

        /// <summary>
        /// 资源文件路径
        /// </summary>
        public static string RES_FILE_PATH;

        private static HonorSDKImpl instance;

        protected HonorSDKImpl() {
        }

        public static void CreateInstance(ePlat plant) {
            if (instance == null) {
                if (plant == ePlat.Android)
                    instance = new CustomAndroidSdkImpl();
                else if (plant == ePlat.Ios)
                    instance = new CustomIosSdkImpl();
                else
                    instance = new NoSdkImpl();
            }
        }

        new public static HonorSDKImpl GetInstance() {
            return instance;
        }
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
        //下载文本文件成功
        public const string DOWNLOAD_TEXT_SUCCESS = "download_text_success";
        //下载文本文件失败
        public const string DOWNLOAD_TEXT_FAILED = "download_text_failed";
        //获取耳机状态成功
        public const string GET_HEADSET_STATE_SUCCESS = "get_headset_state_success";
        //获取设备信息
        public const string GET_DEVICE_INFO = "get_device_info";

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
        private OnFinish<DownloadInfo> downloadListener;
        private OnFinish<NetStateInfo> networkStateListener;
        private OnFinish<ResultDownloadText> downloadTextListener;
        private OnFinish<ResultGetHeadsetState> getHeadsetStateListener;
        private OnFinish<string> getDeviceInfoListener;

        protected override void OnReceive(string head, string body) {
            base.OnReceive(head, body);
            switch (head) {
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
                case DOWNLOAD_TEXT_SUCCESS:
                    DownloadTextFinish(true, body);
                    break;
                case DOWNLOAD_TEXT_FAILED:
                    DownloadTextFinish(false, body);
                    break;
                case GET_HEADSET_STATE_SUCCESS:
                    GetHeadsetStateFinish(true, body);
                    break;
                case GET_DEVICE_INFO:
                    getDeviceInfoListener(body);
                    break;
                default: break;
            }
        }


        public const string CONFIG_UNITY_ID = "unity_id";



        /// <summary>
        /// 
        /// </summary>
        /// <returns>返回外部存储信息</returns>
        public virtual DiskInfo GetDiskInfo() {

            return null;
        }
        /// <summary>
        /// 获取认证信息，用户登陆成功后调用
        /// </summary>
        /// <returns></returns>
        public virtual string GetAuthInfo() {
            return "";
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">文档url</param>
        /// <param name="retry">失败重试次数</param>
        /// <param name="timeout">下载超时时间 单位ms</param>
        /// <param name="downloadTextListener"></param>
        public virtual void DownloadText(string url,int retry,int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            this.downloadTextListener = downloadTextListener;
        }

        /// <summary>
        /// 返回资源文件存储路径，应用卸载后该目录自动被删除
        /// </summary>
        public virtual string GetResFilePath() {
            return "";
        }

  
     
        /// <summary>
        /// 注册网络改变监听
        /// </summary>
        /// <param name="networkStateListener"></param>
        public virtual void RegisterNetworkState(OnFinish<NetStateInfo> networkStateListener) {
            this.networkStateListener = networkStateListener;
        }
    

        /// <summary>
        /// 游戏资源路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetGameResUrl() {
            return "";
        }

        /// <summary>
        /// 获取耳机插入状态
        /// </summary>
        /// <param name="notifyWhenHeadsetChanged">当耳机状态发送改变时回调通知</param>
        /// <param name="getHeadsetStateListener">返回耳机插入状态<see cref="ResultGetHeadsetState"/></param>
        public virtual void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<ResultGetHeadsetState> getHeadsetStateListener) {
            this.getHeadsetStateListener = getHeadsetStateListener;
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="srcContent">待翻译文本内容</param>
        /// <param name="targetLan">目标语言码</param>
        /// <param name="translateContentListener">成功返回翻译后文本，失败返回原文本</param>
        public virtual void TranslateContent(string srcContent, string targetLan, OnFinish<ResultTranslate> translateContentListener) {
            this.translateContentListener = translateContentListener;
        }

        /// <summary>
        /// 获取cpu gpu信息
        /// </summary>
        /// <param name="getHardwareInfoListener">返回cpu gpu信息<see cref="HardwareInfo"/></param>
        public virtual void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareInfoListener) {
            this.getHardwareListener = getHardwareInfoListener;

        }
        /// <summary>
        /// 远程推送,如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="ip">远程推送服务器ip</param>
        /// <param name="port">远程推送服务器端口</param>
        /// <param name="gameRoleId">角色id</param>
        public virtual void UdpPush(string ip, string port, string gameRoleId) {

        }

        /// <summary>
        /// 开始录音，如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="serverURL">录音完毕后语音存储服务器url</param>
        /// <param name="bit">语音录音品质，低品质1，中品质2，高品质3</param>
        /// <param name="recordMaxTime">录音最大时长，超过该时长自动结束录音,单位毫秒</param>
        public virtual void StartRecordVideo(string serverURL, string bit, long recordMaxTime) {

        }
        /// <summary>
        /// 结束录音
        /// </summary>
        /// <param name="stopRecordVideoListener">返回录音结果<see cref="ResultVideoRecord"/></param>
        public virtual void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener) {
            this.stopRecordVideoListener = stopRecordVideoListener;
        }
        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="videoUrl">录音存放url</param>
        /// <param name="playVideoListener">返回播放录音结果<see cref="Result"/></param>
        public virtual void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener) {
            this.playVideoListener = playVideoListener;
        }
        /// <summary>
        /// 获取热更信息
        /// </summary>
        /// <param name="rootDir">热更目录</param>
        /// <param name="getDynamicUpdateListener">返回热更信息<see cref="ResultGetDynamic"/></param>
        public virtual void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener) {
            this.getDynamicUpdateListener = getDynamicUpdateListener;
        }
        /// <summary>
        /// 下载热更
        /// </summary>
        /// <param name="downDynamicUpdateListener">返回下载热更结果<see cref="ResultDownload"/></param>
        public virtual void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener) {
            this.downDynamicUpdateListener = downDynamicUpdateListener;
        }

        /// <summary>
        /// 删除所有热更资源
        /// </summary>
        public virtual void RepairUpdateRes() {

        }
        /// <summary>
        /// 获取强更信息
        /// </summary>
        /// <param name="getForceUpdateListener">返回强更信息<see cref="ResultGetForce"/></param>
        public virtual void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener) {
            this.getForceUpdateListener = getForceUpdateListener;
        }

        /// <summary>
        /// 强更下载
        /// </summary>
        /// <param name="downForceUpdateListener">返回强更下载结果<see cref="ResultDownload"/></param>
        public virtual void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener) {
            this.downForceUpdateListener = downForceUpdateListener;
        }
        /// <summary>
        /// 是否需要obb下载，Android包上架google商店且包体大于100M才需要使用到obb下载功能
        /// </summary>
        /// <returns>true必须要下载，false需要下载</returns>
        public virtual bool HasObbUpdate() {
            return false;
        }
        /// <summary>
        /// 下载obb
        /// </summary>
        /// <param name="downObbUpdateListener">返回obb下载结果<see cref="ResultObbDownload"/></param>
        public virtual void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener) {
            this.downObbUpdateListener = downObbUpdateListener;
        }
        /// <summary>
        /// obb下载中断时调用此方法继续下载
        /// </summary>
        public virtual void ContinueUpdateObb() {

        }
        /// <summary>
        /// obb下载完成后重新加载obb
        /// </summary>
        public virtual void ReloadObb() {

        }
        /// <summary>
        /// 获取ABTest
        /// </summary>
        /// <param name="getABTestVerListener">返回获取ABTest<see cref="ResultGetABTestVer"/></param>
        public virtual void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener) {
            this.getABTestVerListener = getABTestVerListener;
        }
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="getDeviceInfoListener"></param>
        public virtual void GetDeviceInfo(OnFinish<string> getDeviceInfoListener)
        {
            this.getDeviceInfoListener = getDeviceInfoListener;
        }

        const string HEAD_NAME_IS_SUPPORT_ALERT_AGREEMENT = "is_support_alert_agreement";
        const string FUNCTION_NAME_IS_SUPPORT_ALERT_AGREEMENT = HEAD_NAME_IS_SUPPORT_ALERT_AGREEMENT;
        /// <summary>
        /// 判断是否需要弹出协议对话框
        /// </summary>
        /// <param name="isSupportAlertAgreementListener">回调是否需要弹出协议对话框</param>
        public virtual void IsSupportAlertAgreement(OnFinish<bool> isSupportAlertAgreementListener)
        {
            ExpandFunction(FUNCTION_NAME_IS_SUPPORT_ALERT_AGREEMENT, "", HEAD_NAME_IS_SUPPORT_ALERT_AGREEMENT, delegate (ResultExpand result)
            {
                JSONNode node =  JSONNode.Parse(result.originResult);
                isSupportAlertAgreementListener(node["isSupportAlertAgreement"].AsBool);
            });

        }

        const string HEAD_NAME_ALERT_AGREEMENT = "alert_agreement";
        const string FUNCTION_NAME_ALERT_AGREEMENT = HEAD_NAME_ALERT_AGREEMENT;
        /// <summary>
        /// 弹出协议框
        /// </summary>
        /// <param name="isSupportAlertAgreementListener">回调玩家是否接受协议</param>
        public virtual void AlertAgreement(OnFinish<bool> alertAgreementListener)
        {
            ExpandFunction(FUNCTION_NAME_ALERT_AGREEMENT, "", HEAD_NAME_ALERT_AGREEMENT, delegate (ResultExpand result)
            {
                JSONNode node = JSONNode.Parse(result.originResult);
                alertAgreementListener(node["isAcceptAgreement"].AsBool);
            });
        }    
        const string HEAD_REGISTER_IDENTIFY = "register_identify";
        const string FUNCTION_NAME_REGISTER_IDENTIFY = HEAD_REGISTER_IDENTIFY;
        /// <summary>
        ///     监听实名认证结果，登录成功后回调该接口实名认证状态，当玩家实名认证状态发生改变时回调该接口
        /// </summary>
        /// <param name="identifyListener"></param>
        public virtual void RegisterIdentifyListener(OnFinish<IdentifyInfo> identifyListener) {
            ExpandFunction(FUNCTION_NAME_REGISTER_IDENTIFY, "", HEAD_REGISTER_IDENTIFY, delegate (ResultExpand result)
            {
                JSONNode node = JSONNode.Parse(result.originResult);
                IdentifyInfo info = new IdentifyInfo();
                info.identify = node["identify"].AsInt;
                identifyListener(info);
            });
        }
        const string FUNCTION_ALERT_IDENTIFY = "alert_identify";
        /// <summary>
        /// 弹出实名认证弹窗，回调RegisterIdentifyListener接口
        /// </summary>
        public virtual void AlertIdentify() {
            ExpandFunction(FUNCTION_ALERT_IDENTIFY);
        }


        private void DownloadTextFinish(bool success, string body) {
            ResultDownloadText result = new ResultDownloadText();
            result.success = success;
            if (success) {
                result.content = body;
            }
            else {
                result.message = body;
            }
            downloadTextListener(result);
        }

        private void GetABTestVerFinish(bool success, string body) {
            ResultGetABTestVer result = new ResultGetABTestVer();
            result.success = success;
            int plan;
            bool parseResult = int.TryParse(body, out plan);
            if (parseResult) {
                result.plan = plan;
            }
            getABTestVerListener(result);
        }

        private void DownObbUpdateFinish(bool stateChanged, string body) {
            ResultObbDownload result = new ResultObbDownload();
            result.stateChanged = stateChanged;
            if (stateChanged) {
                result.state = body;
            }
            else {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            downObbUpdateListener(result);
        }

        private void DownForceUpdateFinish(bool success, string body) {
            ResultDownload result = new ResultDownload();
            result.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            else {
                result.message = body;
            }
            downForceUpdateListener(result);
        }

        private void GetForceUpdateFinish(bool success, string body) {
            ResultGetForce result = new ResultGetForce();
            result.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
            }
            else {
                result.message = body;
            }
            getForceUpdateListener(result);
        }

        private void DownDynamicUpdateFinish(bool success, string body) {
            ResultDownload result = new ResultDownload();
            result.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.currentSize = node["currentSize"].AsLong;
            }
            else {
                result.message = body;
            }
            downDynamicUpdateListener(result);
        }
        private void GetDynamicUpdateFinish(bool success, string body) {
            ResultGetDynamic result = new ResultGetDynamic();
            result.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                result.totalSize = node["totalSize"].AsLong;
                result.dynamicResPath = node["dynamicResPath"].Value;
            }
            else {
                result.message = body;
            }
            getDynamicUpdateListener(result);
        }
        private void StopRecordVideoFinish(bool success, string body) {
            ResultVideoRecord result = new ResultVideoRecord();
            result.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                result.time = node["time"].AsInt;
                result.url = node["url"].Value;
            }
            else {
                result.message = body;
            }
            stopRecordVideoListener(result);
        }
        private void PlayVideoFinish(bool success, string body) {
            Result result = new Result();
            result.success = success;
            playVideoListener(result);
        }
        private void GetHardwareFinish(bool success, string body) {
            HardwareInfo info = new HardwareInfo();
            info.success = success;
            if (success) {
                JSONNode node = JSONNode.Parse(body);
                //cpu 是json 字符串数组
                info.cpu = node["cpu"].Value;
                info.gpu = node["gpu"].Value;
                info.cpuFreq = node["cpuFreq"].AsInt;
                info.cpuNum = node["cpuNum"].AsInt;
                info.totalMemory = node["totalMemory"].AsInt;
                info.freeMemory = node["freeMemory"].AsInt;
                info.deviceModel = node["deviceModel"].Value;
                info.temperature = node["temperature"].AsDouble;
            }
            else {
                info.message = body;
            }
            getHardwareListener(info);
        }
        private void TranslateContentFinish(bool success, string body) {
            ResultTranslate result = new ResultTranslate();
            result.success = success;
            result.translateReult = body;
            translateContentListener(result);
        }

        private void GetHeadsetStateFinish(bool success, string body) {
            ResultGetHeadsetState result = new ResultGetHeadsetState();
            result.success = success;
            int headsetState;
            bool parseResult = int.TryParse(body, out headsetState);
            result.headsetState = headsetState;
            getHeadsetStateListener(result);
        }
    }
}
