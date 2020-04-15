
using System.Collections.Generic;

namespace GCloud.GPM
{
    public delegate void GPMDeviceLevelEventHandle(int deviceclass);
 
    public interface IGPMService
    {
        /// <summary>
        /// 获取SDK的版本信息
        /// </summary>
        /// <returns>SDK版本信息</returns>
        string GetSDKVersion();

        /// <summary>
        ///  初始化环境，switches用于控制
        ///  InitContext("12345678", TAPM_GEM | TGPA);
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="switches">SDK开关控制,或操作</param>
        /// <returns>状态码</returns>
        int InitContext(string appId, bool debug);

        /// <summary>
        /// 设置OpenId
        /// </summary>
        /// <param name="openId">openid信息</param>
        void SetOpenId(string openId);

        /// <summary>
        /// 设置游戏资源版本号
        /// </summary>
        /// <param name="version">游戏资源版本号</param>
        void SetResourceVersion(string version);

        /// <summary>
        /// 标记场景开始
        /// </summary>
        /// <param name="sceneName">场景名</param>
        void MarkLevelLoad(string sceneName);

        /// <summary>
        /// 标价场景加载结束
        /// </summary>
        void MarkLevelLoadCompleted();

        /// <summary>
        /// 标记场景结束
        /// </summary>
        void MarkLevelFin();

        /// <summary>
        /// 设置服务器信息
        /// </summary>
        /// <param name="zoneId">区服信息</param>
        /// <param name="roomIp">房间信息</param>
        void SetServerInfo(string zoneId, string roomIp);

        /// <summary>
        /// 场景内区域标记
        /// </summary>
        /// <param name="tagName">区域名</param>
        void BeginTag(string tagName);

        /// <summary>
        /// 结束区域标记
        /// </summary>
        void EndTag();

        

        /// <summary>
        /// 玩家染色事件
        /// </summary>
        /// <param name="key">事件键</param>
        /// <param name="value">事件值</param>
        void PostEvent(int key, string value);



        /// <summary>
        /// 自定义上报接口
        /// </summary>
        /// <param name="eventName">上报事件名称</param>
        /// <param name="eventParams">上报事件内容</param>
        void ReportEvent(string eventName, Dictionary<string, string> eventParams);


        /// <summary>
        /// 获取机型分档
        /// </summary>
        /// <param name="domainName">配置域名</param>
        void CheckDeviceClass(string domainName);

        /// <summary>
        /// 异步获取机型分档
        /// </summary>
        /// <param name="domainName">配置域名</param>
        /// <param name="handler">回调handler</param>
        void CheckDeviceClassAsync(string domainName, GPMDeviceLevelEventHandle handler);

        /// <summary>
        /// 开始区域剔除，MMO游戏可以用于剔除挂机
        /// </summary>
        void BeginExclude();

        /// <summary>
        /// 结束区域剔除
        /// </summary>
        void EndExclude();

        /// <summary>
        /// 设定场景画质
        /// </summary>
        /// <param name="sceneQuality">画质值</param>
        void SetSceneQuality(int sceneQuality);

        /// <summary>
        /// 设置机型分档
        /// 后台会按照设定的机型分档做数据聚合
        /// NOTE： 机型分档值需大于等于1
        /// </summary>
        /// <param name="deviceLevel">机型分档值</param>
        void SetDeviceLevel(int deviceLevel);

        /// <summary>
        /// 网络探测接口，游戏发生超时卡顿时调用
        /// </summary>
        void DetectInTimeout();

        /// <summary>
        /// 上报游戏逻辑网络延时
        /// </summary>
        /// <param name="mills">网络延时值</param>
        void PostNetworkLatency(int mills);
        

        ///// <summary>
        ///// 标记上传回扯距离
        ///// </summary>
        ///// <param name="distance">回扯距离</param>
        //void PostLagStatus(float distance);

        #region PostValueUtils
        /// <summary>
        /// 自定义数据系列函数
        /// 支持自由扩展
        /// </summary>
        void BeginTupleWrap(string tupleName);
        void EndTupleWrap();

        void PostValueF(string category, string key, float a);
        void PostValueF(string category, string key, float a, float b);
        void PostValueF(string category, string key, float a, float b, float c);

        void PostValueI(string category, string key, int a);
        void PostValueI(string category, string key, int a, int b);
        void PostValueI(string category, string key, int a, int b, int c);
        void PostValueS(string category, string key, string value);
        #endregion

        /// <summary>
        /// 关键路径转化
        /// </summary>
        /// <param name="category">路径定义名</param>
        /// <param name="stepId">路径步骤</param>
        /// <param name="status">路径状态</param>
        /// <param name="code">路径状态值</param>
        /// <param name="msg">信息</param>
        /// <param name="extraKey">统计关键域信息</param>
        void PostStepEvent(string category, int stepId, int status, int code, string msg, string extraKey , bool authorize, bool finish);


        ///// <summary>
        ///// 设置支付过程事件统计
        ///// </summary>
        ///// <param name="id">物品id</param>
        ///// <param name="tag">事件场景（游戏自定义）</param>
        ///// <param name="status">此次事件操作的结果状态</param>
        ///// <param name="msg">此次事件返回的信息</param>
        //void SetPayEvent(int id, int tag, bool status, string msg);

        //#region TGPAUtils

        ///// <summary>
        ///// 设备状态回调接口
        ///// </summary>
        ///// <param name="callback"></param>
        //void RegisterDeviceCallback(IDeviceCallback callback);
        ///// <summary>
        ///// 游戏给sdk发数据
        ///// </summary>
        ///// <param name="key">键名</param>
        ///// <param name="value">值名</param>
        //void UpdateGameInfo(GameDataKey key, string value);
        //void UpdateGameInfo(GameDataKey key, int value);
        //void UpdateGameInfo(Dictionary<GameDataKey, string> dict);
        //void UpdateGameInfo(string key, string value);
        //void UpdateGameInfo(string key, Dictionary<string, string> dict);

        ///// <summary>
        ///// 游戏从TGPA获取信息
        ///// </summary>
        ///// <param name="key">键名</param>
        ///// <param name="value">值名</param>
        ///// <returns></returns>
        //string GetDataFromTGPA(string key, string value);
        //#endregion


        void LinkLastStepEventSession(string eventName);

        void InitStepEventContext();

        void ReleaseStepEventContext();


    }
};
