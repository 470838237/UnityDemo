
using System.Collections.Generic;

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GCloud.GPM
{
    public class GPMAgent
    {

        private static GameObject sCallbackGameObject = null;


        public delegate void GPMOnMarkLevelLoadObserver(string sceneId);
        private static event GPMOnMarkLevelLoadObserver sMarkLevelLoadNotifyEvent;
		public delegate void GPMQualityObserver(string quality);
		private static event GPMQualityObserver sQualityNotifyEvent;
		public delegate void GPMLogObserver(int errCode, string errMsg, string extMsg);
		private static event GPMLogObserver sLogNotifyEvent;



#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject sGPMObj = null;
        private static AndroidJavaClass sGPMPlatformClass = null;

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostFrame(int frametime);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostCoordinate(float x, float y, float z, float pitch, float yaw, float roll);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostNTL(int latency);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1F(string category, string key, float a);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV2F(string category, string key, float a, float b);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV3F(string category, string key, float a, float b, float c);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1I(string category, string key, int a);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV2I(string category, string key, int a, int b);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV3I(string category, string key, int a, int b, int c);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1S(string category, string key, string value);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativeBeginTupleWrap(string key);

        [DllImport("GPM", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativeEndTupleWrap();


#elif UNITY_IOS && !UNITY_EDITOR
                [DllImport("__Internal")]
        private static extern  int gpm_initContext(string appId, string engine, bool debug);
        [DllImport("__Internal")]
        private static extern  void gpm_enableDebugMode();
        [DllImport("__Internal")]
        private static extern  void gpm_setServerInfo(string zoneId, string roomIp);
        [DllImport("__Internal")]
        private static extern  void gpm_markLevelLoad(string sceneId);
        [DllImport("__Internal")]
        private static extern  void gpm_markLevelFin();
        [DllImport("__Internal")]
        private static extern  void gpm_markLevelLoadCompleted();
        [DllImport("__Internal")]
        private static extern  void gpm_setOpenId(string openId);
        [DllImport("__Internal")]
        private static extern  void gpm_setQulaity(int quality);
        [DllImport("__Internal")]
        private static extern  void gpm_postStepEvent(string eventCategory, int stepId, int status, int code, string msg, string extraKey, bool authorize, bool finish);
        [DllImport("__Internal")]
        private static extern  void gpm_detectInTimeout();
        [DllImport("__Internal")]
        private static extern  void gpm_postTrackState(float x, float y, float z, float pitch, float yaw, float roll);
        [DllImport("__Internal")]
        private static extern  int gpm_checkDCLSByQcc(string absolutePath,string configName);
        [DllImport("__Internal")]
        private static extern  int gpm_checkDCLSByQccSync(string absolutePath,string configName);
        [DllImport("__Internal")]
        private static extern  void gpm_postFrame(float deltaTime);
        [DllImport("__Internal")]
        private static extern  void gpm_postNetLatency(int latency);
        [DllImport("__Internal")]
        private static extern  void gpm_beginTupleWrap(string category);
        [DllImport("__Internal")]
        private static extern void gpm_endTupleWrap();
        [DllImport("__Internal")]
        private static extern void gpm_postValueF1(string category, string key, float a);
        [DllImport("__Internal")]
        private static extern  void gpm_postValueF2(string category, string key, float a, float b) ;
        [DllImport("__Internal")]
        private static extern  void gpm_postValueF3(string category, string key, float a, float b, float c);
        [DllImport("__Internal")]
        private static extern void gpm_postValueI1(string category, string key, int a);
        [DllImport("__Internal")]
        private static extern void gpm_postValueI2(string category, string key, int a, int b);
        [DllImport("__Internal")]
        private static extern void gpm_postValueI3(string category, string key, int a, int b, int c);
        [DllImport("__Internal")]
        private static extern void gpm_postValueS(string category, string key, string value);
        [DllImport("__Internal")]
        private static extern void gpm_setDefinedDeviceClass(int deviceClass);
        [DllImport("__Internal")]
        private static extern void gpm_beginTag(string tagName);
        [DllImport("__Internal")]
        private static extern void gpm_endTag();
        [DllImport("__Internal")]
        private static extern void gpm_setVersionIden(string versionName);
        [DllImport("__Internal")]
        private static extern void gpm_beignExclude();
        [DllImport("__Internal")]
        private static extern void gpm_endExclude();
        [DllImport("__Internal")]
        private static extern string gpm_getErrorMsg(int errorCode);
        [DllImport("__Internal")]
        private static extern void gpm_linkSession(string eventName);
        [DllImport("__Internal")]
        private static extern void gpm_initStepEventContext();
        [DllImport("__Internal")]
        private static extern void gpm_releaseStepEventContext();
        [DllImport("__Internal")]
        private static extern void gpm_postEventIS(int key,string value);
        [DllImport("__Internal")]
        private static extern void gpm_postEventSS(string key,string value);
        [DllImport("__Internal")]
        private static extern string gpm_getSDKVersion();
        [DllImport("__Internal")]
        private static extern void gpm_log(string log);

        [DllImport("__Internal")]
        private static extern void MySetGPMObserver();
#endif
        /// <summary>
        /// 获取SDK的版本信息
        /// </summary>
        /// <returns>SDK版本信息</returns>
        public static string GetSDKVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
                return gpm_getSDKVersion();
#elif UNITY_ANDROID && !UNITY_EDITOR
             if (sGPMObj == null)
            {
                return "error";
            }

            return sGPMObj.Call<string>("getSDKVersion");
#endif
            return null;
        }

        /// <summary>
        ///  初始化环境，switches用于控制
        ///  InitContext("12345678", false);
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="debug">debug模式</param>
        /// <returns>状态码</returns>
        public static int InitContext(string appId, bool debug)
        {
            TGPA.TGPAHelper.EnableLog(debug);
            TGPA.TGPAHelper.Init();

#if UNITY_IOS && !UNITY_EDITOR
            int initRetValue =  gpm_initContext(appId, "unity", debug);
			
			GameObject tapmGameObject = new GameObject("TApmGameObject");
            GameObject.DontDestroyOnLoad(tapmGameObject);
            tapmGameObject.AddComponent<FrameProcessor>();
			
            MySetGPMObserver();
            return initRetValue;
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJNI.ExceptionClear();
            if(sGPMObj != null)
            {
                return -1;
            }
            sGPMObj = new AndroidJavaObject("com.tencent.gcloud.gpm.portal.GPMAgent");
            if(sGPMObj == null)
            {
                return -2;
            }
          
            AndroidJavaObject unityContext = null;
            using (AndroidJavaClass c =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                unityContext = c.GetStatic<AndroidJavaObject>("currentActivity");
            }

            if(unityContext == null)
            {
                return -3;
            }
            
            sGPMObj.Call<int>("initContext", new object[] { unityContext, appId, "unity", debug });
           
            GameObject tapmGameObject = new GameObject("TApmGameObject");

            GameObject.DontDestroyOnLoad(tapmGameObject);

            tapmGameObject.AddComponent<FrameProcessor>();

#endif


            return 0;
        }

        /// <summary>
        /// 设置OpenId
        /// </summary>
        /// <param name="openId">openid信息</param>
        public static void SetOpenId(string openId)
        {
#if UNITY_IOS && !UNITY_EDITOR
             gpm_setOpenId(openId);
#elif UNITY_ANDROID && !UNITY_EDITOR
            if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("setOpenId", new object[] { openId });
#endif
        }

        /// <summary>
        /// 设置游戏资源版本号
        /// </summary>
        /// <param name="version">游戏资源版本号</param>
        public static void SetResourceVersion(string version)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_setVersionIden(version);
#elif UNITY_ANDROID && !UNITY_EDITOR
             if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("setVersionIden", new object[] { version });
#endif
        }

        /// <summary>
        /// 标记场景开始
        /// </summary>
        /// <param name="sceneName">场景名</param>
        public static void MarkLevelLoad(string sceneName)
        {
            if (sCallbackGameObject == null) 
			{
                sCallbackGameObject = new GameObject ("UnityGPMCallBackGameObejct");
				sCallbackGameObject.AddComponent<GPMCallBackComponent> ();
				GameObject.DontDestroyOnLoad (sCallbackGameObject);
			}


#if UNITY_IOS && !UNITY_EDITOR
            gpm_markLevelLoad(sceneName);
#elif UNITY_ANDROID && !UNITY_EDITOR
              if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("markLevelLoad", new object[] { sceneName });
#endif
        }

        /// <summary>
        /// 标价场景加载结束
        /// </summary>
        public static void MarkLevelLoadCompleted()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_markLevelLoadCompleted();
#elif UNITY_ANDROID && !UNITY_EDITOR
             if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("markLevelLoadCompleted");
#endif
        }

        /// <summary>
        /// 标记场景结束
        /// </summary>
        public static void MarkLevelFin()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_markLevelFin(); 
#elif UNITY_ANDROID && !UNITY_EDITOR
                if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("markLevelFin");
#endif
        }

        /// <summary>
        /// 设置服务器信息
        /// </summary>
        /// <param name="zoneId">区服信息</param>
        /// <param name="roomIp">房间信息</param>
        public static void SetServerInfo(string zoneId, string roomIp)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_setServerInfo(zoneId, roomIp);
