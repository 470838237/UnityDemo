using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using honorsdk.SimpleJSON;
using UnityEngine;
using honorsdk;

namespace honorsdk
{

    public class Result
    {

        public int code { set; get; }
        public bool success { set; get; }
        public string message { set; get; }
    }

    public class UserInfo : Result
    {
        public string uid { set; get; }
        public string accessToken { set; get; }
        public string nickName { set; get; }

    }

    public class ResultBind : Result
    {
        public string nickName { set; get; }
    }


    public class AppInfo : Result
    {
        public string appId { set; get; }
        public string deviceId { set; get; }
        public string version { set; get; }
        public string platform { set; get; }
        public string appName { set; get; }
    }

    public class NotchScreenInfo : Result
    {
        public int width { set; get; }
        public int height { set; get; }
    }

    public class MemoryInfo : Result
    {
        public int availMem { set; get; }
        public int totalMem { set; get; }
    }


    public class BatteryInfo : Result
    {
        public int level { set; get; }
        public int scale { set; get; }
    }
    public class CpuGpuInfo : Result
    {

        public string gpu { set; get; }
        public string cpu { set; get; }
        public int cpuNum { set; get; }
        public int cpuFreq { set; get; }

    }

    public class ResultVideoRecord : Result
    {
        public string url { set; get; }
        public long time { set; get; }
    }

    public class GameRoleInfo
    {
        public const int TYPE_CREATE_ROLE = 1;
        public const int TYPE_ENTER_GAME = 2;
        public const int TYPE_ROLE_LEVEL = 3;
        public string roleId { set; get; }
        public string roleName { set; get; }
        public string serverId { set; get; }
        public int roleLevel { set; get; }
        public string roleVip { set; get; }
        public string extra { set; get; }
        public int type { set; get; }
        public int lastUpdate { set; get; }

    }
    public class OrderInfo
    {
        public string serverId { set; get; }
        public string roleId { set; get; }
        public string roleName { set; get; }
        public int roleLevel { set; get; }
        public string goodsId { set; get; }
        public int count { set; get; }
        public string extra { set; get; }
    }

    public class ResultPay : Result
    {
        public string orderId { set; get; }
    }
    public class NoticeInfo
    {
        public int type { set; get; }
        public int mode { set; get; }
        public string title { set; get; }
        public string content { set; get; }
        public string image { set; get; }
        public string link { set; get; }
        public int sort { set; get; }
        public int important { set; get; }
        public int status { set; get; }
        public long startTime { set; get; }
        public long endTime { set; get; }
    }

    public class NoticeList : Result
    {
        public List<NoticeInfo> notices = new List<NoticeInfo>();
    }
    public class ServerInfo
    {
        public string serverId { set; get; }
        public string serverName { set; get; }
        public int status { set; get; }
        public string label { set; get; }
        public string address { set; get; }
        public string tag { set; get; }
        public long openTime { set; get; }
        public long closeTime { set; get; }
        public List<GameRoleInfo> roles = new List<GameRoleInfo>();
    }


    public class ServerList : Result
    {
        public long time { set; get; }
        public int tester { set; get; }
        public List<ServerInfo> servers = new List<ServerInfo>();
    }


    public class GoodsInfo
    {
        public string goodsId { set; get; }
        public string goodsName { set; get; }
        public string description { set; get; }
        public string url { set; get; }
        public double price { set; get; }
        public string priceDisplay { set; get; }
        public string currency { set; get; }
        public int count { set; get; }
        public string ratio { set; get; }
        public string gift { set; get; }
        public string category { set; get; }
        public string tag { set; get; }
        public int limitByDay { set; get; }
        public long startTime { set; get; }
        public long endTime { set; get; }
    }

    public enum Api
    {
        SWITCH_ACCOUNT=0,
        LOGOUT,
        BIND,
        UNBIND,
        EXIT
    }

public class GoodsList : Result
    {
        public List<GoodsInfo> goods = new List<GoodsInfo>();
    }

