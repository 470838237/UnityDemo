using System;
using GCloud.MSDK;
using UnityEngine;
using System.Collections.Generic;
using GCloud;

namespace HonorSDK
{
    public class RemoteNotification
    {
        //true为前台消息，false为后台消息
        public bool foreground
        {
            set; get;
        }
        public string notification
        {
            set; get;
        }
    }
    public class Wrapper<T> {
 
        public bool success
        {
            set; get;
        }
        public T obj
        {
            set; get;
        }
    }

 
    public interface CrashCallback
    {
        /// <summary>
        /// 这里是非unity进程，注意不要做unity相关的操作
        /// </summary>
        /// <returns>崩溃时回调上传额外的日志</returns>
        string GetExtraMessage();
        /// <summary>
        /// 里是非unity进程，注意不要做unity相关的操作
        /// </summary>
        /// <returns>崩溃时回调上传额外的二进制文件</returns>
        string GetExtraData();
    }

    public interface LoginCallback
    {
        /// <summary>
        /// 回调该接口用户登录成功
        /// onlyRefreshData为false需要走一遍登录流程
        /// onlyRefreshData为true，游戏只需要刷新MSDKLoginRet信息就
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="onlyRefreshData"></param>
        void OnLoginSuccess(MSDKLoginRet ret, bool onlyRefreshData = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retCode"><see cref="MSDKError"/></param>
        /// <param name="msg"></param>
        void OnLoginFailure(int retCode, string msg);
        /// <summary>
        /// 回调该接口游戏需弹出询问玩家是否切换账号对话框，玩家响应后调用SwitchUser接口
        /// </summary>
        void OnSwitchUser();
        /// <summary>
        /// 回调该接口游戏需要返回登录界面，让玩家选择渠道登录，例如QQ，微信
        /// </summary>
        void OnAutoLoginFailed();
        /// <summary>
        /// 注销成功后游戏应返回登录界面，让玩家选择渠道登录，例如QQ，微信
        /// </summary>
        void OnLogoutSuccess();
    }

    public class HonorTencentSDKImpl : HonorSDKImpl
    {
        private static HonorTencentSDKImpl instance;
        protected HonorSDKImpl parentImpl;
      

        protected HonorTencentSDKImpl()
        {
        }

        public const string  GCLOUD_GAME_ID = "gcloud_game_id";
        public const string  GCLOUD_GAME_KEY = "gcloud_game_key";

        new public static void CreateInstance(ePlat plant)
        {
            if (instance == null)
            {
                if (plant == ePlat.Android)
                {
                    instance = new TencentAndroidImpl();
                    instance.parentImpl = new CustomAndroidSdkImpl();
                }

            }
        }