#elif UNITY_ANDROID && !UNITY_EDITOR
               if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("setServerInfo", new object[] { zoneId, roomIp });
#endif
        }

        /// <summary>
        /// 场景内区域标记
        /// </summary>
        /// <param name="tagName">区域名</param>
        public static void BeginTag(string tagName)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_beginTag(tagName);
#elif UNITY_ANDROID && !UNITY_EDITOR
                if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("beginTag", new object[] { tagName });
#endif
        }

        /// <summary>
        /// 结束区域标记
        /// </summary>
        public static void EndTag()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_endTag();
#elif UNITY_ANDROID && !UNITY_EDITOR
            if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("endTag");
#endif
        }

        public static void PostFrame()
        {
#if UNITY_IOS && !UNITY_EDITOR
         gpm_postFrame(Time.deltaTime);
#elif UNITY_ANDROID && !UNITY_EDITOR
            if(sGPMObj == null)
            {
                return;
            }
            tapmNativePostFrame((int)(Time.deltaTime * 100));
            
#endif
        }



        /// <summary>
        /// 玩家染色事件
        /// </summary>
        /// <param name="key">事件键</param>
        /// <param name="value">事件值</param>
        public static void PostEvent(int key, string value)
        {
#if UNITY_IOS && !UNITY_EDITOR
             gpm_postEventIS(key,value);
#elif UNITY_ANDROID && !UNITY_EDITOR
            if(sGPMObj == null)
            {
                return;
            }
            sGPMObj.Call("postEvent", new object[] { key, value });
#endif
        }



        /// <summary>
        /// 自定义上报接口
        /// </summary>
        /// <param name="eventName">上报事件名称</param>
        /// <param name="eventList">上报事件内容</param>
        public static void ReportEvent(string eventName, Dictionary<string, string> eventList)
        {
#if UNITY_IOS && !UNITY_EDITOR

            string jsonString = "";
            foreach (string key in eventList.Keys)
            {
                jsonString = jsonString + key + ":" + eventList[key] + ";";
            }
            gpm_postEventSS (eventName, jsonString);

#elif UNITY_ANDROID && !UNITY_EDITOR

            if(sGPMObj == null)
            {
                return;
            }
            
            if (eventList == null || eventName == null) 
            {
                return;
            }

            AndroidJNI.ExceptionClear();
                StringBuilder eventParamsBuilder = new StringBuilder("{");
                foreach (KeyValuePair<string, string> eventParam in eventList)
                {
                    if (!eventParam.Equals(default(KeyValuePair<string, string>)))
                    {
                        // NOTE: .NET 4.8文档指出, Append null时StringBuilder不会有更改
                        eventParamsBuilder
                        .Append('\"').Append(eventParam.Key ?? "null").Append('\"')
                        .Append(':')
                        .Append('\"').Append(eventParam.Value ?? "null").Append('\"')
                        .Append(',');
                    }
                }
                // NOTE: Length > 1即JSON串不为空
                if (1 < eventParamsBuilder.Length)
                {
                    eventParamsBuilder.Remove(eventParamsBuilder.Length - 1, 1);
                }
                eventParamsBuilder.Append('}');
                string eventParams = eventParamsBuilder.ToString();
            sGPMObj.Call("postEvent", new object[] { eventName , eventParams});

#endif
        }


        /// <summary>
        /// 获取机型分档
        /// </summary>
        /// <param name="domainName">配置域名</param>
        public static int CheckDeviceClass(string domainName)
        {
#if UNITY_IOS && !UNITY_EDITOR
            string path = Application.streamingAssetsPath;
            return gpm_checkDCLSByQcc(path, domainName);
#elif UNITY_ANDROID && !UNITY_EDITOR
            if(sGPMObj == null)
            {
                return -2;
            }
            string vendor = SystemInfo.graphicsDeviceVendor;
            string renderer = SystemInfo.graphicsDeviceName;
            return sGPMObj.Call<int>("checkDCLSByQcc", new object[] { domainName, vendor, renderer });

#endif
            return -2;
        }

        /// <summary>
        /// 异步获取机型分档
        /// </summary>
        /// <param name="domainName">配置域名</param>
        /// <param name="handler">回调handler</param>
        public static void CheckDeviceClassAsync(string domainName, GPMDeviceLevelEventHandle handler)
        {
#if UNITY_IOS && !UNITY_EDITOR
            System.Threading.Thread thr = new System.Threading.Thread(
                delegate ()
                {
                    string path = Application.streamingAssetsPath;
                    int quality = gpm_checkDCLSByQccSync(path, domainName);
                    handler(quality);
                }
            );
            thr.Start();
#elif UNITY_ANDROID && !UNITY_EDITOR

            string vendor = SystemInfo.graphicsDeviceVendor;
            string renderer = SystemInfo.graphicsDeviceName;

              if(sGPMObj == null)
            {
                return;
            }

            System.Threading.Thread thr = new System.Threading.Thread(
                delegate ()
                {
                    //HawkLogMsg("thread try to attach to jni environment");
                    int ret = AndroidJNI.AttachCurrentThread();
                    if (ret < 0)
                    {
                        //HawkLogError("thread attach to jni enviromnet failed");
                        return;
                    }
                    //HawkLogMsg("begin check dcls async " + vendor + " " + renderer);
                //int quality = _checkdclsByQccSync(configureDomainName, vendor, renderer);
                int quality = sGPMObj.Call<int>("checkDCLSByQccSync", new object[] { domainName, vendor, renderer });
                   // HawkLogMsg("recv quality, exec callback");
                    handler(quality);
                    AndroidJNI.DetachCurrentThread();
                   // HawkLogMsg("thread detach to jni environment");
                }
                );
            thr.Start();

#endif
        }

        /// <summary>
        /// 开始区域剔除，MMO游戏可以用于剔除挂机
        /// </summary>
        public static void BeginExclude()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_beignExclude();
