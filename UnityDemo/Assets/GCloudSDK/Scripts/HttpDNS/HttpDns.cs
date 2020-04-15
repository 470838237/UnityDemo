using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GCloud.HttpDns
{
    class HttpDns
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private static Thread mMainThread = Thread.CurrentThread;
        private static bool mIsMainThread
        {
            get
            {
                return Thread.CurrentThread == mMainThread;
            }
        }

        private static AndroidJavaObject mHttpDnsObj = null;
#endif

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string WGGetHostByName(string domain);
        [DllImport("__Internal")]
        private static extern bool WGSetDnsOpenId(string dnsOpenId);
        [DllImport("__Internal")]
        private static extern bool WGSetInitParams(string dnsAppId, bool enableLog, int timeOut);
#endif

        public static void Init(string appId, bool debug, int timeout)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            if (unityPlayerClass == null)
            {
                Debug.Log("unityPlayerClass == null");
                return;
            }
            AndroidJavaObject currentActivityObj = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            if (currentActivityObj == null)
            {
                Debug.Log("currentActivityObj == null");
                return;
            }
            mHttpDnsObj = new AndroidJavaObject("com.tencent.gcloud.httpdns.HttpDnsService");
            if (mHttpDnsObj == null)
            {
                Debug.Log("mHttpDnsObj == null");
                return;
            }
            mHttpDnsObj.Call("init", currentActivityObj, appId, debug, timeout);
#endif
#if UNITY_IOS && !UNITY_EDITOR
            WGSetInitParams(appId, debug, timeout);
#endif
        }

        public static bool SetOpenId(string openId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return mHttpDnsObj.Call<bool>("setOpenId", openId);
#else
#if UNITY_IOS && !UNITY_EDITOR
            return WGSetDnsOpenId (openId);
#endif
            return false;
#endif
        }

        public static string GetAddrByName(string domain)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!mIsMainThread && AndroidJNI.AttachCurrentThread() != 0)
            {
                return null;
            }
            string ips = mHttpDnsObj.Call<string>("getAddrByName", domain);
            if (!mIsMainThread)
            {
                AndroidJNI.DetachCurrentThread();
            }
            return ips;
#else
#if UNITY_IOS && !UNITY_EDITOR
            return WGGetHostByName (domain);
#endif
            return null;
#endif
        }
    }
}
