﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSDK
{

    /// <summary>
    /// 类说明：基础返回信息
    /// </summary>
    public class Result
    {
        //错误码
        public string code
        {
            set; get;
        }
        //处理结果
        public bool success
        {
            set; get;
        }
        //处理失败的信息
        public string message
        {
            set; get;
        }
    }

    /// <summary>
    /// 类说明：基础返回信息
    /// </summary>
    public class ResultInit : Result
    {

        public const string DYNAMIC_SETTING_FILE_PATH = "dynamic_setting_file_path";

        private Dictionary<string, string> customParams;

        public ResultInit(Dictionary<string, string> customParams)
        {
            this.customParams = customParams;
        }
        public string getCustomParameter(string key)
        {
            return customParams[key];
        }
    }

    public class NetStateInfo : Result
    {
        //是否是wifi连接
        public bool wifi
        {
            set; get;
        }
        //是否有网络
        public bool networkConnect
        {
            set; get;
        }
    }

    public class UserInfo : Result
    {

        public string uid
        {
            set; get;
        }
        //用户token，用于验证用户合法性
        public string accessToken
        {
            set; get;
        }
        //登录失败时，hasCode为true表示用户信息有缓存不需要重新登陆，false表示无缓存需要重新登陆
        public bool hasCache
        {
            set; get;
        }
        //用户昵称
        public string nickName
        {
            set; get;
        }
        //当前登录平台类型google,fb,twitter，可以从bindStates集合中查找当前平台的绑定状态
        public string platform { set; get; }

        public List<BindState> bindStates = new List<BindState>();

        private Dictionary<string, string> extra;
        //当前账号今天的累计登录时间
        public const string EXTRA_PLAY_TIME = "playtime";
        public const string EXTRA_IDENTIFY = "identify";
        public UserInfo(Dictionary<string, string> extra)
        {
            this.extra = extra;
        }
        public string getExtra(string key)
        {
            return extra[key];
        }
    }

    public class ResultBind : Result
    {
        //用户昵称
        public List<BindState> bindStates = new List<BindState>();
        public string nickName
        {
            set; get;
        }
    }

    public class BindState
    {
        public string platform;
        public int bindState;
        //已绑定
        public int STATE_BIND = 1;
        //未绑定
        public int STATE_UNBIND = 0;
        const string PLATFORM_GOOGLE = "google";
        const string PLATFORM_FACEBOOK = "facebook";
        const string PLATFORM_TWITTER = "twitter";
    }

    public class AppInfo : Result
    {
        //IOS平台标识
        const string PLATFORM_IOS = "0";
        //Android平台标识
        const string PLATFORM_ANDROID = "1";
        //包名
        public string appId
        {
            set; get;
        }
        //设备id
        public string deviceId
        {
            set; get;
        }
        //应用版本号
        public string version
        {
            set; get;
        }
        //平台标识
        public string platform
        {
            set; get;
        }
        //应用名称
        public string appName
        {
            set; get;
        }
    }

    public class NotchScreenInfo : Result
    {
        //刘海屏宽度 单位px
        public int width
        {
            set; get;
        }
        //刘海屏高度 单位px
        public int height
        {
            set; get;
        }
    }


    public class MemoryInfo : Result
    {
        //可用内存大小 单位Byte
        public long availMem
        {
            set; get;
        }
        //总内存大小 单位Byte
        public long totalMem
        {
            set; get;
        }
        public long pssmemory
        {
            set; get;
        }
    }

    public class Locale : Result
    {
        //国家
        public string country
        {
            set; get;
        }
        //操作系统语言
        public string language
        {
            set; get;
        }
    }

    public class BatteryInfo : Result
    {
        //当前电量(0-100)
        public int level
        {
            set; get;
        }
        //总电量(100)
        public int scale
        {
            set; get;
        }
        //电池温度
        public double temperature
        {
            set; get;
        }
        //是否在充电
        public bool isCharge
        {
            set; get;
        }
    }
    public class GameRoleInfo
    {
        //创建角色节点
        public const int TYPE_CREATE_ROLE = 1;
        //进入游戏节点
        public const int TYPE_ENTER_GAME = 2;
        //角色升级节点
        public const int TYPE_ROLE_LEVEL = 3;

        public const int GENDERMALE = 1;

        public const int GENDER_FEMALE = 2;
        //角色ID
        public string roleId
        {
            set; get;
        }
        //角色名称
        public string roleName
        {
            set; get;
        }
        //服务器id
        public string serverId
        {
            set; get;
        }
        //角色等级
        public int roleLevel
        {
            set; get;
        }
        //角色vip等级
        public string roleVip
        {
            set; get;
        }

        //角色性别
        public int gender
        {
            set; get;
        }

        //账户金币或钻石等货币余额
        public int balance
        {
            set; get;
        }


        //角色其他信息
        public string extra
        {
            set; get;
        }
        //角色上传的节点类型
        public int type
        {
            set; get;
        }
        //角色最后更新时间
        public int lastUpdate
        {
            set; get;
        }

    }
    public class OrderInfo
    {
        //是否是首充
        public bool isFirstPay
        {
            set; get;
        }

        //服务器id
        public string serverId
        {
            set; get;
        }
        //角色id
        public string roleId
        {
            set; get;
        }
        //角色名称
        public string roleName
        {
            set; get;
        }
        //角色等级
        public int roleLevel
        {
            set; get;
        }

        //vip等级
        public int vipLevel
        {
            set; get;
        }

        //账户余额
        public int balance
        {
            set; get;
        }
        //商品id
        public string goodsId
        {
            set; get;
        }
        //商品数量 不传时默认为1
        public int count
        {
            set; get;
        }

        //透传参数，游戏传入的值，将在查询订单信息时原样返回
        public string extra
        {
            set; get;
        }
    }

    public class DiskInfo
    {
        // 外部存储总大小 单位Byte
        public long totalSize { set; get; }
        //外部存储可用大小 单位Byte
        public long availSize { set; get; }
    }

    public class ResultPay : Result
    {
        //平台订单号
        public string orderId
        {
            set; get;
        }
    }
    public class NoticeInfo
    {
        public enum eNoticeType
        {
            NomalNotice = 0,//普通公告
            ActivityNotice = 1,//活动公告
            UpdateNotice = 2,//更新公告
            MarqueeNotice = 3,//跑马灯公告
            LoginNotice = 4,//登录公告
            LogoutNotice = 5,//登出公告
            InterceptNotice = 6//截断公告
        }

        //公告类型(0普通1活动2更新3跑马灯4登录5登出)
        public int id
        {
            set; get;
        }

        public int type
        {
            set; get;
        }
        //模式(0文本1海报)
        public int mode
        {
            set; get;
        }
        //公告标题
        public string title
        {
            set; get;
        }
        //文本公告内容
        public string content
        {
            set; get;
        }
        //海报公告图片地址
        public string image
        {
            set; get;
        }
        //海报公告跳转地址
        public string link
        {
            set; get;
        }
        //公告排序 返回值越小越优先
        public int sort
        {
            set; get;
        }
        //公告重要程度(1重要0一般)
        public int important
        {
            set; get;
        }
        //公告状态(1启用0禁用)
        public int status
        {
            set; get;
        }
        //公告开始时间
        public long startTime
        {
            set; get;
        }
        //公告结束时间
        public long endTime
        {
            set; get;
        }
        //公告剩余秒数
        public long remainTime
        {
            set; get;
        }
    }

    public class NoticeList : Result
    {
        //公告列表
        public List<NoticeInfo> notices = new List<NoticeInfo>();
    }
    public class ServerInfo
    {
        //服务器id
        public string serverId
        {
            set; get;
        }
        //服务器名称
        public string serverName
        {
            set; get;
        }
        //服务器状态（1正常/2维护）
        public int status
        {
            set; get;
        }
        //显示标签（new新服/recommend推荐/hot火爆/full爆满），多个标签逗号分隔
        public string label
        {
            set; get;
        }
        //服务器地址（例如：123.123.123.123:1234或https://server1.game.com/play）
        public string address
        {
            set; get;
        }
        //功能标记(有的项目会有自己的特殊功能主机，可以自行标记，例如：login=登录服)
        public string tag
        {
            set; get;
        }
        //自动开服时间（秒级时间戳）
        public long openTime
        {
            set; get;
        }
        //自动关服时间（秒级时间戳）
        public long closeTime
        {
            set; get;
        }
        //角色列表信息
        public List<GameRoleInfo> roles = new List<GameRoleInfo>();
    }


    public class ServerList : Result
    {
        //当前服务器时间（秒级时间戳），用于和客户端同步开关服时间
        public long time
        {
            set; get;
        }
        //是否测试人员，非测试人员没有该字段;如果是测试人员则tester=1
        public int tester
        {
            set; get;
        }
        public List<ServerInfo> servers = new List<ServerInfo>();
    }

    public class GoodsInfo
    {
        //商品id
        public string goodsId
        {
            set; get;
        }
        //商品名称
        public string goodsName
        {
            set; get;
        }
        //商品描述
        public string description
        {
            set; get;
        }
        //商品图标资源地址
        public string url
        {
            set; get;
        }
        //商品价格（实际付款价格）---后台配置的
        public double price
        {
            set; get;
        }
        //商品展示价（游戏可显示为商品原价之类）
        public string priceDisplay
        {
            set; get;
        }
        //币种代码 例:"USD" "CNY"
        public string currency
        {
            set; get;
        }
        //货币符号 例:"¥", "$", "€"---后台配置的
        public string localSymbol;
        //当地价格,主要针对海外多地区发行，国内发行等同price---google获取

        public double localPrice { set; get; }

        //币种代码 例:"USD" "CNY"
        public string localCurrency { set; get; }

        //游戏币数量
        public int count
        {
            set; get;
        }
        //发放倍率（比如首次购买得双倍游戏币）(首充双倍 或者打折 都在这里取)
        public string ratio
        {
            set; get;
        }
        //赠品 返回json字符串
        public string gift
        {
            set; get;
        }
        // 商品类别（比如商城1：store1(钻石) 商城2：store2(限时直购) / 商城3：store3）
        public string category
        {
            set; get;
        }
        //功能标记：coin金币类商品 / card卡类商品 / prop道具类商品
        public string tag
        {
            set; get;
        }
        //每日限购次数
        public int limitByDay
        {
            set; get;
        }
        //商品生效时间
        public long startTime
        {
            set; get;
        }
        //商品失效时间
        public long endTime
        {
            set; get;
        }
    }

    public enum Api
    {
        //切换账号
        SWITCH_ACCOUNT = 0,
        //注销
        LOGOUT,
        //绑定
        BIND,
        //开始新游戏
        START_NEW_GAME
    }

    public class GoodsList : Result
    {
        //商品列表
        public List<GoodsInfo> goods = new List<GoodsInfo>();
    }
    public class ResultExpand : Result
    {
        //成功时返回处理结果
        public string originResult
        {
            set; get;
        }
    }

    public class ResultGetHeadsetState : Result
    {
        //插入耳机时状态
        public const int HEADSET_OPEN = 1;
        //取消插入耳机时状态
        public const int HEADSET_CLOSE = 0;
        //耳机状态
        public int headsetState
        {
            set; get;
        }
    }
    public delegate void OnFinish();
    public delegate void OnFinish<T>(T result);
    public delegate void OnReceiveMsg(string head, string body);

    public class SDKManager
    {
        private static SDKManager instance;
        public enum ePlat
        {
            none,
            Android,
            Ios,
        }
        protected SDKManager()
        {

        }
        public static void CreateInstance(ePlat plant)
        {
            if (instance == null)
            {
                if (plant == ePlat.Android)
                    instance = new AndroidSdkImpl();
                else if (plant == ePlat.Ios)
                    instance = new IosSdkImpl();
                else
                    instance = new NoSdkImpl();
            }
        }

        public static SDKManager GetInstance()
        {

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
        //账号绑定成功
        const string BIND_SUCCESS = "bind_success";
        //账号绑定失败
        const string BIND_FAILED = "bind_failed";
        //获取网络状态
        const string GET_NET_STATE_INFO = "get_net_state_info";

        private OnFinish<ResultInit> initListener;
        private OnFinish<UserInfo> loginListener;
        private OnFinish<UserInfo> switchAccountListener;
        private OnFinish<AppInfo> appInfoListener;
        private OnFinish getNotchInfoListener;
        private OnFinish<Locale> getCountryAndLanguageListener;
        private OnFinish<MemoryInfo> getMemroyInfoListener;
        private OnFinish<BatteryInfo> getBatteryInfoListener;
        private OnFinish<Result> playVideoListener;
        private OnFinish<Result> logoutListener;
        private OnFinish<ResultPay> payListener;
        private OnFinish<Result> exitListener;
        private OnFinish<NoticeList> getNoticeListListener;
        private OnFinish<ServerList> getServerListListener;
        private OnFinish<GoodsList> getGoodsListListener;
        private OnFinish<ResultBind> startBindListener;
        private OnFinish<NetStateInfo> getNetStateInfoListener;
        private Dictionary<string, OnFinish<ResultExpand>> expandListeners = new Dictionary<string, OnFinish<ResultExpand>>();

        protected virtual void OnReceive(string head, string body)
        {
            switch (head)
            {
                case SET_GAME_OBJECT_NAME_SUCCESS:
                    Init(mConfigs);
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
                    GetCountryAndLanguageFinish(true, body);
                    break;
                case GET_MEMORY:
                    success = (body != null) && body.Length != 0;
                    GetMemoryFinish(success, body);
                    break;
                case CURRENT_BATTERY:
                    success = (body != null) && body.Length != 0;
                    GetBatteryFinish(success, body);
                    break;
                case LOGIN_SUCCESS:
                    LoginFinish(true, body, true);
                    break;
                case LOGIN_FAILED:
                    LoginFinish(false, body, true);
                    break;
                case SWITCH_ACCOUNT_SUCCESS:
                    LoginFinish(true, body, false);
                    break;
                case SWITCH_ACCOUNT_FAILED:
                    LoginFinish(false, body, false);
                    break;
                case BIND_SUCCESS:
                    StartBindFinish(true, body);
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
                case GET_NET_STATE_INFO:
                    GetNetStateInfoFinish(true, body);
                    break;
                default:
                    if (expandListeners.ContainsKey(head))
                    {
                        ExpandFunctionFinish(head, body);
                    }
                    break;
            }
        }



        private string mConfigs;

        /// <summary>
        /// 初始化，此接口必须最先调用
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="initListener">返回初始化结果<see cref="ResultInit"/></param>
        public virtual void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            Debug.Log("SDKManager.Init");
            this.initListener = initListener;
            if (configs == null)
            {
                configs = new Dictionary<string, string>();
            }
            configs.Add("gameResVersion", gameResVersion);
            JSONClass json = new JSONClass();
            foreach (KeyValuePair<string, string> kv in configs)
            {
                json.Add(kv.Key, new JSONData(kv.Value));
            }
            mConfigs = json.ToString();
            gameObject.SetOnReceiveListener(new OnReceiveMsg(this.OnReceive));
            SetGameObjectName(gameObject.gameObject.name);
            Debug.Log(this.ToString());
        }
        protected virtual void SetGameObjectName(string gameObjectName)
        {
            Debug.Log("SDKManager.SetGameObjectName");
        }
        protected virtual void Init(string configsJson)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="step">步骤</param>
        /// <param name="fps">游戏帧率</param>
        /// <param name="type">步骤类型</param>
        public virtual void GameStepInfo(string step, string type = "")
        {

        }


        /// <summary>
        /// 注册刘海屏监听,<see cref="GetNotchScreenInfo"/>  获取刘海屏宽高 </param>
        /// </summary>
        /// <param name="getNotchInfoListener">获取刘海屏成功回调<see cref="NotchScreenInfo"/></param>
        public virtual void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {
            this.getNotchInfoListener = getNotchInfoListener;

        }
        /// <summary>
        ///  获取刘海屏宽高 </param>
        /// </summary>

        public NotchScreenInfo GetNotchScreenInfo()
        {
            return notchInfo;
        }

        /// <summary>
        /// 获取国家码 
        /// </summary>
        /// <param name="getCountryAndLanguageListener">返回国家码例:中国CN，美国US</param>
        public virtual void GetCountryAndLanguage(OnFinish<Locale> getCountryAndLanguageListener)
        {
            this.getCountryAndLanguageListener = getCountryAndLanguageListener;
        }

        /// <summary>
        /// 上报异常日志
        /// </summary>
        /// <param name="errorMsg">异常日志内容</param>
        /// <param name="type">异常日志分类,不传递时默认分类为default</param>
        public virtual void ReportError(string errorMsg, string type = "")
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
        /// 账号登录
        /// </summary>
        /// <param name="loginListener">返回账号登录结果<see cref="UserInfo"/></param>
        public virtual void Login(OnFinish<UserInfo> loginListener)
        {
            this.loginListener = loginListener;
        }

        /// <summary>
        ///不管之前是否有游客账号都从新生成新的游客账号
        /// </summary>
        /// <param name="startNewGameListener"></param>
        public virtual void StartNewGame(OnFinish<UserInfo> startNewGameListener)
        {
            this.loginListener = startNewGameListener;
        }
        /// <summary>
        /// 账号切换
        /// </summary>

        public virtual void SwitchAccount()
        {

        }
        /// <summary>
        /// 注册监听
        /// </summary>
        /// <param name="switchAccountListener">返回账号切换结果<see cref="UserInfo"/></param>
        public virtual void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
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
            json.Add("type", new JSONData(gameRoleInfo.type));
            json.Add("roleId", new JSONData(gameRoleInfo.roleId));
            json.Add("roleName", new JSONData(gameRoleInfo.roleName));
            json.Add("roleLevel", new JSONData(gameRoleInfo.roleLevel));
            json.Add("roleVip", new JSONData(gameRoleInfo.roleVip));
            json.Add("serverId", new JSONData(gameRoleInfo.serverId));
            json.Add("sex", new JSONData(gameRoleInfo.gender));
            json.Add("balance", new JSONData(gameRoleInfo.balance));
            json.Add("lastUpdate", new JSONData(gameRoleInfo.lastUpdate));
            json.Add("extra", new JSONData(gameRoleInfo.extra));
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
            json.Add("roleId", new JSONData(orderInfo.roleId));
            json.Add("isFirstPay", new JSONData(orderInfo.isFirstPay));
            json.Add("roleName", new JSONData(orderInfo.roleName));
            json.Add("roleLevel", new JSONData(orderInfo.roleLevel));
            json.Add("count", new JSONData(orderInfo.count));
            json.Add("goodsId", new JSONData(orderInfo.goodsId));
            json.Add("extra", new JSONData(orderInfo.extra));
            json.Add("vipLevel", new JSONData(orderInfo.vipLevel));
            json.Add("balance", new JSONData(orderInfo.balance));
            json.Add("serverId", new JSONData(orderInfo.serverId));
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
        /// <param name="type">公告类型(可选) (0普通,1活动,2强更,3热更,4拦截,5跑马灯,6登录,7登出)</param>
        /// <param name="getNoticeListListener"></param>
        public virtual void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId = "", string language = "", string country = "", string type = "")
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
        public virtual void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category = "", string currency = "")
        {
            this.getGoodsListListener = getGoodsListListener;
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
        public virtual void ExpandFunction(string functionName, string jsonParameter = "", string headName = "", OnFinish<ResultExpand> expandFunctionListener = null)
        {
            if (headName != null && headName != "" && !expandListeners.ContainsKey(headName))
            {
                expandListeners.Add(headName, expandFunctionListener);
            }

        }

        /// <summary>
        /// 设置SDK应用语言和国家
        /// </summary>
        /// <param name="language"></param>
        /// <param name="country"></param>
        public virtual void SetApplicationLocale(string language, string country = "")
        {

        }

        /// <summary>
        /// 新手引导完调用
        /// </summary>
        public virtual void SendGuideFinish()
        {
        }

        /// <summary>
        /// 获取网络状态
        /// </summary>
        /// <param name="getNetStateInfoListener"></param>
        public virtual void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {
            this.getNetStateInfoListener = getNetStateInfoListener;
        }

        public virtual NetStateInfo GetNetStateInfo()
        {
            return null;
        }

        //--------------------------------------------------------------------------------------------
        protected virtual void GetNetStateInfoFinish(bool success, string body)
        {
            NetStateInfo result = new NetStateInfo();
            JSONNode node = JSONNode.Parse(body);
            result.success = success;
            result.wifi = node["wifi"].AsBool;
            result.networkConnect = node["networkConnect"].AsBool;
            getNetStateInfoListener(result);
        }


        protected virtual void GetCountryAndLanguageFinish(bool success, string body)
        {
            Locale locale = new Locale();
            JSONNode node = JSONNode.Parse(body);
            locale.success = success;
            locale.language = node["language"];
            locale.country = node["country"];
            getCountryAndLanguageListener(locale);
        }

        protected virtual void ExpandFunctionFinish(string head, string body)
        {
            OnFinish<ResultExpand> listener = expandListeners[head];
            if (listener == null)
                return;
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

        protected virtual void StartBindFinish(bool success, string body)
        {
            ResultBind result = new ResultBind();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.nickName = node["nickName"].Value;
                result.message = node["message"].Value;
                JSONArray arrayBindStates = node["bindStates"].AsArray;
                List<BindState> bindStates = result.bindStates;
                foreach (JSONNode item in arrayBindStates.Childs)
                {
                    BindState bindState = new BindState();
                    bindState.bindState = item["bindState"].AsInt;
                    bindState.platform = item["platform"].Value;
                    bindStates.Add(bindState);
                }
            }
            else
            {
                result.message = body;
            }
            startBindListener(result);
        }

        protected virtual void GetGoodsListFinish(bool success, string body)
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
                    goodsInfo.localCurrency = item["localCurrency"].Value;
                    goodsInfo.localPrice = item["localPrice"].AsDouble;
                    goodsInfo.localSymbol = item["localSymbol"].Value;
                    goods.Add(goodsInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getGoodsListListener(result);
        }

        protected virtual void GetServerListFinish(bool success, string body)
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
                    serverInfo.address = item["address"].Value;
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
                        roleInfo.gender = itemRole["gender"].AsInt;
                        roleInfo.balance = itemRole["balance"].AsInt;
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

        protected virtual void GetNoticeListFinish(bool success, string body)
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
                    noticeInfo.id = item["id"].AsInt;
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
                    noticeInfo.remainTime = item["remain_time"].AsLong;
                    notices.Add(noticeInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getNoticeListListener(result);
        }

        protected virtual void ExitFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            exitListener(result);
        }

        protected virtual void PayFinish(bool success, string body)
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

        protected virtual void LogoutFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            logoutListener(result);
        }

        protected virtual void InitFinish(bool success, string body)
        {
            Dictionary<string, string> customParams = new Dictionary<string, string>();
            ResultInit result = new ResultInit(customParams);
            result.success = success;
            if (!success)
                result.message = body;
            else
            {
                JSONNode node = JSONNode.Parse(body);
                foreach (KeyValuePair<string, JSONNode> item in node.AsObject)
                {
                    customParams[item.Key] = item.Value.Value;
                }
            }
            initListener(result);
        }
        private NotchScreenInfo notchInfo = new NotchScreenInfo();

        protected virtual void GetNotchInfoFinish(bool success, string body)
        {

            notchInfo.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                notchInfo.width = node["width"].AsInt;
                notchInfo.height = node["height"].AsInt;
            }
            if (getNotchInfoListener != null)
                getNotchInfoListener();
            getNotchInfoListener = null;
        }


        protected virtual void GetAppInfoFinish(bool success, string body)
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

        protected virtual void GetBatteryFinish(bool success, string body)
        {
            BatteryInfo info = new BatteryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.scale = node["scale"].AsInt;
                info.level = node["level"].AsInt;
                info.temperature = node["temperature"].AsInt;
                info.isCharge = node["isCharge"].AsBool;
            }
            getBatteryInfoListener(info);
        }

        protected virtual void GetMemoryFinish(bool success, string body)
        {
            MemoryInfo info = new MemoryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.totalMem = node["totalMem"].AsLong;
                info.availMem = node["availMem"].AsLong;
                info.pssmemory = node["pssmemory"].AsLong;
            }
            getMemroyInfoListener(info);
        }

        protected virtual void LoginFinish(bool success, string body, bool login)
        {
            Dictionary<string, string> extra = new Dictionary<string, string>();
            UserInfo userInfo = new UserInfo(extra);
            userInfo.success = success;
            JSONNode node = JSONNode.Parse(body);
            userInfo.message = node["message"].Value;
            userInfo.code = node["code"].Value;
            userInfo.hasCache = node["hasCache"].AsBool;
            if (success)
            {
                JSONClass extraNode = node["extra"].AsObject;
                foreach (KeyValuePair<string, JSONNode> item in extraNode)
                {
                    extra[item.Key] = item.Value.Value;
                }
                userInfo.message = node["message"].Value;
                userInfo.uid = node["uid"].Value;
                userInfo.accessToken = node["accessToken"].Value;
                userInfo.nickName = node["nickname"].Value;
                JSONArray arrayBindStates = node["bindStates"].AsArray;
                List<BindState> bindStates = userInfo.bindStates;
                foreach (JSONNode item in arrayBindStates.Childs)
                {
                    BindState bindState = new BindState();
                    bindState.bindState = item["bindState"].AsInt;
                    bindState.platform = item["platform"].Value;
                    bindStates.Add(bindState);
                }
            }
            if (login)
                loginListener(userInfo);
            else
                switchAccountListener(userInfo);
        }
    }
}
