// ------------------------------------------------------------------------
// 
// File: TGPAServiceAndroid
// Module: TGPA
// Version: 1.0
// Author: zohnzliu
// Modifyed: 2019/06/10
// 
// ------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using AOT;
using UnityEngine;

namespace GCloud.TGPA
{
    //----------------------- TGPA 内部逻辑实现，游戏侧无需关注 --------------------------
    //----------------- Android平台 ------------------------
    #region TGPAServiceAndroid Definition
    public class TGPAServiceAndroid : ITGPAService
    {
        #region TGPAServiceAndroid Public Definition
        private static TGPAServiceAndroid mInstance;
        private static object _lock = new object ();

        public static TGPAServiceAndroid GetInstance ()
        {
            if (mInstance == null)
            {
                lock (_lock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new TGPAServiceAndroid ();
                    }
                }
            }
            return mInstance;
        }
        public int GetVersionCode ()
        {
            return _GetVersionCode ();
        }

        public void EnableLog (bool enable)
        {
            _EnableLog (enable);
        }

        public void Init ()
        {
            _InitSdk ();
        }

        public void RegisterCallback (ITGPACallback callback)
        {
            _RegisterCallback (callback);
        }

        public void UpdateGameFps (float value)
        {
            _UpdateGameFps (value);
        }

        public void UpdateGameInfo (GameDataKey key, string value)
        {
            _UpdateGameInfo (key, value);
        }

        public void UpdateGameInfo (GameDataKey key, int value)
        {
            _UpdateGameInfo (key, value);
        }

        public void UpdateGameInfo (string key, string value)
        {
            _UpdateGameInfo (key, value);
        }

        public void UpdateGameInfo (Dictionary<GameDataKey, string> dict)
        {
            _UpdateGameInfo (dict);
        }

        public void UpdateGameInfo (string key, Dictionary<string, string> dict)
        {
            switch (key)
            {
                case "DeviceBind":
                    _ReportUserInfo (dict);
                    return;
                case "PreDownload":
                    _UpdateGameInfo (key, dict);
                    return;
                default:
                    _UpdateGameInfo (key, dict);
                    return;
            }
        }

        public string GetDataFromTGPA (string key, string value)
        {
            return _GetDataFromTGPA (key, value);
        }

        public string GetOptCfgStr ()
        {
            return _GetOptCfgStr ();
        }

        public string CheckDeviceIsReal ()
        {
            return _CheckDeviceIsReal ();
        }

        public int GetCurrentThreadTid ()
        {
            return _GetCurrentThreadTid ();
        }

        public void ReportUserInfo (Dictionary<string, string> dict)
        {
            _ReportUserInfo (dict);
        }

        #endregion

        #region TGPAService Private Definition
        private TGPAServiceAndroid ()
        {
#if UNITY_ANDROID
            IsOK = _CheckSdk ();
#endif
        }

#if UNITY_ANDROID
        private bool IsOK = false; // sdk是否正常合入
        private bool CanWork = false; // 对于一些频繁调用的接口，若sdk内部功能没有开启，C#层面不要再调用方法到Java层
        private AndroidJavaObject performanceAjuster;
        private float[] fpsArr = new float[5];
        private float lastFps = 0;
        private int fpsCount = 0;
#endif

        // 初始化检查sdk是否正常合入
        private bool _CheckSdk ()
        {
#if UNITY_ANDROID
            performanceAjuster = new AndroidJavaObject ("com.tencent.kgvmp.PerformanceAdjuster");
            if (performanceAjuster == null)
            {
                Debug.Log ("TGPA main class PerformanceAdjuster get failed.");
                return false;
            }
            return true;
#else
            return false;
#endif
        }

        private int _GetVersionCode ()
        {
#if UNITY_ANDROID
            if (IsOK) { return performanceAjuster.Call<int> ("getVersionCode"); }
#endif
            return 0;
        }

        private void _EnableLog (bool enable)
        {
#if UNITY_ANDROID
            if (IsOK) { performanceAjuster.Call ("setLogAble", enable); }
#endif
        }

        private void _InitSdk ()
        {
#if UNITY_ANDROID
            if (IsOK)
            {
                performanceAjuster.Call ("updateGameInfo", "GPU", SystemInfo.graphicsDeviceName);
                performanceAjuster.Call ("initForUnity");
                CanWork = performanceAjuster.Call<bool> ("checkSdkCanWork");
            }
#endif
        }

        // 注册回调接口
        private void _RegisterCallback (ITGPACallback callback)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                GameObject tgpaGameObj = new GameObject("TGPAGameObject");
                tgpaGameObj.AddComponent<TGPAGameComponent>();
                GameObject.DontDestroyOnLoad(tgpaGameObj);
                TGPAGameComponent tgpaComponent = (TGPAGameComponent)tgpaGameObj.GetComponent(typeof(TGPAGameComponent));
                tgpaComponent.setGameCallback(callback);
                performanceAjuster.Call ("registerCallbackForUnity");
            }
