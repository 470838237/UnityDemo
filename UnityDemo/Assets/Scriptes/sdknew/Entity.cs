

using System.Collections.Generic;

namespace HonorSdk
{
    /// <summary>
    /// 类说明：基础返回信息
    /// </summary>
    public class Result
    {
        //错误码
        public int code
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



    public class DiskInfo
    {
        // 外部存储总大小 单位Byte
        public long totalSize { set; get; }
        //外部存储可用大小 单位Byte
        public long availSize { set; get; }
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


    public class HeadsetStateInfo : Result
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

    /// <summary>
    /// 类说明：基础返回信息
    /// </summary>
    public class ResultInit : Result
    {
        protected Dictionary<string, string> customParams;

        public ResultInit(Dictionary<string, string> customParams)
        {
            this.customParams = customParams;
        }
        public string getCustomParameter(string key)
        {
            return customParams[key];
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
        //用户昵称
        public string nickName
        {
            set; get;
        }

        protected Dictionary<string, string> extra;
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
   

}
