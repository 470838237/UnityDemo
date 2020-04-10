

using System.Collections.Generic;

namespace HonorSdk
{
    public class HonorResultInit : ResultInit
    {

        public const string DYNAMIC_SETTING_FILE_PATH = "dynamic_setting_file_path";

        public HonorResultInit(Dictionary<string, string> customParams) : base(customParams)
        {
            this.customParams = customParams;
        }
    }

    public class HonorUserInfo : UserInfo
    {

        //当前登录平台类型google,fb,twitter，可以从bindStates集合中查找当前平台的绑定状态
        public string platform { set; get; }

        public List<BindState> bindStates = new List<BindState>();


        //当前账号今天的累计登录时间
        public const string EXTRA_PLAY_TIME = "playtime";
        public const string EXTRA_IDENTIFY = "identify";

        public HonorUserInfo(Dictionary<string, string> extra) : base(extra)
        {
            this.extra = extra;
        }


    }

    public class IdentifyInfo
    {

        //-1:游客 0:未认证 1:未满8岁 2:未满16岁 3:未满18岁 4:成年
        public int identify
        {
            set; get;
        }

    }
    public class HardwareInfo : Result
    {
        //Gpu型号
        public string gpu
        {
            set; get;
        }
        //cpu型号 JSONArray类型字符串,数组成员为字符串类型  
        //由于各个手机获取cpu型号的接口不一致,该接口返回了常用的接口,开发者根据业务逻辑筛选
        public string cpu
        {
            set; get;
        }
        //Cpu核心数量
        public int cpuNum
        {
            set; get;
        }
        //Cpu最大频率
        public int cpuFreq
        {
            set; get;
        }
        //总内存
        public int totalMemory
        {
            set; get;
        }
        //空闲内存
        public int freeMemory
        {
            set; get;
        }

        //机型
        public string deviceModel
        {
            set; get;
        }

    }

    public class ResultVideoRecord : Result
    {
        //语音存放链接
        public string url
        {
            set; get;
        }
        //语音时长 单位秒
        public long time
        {
            set; get;
        }
    }
    public class ResultTranslate : Result
    {
        public string translateReult
        {
            set; get;
        }
        public int id
        {
            set; get;
        }
    }

    public class ResultGetDynamic : Result
    {
        //热更总大小 单位Byte
        public long totalSize
        {
            set; get;
        }
        //下载热更文件存储路径
        public string dynamicResPath
        {
            set; get;
        }
    }
    public class ResultDownload : Result
    {
        //下载总大小 单位Byte
        public long totalSize
        {
            set; get;
        }
        //已下载总大小 单位Byte
        public long currentSize
        {
            set; get;
        }
    }
    public class ResultGetForce : Result
    {
        //跳转应用商店返回强更大小
        public const int JUMP_APP_STORE = 1;
        //强更大小 单位Byte
        public long totalSize
        {
            set; get;
        }
    }
    public class ResultObbDownload
    {
        //需要下载总大小 单位Byte
        public long totalSize
        {
            set; get;
        }
        //已下载大小 单位Byte
        public long currentSize
        {
            set; get;
        }
        //obb下载过程返回的状态信息
        public string state
        {
            set; get;
        }
        //true表示状态发送变化返回状态信息,false表示没有状态变化时返回下载信息
        public bool stateChanged
        {
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

    public class ResultGetABTestVer : Result
    {
        //ABTest方案
        public int plan
        {
            set; get;
        }
    }



    public class ResultDownloadText : Result
    {
        public string content
        {
            set; get;
        }
    }

}
