// ------------------------------------------------------------------------
// 
// File: TGPAServiceiOS
// Module: TGPA
// Version: 1.0
// Author: zohnzliu
// Modifyed: 2019/06/10
// 
// ------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace GCloud.TGPA
{
    //----------------------- TGPA 内部逻辑实现，游戏侧无需关注 --------------------------
    //----------------- iOS平台 ------------------------
    public class TGPAServiceiOS : ITGPAService
    {
        #region TGPAServiceiOS Public Definition
        private static TGPAServiceiOS mInstance;
        private static object _lock = new object ();

        public static TGPAServiceiOS GetInstance ()
        {
            if (mInstance == null)
            {
                lock (_lock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new TGPAServiceiOS ();
                    }
                }
            }
            return mInstance;
        }

        private TGPAServiceiOS () { }

#if UNITY_IOS
        private bool CanWork = false;
        private float fpsTotal = 0;
        private int fpsCount = 0;
#endif
        public int GetVersionCode ()
        {
#if UNITY_IOS
            return _GetVersionCode ();
#else
            return 0;
#endif
        }

        public void EnableLog (bool enable)
        {
#if UNITY_IOS
            _SetLogAble (enable);
            if (enable) {
                _EnableDebugMode ();
            }
#endif
        }

        public void Init ()
        {
#if UNITY_IOS
            _InitTGPA ();
            CanWork = _CheckSdkCanWork ();
#endif
        }

#if UNITY_IOS
        private static ITGPACallback mCallback;

        delegate void GameCallbackDelegate (int retCode);
        [MonoPInvokeCallback (typeof (GameCallbackDelegate))]
        public static void _OnCallback (int retCode)
        {
            if (mCallback != null && retCode == 2)
            {
                mCallback.notifySystemInfo ("{\"1\":\"2\",\"4\":\"2\"}");
            }
        }
#endif

        public void RegisterCallback (ITGPACallback callback)
        {
#if UNITY_IOS
            mCallback = callback;
            if (CanWork)
            {
                _RegisterCallback (_OnCallback);
            }
#endif
        }

        public void UpdateGameFps (float value)
        {
#if UNITY_IOS
            if (CanWork)
            {
                fpsTotal += value;
                fpsCount += 1;
                if (fpsCount == 5)
                {
                    _UpdateGameInfoIF ((int) GameDataKey.FPS, fpsTotal / 5);
                    fpsCount = 0;
                    fpsTotal = 0;
                }

            }
#endif
        }

        private bool checkGameDataNeedSend (GameDataKey key)
        {
            switch (key)
            {
                case GameDataKey.MainVersion:
                case GameDataKey.SubVersion:
                case GameDataKey.Scene:
                case GameDataKey.FPSTarget:
                case GameDataKey.PictureQuality:
                case GameDataKey.EffectQuality:
                case GameDataKey.Resolution:
                case GameDataKey.RoleCount:
                case GameDataKey.NetDelay:
                case GameDataKey.RoleOutline:
                case GameDataKey.MTR:
                case GameDataKey.SceneType:
                case GameDataKey.PictureStyle:
                case GameDataKey.AntiAliasing:
                case GameDataKey.Shadow:
                    return true;
                default:
                    return false;
            }
        }

        public void UpdateGameInfo (GameDataKey key, string value)
        {
#if UNITY_IOS
            if (CanWork && checkGameDataNeedSend (key))
            {
                UpdateGameInfo ((int) key, value);
            }
#endif
        }

        public void UpdateGameInfo (Dictionary<GameDataKey, string> dict)
        {
#if UNITY_IOS
            if (CanWork)
            {
                if (dict.ContainsKey (GameDataKey.OpenID))
                {
                    UpdateGameInfo ("OpenID", dict[GameDataKey.OpenID]);
                }
                foreach (KeyValuePair<GameDataKey, string> pair in dict)
                {
                    UpdateGameInfo (pair.Key, pair.Value);
                }
            }
#endif
        }

        public void UpdateGameInfo (int key, string value)
        {
#if UNITY_IOS
            if (CanWork)
            {
                _UpdateGameInfoIS (key, value);
            }
#endif
        }

        public void UpdateGameInfo (string key, string value)
        {
#if UNITY_IOS
            if (CanWork)
            {
                _UpdateGameInfoSS (key, value);
            }
#endif
        }

        public void UpdateGameInfo (GameDataKey key, int value)
        {
#if UNITY_IOS
            if (CanWork && checkGameDataNeedSend (key))
            {
                _UpdateGameInfoII ((int) key, value);

            }
#endif
        }

        public void UpdateGameInfo (string key, Dictionary<string, string> dict)
        {
            // do not need
        }

        public string GetDataFromTGPA(string key, string value)
        {
            return "-1";
        }

        public string CheckDeviceIsReal ()
        {
            // do not need
            return "{\"result\": 0}";
        }

        public string GetOptCfgStr () { return "-1"; }

        public int GetCurrentThreadTid ()
        {
            // do not need
            return -1;
        }

        public void ReportUserInfo (Dictionary<string, string> dict)
        {
            // do not need
        }

#if UNITY_IOS

        [DllImport ("__Internal")]
        private static extern int _GetVersionCode ();

        [DllImport ("__Internal")]
        private static extern string _GetVersionName ();

        [DllImport ("__Internal")]
        private static extern void _InitTGPA ();

        [DllImport ("__Internal")]
        private static extern bool _CheckSdkCanWork ();

        [DllImport ("__Internal")]
        private static extern void _RegisterCallback (GameCallbackDelegate callback);

        [DllImport ("__Internal")]
        private static extern void _SetLogAble (bool enable);

        [DllImport ("__Internal")]
        private static extern void _EnableDebugMode ();

        [DllImport ("__Internal")]
        private static extern void _UpdateGameInfoII (int key, int value);

        [DllImport ("__Internal")]
        private static extern void _UpdateGameInfoIS (int key, string value);

        [DllImport ("__Internal")]
        private static extern void _UpdateGameInfoSS (string key, string value);

        [DllImport ("__Internal")]
        private static extern void _UpdateGameInfoIF (int key, float value);

        [DllImport ("__Internal")]
        private static extern void _PostMatchFPSData (int size, float[] fpsArr);

#endif
        #endregion
    }
}