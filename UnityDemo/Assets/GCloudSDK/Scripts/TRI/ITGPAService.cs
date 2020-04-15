// ------------------------------------------------------------------------
// 
// File: ITGPAService
// Module: TGPA
// Version: 1.0
// Author: zohnzliu
// Modifyed: 2019/06/10
// 
// ------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCloud.TGPA
{

    //----------------------- TGPA Callback -------------------------------
    #region TGPA Callback
#if UNITY_ANDROID
    public abstract class ITGPACallback : AndroidJavaProxy
    {
        public ITGPACallback () : base ("com.tencent.kgvmp.VmpCallback") { }

        /// <summary> 回传系统返回的数据，如降频信号等
        /// <para>数据示例：{"1":"2","4":"2"}</para>
        /// <para>具体数据利用方式，请参见TGPA接入文档</para>
        /// </summary>
        /// <param name="json">手机系统返回的数据</param>
        public abstract void notifySystemInfo (string json);
    }
#else
    public abstract class ITGPACallback
    {
        public abstract void notifySystemInfo (string json);
    }
#endif
    #endregion

    //----------------------- TGPA Game Data Key --------------------------
    #region TGPA Game Data Key
    public enum GameDataKey
    {
        OpenID = 0, // openid
        MainVersion = 1, // 游戏的主版本号
        SubVersion = 2, // 游戏的资源版本号
        TimeStamp = 3, // 时间戳, deprecated
        Scene = 4, // 游戏侧需要保障的游戏场景
        FPS = 5, // 游戏的FPS
        FrameMiss = 6, // 游戏丢帧数
        FPSTarget = 7, // 游戏的相应地图限制的最高帧率
        PictureQuality = 8, // 游戏的画面质量
        EffectQuality = 9, // 游戏的特效质量
        Resolution = 10, // 游戏的分辨率
        RoleCount = 11, // 游戏的同屏人数
        NetDelay = 12, // 游戏的网络延迟
        Recording = 13, // 录屏状态
        UrgentSignal = 14, // 紧急信号, deprecated
        ServerIP = 15, // 服务器IP
        RoleStatus = 16, // 角色状态
        SceneType = 40, // 场景类型, 标记游戏模式
        LoadTrunk = 41, // 局部地图加载
        BloomArea = 42, // 吃鸡模式的轰炸区标记
        MTR = 43, // 多核多线程渲染开启状态
        KillReport = 44, // 游戏的杀人数播报类型
        LightThreadTid = 50, // 游戏的轻负载线程
        HeavyThreadTid = 51, // 游戏的重负载线程
        RoleOutline = 52, // 角色描边状态
        PictureStyle = 53, // 画面风格，鲜艳、写实等
        AntiAliasing = 54, // 抗锯齿是否开启
        ServerPort = 55, // 服务器地址端口
        SocketType = 56, // 数据包类型
        Shadow = 57 // 阴影是否开启
    }
    #endregion

    //----------------------- TGPA Public Interface -----------------------
    #region TGPA Public Interface
    public interface ITGPAService
    {
        int GetVersionCode ();

        /// <summary> 是否打开调试日志，需要在初始化之前调用
        /// </summary>
        /// <param name="enable">true: 打开；false：关闭</param>
        void EnableLog (bool enable);

        /// <summary> 初始化SDK，调用功能接口前必须调用, 需要在登录前，msdk初始化后进行调用 </summary>
        void Init ();

        /// <summary> 注册回调，用于接收厂商传递过来的数据信息</summary>
        void RegisterCallback (ITGPACallback callback);

        /// <summary> 发送游戏的FPS数据，需要每s统计并调用</summary>
        void UpdateGameFps (float value);

        /// <summary> 发送游戏的信息用于性能调优</summary>
        void UpdateGameInfo (GameDataKey key, string value);

        /// <summary> 发送游戏的信息用于性能调优</summary>
        void UpdateGameInfo (GameDataKey key, int value);

        /// <summary> 批量发送游戏的信息，场景不可使用此接口发送</summary>
        void UpdateGameInfo (Dictionary<GameDataKey, string> dict);

        /// <summary> 发送游戏的信息用于TGPA上报</summary>
        void UpdateGameInfo (string key, string value);

        /// <summary>
        /// 用于处理预下载上报相关逻辑
        /// </summary>
        /// <param name="key"></param>
        /// "PreDownload";
        /// "DeviceBind"
        /// <param name="dict"></param>
        void UpdateGameInfo (string key, Dictionary<string, string> dict);

        /// <summary>
        ///  从TGPA侧获取数据
        /// </summary>
        /// <param name="key"></param>
        /// key为"GetPredownPath"时，表示获取对应文件的预下载目录
        /// key为"CheckDevice"时，表示使用真机鉴定功能，实际调用的也是CheckDeviceIsReal接口
        /// key为"GetOptCfg"时，表示使用配置推荐功能，实际调用的也是GetOptCfgStr接口
        /// <param name="value"></param>
        /// key为"GetPredownPath"时，value为更新文件名
        /// key为"CheckDevice"时，表示使用真机鉴定功能，实际调用的也是CheckDeviceIsReal接口
        /// key为"GetOptCfg"时，表示使用配置推荐功能，实际调用的也是GetOptCfgStr接口
        /// <returns></returns>
        string GetDataFromTGPA (string key, string value);

        /// <summary> 检测当前设备是否是真机，
        /// 如需使用此功能，请联系zohnzliu
        /// </summary>
        /// <returns> 真机：{"result":0} </returns>
        /// <returns> 伪造：{"result":1} </returns>
        string CheckDeviceIsReal ();

        /// <summary> 获取当前调用线程的Tid </summary>
        /// <returns> Android平台返回线程的tid </returns>
        /// <returns> 获取失败或非Android平台返回: -1 </returns>
        int GetCurrentThreadTid ();

        /// <summary> 推荐配置获取接口 </summary>
        /// <returns>
        /// 异常返回: "-1"
        /// 正常返回: "111", 对应游戏的性能设置选项
        /// </returns>
        string GetOptCfgStr ();

        /// <summary> 账号登录或切换后登录时需要调用此接口上报用户数据 
        /// Dictionary中key需要包含以下key数据：
        /// 1.角色名称：roleid
        /// 2.玩家本次登录的openid: openid
        /// 3.小区/服务器id：areaid
        /// 4.应用appid: appid
        /// 5.平台platid: Wechat/QQ
        /// </summary>
        void ReportUserInfo (Dictionary<string, string> dict);
    }
    #endregion
}