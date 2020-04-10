using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GCloud.MSDK
{
    public class MSDKBuglyCrash
    {
        public static void Init()
        {
#if UNITY_ANDROID
            BuglyAgent.ConfigCrashReporter(1, 1);
#endif
            BuglyAgent.ConfigDebugMode(MSDK.isDebug);
            BuglyAgent.EnableExceptionHandler();

            MSDKLog.Log("InitCrash of Bugly finished");

        }

        public static void SetAutoReportLogLevel(int level)
        {
            try
            {
                if (level == 5) //BuglyLogLevelVerbose
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.Log);
                }
                else if (level == 4) //BuglyLogLevelDebug
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogDebug);
                }
                else if (level == 3) //BuglyLogLevelInfo
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogInfo);
                }
                else if (level == 2) //BuglyLogLevelWarn
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogWarning);
                }
                else if (level == 1) //BuglyLogLevelError
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogError);
                }
                else if (level == 0) //BuglyLogLevelSilent
                {
                    BuglyAgent.ConfigAutoReportLogLevel(LogSeverity.LogException);
                    BuglyAgent.ConfigDebugMode(false);
                }
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("SetAutoReportLogLevel with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
