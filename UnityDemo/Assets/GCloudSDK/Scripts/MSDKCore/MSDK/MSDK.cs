//------------------------------------------------------------------------------
//
// File: MSDK
// Module: MSDK
// Date: 2020-03-31
// Hash: 3b864dc88c6b774505301a36dcb83995
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using UnityEngine;

namespace GCloud.MSDK
{
    #region MSDK
	/// <summary>
	/// 回调的范型
	/// </summary>
	public delegate void OnMSDKRetEventHandler<T> (T ret);
	public delegate string OnMSDKStringRetEventHandler<T> (T ret);

    public class MSDK
    {
#if GCLOUD_MSDK_WINDOWS
        public const string LibName = "MSDKUnityAdapter";
#elif UNITY_ANDROID
		public const string LibName = "MSDKUnityAdapter";
#else
        public const string LibName = "__Internal";
#endif
        private static bool initialized;

        public static bool isDebug = true;

		/// <summary>
		/// MSDK init，游戏开始的时候设置
		/// </summary>
        public static void Init()
		{
            if (initialized) return;
            initialized = true;

            if (isDebug)
                MSDKLog.SetLevel(MSDKLog.Level.Log);
            else
                MSDKLog.SetLevel(MSDKLog.Level.Error);
            
			MSDKMessageCenter.Instance.Init();
			
#if GCLOUD_MSDK_WINDOWS

#elif UNITY_EDITOR || UNITY_STANDALONE_WIN

#else
            MSDKCrash.InitCrash();
#endif
			MSDKLog.Log ("MSDK initialed !");
        }

#if GCLOUD_MSDK_WINDOWS
		/// <summary>
		/// MSDK init，游戏关闭的时候调用
		/// </summary>
        public static void UnInstall()
		{
			MSDKLogin.UnInstall();
			MSDKLog.Log ("MSDK UnInstall");
		}
#endif
    }

    #endregion
}