    public class ResultGetDynamic : Result
    {
        public long totalSize { set; get; }
        public string dynamicResPath { set; get; }
    }
    public class ResultDownload : Result
    {
        public long totalSize { set; get; }
        public long currentSize { set; get; }
    }
    public class ResultGetForce : Result
    {
        public long totalSize { set; get; }
    }
    public class ResultObbDownload {
        public long totalSize { set; get; }
        public long currentSize { set; get; }
        public string state { set; get; }
        public bool stateChanged { set; get; }
        public const string STATE_COMPLETED = "state_completed";
        public const string STATE_IDLE = "state_idle";
        public const string STATE_FETCHING_URL = "state_fetching_url";
        public const string STATE_CONNECTING = "state_connecting";
        public const string STATE_DOWNLOADING = "state_downloading";
        public const string STATE_PAUSED_NETWORK_UNAVAILABLE = "state_paused_network_unavailable";
        public const string STATE_PAUSED_BY_REQUEST = "state_idle";
        public const string STATE_PAUSED_WIFI_DISABLED_NEED_CELLULAR_PERMISSION = "state_paused_wifi_disabled";
        public const string STATE_PAUSED_NEED_CELLULAR_PERMISSION = "state_paused_wifi_unavailable";
        public const string STATE_PAUSED_WIFI_DISABLED = "state_paused_wifi_disabled";
        public const string STATE_PAUSED_NEED_WIFI = "state_paused_wifi_unavailable";
        public const string STATE_PAUSED_ROAMING = "state_paused_roaming";
        public const string STATE_PAUSED_NETWORK_SETUP_FAILURE = "state_paused_network_setup_failure";
        public const string STATE_PAUSED_SDCARD_UNAVAILABLE = "state_paused_sdcard_unavailable";
        public const string STATE_FAILED_UNLICENSED = "state_failed_unlicensed";
        public const string STATE_FAILED_FETCHING_URL = "state_failed_sdcard_full";
        public const string STATE_FAILED_SDCARD_FULL = "state_failed_fetching_url";
        public const string STATE_FAILED_CANCELED = "state_failed_cancelled";
        public const string STATE_UNKNOWM = "state_unknown";
    }


    public class ResultExpand : Result
    {
        public string originResult { set; get; }
    }

    public class ResultGetHeadsetState : Result
    {
        public const int HEADSET_OPEN = 1;
        public const int HEADSET_CLOSE = 0;
        public int headsetState { set; get; }
    }
    public delegate void OnFinish<T>(T result);
    public delegate void OnReceiveMsg(string head, string body);

    class HonorSDK
    {

        private static HonorSDK instance;

        protected HonorSDK()
        {
        }

        public static HonorSDK GetInstance()
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (instance == null)
                instance = new IosSDKImpl();

#elif UNITY_ANDROID && !UNITY_EDITOR
            if (instance == null)
                instance = new AndroidSDKImpl();
#endif
            return instance;
        }