        internal void RegisterLoginCallback(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        new public static HonorTencentSDKImpl GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// 在Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)初始化成功后调用
        /// 初始化游戏云
        /// 初始化TDir
        /// <see cref="https://sdk.gcloud.tencent.com/documents/details/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%20Maple/%E6%8E%A5%E5%85%A5%E6%8C%87%E5%8D%97/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%EF%BC%88Dir%E6%9C%8D%E5%8A%A1%EF%BC%89"/>
        /// </summary>
        /// <param name="cloudInfo"></param>
        /// <param name="tdirInfo"></param>

        public virtual void InitGCloud(InitializeInfo cloudInfo, TdirInitInfo tdirInfo) {
            
        }

        /// <summary>
        /// 在调用上述初始化接口之后，游戏模块需要在MonoBehaviour的Update函数里，定时调用Update函数，驱动Tdir底层模块进行收发数据。
        /// </summary>
        public virtual void Update()
        {
           
        }
        /// <summary>
        /// 拉取目录树 SDK提供拉取单个目录树(大区)的功能
        /// 可以获取指定目录树（treeId即OMS界面-区服编辑器页面中的ID，如下图显示）的结构信息
        /// <see cref="https://sdk.gcloud.tencent.com/documents/details/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%20Maple/%E6%8E%A5%E5%85%A5%E6%8C%87%E5%8D%97/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%EF%BC%88Dir%E6%9C%8D%E5%8A%A1%EF%BC%89"/>
        /// </summary>
        /// <param name="treeId">TreeId == PlatformId</param>
        /// <returns>-1表示查询失败</returns>
        public virtual int QueryTree(int treeId , OnFinish<Wrapper<TreeInfo>> callback) {

            return -1;
        }
        /// <summary>
        /// 拉取指定服务节点_SDK还提供拉取某个指定服的信息的功能，以满足一些特殊的业务需求
        /// 此处的treeId， 是整棵树的id，可以通过TreeInfo的TreeId属性获取。此处的leafId是叶子节点的Id。
        /// <see cref="https://sdk.gcloud.tencent.com/documents/details/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%20Maple/%E6%8E%A5%E5%85%A5%E6%8C%87%E5%8D%97/%E5%8C%BA%E6%9C%8D%E5%AF%BC%E8%88%AA%EF%BC%88Dir%E6%9C%8D%E5%8A%A1%EF%BC%89"/>
        /// </summary>
        /// <param name="treeId">TreeId == PlatformId</param>
        /// <param name="nodeId">LeafId == ZoneId</param>
        /// <returns>-1表示查询失败</returns>
        public virtual int QueryLeaf(int treeId,int leafId, OnFinish<Wrapper<NodeWrapper>> callback)
        {
            return -1;
        }

        /// <summary>
        /// 登录指定渠道，获取第三方平台登录态并到 MSDK 服务器鉴权，返回 MSDK 统一账号。
        ///目前只有微信渠道支持二维码登录，详情见微信渠道的登录功能。
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Login.html"/>
        /// </summary>
        /// <param name="channel"><see cref="MSDKChannel"/>不传递channel表示自动登录</param>
        /// <param name="permissions"></param>
        /// <param name="subChannel"></param>
        /// <param name="extraJson"></param>
        public virtual void Login(string channel = "", string permissions = "", string subChannel = "", string extraJson = "")
        {

        }
        /// <summary>
        /// 接受 MSDK 登录模块的回调，游戏需要注册回调函数进行处理；强烈建议游戏在应用启动函数中进行注册。
        /// </summary>
        /// <param name="LoginCallback"></param>
        public virtual void RegisterLoginCallback(LoginCallback LoginCallback)
        {

        }

        /// <summary>
        /// 在出现异账号的情况，选择是否切换另一个账号登录。 异账号的情况：比如游戏用 QQ 登录，按 Home 键返回桌面后，又从微信游戏中心启动游戏，就会出现是否需要切换到微信账号的情况，称为异账号。 一般国内会出现异账号的情况
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Login.html"/>
        /// </summary>
        /// <param name="confirm">true表示玩家同意切换账号,false表示玩家不同意切换账号</param>
        public virtual void SwitchUser(bool confirm)
        {

        }
        /// <summary>
        /// 获取已经配置的公告信息。该接口返回请求id，回调中带回获取到的公告信息以及请求id等。
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Notice.html"/>
		/// </summary>
		/// <param name="noticeGroup">公告分组，后台填好公告并且分好组就可以对应拉取公告信息</param>
		/// <param name="language">语种</param>
		/// <param name="region">地区，北美、亚太、南美等等</param>
		/// <param name="partition">游戏大区</param>
		/// <param name="extraJson">扩展字段</param>
        public virtual string LoadNoticeData(OnFinish<MSDKNoticeRet> callback, string noticeGroup, string language, int region, string partition, string extra)
        {
            return "";
        }

        /// <summary>
        /// 游戏可以针对用户设置标签，如性别、年龄、学历、爱好等，推送时可根据不同的标签有针对的进行推送。另外说明，XG SDK 有默认标签，详情可咨询 TPNS_helper(腾讯移动推送助手)。
        ///发送标签推送消息同发送推送消息一样，只是在添加推送消息时发送人群范围需选择个性化推送。
        /// 设置标签时注意标签中不可包含空格
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Push.html"/>
        /// </summary>
        /// <param name="channel">三方渠道专有名词，比如 "XG", "Firebase",默认为XG</param>
        /// <param name="tag">标签，不能为 null 或包含空格</param>
        public virtual void SetTag(string tag, string channel = "")
        {

        }
        public virtual void DeleteTag(string tag, string channel = "")
        {

        }
        /// <summary>
        /// 注册指定渠道推送，可以接收这个渠道的消息推送。在绑定设备注册的基础上，同时可以绑定账号，使用指定的账号，这样可以通过后台向指定的账号发送推送消息
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Push.html"/>
        /// </summary>
        /// <param name="callback">接受远程推送回调</param>
        /// <param name="channel">三方渠道专有名词，比如 "XG", "Firebase",默认为XG </param>
        /// <param name="account">绑定的账号，绑定后可以针对账号发送推送消息，account不能为单个字符如“2”，“a”,msdk的openid</param>

        public virtual void RegisterPush(OnFinish<RemoteNotification> callback = null, string account = "", string channel = "")
        {

        }
        /// <summary>
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Report.html"/>
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="paramsDic">事件所需参数</param>
        /// <param name="spChannels">指定单个渠道，若无可填空字符表示上报全部渠道</param>
        /// <param name="isRealTime">是否实时上报</param>
        public virtual void ReportEvent(string eventName, Dictionary<string, string> paramsDic, string spChannels = "", bool isRealTime = true)
        {

        }
        /// <summary>
        /// 自定义日志打印接口，用于记录一些关键的业务调试信息，可以更全面地反映 APP 发生崩溃或异常的上下文环境。
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Crash.html"/>
        /// </summary>
        /// <param name="level">日志级别，0-silent，1-error，2-warning，3-inifo，4-debug，5-verbose</param>
        /// <param name="tag">日志模块分类</param>
        /// <param name="log">日志内容</param>
        public virtual void LogInfo(MSDKCrashLevel level, string tag, string log)
        {

        }
        /// <summary>
        /// 
        /// 设置关键数据键值对，随崩溃信息上报。最多允许设置 9 对键值。
        ///  <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Crash.html"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void SetUserValue(string key, string value)
        {

        }
        /// <summary>
        /// 支持自定义设置用户 ID，您可能会希望能精确定位到某个用户的异常，通过该接口记录用户 ID，在页面上可以精确定位到每个用户发生 Crash 的情况
        /// <see cref="https://docs.msdk.qq.com/v5/zh-CN/Module/Crash.html"/>
        /// </summary>
        /// <param name="userId">用户 ID</param>
        public virtual void SetUserId(string userId)
        {
        }
        /// <summary>
        /// 设置崩溃回调
        /// </summary>
        /// <param name="crashCallback"></param>
        public virtual void SetCrashCallback(CrashCallback crashCallback)
        {

        }

    }
}
