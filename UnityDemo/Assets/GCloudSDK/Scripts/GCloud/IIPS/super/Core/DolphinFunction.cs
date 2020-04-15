using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace GCloud
{
    namespace Dolphin
    {
        class DolphinFunction
        {
            public static string GetCurrentVersionApkPath()
            {
#if UNITY_ANDROID
		    AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"); 
		    if(jc == null)
		    {
			    return null;
		    }
		    AndroidJavaObject m_jo = jc.GetStatic<AndroidJavaObject> ("currentActivity"); 
		    if(m_jo == null)
		    {
			    return null;
		    }
            AndroidJavaClass clazz = new AndroidJavaClass("com.tencent.gcloud.dolphin.CuIIPSMobile");
            if (clazz == null)
            {
                return null;
            }
            string apkpath = clazz.CallStatic<string>("GetApkAbsPath", m_jo);
		    if (apkpath == "error")
		    {
			    ADebug.LogError ("[ApolloUpdate] getpath failed");
			    return null;
		    }
		    else
		    {
			    ADebug.Log (string.Format("[ApolloUpdate] getpath success  path:{0}",apkpath));
			    return apkpath;
		    }
#else
                return null;
#endif
            }
            public static string GetCurrentVersionObbPath()
            {
#if UNITY_ANDROID
		    AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"); 
		    if(jc == null)
		    {
			    return null;
		    }
		    AndroidJavaObject m_jo = jc.GetStatic<AndroidJavaObject> ("currentActivity"); 
		    if(m_jo == null)
		    {
			    return null;
		    }
            AndroidJavaClass clazz = new AndroidJavaClass("com.tencent.gcloud.dolphin.CuIIPSMobile");
            if (clazz == null)
            {
                return null;
            }
            string apkpath = clazz.CallStatic<string>("GetObbAbsPath", m_jo);
		    if (apkpath == "error")
		    {
			    ADebug.LogError ("[ApolloUpdate] getpath failed");
			    return null;
		    }
		    else
		    {
			    ADebug.Log (string.Format("[ApolloUpdate] getpath success  path:{0}",apkpath));
			    return apkpath;
		    }
#else
                return null;
#endif
            }

            public static string GetCurrentVersionCode()
            {
#if UNITY_ANDROID
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                if (jc == null)
                {
                    return null;
                }
                AndroidJavaObject m_jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                if (m_jo == null)
                {
                    return null;
                }
                AndroidJavaClass clazz = new AndroidJavaClass("com.tencent.gcloud.dolphin.CuIIPSMobile");
                if (clazz == null)
                {
                    return null;
                }
                string apkpath = clazz.CallStatic<string>("getVersionCode", m_jo);
                if (apkpath == "error")
                {
                    ADebug.LogError("[ApolloUpdate] getVersionCode failed");
                    return null;
                }
                else
                {
                    ADebug.Log(string.Format("[ApolloUpdate] getVersionCode success  path:{0}", apkpath));
                    return apkpath;
                }
#else
                return null;
#endif
            }
            public static string GetCurrentBundleId()
            {
#if UNITY_ANDROID
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                if (jc == null)
                {
                    return null;
                }
                AndroidJavaObject m_jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                if (m_jo == null)
                {
                    return null;
                }
                AndroidJavaClass clazz = new AndroidJavaClass("com.tencent.gcloud.dolphin.CuIIPSMobile");
                if (clazz == null)
                {
                    return null;
                }
                string apkpath = clazz.CallStatic<string>("getBundleId", m_jo);
                if (apkpath == "error")
                {
                    ADebug.LogError("[ApolloUpdate] getBundleId failed");
                    return null;
                }
                else
                {
                    ADebug.Log(string.Format("[ApolloUpdate] getBundleId success  path:{0}", apkpath));
                    return apkpath;
                }
#else
                return null;
#endif
            }

            public static string GetFirstExtractIfsPath(bool bFirst_source_in_apk,bool bObb_type)
            {
#if UNITY_ANDROID
                string apkpath = null;
                if (bFirst_source_in_apk)
                {
                    
                    apkpath = GetCurrentVersionApkPath();//
                }
                else
                {
                    string versioncode = GetCurrentVersionCode();
                    string bundleid = GetCurrentBundleId();
                    if (versioncode == null || bundleid == null)
                    {
                        return null;
                    }
                    apkpath = GetCurrentVersionObbPath() +(bObb_type ? "/main.":"/patch.") + versioncode + "." + bundleid + ".obb";
                }
		           
                if (apkpath == null)
                    return null;

                // string configifspath = "apk://"+apkpath + "?assets/first_source.png";
                string configifspath = "apk://" + apkpath + "?assets/first_source.png";
                ADebug.Log (string.Format("configapkpath:{0}",configifspath));
		    return configifspath;
#elif UNITY_IOS
		    string configifspath = Application.streamingAssetsPath + "/first_source.png";
		    if(File.Exists(configifspath))
		    {
			    ADebug.Log (string.Format("ifs exist,configapkpath:{0}",configifspath));
		    }
		    else
		    {
			    ADebug.LogError (string.Format("ifs not exist,configapkpath:{0}",configifspath));
                return null;
		    }
		    return configifspath;
#else
                string configifspath = Application.dataPath + "/StreamingAssets/first_source.png";
                if (File.Exists(configifspath))
                {
                    ADebug.Log(string.Format("ifs exist,configapkpath:{0}", configifspath));
                }
                else
                {
                    ADebug.LogError(string.Format("ifs not exist,configapkpath:{0}", configifspath));
                    return null;
                }
                return configifspath;
#endif
            }
        }
    }
}
