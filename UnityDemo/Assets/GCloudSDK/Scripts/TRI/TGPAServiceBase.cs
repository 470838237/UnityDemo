// ------------------------------------------------------------------------
// 
// File: TGPAServiceBase
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
    #region TGPAServiceBase Definition
    public class TGPAServiceBase : ITGPAService
    {
        #region TGPAServiceBase Public Definition
        private static readonly TGPAServiceBase mInstance = new TGPAServiceBase ();

        public static TGPAServiceBase GetInstance ()
        {
            return mInstance;
        }

        private TGPAServiceBase () { }

        public int GetVersionCode ()
        {
            return 0;
        }
        public void EnableLog (bool enable) { }

        public void Init () { }

        public void RegisterCallback (ITGPACallback callback) { }

        public void UpdateGameFps (float value) { }

        public void UpdateGameInfo (GameDataKey key, int value) { }

        public void UpdateGameInfo (GameDataKey key, string value) { }

        public void UpdateGameInfo (Dictionary<GameDataKey, string> dict) { }

        public void UpdateGameInfo (string key, string value) { }

        public void UpdateGameInfo (string key, Dictionary<string, string> dict) { }

        public string GetDataFromTGPA (string key, string value) { return "-1"; }

        public string CheckDeviceIsReal ()
        {
            return "{\"result\":0}";
        }

        public string GetOptCfgStr () { return "-1"; }

        public int GetCurrentThreadTid ()
        {
            return -1;
        }

        public void ReportUserInfo (Dictionary<string, string> dict) { }
        #endregion
    }
    #endregion
}