        const string SET_GAME_OBJECT_NAME_SUCCESS = "set_game_object_name_success";
        const string INIT_SUCCESS = "init_success";
        const string INIT_FAILED = "init_failed";
        const string LOGIN_SUCCESS = "login_success";
        const string LOGIN_FAILED = "login_failed";
        const string GET_APP_INFO = "get_app_info";
        const string GET_NOTCH_SCREEN_INFO = "get_notch_screen_info_success";
        const string GET_COUNTRY_CODE = "get_country_code";
        const string GET_MEMORY = "get_memory";
        const string CURRENT_BATTERY = "current_battery";
        const string TRANSLATE_SUCCESS = "translate_success";
        const string TRANSLATE_FAILED = "translate_failed";
        const string GET_CPU_GPU_INFO = "get_cpu_gpu_info";
        const string RECORD_VIDEO_SUCCESS = "record_video_success";
        const string RECORD_VIDEO_FAILED = "record_video_failed";
        const string START_PLAY_VIDEO = "play_video";
        const string SWITCH_ACCOUNT_SUCCESS = "switch_account_success";
        const string SWITCH_ACCOUNT_FAILED = "switch_account_failed";
        const string LOGOUT_SUCCESS = "logout_success";
        const string LOGOUT_FAILED = "logout_failed";
        const string PAY_SUCCESS = "pay_success";
        const string PAY_FAILED = "pay_failed";
        const string EXIT_SUCCESS = "exit_success";
        const string EXIT_FAILED = "exit_failed";
        const string GET_NOTICE_SUCCESS = "get_notice_success";
        const string GET_NOTICE_FAILED = "get_notice_failed";
        const string GET_SERVER_LIST_SUCCESS = "get_server_list_success";
        const string GET_SERVER_LIST_FAILED = "get_server_list_failed";
        const string GET_GOODS_LIST_SUCCESS = "get_goods_list_success";
        const string GET_GOODS_LIST_FAILED = "get_goods_list_failed";
        const string GET_DYNAMIC_UPDATE_SUCCESS = "get_dynamic_update_success";
        const string GET_DYNAMIC_UPDATE_FAILED = "get_dynamic_update_failed";
        const string DOWN_DYNAMIC_UPDATE_SUCCESS = "down_dynamic_update_success";
        const string DOWN_DYNAMIC_UPDATE_FAILED = "down_dynamic_update_failed";
        const string GET_FORCE_UPDATE_SUCCESS = "get_force_update_success";
        const string GET_FORCE_UPDATE_FAILED = "get_force_update_failed";
        const string DOWN_FORCE_UPDATE_SUCCESS = "down_force_update_success";
        const string DOWN_FORCE_UPDATE_FAILED = "down_force_update_failed";
        const string DOWN_OBB_UPDATE_SUCCESS = "down_obb_update_success";
        const string DOWN_OBB_STATE_CHANGED = "down_obb_state_changed";
        const string BIND_SUCCESS = "bind_success";
        const string BIND_FAILED = "bind_failed";
        const string GET_HEADSET_STATE_SUCCESS = "get_headset_state_success";

        private OnFinish<Result> initListener;
        private OnFinish<UserInfo> loginListener;
        private OnFinish<AppInfo> appInfoListener;
        private OnFinish<NotchScreenInfo> getNotchInfoListener;
        private OnFinish<string> getCountryCodeListener;
        private OnFinish<MemoryInfo> getMemroyInfoListener;
        private OnFinish<BatteryInfo> getBatteryInfoListener;
        private OnFinish<string> translateContentListener;
        private OnFinish<CpuGpuInfo> getCpuAndGpuListener;
        private OnFinish<ResultVideoRecord> stopRecordVideoListener;
        private OnFinish<Result> playVideoListener;
        private OnFinish<UserInfo> switchAccountListener;
        private OnFinish<Result> logoutListener;
        private OnFinish<ResultPay> payListener;
        private OnFinish<Result> exitListener;
        private OnFinish<NoticeList> getNoticeListListener;
        private OnFinish<ServerList> getServerListListener;
        private OnFinish<GoodsList> getGoodsListListener;
        private OnFinish<ResultGetDynamic> getDynamicUpdateListener;
        private OnFinish<ResultDownload> downDynamicUpdateListener;
        private OnFinish<ResultGetForce> getForceUpdateListener;
        private OnFinish<ResultDownload> downForceUpdateListener;
        private OnFinish<ResultObbDownload> downObbUpdateListener;
        private OnFinish<ResultBind> startBindListener;
        private OnFinish<ResultGetHeadsetState> getHeadsetStateListener;
        private Dictionary<string, OnFinish<ResultExpand>> expandListeners = new Dictionary<string, OnFinish<ResultExpand>>();



