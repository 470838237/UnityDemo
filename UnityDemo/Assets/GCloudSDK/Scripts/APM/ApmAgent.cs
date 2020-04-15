//#undef UNITY_EDITOR 
//#undef UNITY_STANDALONE
//#undef UNITY_ANDROID
//#define UNITY_IOS

//------------------------------------------------------------------------------
//
// File: ApmAgent
// Module: APM
// Version: 1.0
// Author: vincentwgao
//
//------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Runtime.InteropServices;


#region GCloud.APM definition

namespace GCloud.APM
{
    public sealed class ApmAgent
    {
        // private const string ApmVersion = "1.0.0.0.23";
        private const string ApmLogTag = "gcloud apm";
        private static bool ApmInitOnce = false;
        private static bool ApmInitFlag = false;

        public const string TupleKeyIden = "#KEY#";
        public const string TupleValueIden = "#VALUE#";

        private static ApmCallBackInterface mCallBack = null;

        private static void HawkLogMsg(String msg)
        {
            Debug.Log(string.Format("[{0}]:{1}", ApmLogTag, msg));
        }

        private static void HawkLogError(String msg)
        {
            Debug.LogError(string.Format("[{0}]:{1}", ApmLogTag, msg));
        }

        private static void HawkEmptyImp(String funcName)
        {
            HawkLogMsg("GCLOUD TAPM EMPTY IMPLEMENTATION IN EDITOR: " + funcName);
        }

        public static void SetCallBack(ApmCallBackInterface callback)
        {
            mCallBack = callback;
        }

      

        //private static string sSceneName = null;

        public enum StreamStep { Launch = 1, CheckUpdate, Updating, Platform, MSDKAuth, FetchZone, GameStart, EnterLobby };
        public enum StreamStatus { Success, Failed };

        public delegate void TApmDeviceClassEventHandle(int deviceclass);
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostFrame(int frametime);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostCoordinate(float x, float y, float z, float pitch, float yaw, float roll);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostNTL(int latency);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1F(string category, string key, float a);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV2F(string category, string key, float a, float b);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV3F(string category, string key, float a, float b, float c);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1I(string category, string key, int a);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV2I(string category, string key, int a, int b);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV3I(string category, string key, int a, int b, int c);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativePostV1S(string category, string key, string value);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativeBeginTupleWrap(string key);