#elif UNITY_ANDROID && !UNITY_EDITOR
              if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("beignExclude");
#endif
        }

        /// <summary>
        /// 结束区域剔除
        /// </summary>
        public static void EndExclude()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_endExclude();
#elif UNITY_ANDROID && !UNITY_EDITOR
              if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("endExclude");
#endif
        }

        /// <summary>
        /// 设定场景画质
        /// </summary>
        /// <param name="sceneQuality">画质值</param>
        public static void SetSceneQuality(int sceneQuality)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_setQulaity(sceneQuality);
#elif UNITY_ANDROID && !UNITY_EDITOR

            if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("setQulaity", sceneQuality);
            
#endif
        }

        /// <summary>
        /// 设置机型分档
        /// 后台会按照设定的机型分档做数据聚合
        /// NOTE： 机型分档值需大于等于1
        /// </summary>
        /// <param name="deviceLevel">机型分档值</param>
        public static void SetDeviceLevel(int deviceLevel)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_setDefinedDeviceClass(deviceLevel);
#elif UNITY_ANDROID && !UNITY_EDITOR
              if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("setDefinedDeviceClass", deviceLevel);
#endif
        }

        /// <summary>
        /// 网络探测接口，游戏发生超时卡顿时调用
        /// </summary>
        public static void DetectInTimeout()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_detectInTimeout();