        protected void OnReceive(string head, string body)
        {
            Debug.Log("head=" + head + ";body=" + body);       
            switch (head)
            {
                case SET_GAME_OBJECT_NAME_SUCCESS:
                    Init();
                    break;
                case INIT_SUCCESS:
                    InitFinish(true, body);
                    break;
                case INIT_FAILED:
                    InitFinish(false, body);
                    break;
                case GET_APP_INFO:
                    bool success = (body != null) && body.Length != 0;
                    GetAppInfoFinish(success, body);
                    break;
                case GET_NOTCH_SCREEN_INFO:
                    success = (body != null) && body.Length != 0;
                    GetNotchInfoFinish(success, body);
                    break;
                case GET_COUNTRY_CODE:
                    getCountryCodeListener(body);
                    break;
                case GET_MEMORY:
                    success = (body != null) && body.Length != 0;
                    GetMemoryFinish(success, body);
                    break;
                case CURRENT_BATTERY:
                    success = (body != null) && body.Length != 0;
                    GetBatteryFinish(success, body);
                    break;
                case TRANSLATE_FAILED:
                    TranslateContentFinish(false, body);
                    break;
                case TRANSLATE_SUCCESS:
                    TranslateContentFinish(true, body);
                    break;
                case GET_CPU_GPU_INFO:
                    GetCpuAndGpuFinish(true, body);
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
                case LOGIN_SUCCESS:
                    LoginFinish(true, body);
                    break;
                case LOGIN_FAILED:
                    LoginFinish(false, body);
                    break;
                case SWITCH_ACCOUNT_SUCCESS:
                    SwitchAccountFinish(true, body);
                    break;
                case SWITCH_ACCOUNT_FAILED:
                    SwitchAccountFinish(false, body);
                    break;
                case BIND_SUCCESS:
                    StartBindFinish(true,body);
                    break;
                case BIND_FAILED:
                    StartBindFinish(false, body);
                    break;
                case LOGOUT_SUCCESS:
                    LogoutFinish(true, body);
                    break;
                case LOGOUT_FAILED:
                    LogoutFinish(false, body);
                    break;
                case PAY_SUCCESS:
                    PayFinish(true, body);
                    break;
                case PAY_FAILED:
                    PayFinish(false, body);
                    break;
                case EXIT_SUCCESS:
                    ExitFinish(true, body);
                    break;
                case EXIT_FAILED:
                    ExitFinish(false, body);
                    break;
                case GET_NOTICE_SUCCESS:
                    GetNoticeListFinish(true, body);
                    break;
                case GET_NOTICE_FAILED:
                    GetNoticeListFinish(false, body);
                    break;
                case GET_SERVER_LIST_SUCCESS:
                    GetServerListFinish(true, body);
                    break;
                case GET_SERVER_LIST_FAILED:
                    GetServerListFinish(false, body);
                    break;
                case GET_GOODS_LIST_SUCCESS:
                    GetGoodsListFinish(true, body);
                    break;
                case GET_GOODS_LIST_FAILED:
                    GetGoodsListFinish(false, body);
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
                case GET_HEADSET_STATE_SUCCESS:
                    GetHeadsetStateFinish(true,body);
                    break;
                default:
                    if (expandListeners.ContainsKey(head))
                    {
                        ExpandFunctionFinish(head,body);
                    }
                  break;
            }
        }

        public void Init(HonorSDKGameObject gameObject, OnFinish<Result> initListener)
        {
            this.initListener = initListener;
            gameObject.SetOnReceiveListener(new OnReceiveMsg(this.OnReceive));
            SetGameObjectName(gameObject.gameObject.name);
        }
        protected virtual void SetGameObjectName(string gameObjectName)
        {

        }
        protected virtual void Init()
        {

        }