        [DllImport("apm", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tapmNativeEndTupleWrap();

        private static readonly string CLASS_UNITYAGENT = "com.tencent.gcloud.apm.TApmAgent";
        private static AndroidJavaObject _apm_bridge;
        public static AndroidJavaObject APMBridge
        {
            get
            {
                if (_apm_bridge == null)
                {
                    _apm_bridge = new AndroidJavaObject(CLASS_UNITYAGENT);
                }
                return _apm_bridge;
            }
        }

#elif (UNITY_IOS || UNITY_IPHONE)
        [DllImport("__Internal")]
        private static extern int g_InitContext(string appId);
        [DllImport("__Internal")]
        private static extern int g_MarkLoadlevel(string sceneName, int quality);
        [DllImport("__Internal")]
        private static extern int g_MarkLoadlevelCompleted();
        [DllImport("__Internal")]
        private static extern int g_MarkLevelFin();
        [DllImport("__Internal")]
        private static extern void g_BeginTag(string tagName);
        [DllImport("__Internal")]
        private static extern void g_EndTag();
        [DllImport("__Internal")]
        private static extern void g_PostFrame(float deltaSeconds);
        [DllImport("__Internal")]
        private static extern void g_SetQuality(int quality);
        [DllImport("__Internal")]
        private static extern void g_SetUserID(string userID);
        [DllImport("__Internal")]
        private static extern void g_SetLocal(string cLocal);
        [DllImport("__Internal")]
        private static extern void g_PostNTL(int latency);
        [DllImport("__Internal")]
        private static extern void g_PostLagStatus(int distance);
        [DllImport("__Internal")]
        private static extern void g_PostEvent(int key, string info);
        [DllImport("__Internal")]
        private static extern void g_SetVersionIden(string version);
        //[DllImport("__Internal")]
        //private static extern void g_PostStreamEvent(int stepId, int code, int status, string msg);
        [DllImport("__Internal")]
        private static extern int g_GetDeviceLevel(string path, string configLevel);
        [DllImport("__Internal")]
        private static extern void g_PostCoordinate(int x, int y, int z, int pitch, int yaw, int roll);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV3F(string category, string key, float a, float b, float c);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV2F(string category, string key, float a, float b);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV1F(string category, string key, float a);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV3I(string category, string key, int a, int b, int c);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV2I(string category, string key, int a, int b);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV1I(string category, string key, int a);
        [DllImport("__Internal")]
        private static extern void tapmNativePostV1S(string category, string key, string value);
        [DllImport("__Internal")]
        private static extern void tapmNativeBeginTupleWrap(string key);
        [DllImport("__Internal")]
        private static extern void tapmNativeEndTupleWrap();
        [DllImport("__Internal")]
        private static extern void g_BeginExclude();
        [DllImport("__Internal")]
        private static extern void g_EndExclude();

        [DllImport("__Internal")]
        private static extern void g_InitStepEventContext();
        [DllImport("__Internal")]
        private static extern void g_ReleaseStepEventContext();
        [DllImport("__Internal")]
        private static extern void g_PostStepEvent(string eventCategory, int stepId, int status, int code, string msg, string extraKey);
        [DllImport("__Internal")]
        private static extern void g_LinkSession(string category);
        [DllImport("__Internal")]
        private static extern void g_SetDeviceLevel(int deviceLevel);

        [DllImport("__Internal")]
        private static extern int g_GetSceneMaxPss();
        [DllImport("__Internal")]
        private static extern int g_GetSceneTotalTime();
        [DllImport("__Internal")]
        private static extern int g_GetSceneTotalFrames();
        [DllImport("__Internal")]
        private static extern string g_GetCurrentSceneName();
        [DllImport("__Internal")]
        private static extern int g_GetSceneLoadedTime();
        [DllImport("__Internal")]
        private static extern string g_GetErrorMsg(int errorCode);
		
		[DllImport("__Internal")]
		private static extern string g_GetSceneSessionId();
#endif

        private static string getErrorMsg(int errorCode)
        {
#if (UNITY_EDITOR || UNITY_STANDALONE)
            return "";
#elif UNITY_ANDROID
            try
            {
                return APMBridge.CallStatic<string>("getErrorMsg", errorCode);
            }
            catch (Exception e)
            {
                HawkLogError("invoke getErrorMsg failed : " + e);
                return "java error: "+e;
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            return g_GetErrorMsg(errorCode);
#endif
        }

        /// <summary> OpenId设置
        /// 通过OpenId的设置，可以跟踪单个用户的性能状况
        ///
        /// </summary>
        /// <param name="openId">openid</param>
        public static void SetOpenId(string openId)
        {
            if (string.IsNullOrEmpty(openId))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetOpenId");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setUserId", openId);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setUserId failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_SetUserID(openId);
#endif

        }

        /// <summary> 初始化接口
        ///
        /// </summary>
        /// <param name="appId">TApm分配的appId，分配appid，请联系vincentwgao</param>
        /// <returns></returns>
        public static int InitContext(string appId)
        {
            if (string.IsNullOrEmpty(appId))
            {
                return -1;
            }

            if (!ApmInitOnce)
            {
                ApmInitOnce = true;
            }
            else
            {
                HawkLogError("_initContext function cannot be invoked more than once");

                // error callback
                if (mCallBack != null)
                {
                    mCallBack.OnInitContext(false, "initContext function cannot be invoked more than once");
                }

                return -1;
            }

            int initFlag = 0;

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("InitContext");
#elif UNITY_ANDROID

            // HawkLogMsg("APM version is: " + ApmVersion);

            string gpuVendor = SystemInfo.graphicsDeviceVendor;
            string gpuRenderer = SystemInfo.graphicsDeviceName;
            string gpuVersion = SystemInfo.graphicsDeviceVersion;

            if (string.IsNullOrEmpty(gpuVendor))
            {
                gpuVendor = "NA";
            }

            if (string.IsNullOrEmpty(gpuRenderer))
            {
                gpuRenderer = "NA";
            }

            if (string.IsNullOrEmpty(gpuVersion))
            {
                gpuVersion = "NA";
            }

            try
            {
                initFlag = APMBridge.CallStatic<int>("hawkInitForUnity", appId, gpuVendor, gpuRenderer, gpuVersion);
                HawkLogMsg("end tapm init flag : " + initFlag);
            }
            catch (Exception e)
            {
                HawkLogError("invoke initContext failed : " + e);

                if (mCallBack != null)
                {
                    mCallBack.OnInitContext(false, "invoke initContext failed : " + e);
                }

                return -2;
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            initFlag = g_InitContext(appId);
#endif

            GameObject tapmGameObject = new GameObject("TApmGameObject");

            // NOTE:
            // 在TApmFrameProcessor.cs脚本中的start方法中调用DontDestroyOnLoad时，
            // tapmGameObject可能会被销毁
            // 导致帧率采集的问题
            // fixed on 180713

            GameObject.DontDestroyOnLoad(tapmGameObject);

            tapmGameObject.AddComponent<FrameProcessor>();

            ApmInitFlag = true;

            if (mCallBack != null)
            {
                mCallBack.OnInitContext(initFlag == 0, getErrorMsg(initFlag));
            }

            return 0;
        }


        /// <summary>标记场景开始,
        /// 如果需要设置场景的画质，请使用<seealso cref="SetQuality" target="_self"/>接口,
        /// 此接口中的quality参数废弃
        /// </summary>
        /// <param name="scene_name">场景名</param>
        public static void MarkLoadlevel(string sceneName)
        {
            int retCode = 0;

            if (string.IsNullOrEmpty(sceneName))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("MarkLoadlevel");
#elif UNITY_ANDROID
            try
            {
                retCode = APMBridge.CallStatic<int>("markLevelLoad", sceneName, 0);
            }
            catch (Exception e)
            {
                HawkLogError("invoke markLevelLoad failed : " + e);

                if (mCallBack != null)
                {
                    mCallBack.OnMarkLevelLoad(false, "invoke markLevelLoad failed : " + e, sceneName);
                }
                return;
            }
#elif (UNITY_IOS || UNITY_IPHONE)

            retCode = g_MarkLoadlevel(sceneName, 0);
#endif
            if (mCallBack != null)
            {
                mCallBack.OnMarkLevelLoad(retCode == 0, getErrorMsg(retCode), sceneName);
            }
        }
		


        /// <summary>标记场景记载结束，
        /// 如果需要关注场景加载时间，可在场景结束的时候通过此接口进行标记
        ///
        /// </summary>
        public static void MarkLoadlevelCompleted()
        {
            int retCode = 0;
            String currentSceneName = "";
            int sceneLoadedTime = 0;
#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("MarkLoadlevelCompleted");
#elif UNITY_ANDROID
            try
            {
 				retCode = APMBridge.CallStatic<int>("markLevelLoadCompleted");                if (mCallBack != null)
                {
                    currentSceneName = APMBridge.CallStatic<string>("getCurrentSceneName");
                    sceneLoadedTime = APMBridge.CallStatic<int>("getSceneLoadedTime");
                }

               
            }
            catch (Exception e)
            {
                HawkLogError("invoke markLevelLoadCompleted failed : " + e);

                if (mCallBack != null)
                {
                    mCallBack.OnMarkLevelLoadCompleted(false, "invoke markLevelLoadCompleted failed : " + e, "", 0);
                }
                return;
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            retCode = g_MarkLoadlevelCompleted();
            if (mCallBack != null)
            {
                currentSceneName = g_GetCurrentSceneName();
                sceneLoadedTime = g_GetSceneLoadedTime();
            }
#endif

            if (mCallBack != null)
            {
                mCallBack.OnMarkLevelLoadCompleted(retCode == 0, getErrorMsg(retCode), currentSceneName, sceneLoadedTime);
            }

        }

        /// <summary>标记场景结束
        ///
        /// </summary>
        public static void MarkLevelFin()
        {
            int retCode = 0;
            string currentSceneName = "";
            int totalFrames = 0;
            int totalTimes = 0;
            int maxPss = 0;

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("MarkLevelFin");
#elif UNITY_ANDROID
            try
            {
                if (mCallBack != null)
                {
                    currentSceneName = APMBridge.CallStatic<string>("getCurrentSceneName");
                    totalFrames = APMBridge.CallStatic<int>("getTotalFrames");
                    totalTimes = APMBridge.CallStatic<int>("getTotalTimes");
                    maxPss = APMBridge.CallStatic<int>("getMaxPss");
					maxPss = maxPss/1024;                }

                retCode = APMBridge.CallStatic<int>("markLevelFin");
               
            }
            catch (Exception e)
            {
                HawkLogError("invoke markLevelFin failed : " + e);
                if (mCallBack != null)
                {
                    mCallBack.OnMarkLevelFin(false, "invoke markLevelFin failed : " + e, "", 0, 0, 0);
                }
                return;
            }
#elif (UNITY_IOS || UNITY_IPHONE)
           

            if (mCallBack != null)
            {
                currentSceneName = g_GetCurrentSceneName();
                totalFrames = g_GetSceneTotalFrames();
                totalTimes = g_GetSceneTotalTime();
                maxPss = g_GetSceneMaxPss();
            }
 			retCode = g_MarkLevelFin();
#endif
            if (mCallBack != null)
            {
                mCallBack.OnMarkLevelFin(retCode == 0, getErrorMsg(retCode), currentSceneName, totalTimes, totalFrames, maxPss);
            }

        }

        /// <summary> 标记标签开始，
        /// 标签是场景中的一段区域
        /// </summary>
        /// <param name="tagName">标签名</param>
        public static void BeginTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return;
            }

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("BeginTag");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("beginTag", tagName);
            }
            catch (Exception e)
            {
                HawkLogError("invoke beginTag failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_BeginTag(tagName);
#endif
        }


        /// <summary> 标记标签结束
        ///
        /// </summary>
        public static void EndTag()
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("EndTag");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("endTag");
            }
            catch (Exception e)
            {
                HawkLogError("invoke endTag failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_EndTag();
#endif
        }


        /// <summary> 上传网络延迟
        ///
        /// </summary>
        /// <param name="latency">ping值</param>
        public static void UpdateNetLatency(int latency)//unit: ms
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("UpdateNetLatency");
#elif UNITY_ANDROID
            tapmNativePostNTL(latency);
#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostNTL(latency);
#endif
        }

        /// <summary>开启调试日志
        ///
        /// </summary>
        public static void EnableDebugMode()
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("EnableDebugMode");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("enableDebugMode");
            }
            catch (Exception e)
            {
                HawkLogError("invoke enableDebugMode failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)

#endif
        }

        /// <summary>设置全局自定义画质,后台会根据此值进行分档计算,
        /// <para>可以将目标帧率，机型档位等其它维度融合为一个值，通过setquality进行上传，</para>
        /// <para>后台可以根据各个维度的取值以及整体的取值进行计算；</para>
        /// <para>比如帧率有30，40，60三档，机型分档为低、中、高，</para>
        /// <para>可以使用各位代表帧率（1，2，3），十位代表机型分档（1，2，3），</para>
        /// <para>即有这样的组合：</para>
        /// <para>30帧低端机->1011</para>
        /// <para>30帧高端机->1031</para>
        /// <para>60帧高端机->1033，</para>
        ///
        /// <para>APM后台会分别计算3*3（组合）+3（30，40，60）+3（低、中、高）+1（总体) 维度的数据</para>
        ///
        /// </summary>
        /// <param name="quality">自定义画质参数</param>
        /// <example> <c>SetQuality(1001)</c> </example>
        /// <example> <c>SetQuality(1002)</c> </example>
        /// <example> <c>SetQuality(10011)</c> </example>
        public static void SetQuality(int quality)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetQuality");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setGlobalQuality", quality);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setGlobalQuality failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_SetQuality(quality);
#endif
        }


