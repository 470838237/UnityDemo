using UnityEngine;
using System.Collections;
namespace GCloud
{
    public class SDKName
    {
        public const string GCLOUD = "GCloud";
        public const string MSDK = "MSDK";
        public const string HTTPDNS =  "HttpDNS";
        public const string TGPA  = "TGPA";
        public const string APM   = "APM";
        public const string GEM  =  "GEM";
        public const string TSS  =  "TSS";
        public const string GVOICE = "GVoice";
        public const string TDM   =  "TDM";
        public const string QROBOT = "GRobot"; 
    }


    public class GCloudCoreCommon
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_STANDALONE_OSX
        public const string PluginName = "gcloud";
#else
        
#if UNITY_IPHONE || UNITY_XBOX360
        // On iOS and Xbox 360 plugins are statically linked into
        // the executable, so we have to use __Internal as the
        // library name
        public const string PluginName = "__Internal";
#else
        // Other platforms load plugins dynamically, so pass the name
        // of the plugin's dynamic library.
        public const string PluginName = "gcloudcore";
#endif
        
#endif
    }
}