#endif
        }

        private void _UpdateGameFps (float fps)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                if (CheckGameFps (fps))
                {
                    performanceAjuster.Call ("updateGameInfo", (int) GameDataKey.FPS, (int) fps);
                }
                fpsCount += 1;
                fpsArr[fpsCount - 1] = fps;
                if (fpsCount == 5)
                {
                    performanceAjuster.Call ("updateGameInfo", (int) GameDataKey.FPS, fpsArr);
                    fpsCount = 0;
                }
            }
#endif
        }

        // 发送游戏的信息
        private void _UpdateGameInfo (GameDataKey key, string value)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                performanceAjuster.Call ("updateGameInfo", (int) key, value);
            }
#endif
        }

        private void _UpdateGameInfo (int key, string value)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                performanceAjuster.Call ("updateGameInfo", key, value);
            }
#endif
        }

        private void _UpdateGameInfo (int key, int value)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                performanceAjuster.Call ("updateGameInfo", key, value);
            }
#endif
        }

        // 发送游戏的信息
        private void _UpdateGameInfo (Dictionary<GameDataKey, string> dict)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                if (dict != null && dict.Count > 0)
                {
                    string data = convertDict2JsonStr (dict);
                    performanceAjuster.Call ("updateGameInfo", "MultiGameData", data);
                }
            }
#endif
        }

        private void _UpdateGameInfo (string key, string value)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                performanceAjuster.Call ("updateGameInfo", key, value);
            }
#endif
        }

        private void _UpdateGameInfo (GameDataKey key, int value)
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                performanceAjuster.Call ("updateGameInfo", (int) key, value);
            }
#endif
        }
        private void _UpdateGameInfo (string key, Dictionary<string, string> dict)
        {
#if UNITY_ANDROID
            if (IsOK)
            {
                if (dict != null && dict.Count > 0)
                {
                    string data = convertDict2JsonStr (dict);
                    performanceAjuster.Call ("updateGameInfo", key, data);
                }
            }
#endif
        }

        private string _GetDataFromTGPA (string key, string value)
        {
#if UNITY_ANDROID
            if (IsOK)
            {
                return performanceAjuster.Call<string> ("getDataFromTGPA", key, value);
            }
#endif
            return "-1";
        }

        private string _CheckDeviceIsReal ()
        {
#if UNITY_ANDROID
            if (IsOK)
            {
                return performanceAjuster.Call<string> ("checkDeviceIsReal");
            }
#endif
            return "{\"result\": 0}";
        }

        private string _GetOptCfgStr ()
        {
#if UNITY_ANDROID
            if (IsOK && CanWork)
            {
                return performanceAjuster.Call<string> ("getOptCfgStr");
            }
#endif
            return "-1";
        }

        private int _GetCurrentThreadTid ()
        {
#if UNITY_ANDROID
            if (IsOK)
            {
                return performanceAjuster.Call<int> ("getCurrentThreadTid");
            }
#endif
            return -1;
        }

        private void _ReportUserInfo (Dictionary<string, string> dict)
        {
#if UNITY_ANDROID 
            if (IsOK)
            {
                if (dict != null && dict.Count > 0)
                {
                    string data = convertDict2JsonStr (dict);
                    performanceAjuster.Call ("updateGameInfo", "DeviceBind", data);
                }
            }
#endif
        }

        private string convertDict2JsonStr (Dictionary<GameDataKey, string> dict)
        {
            string result = "{";
            foreach (KeyValuePair<GameDataKey, string> pair in dict)
            {
                result += string.Format ("\"{0}\":\"{1}\",", (int)pair.Key, pair.Value);
            }
            return result.TrimEnd (',') + "}";
        }

        private string convertDict2JsonStr (Dictionary<string, string> dict)
        {
            string result = "{";
            foreach (KeyValuePair<string, string> pair in dict)
            {
                result += string.Format ("\"{0}\":\"{1}\",", pair.Key, pair.Value);
            }
            return result.TrimEnd (',') + "}";
        }

        // 30帧模式下： fps存在大于等于30的情况，此时当前fps level和上次fps level如果都大于等于9，则无需发送fps给厂商
        // 60帧模式下：fps存在大于等于60的情况，当前fps level和上次fps level如果都大于等于11，则无需发送fps给厂商
        // 返回true，则发送fps数据给厂商；返回false，则不发送
#if UNITY_ANDROID
        // 帧率上下波动超过3帧，就通知给厂商
        private bool CheckGameFps (float fps)
        {
            bool result = false;
            if (System.Math.Abs (fps - lastFps) >= 3)
            {
                result = true;
            }
            lastFps = fps;
            return result;
        }
#endif
        #endregion
    }
    #endregion
}