        /// <summary> 设置目标帧率,
        /// 可将目标帧率的设定，采用setQuality接口进行组合设定
        ///
        /// </summary>
        /// <param name="target">目标帧率</param>
        /// <seealso cref="SetQuality" target="_self"/>
        public static void SetTargetFrameRate(int target)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetTargetFrameRate");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setTargetFramerate", target);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setTargetFramerate failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)

#endif
        }

        /// <summary>地区设定
        /// 用于海外发行的游戏，可以根据地区进行性能的监控分析，
        /// 同样的，也可使用setQuality,将地区维度融合到qulaity参数中
        /// </summary>
        /// <param name="locale">地区</param>
        public static void SetLocale(string locale)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetLocale");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setLocale", locale, 1);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setLocale failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_SetLocal(locale);
#endif
        }

        /// <summary>自定义事件上报
        ///
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void PostEvent(int key, string value)
        {

            if (string.IsNullOrEmpty(value))
            {
                return;
            }

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("PostEvent");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("postEvent", key, value);
            }
            catch (Exception e)
            {
                HawkLogError("invoke PostEvent failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostEvent(key, value);
#endif
        }

        /// <summary> 自定义设置版本号
        /// 可用于设定热更版本
        /// </summary>
        /// <param name="version">VersionName</param>
        public static void SetVersionIden(string version)
        {

            if (string.IsNullOrEmpty(version))
            {
                return;
            }

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetVersionIden");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setVersionIden", version);
            }
            catch (Exception e)
            {
                HawkLogError("invoke SetVersionIden failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)

            g_SetVersionIden(version);
#endif
        }

        /// <summary>将PSS的获取方式变为手动模式，默认自动模式，
        /// 手动模式下，只有通过<seealso cref="ApmAgent.RequestPssSample" target="_self"/>才会采集PSS数据
        ///
        /// </summary>
        public static void SetPssManualMode()
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("SetPssManualMode");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("setPssManualMode");
            }
            catch (Exception e)
            {
                HawkLogError("invoke SetPssManualMode failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)

#endif
        }

        /// <summary> 手动模式下，申请进行PSS采集，最小间隔为1S
        ///
        /// </summary>
        public static void RequestPssSample()
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("RequestPssSample");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("requestPssSample");
            }
            catch (Exception e)
            {
                HawkLogError("invoke RequestPssSample failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)

#endif
        }




        /// <summary> 机型分档接口，
        /// 如需使用此功能，请联系vincentwgao
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <returns></returns>
        public static int CheckDeviceClass(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return 0;
            }



#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("CheckDeviceClass");
            return 0;
#elif UNITY_ANDROID

            string vendor = SystemInfo.graphicsDeviceVendor;
            string renderer = SystemInfo.graphicsDeviceName;
            try
            {
                HawkLogMsg("begin to _checkdclsByQcc");
                int ret = APMBridge.CallStatic<int>("checkDCLSByQcc", configName, vendor, renderer);
                return ret;
            }
            catch (Exception e)
            {
                HawkLogError("invoke checkDCLSByQcc failed : " + e);
            }
            return 0;

#elif (UNITY_IOS || UNITY_IPHONE)
            
            string path = Application.streamingAssetsPath;
            int ret = g_GetDeviceLevel(Application.streamingAssetsPath, configName);
            return ret;
#else
            return 0;
#endif


        }


        /// <summary> 机型分档接口-异步，
        /// 如需使用此功能，请联系vincentwgao
        /// CheckDeviceClassAsync接口中会发生网络请求，
        /// 保证获取到最新的配置文件，否则会返回失败，为一个负数的错误码
        /// </summary>
        /// <param name="configName">配置文件名</param>
        /// <returns>失败时为负数</returns>
        public static void CheckDeviceClassAsync(string configureDomainName, TApmDeviceClassEventHandle handler)
        {
            if (!ApmInitFlag) return;
            if (handler == null) return;
            if (string.IsNullOrEmpty(configureDomainName))
            {
                return;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            string vendor = SystemInfo.graphicsDeviceVendor;
            string renderer = SystemInfo.graphicsDeviceName;

            System.Threading.Thread thr = new System.Threading.Thread(
                delegate ()
                {
                    HawkLogMsg("thread try to attach to jni environment");
                    int ret = AndroidJNI.AttachCurrentThread();
                    if (ret < 0)
                    {
                        HawkLogError("thread attach to jni enviromnet failed");
                        return;
                    }
                    HawkLogMsg("begin check dcls async " + vendor + " " + renderer);
                //int quality = _checkdclsByQccSync(configureDomainName, vendor, renderer);
                int quality = APMBridge.CallStatic<int>("checkDCLSByQccSync", configureDomainName, vendor, renderer);
                    HawkLogMsg("recv quality, exec callback");
                    handler(quality);
                    AndroidJNI.DetachCurrentThread();
                    HawkLogMsg("thread detach to jni environment");
                }
                );
            thr.Start();
#endif
        }


        /// <summary> 帧率采集
        ///
        /// </summary>
        public static void PostFrame()
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("PostFrame");
#elif UNITY_ANDROID
            tapmNativePostFrame((int)(Time.deltaTime * 100));
#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostFrame(Time.deltaTime);
#endif

        }

        /// <summary> 上传回扯状态
        ///
        /// </summary>
        /// <param name="distance">回扯距离</param>
        public static void PostLagStatus(float distance)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("PostLagStatus");
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("postLagState", distance);
            }
            catch (Exception e)
            {
                HawkLogError("invoke PostLagStatus failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostLagStatus((int)(distance * 100));
#endif
        }

        /// <summary>登录转化流程
        /// deprecated, use PostStepEvent instead
        /// </summary>
        /// <param name=""></param>
        [Obsolete("PostStreamEvent is deprecated")]
        public static void PostStreamEvent(StreamStep stepId, StreamStatus status, int code, string msg)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("PostStreamEvent");
#elif UNITY_ANDROID
            //try
            //{
            //    APMBridge.CallStatic("postStreamEvent", (int)stepId, (int)status, code, msg);
            //}
            //catch (Exception e)
            //{
            //    HawkLogError("invoke PostStreamEvent failed : " + e);
            //}
#elif (UNITY_IOS || UNITY_IPHONE)
            // g_PostStreamEvent((int)stepId, (int)status, code, msg);
#endif
        }

        /// <summary> 玩家坐标
        ///
        /// </summary>
        /// <param name=""></param>
        public static void PostCoordinate(int x, int y, int z, float pitch, float yaw, float roll)
        {

#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("PostCoordinate");
#elif UNITY_ANDROID
            tapmNativePostCoordinate(x, y, z, pitch, yaw, roll);
#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostCoordinate((int)(x * 100), (int)(y * 100), (int)(z * 100), (int)(pitch * 100), (int)(yaw * 100), (int)(roll * 100));
#endif
        }

        /// <summary> 上传浮点型数据，
        /// 后台将属于同一个catgory的key做聚合分析
        /// </summary>
        /// <param name="catgory">类别</param>
        /// <param name="key">键值</param>
        /// <param name="a">实际值</param>
        /// <example>
        /// PostValueF("FuntionTime", "Update", 0.33f)
        /// FuntionTime表示定义一个“函数执行耗时”的类别
        /// "Update"表示具体的函数名
        /// "0.33f"表示函数的执行耗时
        /// APM后台服务器会自动的根据上传的信息，做统计分析
        /// 在页面中，选择类别Category以及key的信息，查看相关数据的统计分析结果
        /// </example>
        public static void PostValueF(string category, string key, float a)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }

