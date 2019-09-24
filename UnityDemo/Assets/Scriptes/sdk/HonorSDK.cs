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
        //错误码
        public int code { set; get; }
        //处理结果
        public bool success { set; get; }
        //处理失败的信息
        public string message { set; get; }
    }
    public class ResultInit : Result
    {
        private Dictionary<string, string> customParams;
        public ResultInit(Dictionary<string, string> customParams)
        {
            this.customParams = customParams;
        }
        public  string getCustomParameter(string key)
        {
            return customParams[key];
        }
    }

    public class UserInfo : Result
    {
        //用户唯一表示
        public string uid { set; get; }
        //用户token，用于验证用户合法性
        public string accessToken { set; get; }
        //用户昵称
        public string nickName { set; get; }

    }

    public class ResultBind : Result
    {
        //用户昵称
        public string nickName { set; get; }
    }


    public class AppInfo : Result
    {
        //IOS平台标识
        const string PLATFORM_IOS = "0";
        //Android平台标识
        const string PLATFORM_ANDROID = "1";
        //包名
        public string appId { set; get; }
        //设备id
        public string deviceId { set; get; }
        //应用版本号
        public string version { set; get; }
        //平台标识
        public string platform { set; get; }
        //应用名称
        public string appName { set; get; }
    }

    public class NotchScreenInfo : Result
    {
        //刘海屏宽度 单位px
        public int width { set; get; }
        //刘海屏高度 单位px
        public int height { set; get; }
    }

    public class MemoryInfo : Result
    {
        //可用内存大小 单位Byte
        public long availMem { set; get; }
        //总内存大小 单位Byte
        public long totalMem { set; get; }
    }


    public class BatteryInfo : Result
    {
        //当前电量(0-100)
        public int level { set; get; }
        //总电量(100)
        public int scale { set; get; }
    }
    public class CpuGpuInfo : Result
    {
        //Gpu型号
        public string gpu { set; get; }
        //cpu型号 JSONArray类型字符串,数组成员为字符串类型  
        //由于各个手机获取cpu型号的接口不一致,该接口返回了常用的接口,开发者根据业务逻辑筛选
        public string cpu { set; get; }
        //Cpu核心数量
        public int cpuNum { set; get; }
        //Cpu最大频率
        public int cpuFreq { set; get; }

    }

    public class ResultVideoRecord : Result
    {
        //语音存放链接
        public string url { set; get; }
        //语音时长 单位秒
        public long time { set; get; }
    }

    public class GameRoleInfo
    {
        //创建角色节点
        public const int TYPE_CREATE_ROLE = 1;
        //进入游戏节点
        public const int TYPE_ENTER_GAME = 2;
        //角色升级节点
        public const int TYPE_ROLE_LEVEL = 3;
        //角色ID
        public string roleId { set; get; }
        //角色名称
        public string roleName { set; get; }
        //服务器id
        public string serverId { set; get; }
        //角色等级
        public int roleLevel { set; get; }
        //角色vip等级
        public string roleVip { set; get; }
        //角色其他信息
        public string extra { set; get; }
        //角色上传的节点类型
        public int type { set; get; }
        //角色最后更新时间
        public int lastUpdate { set; get; }

    }
    public class OrderInfo
    {
        //服务器id
        public string serverId { set; get; }
        //角色id
        public string roleId { set; get; }
        //角色名称
        public string roleName { set; get; }
        //角色等级
        public int roleLevel { set; get; }
        //商品id
        public string goodsId { set; get; }
        //商品数量 不传时默认为1
        public int count { set; get; }
        //透传参数，游戏传入的值，将在查询订单信息时原样返回
        public string extra { set; get; }
    }

    public class ResultPay : Result
    {
        //平台订单号
        public string orderId { set; get; }
    }
    public class NoticeInfo
    {
        //公告类型(0普通1活动2更新3跑马灯4登录5登出)
        public int type { set; get; }
        //模式(0文本1海报)
        public int mode { set; get; }
        //公告标题
        public string title { set; get; }
        //文本公告内容
        public string content { set; get; }
        //海报公告图片地址
        public string image { set; get; }
        //海报公告跳转地址
        public string link { set; get; }
        //公告排序 返回值越小越优先
        public int sort { set; get; }
        //公告重要程度(1重要0一般)
        public int important { set; get; }
        //公告状态(1启用0禁用)
        public int status { set; get; }
        //公告开始时间
        public long startTime { set; get; }
        //公告结束时间
        public long endTime { set; get; }
    }

    public class NoticeList : Result
    {
        //公告列表
        public List<NoticeInfo> notices = new List<NoticeInfo>();
    }
    public class ServerInfo
    {
        //服务器id
        public string serverId { set; get; }
        //服务器名称
        public string serverName { set; get; }
        //服务器状态（1正常/2维护）
        public int status { set; get; }
        //显示标签（new新服/recommend推荐/hot火爆/full爆满），多个标签逗号分隔
        public string label { set; get; }
        //服务器地址（例如：123.123.123.123:1234或https://server1.game.com/play）
        public string address { set; get; }
        //功能标记(有的项目会有自己的特殊功能主机，可以自行标记，例如：login=登录服)
        public string tag { set; get; }
        //自动开服时间（秒级时间戳）
        public long openTime { set; get; }
        //自动关服时间（秒级时间戳）
        public long closeTime { set; get; }
        //角色列表信息
        public List<GameRoleInfo> roles = new List<GameRoleInfo>();
    }


    public class ServerList : Result
    {
        //当前服务器时间（秒级时间戳），用于和客户端同步开关服时间
        public long time { set; get; }
        //是否测试人员，非测试人员没有该字段;如果是测试人员则tester=1
        public int tester { set; get; }
        public List<ServerInfo> servers = new List<ServerInfo>();
    }


    public class GoodsInfo
    {
        //商品id
        public string goodsId { set; get; }
        //商品名称
        public string goodsName { set; get; }
        //商品描述
        public string description { set; get; }
        //商品图标资源地址
        public string url { set; get; }
        //商品价格（实际付款价格）
        public double price { set; get; }
        //商品展示价（游戏可显示为商品原价之类）
        public string priceDisplay { set; get; }
        //币种例:"CNY", "USD", "TWD"
        public string currency { set; get; }
        //游戏币数量
        public int count { set; get; }
        //发放倍率（比如首次购买得双倍游戏币）
        public string ratio { set; get; }
        //赠品 返回json字符串
        public string gift { set; get; }
        // 商品类别（比如商城1：store1 / 商城2：store2 / 商城3：store3）
        public string category { set; get; }
        //功能标记：coin金币类商品 / card卡类商品 / prop道具类商品
        public string tag { set; get; }
        //每日限购次数
        public int limitByDay { set; get; }
        //商品生效时间
        public long startTime { set; get; }
        //商品失效时间
        public long endTime { set; get; }
    }

    public enum Api
    {
        //切换账号
        SWITCH_ACCOUNT = 0,
        //注销
        LOGOUT,
        //绑定
        BIND,
        //解绑
        UNBIND,
        //退出
        EXIT
    }

    public class GoodsList : Result
    {
        //商品列表
        public List<GoodsInfo> goods = new List<GoodsInfo>();
    }

    public class ResultGetDynamic : Result
    {
        //热更总大小 单位Byte
        public long totalSize { set; get; }
        //下载热更文件存储路径
        public string dynamicResPath { set; get; }
    }
    public class ResultDownload : Result
    {
        //下载总大小 单位Byte
        public long totalSize { set; get; }
        //已下载总大小 单位Byte
        public long currentSize { set; get; }
    }
    public class ResultGetForce : Result
    {
        //跳转应用商店返回强更大小
        public const int JUMP_APP_STORE = 1;
        //强更大小 单位Byte
        public long totalSize { set; get; }
    }
    public class ResultObbDownload {
        //需要下载总大小 单位Byte
        public long totalSize { set; get; }
        //已下载大小 单位Byte
        public long currentSize { set; get; }
        //obb下载过程返回的状态信息
        public string state { set; get; }
        //true表示状态发送变化返回状态信息,false表示没有状态变化时返回下载信息
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
        //成功时返回处理结果
        public string originResult { set; get; }
    }

    public class ResultGetHeadsetState : Result
    {
        //插入耳机时状态
        public const int HEADSET_OPEN = 1;
        //取消插入耳机时状态
        public const int HEADSET_CLOSE = 0;
        //耳机状态
        public int headsetState { set; get; }
    }
    public class ResultGetABTestVer : Result
    {
        //ABTest方案
        public int plan { set; get; }
    }
    public class ResultGetMobileAdapter : Result
    {
        //机型适配文档内容
        public string content { set; get; }
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
        //初始化成功
        const string INIT_SUCCESS = "init_success";
        //初始化失败
        const string INIT_FAILED = "init_failed";
        //登录成功
        const string LOGIN_SUCCESS = "login_success";
        //登录失败
        const string LOGIN_FAILED = "login_failed";
        //获取应用信息
        const string GET_APP_INFO = "get_app_info";
        //获取刘海屏信息
        const string GET_NOTCH_SCREEN_INFO = "get_notch_screen_info_success";
        //获取国家码
        const string GET_COUNTRY_CODE = "get_country_code";
        //获取手机内存信息
        const string GET_MEMORY = "get_memory";
        //获取手机电量信息
        const string CURRENT_BATTERY = "current_battery";
        //翻译成功
        const string TRANSLATE_SUCCESS = "translate_success";
        //翻译失败
        const string TRANSLATE_FAILED = "translate_failed";
        //获取cpu gpu信息
        const string GET_CPU_GPU_INFO = "get_cpu_gpu_info";
        //录音成功
        const string RECORD_VIDEO_SUCCESS = "record_video_success";
        //录音失败
        const string RECORD_VIDEO_FAILED = "record_video_failed";
        //播放录音
        const string START_PLAY_VIDEO = "play_video";
        //切换账号成功
        const string SWITCH_ACCOUNT_SUCCESS = "switch_account_success";
        //切换账号失败
        const string SWITCH_ACCOUNT_FAILED = "switch_account_failed";
        //注销成功
        const string LOGOUT_SUCCESS = "logout_success";
        //注销失败
        const string LOGOUT_FAILED = "logout_failed";
        //支付成功
        const string PAY_SUCCESS = "pay_success";
        //支付失败
        const string PAY_FAILED = "pay_failed";
        //退出成功
        const string EXIT_SUCCESS = "exit_success";
        //退出失败
        const string EXIT_FAILED = "exit_failed";
        //获取公告列表成功
        const string GET_NOTICE_SUCCESS = "get_notice_success";
        //获取公告列表失败
        const string GET_NOTICE_FAILED = "get_notice_failed";
        //获取服务器列表成功
        const string GET_SERVER_LIST_SUCCESS = "get_server_list_success";
        //获取服务器列表失败
        const string GET_SERVER_LIST_FAILED = "get_server_list_failed";
        //获取商品列表成功
        const string GET_GOODS_LIST_SUCCESS = "get_goods_list_success";
        //获取商品列表失败
        const string GET_GOODS_LIST_FAILED = "get_goods_list_failed";
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
        //账号绑定成功
        const string BIND_SUCCESS = "bind_success";
        //账号绑定失败
        const string BIND_FAILED = "bind_failed";
        //获取耳机状态成功
        const string GET_HEADSET_STATE_SUCCESS = "get_headset_state_success";
        //获取ABTest成功
        const string GET_AB_TEST_FINISH = "get_ab_test_ver_finish";
        //获取手机适配文档成功
        const string GET_MOBILE_ADAPTER_SUCCESS = "get_mobile_adapter_success";

        private OnFinish<ResultInit> initListener;
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
        private OnFinish<ResultGetABTestVer> getABTestVerListener;
        private OnFinish<ResultGetMobileAdapter> getMobileAdapterListener;
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
                case GET_AB_TEST_FINISH:
                    GetABTestVerFinish(true,body);
                    break;
                case GET_MOBILE_ADAPTER_SUCCESS:
                    GetMobileAdapterFinish(true, body);
                    break;
                default:
                    if (expandListeners.ContainsKey(head))
                    {
                        ExpandFunctionFinish(head,body);
                    }
                  break;
            }
        }
        /// <summary>
        /// 初始化，此接口必须最先调用
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="initListener">返回初始化结果<see cref="ResultInit"/></param>
        public void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener)
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
        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="appInfoListener">返回应用信息<see cref="AppInfo"/></param>
        public virtual void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {
            this.appInfoListener = appInfoListener;
        }

        public virtual void GameStepInfo(string step,string type)
        {

        }
        /// <summary>
        /// 获取刘海屏信息
        /// </summary>
        /// <param name="getNotchInfoListener">返回刘海屏信息<see cref="NotchScreenInfo"/></param>
        public virtual void GetNotchScreenInfo(OnFinish<NotchScreenInfo> getNotchInfoListener)
        {
            this.getNotchInfoListener = getNotchInfoListener;

        }
        /// <summary>
        /// 获取国家码 
        /// </summary>
        /// <param name="getCountryCodeListener">返回国家码例:中国CN，美国US</param>
        public virtual void GetCountryCode(OnFinish<string> getCountryCodeListener)
        {
            this.getCountryCodeListener = getCountryCodeListener;
        }
   
        /// <summary>
        /// 上报异常日志
        /// </summary>
        /// <param name="errorMsg">异常日志内容</param>
        /// <param name="type">异常日志分类,不传递时默认分类为default</param>
        public virtual void ReportError(string errorMsg,string type = "")
        {

        }

        /// <summary>
        /// 设置剪切板
        /// </summary>
        /// <param name="content">设置剪切板内容</param>
        public virtual void SetClipboard(string content)
        {

        }
        /// <summary>
        /// 获取手机内存信息
        /// </summary>
        /// <param name="getMemroyInfoListener">返回手机内存信息<see cref="MemoryInfo"/></param>
        public virtual void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {
            this.getMemroyInfoListener = getMemroyInfoListener;
        }

        /// <summary>
        /// 获取手机电量信息
        /// </summary>
        /// <param name="getBatteryInfoListener">返回手机电量信息<see cref="BatteryInfo"/></param>
        public virtual void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {
            this.getBatteryInfoListener = getBatteryInfoListener;
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="srcContent">待翻译文本内容</param>
        /// <param name="targetLan">目标语言码</param>
        /// <param name="translateContentListener">成功返回翻译后文本，失败返回原文本</param>
        public virtual void TranslateContent(string srcContent, string targetLan, OnFinish<string> translateContentListener)
        {
            this.translateContentListener = translateContentListener;
        }

        /// <summary>
        /// 获取cpu gpu信息
        /// </summary>
        /// <param name="getCpuAndGpuListener">返回cpu gpu信息<see cref="CpuGpuInfo"/></param>
        public virtual void GetCpuAndGpu(OnFinish<CpuGpuInfo> getCpuAndGpuListener)
        {
            this.getCpuAndGpuListener = getCpuAndGpuListener;

        }

        /// <summary>
        /// 本地通知
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="delay">延迟时间推送 单位秒</param>
        /// <param name="id">本地通知唯一标识,清除推送时用到</param>
        public virtual void PushNotification(string content, int delay, int id)
        {

        }
        /// <summary>
        /// 清除本地通知
        /// </summary>
        /// <param name="id">本地通知唯一标识，和PushNotification方法参数id对应</param>
        public virtual void CleanNotification(int id)
        {

        }
        /// <summary>
        /// 清除全部本地通知
        /// </summary>
        public virtual void CleanAllNotification()
        {

        }
        /// <summary>
        /// 远程推送,如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="ip">远程推送服务器ip</param>
        /// <param name="port">远程推送服务器端口</param>
        /// <param name="gameRoleId">角色id</param>
        public virtual void UdpPush(string ip, string port, string gameRoleId)
        {

        }

        /// <summary>
        /// 开始录音，如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="serverURL">录音完毕后语音存储服务器url</param>
        /// <param name="bit">语音录音品质，低品质1，中品质2，高品质3</param>
        /// <param name="recordMaxTime">录音最大时长，超过该时长自动结束录音,单位毫秒</param>
        public virtual void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {

        }
        /// <summary>
        /// 结束录音
        /// </summary>
        /// <param name="stopRecordVideoListener">返回录音结果<see cref="ResultVideoRecord"/></param>
        public virtual void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            this.stopRecordVideoListener = stopRecordVideoListener;
        }
        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="videoUrl">录音存放url</param>
        /// <param name="playVideoListener">返回播放录音结果<see cref="Result"/></param>
        public virtual void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            this.playVideoListener = playVideoListener;
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="loginListener">返回账号登录结果<see cref="UserInfo"/></param>
        public virtual void Login(OnFinish<UserInfo> loginListener)
        {
            this.loginListener = loginListener;
        }

        /// <summary>
        /// 账号切换
        /// </summary>
        /// <param name="switchAccountListener">返回账号切换结果<see cref="UserInfo"/></param>
        public virtual void SwitchAccount(OnFinish<UserInfo> switchAccountListener)
        {
            this.switchAccountListener = switchAccountListener;
        }

        /// <summary>
        /// 账号绑定
        /// </summary>
        /// <param name="startBindListener">返回账号绑定结果<see cref="ResultBind"/></param>
        public virtual void StartBind(OnFinish<ResultBind> startBindListener)
        {
            this.startBindListener = startBindListener;
        }

        /// <summary>
        /// 账号注销
        /// </summary>
        /// <param name="logoutListener">返回账号注销结果<see cref="Result"/></param>
        public virtual void Logout(OnFinish<Result> logoutListener)
        {
            this.logoutListener = logoutListener;
        }
        /// <summary>
        /// 渠道是否有退出框
        /// </summary>
        /// <returns>true表示渠道有退出框，false表示渠道没有退出框此时游戏需要创建退出框</returns>
        public virtual bool HasExitDialog()
        {
            return false;
        }

        /// <summary>
        /// 上报角色信息
        /// </summary>
        /// <param name="gameRoleInfo">角色信息</param>
        /// <returns></returns>
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

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="payListener">返回支付结果<see cref="ResultPay"/></param>
        /// <returns></returns>
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

        /// <summary>
        /// 退出游戏
        /// </summary>
        /// <param name="exitListener">返回退出结果<see cref="Result"/></param>
        public virtual void Exit(OnFinish<Result> exitListener)
        {
            this.exitListener = exitListener;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="serverId">服务器id(可选)</param>
        /// <param name="language">公告语言类型(可选) 例:中文zh,英语en</param>
        /// <param name="type">公告类型(可选) (0普通1活动2更新3跑马灯4登录5登出)</param>
        /// <param name="getNoticeListListener"></param>
        public virtual void GetNoticeList(OnFinish<NoticeList> getNoticeListListener,string serverId="", string language = "",string country = "", string type = "")
        {
            this.getNoticeListListener = getNoticeListListener;
        }

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="getServerListListener">返回服务器列表<see cref="ServerList"/></param>
        public virtual void GetServerList(OnFinish<ServerList> getServerListListener)
        {
            this.getServerListListener = getServerListListener;
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="serverId">服务器id</param>
        /// <param name="category">商品类型(可选)，逗号分隔，留空或不传表示所有类型)</param>
        /// <param name="currency">币种过滤(可选)（将只返回符合币种条件的商品）</param>
        /// <param name="getGoodsListListener">返回商品列表<see cref="GoodsList"/></param>
        public virtual void GetGoodsList(OnFinish<GoodsList> getGoodsListListener,string serverId, string category = "", string currency="")
        {
            this.getGoodsListListener = getGoodsListListener;
        }
        /// <summary>
        /// 获取热更信息
        /// </summary>
        /// <param name="type">热更目录</param>
        /// <param name="getDynamicUpdateListener">返回热更信息<see cref="ResultGetDynamic"/></param>
        public virtual void GetDynamicUpdate(string type, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {           
            this.getDynamicUpdateListener = getDynamicUpdateListener;
        }
        /// <summary>
        /// 下载热更
        /// </summary>
        /// <param name="downDynamicUpdateListener">返回下载热更结果<see cref="ResultDownload"/></param>
        public virtual void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            this.downDynamicUpdateListener = downDynamicUpdateListener;
        }

        /// <summary>
        /// 删除所有热更资源
        /// </summary>
        public virtual void RepairUpdateRes()
        {

        }
        /// <summary>
        /// 获取强更信息
        /// </summary>
        /// <param name="getForceUpdateListener">返回强更信息<see cref="ResultGetForce"/></param>
        public virtual void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            this.getForceUpdateListener = getForceUpdateListener;
        }

        /// <summary>
        /// 强更下载
        /// </summary>
        /// <param name="downForceUpdateListener">返回强更下载结果<see cref="ResultDownload"/></param>
        public virtual void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            this.downForceUpdateListener =  downForceUpdateListener;
        }
        /// <summary>
        /// 是否需要obb下载，Android包上架google商店且包体大于100M才需要使用到obb下载功能
        /// </summary>
        /// <returns>true必须要下载，false需要下载</returns>
        public virtual bool HasObbUpdate()
        {
            return false;
        }
        /// <summary>
        /// 下载obb
        /// </summary>
        /// <param name="downObbUpdateListener">返回obb下载结果<see cref="ResultObbDownload"/></param>
        public virtual void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            this.downObbUpdateListener = downObbUpdateListener;
        }
        /// <summary>
        /// obb下载中断时调用此方法继续下载
        /// </summary>
        public virtual void ContinueUpdateObb()
        {

        }
        /// <summary>
        /// obb下载完成后重新加载obb
        /// </summary>
        public virtual void ReloadObb()
        {

        }
        /// <summary>
        /// 获取渠道是否支持接口
        /// </summary>
        /// <param name="api">接口类型</param>
        /// <returns>返回true标识支持，false标识不支持</returns>
        public virtual bool IsSupportApi(Api api)
        {
            return true;
        }

        /// <summary>
        /// 扩展方法 需要与Android联调
        /// </summary>
        /// <param name="functionName">扩展方法名称</param>
        /// <param name="jsonParameter">扩展方法所需参数(可选)</param>
        /// <param name="headName">处理标识确保(可选),不与已存在的HeadName冲突</param>
        /// <param name="expandFunctionListener">返回处理结果(可选) <see cref="ResultExpand"/></param>
        public virtual void ExpandFunction(string functionName, string jsonParameter="",string headName = "",OnFinish<ResultExpand> expandFunctionListener = null)
        {
            if (headName != null && headName != ""&& !expandListeners.ContainsKey(headName))
            {
                expandListeners.Add(headName,expandFunctionListener);
            }

        }
        /// <summary>
        /// 获取耳机插入状态
        /// </summary>
        /// <param name="notifyWhenHeadsetChanged">当耳机状态发送改变时回调通知</param>
        /// <param name="getHeadsetStateListener">返回耳机插入状态<see cref="ResultGetHeadsetState"/></param>
        public virtual void GetHeadsetState(bool notifyWhenHeadsetChanged,OnFinish<ResultGetHeadsetState> getHeadsetStateListener)
        {
            this.getHeadsetStateListener = getHeadsetStateListener;
        }
        /// <summary>
        /// 新手引导完调用
        /// </summary>
        public virtual void SendGuideFinish()
        {
        }
        /// <summary>
        /// 获取ABTest
        /// </summary>
        /// <param name="getABTestVerListener">返回获取ABTest<see cref="ResultGetABTestVer"/></param>
        public virtual void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
            this.getABTestVerListener = getABTestVerListener;
        }
        /// <summary>
        /// 获取手机适配文档
        /// </summary>
        /// <param name="getMobileAdapterListener">返回手机适配文档<see cref="ResultGetMobileAdapter"/></param>
        public virtual void GetMobileAdapter(string url,OnFinish<ResultGetMobileAdapter> getMobileAdapterListener)
        {
            this.getMobileAdapterListener = getMobileAdapterListener;
        }

        

        /*---------------------------------------------------------------------------------------------------------*/

        private void GetABTestVerFinish(bool success, string body)
        {
            ResultGetABTestVer result = new ResultGetABTestVer();
            result.success = success;
            int plan;
            bool parseResult = int.TryParse(body,out plan);
            if (parseResult)
            {
                result.plan = plan;
            }
            getABTestVerListener(result);
        }
        private void GetMobileAdapterFinish(bool success, string body)
        {
            ResultGetMobileAdapter result = new ResultGetMobileAdapter();
            result.success = success;
            result.content = body;
            getMobileAdapterListener(result);
        }


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
            Dictionary<string, string> customParams = new Dictionary<string, string>();
            ResultInit result = new ResultInit(customParams);
            result.success = success;
            if (!success)
                result.message = body;
            JSONNode node = JSONNode.Parse(body);        
            foreach(KeyValuePair<string, JSONNode> item in node.AsObject)
            {
                customParams[item.Key] = item.Value.Value;
            }
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
                info.totalMem = node["totalMem"].AsLong;
                info.availMem = node["availMem"].AsLong;
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