        public virtual void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            this.appInfoListener = appInfoListener;
        }

        public virtual void GameStepInfo(string step,string type)
        {

        }

        public virtual void GetNotchScreenInfo(OnFinish<NotchScreenInfo> getNotchInfoListener)
        {
            this.getNotchInfoListener = getNotchInfoListener;

        }

        public virtual void GetCountryCode(OnFinish<string> getCountryCodeListener)
        {
            this.getCountryCodeListener = getCountryCodeListener;
        }
   
        public virtual void ReportError(string errorMsg,string type = "")
        {

        }
        public virtual void SetClipboard(string content)
        {

        }

        public virtual void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
            this.getMemroyInfoListener = getMemroyInfoListener;
        }


        public virtual void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
            this.getBatteryInfoListener = getBatteryInfoListener;
        }

        public virtual void TranslateContent(string srcContent, string targetLan, OnFinish<string> translateContentListener)
        {
            this.translateContentListener = translateContentListener;
        }

        public virtual void GetCpuAndGpu(OnFinish<CpuGpuInfo> getCpuAndGpuListener)
        {
            this.getCpuAndGpuListener = getCpuAndGpuListener;

        }

        public virtual void PushNotification(string content, int delay, int id)
        {

        }

        public virtual void CleanNotification(int id)
        {

        }
        public virtual void CleanAllNotification()
        {

        }
        public virtual void UdpPush(string ip, string port, string gameRoleId)
        {

        }
        public virtual void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {

        }
        public virtual void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            this.stopRecordVideoListener = stopRecordVideoListener;
        }
        public virtual void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            this.playVideoListener = playVideoListener;
        }

        public virtual void Login(OnFinish<UserInfo> loginListener)
        {
            this.loginListener = loginListener;
        }
        public virtual void SwitchAccount(OnFinish<UserInfo> switchAccountListener)
        {
            this.switchAccountListener = switchAccountListener;
        }

        public virtual void StartBind(OnFinish<ResultBind> startBindListener)
        {
            this.startBindListener = startBindListener;
        }


        public virtual void Logout(OnFinish<Result> logoutListener)
        {
            this.logoutListener = logoutListener;
        }
        public virtual bool HasExitDialog()
        {
            return false;
        }

        public virtual string UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            JSONClass json = new JSONClass();
            json.Add("roleId", gameRoleInfo.roleId);
            json.Add("roleName", gameRoleInfo.roleName);
            json.Add("roleLevel", gameRoleInfo.roleLevel+"");
            json.Add("roleVip", gameRoleInfo.roleVip);
            json.Add("serverId", gameRoleInfo.serverId);
            json.Add("extra", gameRoleInfo.extra);
            return json.ToString();
        }
        public virtual string Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
            this.payListener = payListener;
            JSONClass json = new JSONClass();
            json.Add("roleId", orderInfo.roleId);
            json.Add("roleName", orderInfo.roleName);
            json.Add("roleLevel", orderInfo.roleLevel+"");
            json.Add("count", orderInfo.count + "");
            json.Add("goodsId", orderInfo.goodsId);
            json.Add("extra", orderInfo.extra);
            return json.ToString();
        }

        public virtual void Exit(OnFinish<Result> exitListener)
        {
            this.exitListener = exitListener;
        }
        public virtual void GetNoticeList(string serverId, string language, String type, OnFinish<NoticeList> getNoticeListListener)
        {
            this.getNoticeListListener = getNoticeListListener;
        }

        public virtual void GetServerList(OnFinish<ServerList> getServerListListener)
        {
            this.getServerListListener = getServerListListener;
        }

        public virtual void GetGoodsList(string serverId, string category, string currency, OnFinish<GoodsList> getGoodsListListener)
        {
            this.getGoodsListListener = getGoodsListListener;
        }

        public virtual void GetDynamicUpdate(string type, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {           
            this.getDynamicUpdateListener = getDynamicUpdateListener;
        }
        public virtual void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            this.downDynamicUpdateListener = downDynamicUpdateListener;
        }

        public virtual void RepairUpdateRes()
        {

        }

        public virtual void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            this.getForceUpdateListener = getForceUpdateListener;
        }

        public virtual void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            this.downForceUpdateListener =  downForceUpdateListener;
        }

        public virtual bool HasObbUpdate()
        {
            return false;
        }

        public virtual void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            this.downObbUpdateListener = downObbUpdateListener;
        }

        public virtual void ContinueUpdateObb()
        {

        }
        public virtual void ReloadObb()
        {

        }

        public virtual bool IsSupportApi(Api api)
        {
            return true;
        }


        public virtual void ExpandFunction(string functionName, string jsonParameter="",string headName = "",OnFinish<ResultExpand> expandFunctionListener = null)
        {
            if (headName != null && headName != ""&& !expandListeners.ContainsKey(headName))
            {
                expandListeners.Add(headName,expandFunctionListener);
            }

        }

        public virtual void GetHeadsetState(bool notifyWhenHeadsetChanged,OnFinish<ResultGetHeadsetState> getHeadsetStateListener)
        {
            this.getHeadsetStateListener = getHeadsetStateListener;
        }

        public virtual void SendGuideFinish()
        {
        }

        /*---------------------------------------------------------------------------------------------------------*/


        private void GetHeadsetStateFinish(bool success,string body)
        {
            ResultGetHeadsetState result = new ResultGetHeadsetState();
            result.success = success;
            int headsetState;
            bool parseResult =  int.TryParse(body, out headsetState);
            result.headsetState = headsetState;
            getHeadsetStateListener(result);
        }

        private void ExpandFunctionFinish(string head,string body)
        {
            OnFinish<ResultExpand> listener = expandListeners[head];
            if (listener == null) return;
            JSONNode node = JSONNode.Parse(body);
            bool success = node["success"].AsBool;
            ResultExpand result = new ResultExpand();
            result.success = success;
            if (success)
            {
                string originResult = node["originResult"].Value;
                result.originResult = originResult;
            }
            else
            {
                string errorMessage = node["errorMessage"].Value;
                result.message = errorMessage;
            }
            listener(result);
        }

        private void StartBindFinish(bool success, string body)
        {
            ResultBind result = new ResultBind();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.nickName = node["nickName"].Value;
            }
            else
            {
                result.message = body;
            }
            startBindListener(result);
        }

        private void DownObbUpdateFinish(bool stateChanged, string body)
        {
            ResultObbDownload result = new ResultObbDownload();
            result.stateChanged = stateChanged;
            if (stateChanged)
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


        private void GetGoodsListFinish(bool success, string body)
        {
            GoodsList result = new GoodsList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                JSONArray jsonArrayGoods = node["list"].AsArray;
                List<GoodsInfo> goods = result.goods;
                foreach (JSONNode item in jsonArrayGoods.Childs)
                {
                    GoodsInfo goodsInfo = new GoodsInfo();
                    goodsInfo.category = item["category"].Value;
                    goodsInfo.count = item["amount"].AsInt;
                    goodsInfo.currency = item["currency"].Value;
                    goodsInfo.description = item["description"].Value;
                    goodsInfo.endTime = item["end_time"].AsLong;
                    goodsInfo.gift = item["gift"].Value;
                    goodsInfo.goodsId = item["pid"].Value;
                    goodsInfo.goodsName = item["name"].Value;
                    goodsInfo.limitByDay = item["limit_by_day"].AsInt;
                    goodsInfo.price = item["price"].AsDouble;
                    goodsInfo.priceDisplay = item["price_display"].Value;
                    goodsInfo.ratio = item["ratio"].Value;
                    goodsInfo.startTime = item["start_time"].AsLong;
                    goodsInfo.url = item["url"].Value;
                    goodsInfo.tag = item["tag"].Value;
                    goods.Add(goodsInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getGoodsListListener(result);
        }

        private void GetServerListFinish(bool success, string body)
        {
            ServerList result = new ServerList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.tester = node["tester"].AsInt;
                result.time = node["time"].AsLong;
                JSONArray jsonArrayServers = node["list"].AsArray;
                List<ServerInfo> servers = result.servers;
                foreach (JSONNode item in jsonArrayServers.Childs)
                {
                    ServerInfo serverInfo = new ServerInfo();
                    serverInfo.closeTime = item["close_time"].AsLong;
                    serverInfo.label = item["label"].Value;
                    serverInfo.openTime = item["open_time"].AsLong;
                    serverInfo.serverId = item["sid"].Value;
                    serverInfo.serverName = item["name"].Value;
                    serverInfo.status = item["status"].AsInt;
                    serverInfo.tag = item["tag"].Value;
                    List<GameRoleInfo> roles = serverInfo.roles;
                    JSONArray jsonArrayRoles = node["role"].AsArray;
                    foreach (JSONNode itemRole in jsonArrayRoles.Childs)
                    {
                        GameRoleInfo roleInfo = new GameRoleInfo();
                        roleInfo.extra = itemRole["profile"].Value;
                        roleInfo.lastUpdate = itemRole["last_update"].AsInt;
                        roleInfo.roleId = itemRole["role"].Value;
                        roleInfo.roleLevel = itemRole["level"].AsInt;
                        roleInfo.roleName = itemRole["name"].Value;
                        roleInfo.roleVip = itemRole["vip"].Value;
                        roleInfo.serverId = itemRole["server"].Value;
                        roles.Add(roleInfo);
                    }
                    servers.Add(serverInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getServerListListener(result);

        }

        private void GetNoticeListFinish(bool success, string body)
        {
            NoticeList result = new NoticeList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                JSONArray jsonArrayNotices = node["notice"].AsArray;
                List<NoticeInfo> notices = result.notices;
                foreach (JSONNode item in jsonArrayNotices.Childs)
                {      
                    NoticeInfo noticeInfo = new NoticeInfo();
                    noticeInfo.content = item["content"].Value;
                    noticeInfo.endTime = item["end_time"].AsLong;
                    noticeInfo.image = item["image"].Value;
                    noticeInfo.important = item["important"].AsInt;
                    noticeInfo.link = item["link"].Value;
                    noticeInfo.mode = item["mode"].AsInt;
                    noticeInfo.sort = item["sort"].AsInt;
                    noticeInfo.startTime = item["start_time"].AsLong;
                    noticeInfo.status = item["status"].AsInt;
                    noticeInfo.title = item["title"].Value;
                    noticeInfo.type = item["type"].AsInt;
                    notices.Add(noticeInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getNoticeListListener(result);
        }

        private void ExitFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            exitListener(result);
        }

        private void PayFinish(bool success, string body)
        {
            ResultPay result = new ResultPay();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.orderId = node["orderId"].Value;
            }
            else
            {
                result.message = body;
            }
            payListener(result);
        }

        private void LogoutFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            logoutListener(result);
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
        private void GetCpuAndGpuFinish(bool success, string body)
        {
            CpuGpuInfo info = new CpuGpuInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                //cpu 是json 字符串数组
                info.cpu = node["cpu"].Value;
                info.gpu = node["gpu"].Value;
                info.cpuFreq = node["cpuFreq"].AsInt;
                info.cpuNum = node["cpuNum"].AsInt;
            }
            else
            {
                info.message = body;
            }
            getCpuAndGpuListener(info);
        }


        private void InitFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            initListener(result);
        }

        private void GetNotchInfoFinish(bool success, string body)
        {
            NotchScreenInfo info = new NotchScreenInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.width = node["width"].AsInt;
                info.height = node["height"].AsInt;
            }
            getNotchInfoListener(info);
        }


        private void GetAppInfoFinish(bool success, string body)
        {
            AppInfo appInfo = new AppInfo();
            appInfo.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                appInfo.appId = node["appId"].Value;
                appInfo.deviceId = node["deviceId"].Value;
                appInfo.appName = node["appName"].Value;
                appInfo.platform = node["platform"].Value;
                appInfo.version = node["version"].Value;
            }
            else
            {
                appInfo.message = body;
            }
            appInfoListener(appInfo);
        }

        private void TranslateContentFinish(bool success, string body)
        {
            translateContentListener(body);
        }

        private void GetBatteryFinish(bool success, string body)
        {
            BatteryInfo info = new BatteryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.scale = node["scale"].AsInt;
                info.level = node["level"].AsInt;
            }
            getBatteryInfoListener(info);
        }

        private void GetMemoryFinish(bool success, string body)
        {
            MemoryInfo info = new MemoryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.totalMem = node["totalMem"].AsInt;
                info.availMem = node["availMem"].AsInt;
            }
            getMemroyInfoListener(info);
        }

        private void LoginFinish(bool success, string body)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.success = success;
            if (!success)
                userInfo.message = body;
            else
            {
                JSONNode node = JSONNode.Parse(body);
                userInfo.uid = node["uid"].Value;
                userInfo.accessToken = node["accessToken"].Value;
                userInfo.nickName = node["nickname"].Value;
            }
            loginListener(userInfo);
        }

        private void SwitchAccountFinish(bool success, string body)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.success = success;
            if (!success)
                userInfo.message = body;
            else
            {
                JSONNode node = JSONNode.Parse(body);
                userInfo.uid = node["uid"].Value;
                userInfo.accessToken = node["accessToken"].Value;
                userInfo.nickName = node["nickname"].Value;
            }
            switchAccountListener(userInfo);
        }
    }
}