#elif UNITY_ANDROID && !UNITY_EDITOR

            if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("detectInTimeout");
#endif
        }

        /// <summary>
        /// 上报游戏逻辑网络延时
        /// </summary>
        /// <param name="mills">网络延时值</param>
        public static void PostNetworkLatency(int mills)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postNetLatency(mills);
#elif UNITY_ANDROID && !UNITY_EDITOR
              if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("postNetLatency", mills);
#endif
        }



        #region PostValueUtils
        /// <summary>
        /// 自定义数据系列函数
        /// 支持自由扩展
        /// </summary>
        public static void BeginTupleWrap(string tupleName)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_beginTupleWrap(tupleName);
#elif UNITY_ANDROID && !UNITY_EDITOR
            tapmNativeBeginTupleWrap(tupleName);
#endif
        }
        public static void EndTupleWrap()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_endTupleWrap();
#elif UNITY_ANDROID && !UNITY_EDITOR
              tapmNativeEndTupleWrap();
#endif
        }

        public static void PostValueF(string category, string key, float a)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueF1(category, key, a);
#elif UNITY_ANDROID && !UNITY_EDITOR
             tapmNativePostV1F(category, key, a);
#endif
        }
        public static void PostValueF(string category, string key, float a, float b)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueF2(category, key, a, b);
