// ------------------------------------------------------------------------
// 
// File: TGPAHelper
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
    //----------------------- TGPA 提供给游戏侧调用的接口 --------------------------
    public class TGPAHelper
    {
        #region TGPAHelper Definition

        private static ITGPAService getPlatformService ()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return TGPAServiceAndroid.GetInstance ();
#elif UNITY_IOS && !UNITY_EDITOR
            return TGPAServiceiOS.GetInstance ();
#else
            return TGPAServiceBase.GetInstance ();
#endif
        }

        public static int GetVersionCode ()
        {
            return 0;
        }
        public static void EnableLog (bool enable)
        {
            getPlatformService ().EnableLog (enable);
        }

        public static void Init ()
        {
            getPlatformService ().Init ();
        }

        public static void RegisterCallback (ITGPACallback callback)
        {
            getPlatformService ().RegisterCallback (callback);
        }

        public static void UpdateGameFps (float value)
        {
            getPlatformService ().UpdateGameFps (value);
        }

        public static void UpdateGameInfo (GameDataKey key, string value)
        {
            getPlatformService ().UpdateGameInfo (key, value);
        }

        public static void UpdateGameInfo (string key, string value)
        {
            getPlatformService ().UpdateGameInfo (key, value);
        }

        public static void UpdateGameInfo (Dictionary<GameDataKey, string> dict)
        {
            getPlatformService ().UpdateGameInfo (dict);
        }

        public static void UpdateGameInfo (GameDataKey key, int value)
        {
            getPlatformService ().UpdateGameInfo (key, value);
        }
        public static void UpdateGameInfo (string key, Dictionary<string, string> dict)
        {
            getPlatformService ().UpdateGameInfo (key, dict);
        }

        public static string GetOptCfgStr ()
        {
            return getPlatformService ().GetOptCfgStr ();
        }

        public static string GetDataFromTGPA (string key, string value)
        {
            return getPlatformService ().GetDataFromTGPA (key, value);
        }

        public static string CheckDeviceIsReal ()
        {
            return getPlatformService ().CheckDeviceIsReal ();
        }

        public static void ReportUserInfo (Dictionary<string, string> dict)
        {
            getPlatformService ().ReportUserInfo (dict);
        }

        public static int GetCurrentThreadTid ()
        {
            return getPlatformService ().GetCurrentThreadTid ();
        }
        #endregion
    }
}