#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV1F(category, key, a);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV1F(category, key, a);
#endif
        }

        /// <summary> 上传浮点型数据
        /// 同PostValueF，但是可以上传两个有关联的值，这两个值会作为一个二元组，一起过统计分析
        /// </summary>
        public static void PostValueF(string category, string key, float a, float b)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV2F(category, key, a, b);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV2F(category, key, a, b);
#endif
        }

        /// <summary> 上传浮点型数据
        /// 同PostValueF，但是可以上传三个有关联的值，这两个值会作为一个三元组，一起过统计分析
        /// </summary>
        public static void PostValueF(string category, string key, float a, float b, float c)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV3F(category, key, a, b, c);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV3F(category, key, a, b, c);
#endif
        }

        /// <summary> 上传整型数据
        /// 功能等同于PostValueF
        /// </summary>
        public static void PostValueI(string category, string key, int a)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV1I(category, key, a);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV1I(category, key, a);
#endif
        }

        /// <summary> 上传整型数据
        /// 功能等同于PostValueF
        /// </summary>
        public static void PostValueI(string category, string key, int a, int b)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV2I(category, key, a, b);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV2I(category, key, a, b);
#endif
        }

        /// <summary> 上传整型数据
        /// 功能等同于PostValueF
        /// </summary>
        public static void PostValueI(string category, string key, int a, int b, int c)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV3I(category, key, a, b, c);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV3I(category, key, a, b, c);
