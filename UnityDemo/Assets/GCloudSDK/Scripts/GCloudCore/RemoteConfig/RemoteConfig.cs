using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Reflection;

using System.Text;



namespace GCloud
{

    //delegate void OnConfigureRefreshedDelegate(IntPtr intPtr);
    public class RemoteConfig : IRemoteConfig
    {
        //public event OnRemoteConfigRefreshed RemoteConfigRefreshedEvent;

        public static readonly IRemoteConfig Instance = new RemoteConfig();

        private RemoteConfig()
        {
            //abase_remoteconfig_SetCallback(OnConfigureRefreshed);
        }


        public Int64 GetLong(String key, Int64 defaultValue)
        {
            return abase_remoteconfig_GetLong(key, defaultValue);
        }
        public String GetString(String key, String defaultValue)
        {
            StringBuilder strBuilder = new StringBuilder(1280);
            Boolean result = abase_remoteconfig_GetString(key, strBuilder, 1280, defaultValue);
            string str = strBuilder.ToString();
            if (result == true && str.Length > 0)
            {
                return str;
            }
            return "";
        }
        public Boolean GetBool(String key, Boolean defaultValue)
        {
            return abase_remoteconfig_GetBool(key, defaultValue);
        }

        public Int32 GetInt(String key, Int32 defaultValue)
        {
            return abase_remoteconfig_GetInt(key, defaultValue);
        }

        public Double GetDouble(String key, Double defaultValue) 
        {
            return abase_remoteconfig_GetDouble(key, defaultValue);
        }


        //[MonoPInvokeCallback(typeof(OnConfigureRefreshedDelegate))]
        //static void OnConfigureRefreshed(IntPtr intPtr)
        //{
        //    string str = Marshal.PtrToStringAnsi(intPtr);
        //    Debug.Log("str : " + str);
        //}

        #region Dllimport
        //[DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        //private static extern void abase_remoteconfig_SetCallback([MarshalAs(UnmanagedType.FunctionPtr)]OnConfigureRefreshedDelegate callback);


        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int64 abase_remoteconfig_GetLong(string key, Int64 defaultValue);

        [DllImport(GCloudCoreCommon.PluginName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Boolean abase_remoteconfig_GetString(string key, [MarshalAs(UnmanagedType.LPStr)]StringBuilder value, int len, string defaultValue);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Boolean abase_remoteconfig_GetBool(string key, Boolean defaultValue);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 abase_remoteconfig_GetInt(string key, Int32 defaultValue);

        [DllImport(GCloudCoreCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern Double abase_remoteconfig_GetDouble(string key, Double defaultValue);
        #endregion


    }
}