#elif UNITY_ANDROID && !UNITY_EDITOR
             tapmNativePostV2F(category, key, a, b);
#endif
        }
        public static void PostValueF(string category, string key, float a, float b, float c)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueF3(category, key, a, b, c);
#elif UNITY_ANDROID && !UNITY_EDITOR
             tapmNativePostV3F(category, key, a, b, c);
#endif
        }

        public static void PostValueI(string category, string key, int a)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueI1(category, key, a);
#elif UNITY_ANDROID && !UNITY_EDITOR
              tapmNativePostV1I(category, key, a);
#endif
        }
        public static void PostValueI(string category, string key, int a, int b)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueI2(category, key, a, b);
#elif UNITY_ANDROID && !UNITY_EDITOR
             tapmNativePostV2I(category, key, a, b);
#endif
        }
        public static void PostValueI(string category, string key, int a, int b, int c)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueI3(category, key, a, b, c);
#elif UNITY_ANDROID && !UNITY_EDITOR
             tapmNativePostV3I(category, key, a, b, c);
#endif
        }
        public static void PostValueS(string category, string key, string value)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postValueS(category, key, value);
#elif UNITY_ANDROID && !UNITY_EDITOR
            tapmNativePostV1S(category, key, value);
#endif
        }
        #endregion


        /// <summary>
        /// 关键路径转化
        /// </summary>
        /// <param name="category">路径类别名</param>
        /// <param name="stepId">路径步骤</param>
        /// <param name="status">路径状态</param>
        /// <param name="code">路径状态值</param>
        /// <param name="msg">信息</param>
        /// <param name="extraKey">自定义统计Key</param>
        /// <param name="authorize">是否为鉴权步骤</param>
        /// <param name="finish">步骤是否结束</param>
        public static void PostStepEvent(string category, int stepId, int status, int code, string msg, string extraKey, bool authorize, bool finish)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_postStepEvent(category, stepId, status, code, msg, extraKey, authorize, finish);
#elif UNITY_ANDROID && !UNITY_EDITOR
             if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("postStepEvent", category, stepId, status, code, msg, extraKey, authorize, finish);
#endif
        }


        //        /// <summary>
        //        /// 设置支付过程事件统计
        //        /// </summary>
        //        /// <param name="id">物品id</param>
        //        /// <param name="tag">事件场景（游戏自定义）</param>
        //        /// <param name="status">此次事件操作的结果状态</param>
        //        /// <param name="msg">此次事件返回的信息</param>
        //        public static void SetPayEvent(int id, int tag, bool status, string msg)
        //        {
        //#if UNITY_IOS && !UNITY_EDITOR
        //            gpmpostStepEvent(category, stepId, status, code, msg, extraKey);
        //#elif UNITY_ANDROID && !UNITY_EDITOR
        //             if(sGPMObj == null)
        //            {
        //                return;
        //            }

        //            sGPMObj.Call("postStepEvent", category, stepId, (int)status, code, msg, extraKey);
        //#endif
        //        }

        /// <summary>
        /// stepEvent会话连接
        /// </summary>
        /// <param name="category">事件类别名</param>
        public static void LinkLastStepEventSession(string category)
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_linkSession(category);
#elif UNITY_ANDROID && !UNITY_EDITOR
             if(sGPMObj == null)
            {
                return;
            }

            sGPMObj.Call("linkStepEventSession", category);
#endif
        }

        /// <summary>
        /// 初始化StepEvent环境
        /// </summary>
        public static void InitStepEventContext()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_initStepEventContext();
#elif UNITY_ANDROID && !UNITY_EDITOR
            //  if(sGPMObj == null)
            //{
            //    return;
            //}

            //sGPMObj.Call("InitStepEventContext");
