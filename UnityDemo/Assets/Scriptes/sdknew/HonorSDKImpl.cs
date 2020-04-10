using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSdk
{


    public class HonorSDKImpl : SDKManager,IHonorApi
    {

        public enum ePlat
        {
            none,
            Android,
            Ios,
        }

        /// <summary>
        /// 资源文件路径
        /// </summary>
        public static string RES_FILE_PATH;

        private static HonorSDKImpl instance;
        private  IHonorApi instanceImpl;

        new protected HonorMessageHandler handler;

        protected HonorSDKImpl()
        {
        }

        public static void CreateInstance(ePlat plant)
        {
            if (instance == null)
            {
                instance = new HonorSDKImpl();
                instance.handler = new HonorMessageHandler(instance);

            }
            if (plant == ePlat.Android)
                instance.instanceImpl = new CustomAndroidSdkImpl();
            else if (plant == ePlat.Ios)
                instance.instanceImpl = new CustomIosSdkImpl();
            else
                instance.instanceImpl = new NoSdkImpl();
        }

        new public static HonorSDKImpl GetInstance()
        {
            return instance;
        }
     

        public const string CONFIG_UNITY_ID = "unity_id";
 
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="step">步骤</param>
        /// <param name="fps">游戏帧率</param>
        /// <param name="type">步骤类型</param>
        public virtual void GameStepInfo(string step, string type = "")
        {
            instance.instanceImpl.GameStepInfo(step, type);
        }

        /// <summary>
        /// 获取认证信息，用户登陆成功后调用
        /// </summary>
        /// <returns></returns>
        public virtual string GetAuthInfo()
        {
            return instance.instanceImpl.GetAuthInfo();
        }

     

        /// <summary>
        /// 返回资源文件存储路径，应用卸载后该目录自动被删除
        /// </summary>
        public virtual string GetResFilePath()
        {
            return instance.instanceImpl.GetResFilePath();
        }





        /// <summary>
        /// 游戏资源路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetGameResUrl()
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            return "";
        }

     

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="srcContent">待翻译文本内容</param>
        /// <param name="targetLan">目标语言码</param>
        /// <param name="translateContentListener">成功返回翻译后文本，失败返回原文本</param>
        public virtual void TranslateContent(string srcContent, string targetLan, int id, OnFinish<ResultTranslate> translateContentListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            this.translateContentListener = translateContentListener;
        }

        /// <summary>
        /// 获取cpu gpu信息
        /// </summary>
        /// <param name="getHardwareInfoListener">返回cpu gpu信息<see cref="HardwareInfo"/></param>
        public virtual void GetHardwareInfo(OnFinish<HardwareInfo> getHardwareInfoListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            this.getHardwareListener = getHardwareInfoListener;

        }
        /// <summary>
        /// 远程推送,如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="ip">远程推送服务器ip</param>
        /// <param name="port">远程推送服务器端口</param>
        /// <param name="gameRoleId">角色id</param>
        public virtual void UdpPush(string ip, string port, string gameRoleId)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }

        /// <summary>
        /// 开始录音，如需使用该功能需单独申请参数
        /// </summary>
        /// <param name="serverURL">录音完毕后语音存储服务器url</param>
        /// <param name="bit">语音录音品质，低品质1，中品质2，高品质3</param>
        /// <param name="recordMaxTime">录音最大时长，超过该时长自动结束录音,单位毫秒</param>
        public virtual void StartRecordVideo(string serverURL, string bit, long recordMaxTime)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 结束录音
        /// </summary>
        /// <param name="stopRecordVideoListener">返回录音结果<see cref="ResultVideoRecord"/></param>
        public virtual void StopRecordVideo(OnFinish<ResultVideoRecord> stopRecordVideoListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="videoUrl">录音存放url</param>
        /// <param name="playVideoListener">返回播放录音结果<see cref="Result"/></param>
        public virtual void PlayVideo(string videoUrl, OnFinish<Result> playVideoListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 获取热更信息
        /// </summary>
        /// <param name="rootDir">热更目录</param>
        /// <param name="getDynamicUpdateListener">返回热更信息<see cref="ResultGetDynamic"/></param>
        public virtual void GetDynamicUpdate(string rootDir, OnFinish<ResultGetDynamic> getDynamicUpdateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 下载热更
        /// </summary>
        /// <param name="downDynamicUpdateListener">返回下载热更结果<see cref="ResultDownload"/></param>
        public virtual void DownDynamicUpdate(OnFinish<ResultDownload> downDynamicUpdateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }

        /// <summary>
        /// 删除所有热更资源
        /// </summary>
        public virtual void RepairUpdateRes()
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 获取强更信息
        /// </summary>
        /// <param name="getForceUpdateListener">返回强更信息<see cref="ResultGetForce"/></param>
        public virtual void GetForceUpdate(OnFinish<ResultGetForce> getForceUpdateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }

        /// <summary>
        /// 强更下载
        /// </summary>
        /// <param name="downForceUpdateListener">返回强更下载结果<see cref="ResultDownload"/></param>
        public virtual void DownForceUpdate(OnFinish<ResultDownload> downForceUpdateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// 是否需要obb下载，Android包上架google商店且包体大于100M才需要使用到obb下载功能
        /// </summary>
        /// <returns>true必须要下载，false需要下载</returns>
        public virtual bool HasObbUpdate()
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
            return false;
        }
        /// <summary>
        /// 下载obb
        /// </summary>
        /// <param name="downObbUpdateListener">返回obb下载结果<see cref="ResultObbDownload"/></param>
        public virtual void DownObbUpdate(OnFinish<ResultObbDownload> downObbUpdateListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// obb下载中断时调用此方法继续下载
        /// </summary>
        public virtual void ContinueUpdateObb()
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
        }
        /// <summary>
        /// obb下载完成后重新加载obb
        /// </summary>
        public virtual void ReloadObb()
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);

        }
        /// <summary>
        /// 获取ABTest
        /// </summary>
        /// <param name="getABTestVerListener">返回获取ABTest<see cref="ResultGetABTestVer"/></param>
        public virtual void GetABTestVer(OnFinish<ResultGetABTestVer> getABTestVerListener)
        {
            handler.RegisterCallback(Api.GET_HEADSET_STATE, getHeadsetStateListener);
            instanceImpl.GetHeadsetState(notifyWhenHeadsetChanged, getHeadsetStateListener);
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
                JSONNode node = JSONNode.Parse(result.originResult);
                isSupportAlertAgreementListener(node["isSupportAlertAgreement"].AsBool);
            });

        }
     

        const string HEAD_GET_CPU_TYPE = "get_cpu_type";
        const string FUNCTION_GET_CPU_TYPET = HEAD_GET_CPU_TYPE;

        public enum CPUArchitecture
        {
            ARCH_UNKNOWN = 0,
            ARCH_X86 = 1,
            ARCH_ARMV7 = 2,
            ARCH_ARMV64 = 3,
        }

        /// <summary>
        /// 获取CPU类型 ，类型说明<see cref="CPUArchitecture"/>
        /// </summary>
        /// <param name="getGetCpuTypeListener">回调CPU类型</param>
        public virtual void GetCpuType(OnFinish<int> getGetCpuTypeListener)
        {
            ExpandFunction(FUNCTION_GET_CPU_TYPET, "", FUNCTION_GET_CPU_TYPET, delegate (ResultExpand result)
            {
                JSONNode node = JSONNode.Parse(result.originResult);
                getGetCpuTypeListener(node["cpuType"].AsInt);
            });

        }

        const string HEAD_GET_SO_RELATIVE_PATH = "get_so_relative_path";
        const string FUNCTION_GET_SO_RELATIVE_PATH = HEAD_GET_SO_RELATIVE_PATH;
        /// <summary>
        /// 获取so的相对路径 例如comlibs/x86/
        /// </summary>
        /// <param name="getSoRelativePathListener">回调so的相对路径</param>
        public virtual void GetSoRelativePath(OnFinish<string> getSoRelativePathListener)
        {
            ExpandFunction(HEAD_GET_SO_RELATIVE_PATH, "", HEAD_GET_SO_RELATIVE_PATH, delegate (ResultExpand result)
            {
                JSONNode node = JSONNode.Parse(result.originResult);
                getSoRelativePathListener(node["soRelativePath"].Value);
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
        public virtual void RegisterIdentifyListener(OnFinish<IdentifyInfo> identifyListener)
        {
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
        public virtual void AlertIdentify()
        {
            ExpandFunction(FUNCTION_ALERT_IDENTIFY);
        }


        const string FUNCTION_REGISTER_UNREAD_MESSAGE_COUNT = "register_unread_message_count";

        const string HEAD_REGISTER_UNREAD_MESSAGE_COUNT = FUNCTION_REGISTER_UNREAD_MESSAGE_COUNT;
        /// <summary>
        //注册接受新消息到达监听,回调未读消息条数
        /// </summary>
        public virtual void AiHelpRegisterUnreadMessageCountListener(OnFinish<int> registerUnreadMessageCountListener)
        {
            ExpandFunction(FUNCTION_REGISTER_UNREAD_MESSAGE_COUNT, "", FUNCTION_REGISTER_UNREAD_MESSAGE_COUNT, delegate (ResultExpand result)
            {
                JSONNode node = JSONNode.Parse(result.originResult);
                registerUnreadMessageCountListener(node["unreadMessageCount"].AsInt);
            });
        }

        const string FUNCTION_OPEN_BROWSER = "open_browser";

        public virtual void OpenBrowser(string url)
        {
            JSONClass json = new JSONClass();
            json.Add("url", new JSONData(url));
            ExpandFunction(FUNCTION_OPEN_BROWSER, json.ToString());
        }


        private string createTagJsonString(List<string> tags1, Dictionary<string, string> tags2, string serverId)
        {
            JSONClass json = new JSONClass();
            json.Add("serverId", new JSONData(serverId));
            JSONArray jsonArray = new JSONArray();
            if (tags1 != null)
            {
                foreach (string v in tags1)
                {
                    jsonArray.Add(new JSONData(v));
                }
            }
            json.Add("tags1", jsonArray);
            JSONClass jsonTags2 = new JSONClass();
            if (tags2 != null)
            {
                foreach (KeyValuePair<string, string> item in tags2)
                {
                    jsonTags2.Add(item.Key, new JSONData(item.Value));
                }
            }
            json.Add("tags2", jsonTags2);
            return json.ToString();
        }


        const string FUNCTION_AI_HELP_SHOW_CONVERSATION = "aihelp_show_conversation";
        /// <summary>
        /// 显示人工客服
        /// </summary>
        /// <param name="tags1">分类，所传递的标签需要和客服后台保持一致</param>
        /// <param name="tags2">分类，游戏通过key-value形式自定义标签</param>
        public virtual void AiHelpShowConversation(string serverId, List<string> tags1, Dictionary<string, string> tags2)
        {
            ExpandFunction(FUNCTION_AI_HELP_SHOW_CONVERSATION, createTagJsonString(tags1, tags2, serverId));
        }

        const string FUNCTION_AI_HELP_SHOW_FAQS = "aihelp_showFAQs";
        /// <summary>
        /// 显示帮助页面
        /// </summary>
        /// <param name="tags1">分类，所传递的标签需要和客服后台保持一致</param>
        /// <param name="tags2">分类，游戏通过key-value形式自定义标签</param>
        public virtual void AiHelpShowFAQs(List<string> tags1, Dictionary<string, string> tags2)
        {

            ExpandFunction(FUNCTION_AI_HELP_SHOW_FAQS, createTagJsonString(tags1, tags2, ""));
        }
        const string FUNCTION_AI_HELP_SHOW_ELVA = "aihelp_showElva";
        /// <summary>
        /// 显示机器人客服页面
        /// </summary>
        /// <param name="sererId"></param>
        /// <param name="tags1">分类，所传递的标签需要和客服后台保持一致</param>
        /// <param name="tags2">分类，游戏通过key-value形式自定义标签</param>
        public virtual void AiHelpShowElva(string sererId, List<string> tags1, Dictionary<string, string> tags2)
        {

            ExpandFunction(FUNCTION_AI_HELP_SHOW_ELVA, createTagJsonString(tags1, tags2, sererId));
        }
        const string FUNCTION_OPEN_GOOGLE_PLAY_COMMENTS = "open_google_play_comments";
        /// <summary>
        /// 打开应用市场评论
        /// </summary>
        public virtual void OpenMarketComments()
        {
            ExpandFunction(FUNCTION_OPEN_GOOGLE_PLAY_COMMENTS);
        }

        public void Init(HonorSDKGameObject gameObject, OnFinish<HonorResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            
        }
    }
}