#endif
        }

        /// <summary> 上传整型数据
        /// 功能等同于PostValueF
        /// </summary>
        public static void PostValueS(string category, string key, string value)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativePostV1S(category, key, value);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativePostV1S(category, key, value);
#endif
        }

        /// <summary> 开始结构体定义
        /// 结构体定义为自定义数据提供了强大的扩展能力，
        /// 整个结构体的数据会进行整体的统计分析，
        /// 提供了复合键值的能力以及上传多维度数据的能力。
        /// 类比的说，通过BeginTupleWrap上传的数据，在后台会统计分析结果存为一张二维表，
        /// 表名为tupleName，
        /// BeginTupleWrap和EndTupleWrap中上传的key的值为列名，value为各个列的值
        /// category为TupleKeyIden的key值为复合主键
        /// category为TupleValueIden的key为具体的值
        /// </summary>

        /// <example>
        /// 如上传函数耗时，但是函数的耗时是分模块的(比如不同的模块中定义了相同的函数名),
        /// 再者函数耗时包含两个维度，自身函数耗时以及子函数耗时
        /// 
        /// BeginTupleWrap("FuntionCostAnalyse") //开始上传一个结构体， 结构体的名字为FuntionCostAnalyse
        /// PostValueS(TupleKeyIden, "ModuleName", "ModuleA");//通过TupleKeyIden表明这是一个键值，键名为"TupleKeyIden"，对应的值为"ModuleA"
        /// PostValueS(TupleKeyIden, "FunctionName", "Update");//通过TupleKeyIden表明这是一个键值，键名为"FunctionName"，对应的值为"Update"
        ///  
        /// PostValueF(TupleValueIden, "SelfCost", 0.33f);
        /// PostValueF(TupleValueIden, "childrenCost", 0.16f);
        /// EndTupleWrap()
        /// 
        /// 数据库的表模型为:
        /// TableName:
        /// |————————————|——————————————|——————————|——————————————|
        /// | ModuleName | FunctionName | SelfCost | childrenCost |
        /// |————————————|——————————————|——————————|——————————————|
        /// |   ModuleA  |    Update    |    0.33  |     0.16     |
        /// |————————————|——————————————|——————————|——————————————|
        /// |   ModeuleB |    Update    |    0.33  |     0.20     |
        /// |————————————|——————————————|——————————|——————————————|
        /// primaryKey(ModuleName, FunctionName)
        /// 
        /// PostValueI(string catgory, string key, int a, int b)
        /// 或者 PostValueI(string catgory, string key, int a, int b, int c)
        /// 函数可用BeginTupleWrap，PostValueI替代
        /// 
        /// 通过PostValueI系列函数上传的数据，在后台也会抽成为一张二维表，
        /// 表名为catgory，主键为key，可有多个维度的值（一个，两个，三个）
        ///  
        /// </example>
        /// <remarks>
        /// 位于BeginTupleWrap和EndTupleWrap中的PostValueXXX系列函数，
        /// 其category必须为TupleKeyIden或者TupleValueIden常量值
        /// 否则后台不会解析
        /// </remarks>
        public static void BeginTupleWrap(string tupleName)
        {
            if (!ApmInitFlag) return;
            if (string.IsNullOrEmpty(tupleName))
            {
                return;
            }
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID
            tapmNativeBeginTupleWrap(tupleName);
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativeBeginTupleWrap(tupleName);
#endif
        }

        /// <summary> 结束结构体定义
        /// 
        /// </summary>
        public static void EndTupleWrap()
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            tapmNativeEndTupleWrap();
#elif (UNITY_IOS || UNITY_IPHONE)
            tapmNativeEndTupleWrap();
