//------------------------------------------------------------------------------
//
// File: MSDKCrash
// Module: MSDK
// Date: 2020-03-20
// Hash: 499111872060714f7ca5265308bf77d7
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;


namespace GCloud.MSDK
{
    public enum MSDKCrashLevel
    {
        BuglyLogLevelSilent = 0, //关闭日志记录功能
        BuglyLogLevelError = 1,
        BuglyLogLevelWarn = 2,
        BuglyLogLevelInfo = 3,
        BuglyLogLevelDebug = 4,
        BuglyLogLevelVerbose = 5,
    }

    public class MSDKCrash
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void logInfoAdapter(int level, [MarshalAs(UnmanagedType.LPStr)] string tag,
            [MarshalAs(UnmanagedType.LPStr)] string log);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void setUserValueAdapter([MarshalAs(UnmanagedType.LPStr)] string k,
            [MarshalAs(UnmanagedType.LPStr)] string v);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void reportExceptionAdapter(int type, [MarshalAs(UnmanagedType.LPStr)] string exceptionName,
            [MarshalAs(UnmanagedType.LPStr)] string exceptionMsg, [MarshalAs(UnmanagedType.LPStr)] string exceptionStack,
            [MarshalAs(UnmanagedType.LPStr)] string paramsJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void unityCrashCallback();

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void setUserIdAdapter([MarshalAs(UnmanagedType.LPStr)] string userId);

        /// <summary>
        /// Crash回调方法，提供上报用户数据能力
        /// </summary>
        public static event OnMSDKStringRetEventHandler<MSDKBaseRet> CrashBaseRetEvent;


        /// <summary>
        /// 自定义日志打印接口,用于记录一些关键的业务调试信息, 可以更全面地反应APP发生崩溃或异常的上下文环境.
        /// </summary>
        /// <param name="level">日志级别，0-silent, 1-error,2-warning，3-inifo，4-debug，5-verbose</param>
        /// <param name="tag">日志模块分类</param>
        /// <param name="log">日志内容</param>
        public static void LogInfo(MSDKCrashLevel level, string tag, string log)
        {
            try
            {
                int intLevel = (int)level;
                MSDKLog.Log("LogInfo  level=" + intLevel + " tag=" + tag + " log=" + log);
#if UNITY_EDITOR

#else
                logInfoAdapter (intLevel, tag, log);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("LogInfo with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 设置关键数据[键值对]，随崩溃信息上报
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        public static void SetUserValue(string k, string v)
        {
            try
            {
                MSDKLog.Log("SetUserValue  key=" + k + " value=" + v);
#if UNITY_EDITOR

#else
                setUserValueAdapter (k, v);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("SetUserValue with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void SetUserId(string userId)
        {
            try
            {
                MSDKLog.Log("SetUserId userId = " + userId);
                #if UNITY_EDITOR
                #else
                    setUserIdAdapter(userId);
                #endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("SetUserId with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void SetCrashCallback()
        {
#if UNITY_EDITOR

#else
            unityCrashCallback();
#endif
        }

        // Bugly unity 侧的初始化。使用反射解耦
        public static void InitCrash()
        {
            bool crashEnable = MSDKTools.GetConfig("CRASH_REPORT_ENABLE", false);
            if (!crashEnable) 
                return;
            string crashChannel = MSDKTools.GetConfig("CRASH_REPORT_CHANNEL", "Bulgy");
            string[] channels = crashChannel.Split(',');
            for (int i = 0; i < channels.Length; i++)
            {
                if (string.IsNullOrEmpty(channels[i])) continue;
                try
                {
                    // 开启SDK的日志打印，发布版本请务必关闭
                    var assembly = Assembly.GetExecutingAssembly();
                    var className = "GCloud.MSDK.MSDK"+channels[i]+"Crash";
                    var methodName = "Init";

                    UnityEngine.Debug.Log("Init Crash, channel= " + className);

                    var type = assembly.GetType(className, true, true);
                    if (type != null)
                    {
                        var methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
                        if (methodInfo != null)
                            methodInfo.Invoke(null, new object[] { });
                        else
                            MSDKLog.LogError("cannot get this method, methodName=" + methodName);

                    }
                    else
                    {
                        MSDKLog.LogError("current className is wrong,className=" + className);
                    }
                }
                catch (Exception e)
                {
                    MSDKLog.LogError("InitCrash failed " + e.StackTrace);
                }
            }
        }

        //Bugly 设置自动上报等级
        public static void SetAutoReportLogLevel(int level)
        {
            MSDKLog.Log("SetAutoReportLogLevel, level= " + Convert.ToString(level));
            bool crashEnable = MSDKTools.GetConfig("CRASH_REPORT_ENABLE", false);
            if (!crashEnable)
                return;
            string crashChannel = MSDKTools.GetConfig("CRASH_REPORT_CHANNEL", "Bulgy");
            string[] channels = crashChannel.Split(',');
            for (int i = 0; i < channels.Length; i++)
            {
                if (string.IsNullOrEmpty(channels[i])) continue;
                try
                {
                    // 开启SDK的日志打印，发布版本请务必关闭
                    var assembly = Assembly.GetExecutingAssembly();
                    var className = "GCloud.MSDK.MSDK" + channels[i] + "Crash";
                    var methodName = "SetAutoReportLogLevel";

                    UnityEngine.Debug.Log("Set report level, channel= " + className);

                    var type = assembly.GetType(className, true, true);
                    if (type != null)
                    {
                        var methodInfo = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
                        if (methodInfo != null)
                            methodInfo.Invoke(null, new object[] { level });
                        else
                            MSDKLog.LogError("cannot get this method, methodName= " + methodName);
                    }
                    else
                    {
                        MSDKLog.LogError("cannot get type, className= " + className + " methodName= " + methodName);
                    }
                }
                catch (Exception e)
                {
                    MSDKLog.LogError("SetAutoReportLogLevel failed " + e.StackTrace);
                }
            }
        }

        #region crash
        //callback
        internal static string OnCrashCallbackMessage(string json)
        {
            MSDKLog.Log("OnCrashCallbackMessage  json= " + json);
            if (CrashBaseRetEvent != null)
            {
                var ret = new MSDKBaseRet(json);
                try
                {
                    return CrashBaseRetEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for OnCrashCallbackMessage !");
            }
            return "";
        }

        internal static string OnCrashCallbackData(string json)
        {
            MSDKLog.Log("OnCrashCallbackData  json= " + json);
            if (CrashBaseRetEvent != null)
            {
                var ret = new MSDKBaseRet(json);
                try
                {
                    return CrashBaseRetEvent(ret);
                }
                catch (Exception e)
                {
                    MSDKLog.LogError(e.StackTrace);
                }
            }
            else
            {
                MSDKLog.LogError("No callback for OnCrashCallbackData !");
            }
            return "";
        }

        // Bugly 上报lua等堆栈信息
        public static void ReportException(int type, string exceptionName, string exceptionMsg,
            string exceptionStack, Dictionary<string, string> extInfo)
        {
            try
            {
                string paramsJson = MiniJSON.Json.Serialize(extInfo);
#if UNITY_EDITOR

#else
                reportExceptionAdapter (type, exceptionName, exceptionMsg, exceptionStack, paramsJson);
#endif
            }
            catch (Exception ex)
            {
                MSDKLog.LogError("ReportException with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        #endregion callback
#endif
    }
}