#endif
        }


        /// <summary>
        /// 释放StepEvent环境
        /// </summary>
        public static void ReleaseStepEventContext()
        {
#if UNITY_IOS && !UNITY_EDITOR
            gpm_releaseStepEventContext();
#elif UNITY_ANDROID && !UNITY_EDITOR
            //if(sGPMObj == null)
            //{
            //    return;
            //}

            //sGPMObj.Call("ReleaseStepEventContext");
#endif

        }



        public static void RegisterCallback(TGPA.ITGPACallback callback)
        {
            TGPA.TGPAHelper.RegisterCallback(callback);
        }

    
        public static void UpdateGameInfo(TGPA.GameDataKey key, string value)
        {
            TGPA.TGPAHelper.UpdateGameInfo(key, value);
        }

        public static void UpdateGameInfo(string key, string value)
        {
            TGPA.TGPAHelper.UpdateGameInfo(key, value);
        }

        public static void UpdateGameInfo(Dictionary<TGPA.GameDataKey, string> dict)
        {
            TGPA.TGPAHelper.UpdateGameInfo(dict);
        }

        public static void UpdateGameInfo(TGPA.GameDataKey key, int value)
        {
            TGPA.TGPAHelper.UpdateGameInfo(key, value);
        }
        public static void UpdateGameInfo(string key, Dictionary<string, string> dict)
        {
            TGPA.TGPAHelper.UpdateGameInfo(key, dict);
        }

        public static string GetOptCfgStr()
        {
            return TGPA.TGPAHelper.GetOptCfgStr();
        }

        public static string GetDataFromTGPA(string key, string value)
        {
            return TGPA.TGPAHelper.GetDataFromTGPA(key, value);
        }

        public static string CheckDeviceIsReal()
        {
            return TGPA.TGPAHelper.CheckDeviceIsReal();
        }

        public static void ReportUserInfo(Dictionary<string, string> dict)
        {
            TGPA.TGPAHelper.ReportUserInfo(dict);
        }


        public static int GetCurrentThreadTid()
        {
            return TGPA.TGPAHelper.GetCurrentThreadTid();
        }




        public static void SetOnMarkLevelLoadObserver(GPMOnMarkLevelLoadObserver observer){
		    if (sCallbackGameObject == null) 
			{
				sCallbackGameObject = new GameObject ("UnityGPMCallBackGameObejct");
				sCallbackGameObject.AddComponent<GPMCallBackComponent> ();
				GameObject.DontDestroyOnLoad (sCallbackGameObject);
			}
            sMarkLevelLoadNotifyEvent += observer;
        }

        internal  static void  OnMarkLevelLoadNotify(string sceneId){
            Debug.Log("gpmlog callee in GPMAgent::OnMarkLevelLoadNotify " + sceneId);
            if(sMarkLevelLoadNotifyEvent != null){
                sMarkLevelLoadNotifyEvent(sceneId);
            }
        }

        public static void SetOnSetQualityObserver(GPMQualityObserver observer){
            if (sCallbackGameObject == null) 
			{
				sCallbackGameObject = new GameObject ("UnityGPMCallBackGameObejct");
				sCallbackGameObject.AddComponent<GPMCallBackComponent> ();
				GameObject.DontDestroyOnLoad (sCallbackGameObject);
			}
            sQualityNotifyEvent += observer;
        }

        internal  static void OnQualityNotify(string quality){
            Debug.Log("gpmlog callee in GPMAgent::OnQualityNotify " + quality);
            if(sQualityNotifyEvent != null){
                sQualityNotifyEvent(quality);
            }
        }

        public static void SetOnLogObserver(GPMLogObserver observer){
            if (sCallbackGameObject == null) 
			{
				sCallbackGameObject = new GameObject ("UnityGPMCallBackGameObejct");
				sCallbackGameObject.AddComponent<GPMCallBackComponent> ();
				GameObject.DontDestroyOnLoad (sCallbackGameObject);
			}
            sLogNotifyEvent += observer;
        }

        internal  static  void OnLogNotify(int errCode, string errMsg, string extMsg) {
            Debug.Log("gpmlog callee in GPMAgent::OnLogNotify " + extMsg);
            if (sLogNotifyEvent != null) 
			{
                sLogNotifyEvent(errCode, errMsg, extMsg);
            }
        }
    }


	internal class GPMCallBackComponent : MonoBehaviour
	{
		// 暴露给Android/IOS的回调接口
		public void GPMOnMarkLevelLoad (string sceneId)
		{
			GPMAgent.OnMarkLevelLoadNotify(sceneId);
		}

		public void GPMOnSetQuality (string qualityString)
		{
			GPMAgent.OnQualityNotify(qualityString);
		}

		public void GPMOnLog (string logString)
		{
            
            Debug.Log("logString: " + logString);
            string tSt;
            tSt = logString.Replace("[GPM]", "");
            tSt = tSt.Replace("__", "_");
            string[] configs = tSt.Split(new char[] { '_' });
            if (configs.Length != 2)
            {
                return;
            }
            int errCode;
            int.TryParse(configs[0], out errCode);
            string extMsg = configs[1];
            GPMAgent.OnLogNotify(errCode, "", extMsg);
        }

        public void GPMOnFpsNotify (string fpsString)
        {
            Debug.Log("fpsString: " + fpsString);
        }
	}
}