#endif
        }

        /// <summary> 开始剔除，适用于MMO游戏，将挂机的数据剔除
        /// 
        /// </summary>
        public static void BeginExclude()
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("beignExclude");
            }
            catch (Exception e)
            {
                HawkLogError("invoke beignExclude failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_BeginExclude();
#endif
        }

        /// <summary> 剔除结束，适用于MMO游戏，将挂机的数据剔除
        /// 
        /// </summary>
        public static void EndExclude()
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)
#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("endExclude");
            }
            catch (Exception e)
            {
                HawkLogError("invoke endExclude failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_EndExclude();
#endif
        }


        /// <summary> Qcc判定时用到了cpu频率
        /// 
        /// </summary>
        public static int GetQccJudgeParamCpuFreq()
        {
#if (UNITY_EDITOR || UNITY_STANDALONE)
            return 0;
#elif UNITY_ANDROID
            try
            {
                int ret = APMBridge.CallStatic<int>("getQccJudgeCpuFreq");
                return ret;
            }
            catch (Exception e)
            {
                HawkLogError("invoke getQccJudgeCpuFreq failed : " + e);
            }
            return 0;
#elif (UNITY_IOS || UNITY_IPHONE)
            return 0;
#endif
        }

        /// <summary> 初始化关键路径环境
        /// 
        /// </summary>
        public static void InitStepEventContext()
        {
            if (!ApmInitFlag) return;

#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID
            try
            {
                APMBridge.CallStatic("initStepEventContext");
            }
            catch (Exception e)
            {
                HawkLogError("invoke initStepEventContext failed : " + e);
            }
#elif (UNITY_IOS || UNITY_IPHONE)
            g_InitStepEventContext();

#endif
        }


        /// <summary> 上报关键路径
        /// 
        /// </summary>
        public static void PostStepEvent(string category, int stepId, StreamStatus status, int code, string msg, string extraKey = "NA")
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

            try
            {
                APMBridge.CallStatic("postStepEvent", category, stepId, (int)status, code, msg, extraKey);
            }
            catch (Exception e)
            {
                HawkLogError("invoke postStepEvent failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_PostStepEvent(category, stepId, (int)status, code, msg, extraKey);
#endif

        }

        /// <summary> 释放关键路径环境
        /// 
        /// </summary>
        public static void ReleaseStepEventContext()
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

            try
            {
                APMBridge.CallStatic("releaseStepEventContext");
            }
            catch (Exception e)
            {
                HawkLogError("invoke releaseStepEventContext failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_ReleaseStepEventContext();
#endif

        }

        /// <summary> 链接上一次session
        /// 
        /// </summary>
        public static void LinkLastStepCategorySession(string category)
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

            try
            {
                APMBridge.CallStatic("linkLastStepCategorySession", category);
            }
            catch (Exception e)
            {
                HawkLogError("invoke LinkLastStepCategorySession failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_LinkSession(category);
#endif

        }

        /// <summary> 自定义机型档位
        /// 
        /// </summary>
        public static void SetCustomizedDeviceClass(int deviceclass)
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

            try
            {
                APMBridge.CallStatic("setDefinedDeviceClass", deviceclass);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setDefinedDeviceClass failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            g_SetDeviceLevel(deviceclass);
#endif

        }

		
		/// <summary>获取场景唯一ID
        /// </summary>
        /// <param name="scene_name">场景名</param>
        public static String GetSceneSessionId()
        {
            String uniqueId = "";
#if (UNITY_EDITOR || UNITY_STANDALONE)
            HawkEmptyImp("GetSceneSessionId");
#elif UNITY_ANDROID
            try
            {
                uniqueId = APMBridge.CallStatic<string>("getSceneSessionId");
            }
            catch (Exception e)
            {
                HawkLogError("invoke GetSceneSessionId failed : " + e);         
            }
#elif (UNITY_IOS || UNITY_IPHONE)

            uniqueId = g_GetSceneSessionId();
#endif
           return uniqueId;
        }
		
		
		
		/// <summary>设置Android PSS采集时间间隔
        /// </summary>
        /// <param name="scene_name">场景名</param>
        public static void SetPssIntervals(int intervals)
        {
            if (!ApmInitFlag) return;
#if (UNITY_EDITOR || UNITY_STANDALONE)

#elif UNITY_ANDROID

            try
            {
                APMBridge.CallStatic("setPssIntervals", intervals);
            }
            catch (Exception e)
            {
                HawkLogError("invoke setPssIntervals failed : " + e);
            }

#elif (UNITY_IOS || UNITY_IPHONE)
            //g_SetDeviceLevel(deviceclass);
#endif
        }


    }

}
#endregion
