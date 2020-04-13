//------------------------------------------------------------------------------
//
// File: UnityEditorData
// Module: MSDK
// Date: 2019-12-24
// Hash: a20b2bd4300d6a74b4ba64c440cc5a2c
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.IO;

namespace GCloud.MSDK
{
	public class UnityEditorData
	{

		private static string dataFilePath
        {
            get{
                string msdkDataFilePath = "Assets/MSDK/Scripts/UnityEditor/Data/";
                string dir = Path.Combine(Application.dataPath, "GCloudSDK/Scripts/MSDKCore/MSDK");
                if (Directory.Exists(dir))
                {
                    msdkDataFilePath = "Assets/GCloudSDK/Scripts/MSDKCore/MSDK/UnityEditor/Data/";
                }
                return msdkDataFilePath;
            }
        }

		/// <summary>
		/// 获取模拟登录态
		/// </summary>
		/// <returns>The login data.</returns>
		/// <param name="channel">渠道</param>
		public static string GetLoginData (int methodId, string channel="",string subChannel="")
		{
			string filePath = dataFilePath + channel +subChannel+ "LoginData.txt";
			string data = GetData(filePath);
            if (!string.IsNullOrEmpty(channel) && !string.IsNullOrEmpty(data))
                WriteLoginData(data);
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    // 伪造登录，使用特殊Token，到后台校验，目前token是所有游戏一样，后续改造
                    MSDKLoginRet ret = new MSDKLoginRet(data);
                    ret.MethodNameId = methodId;
                    ret.Token = "MSDKLOGINMOCKER_51433213c2e72a6304fb805b10a2201d";
                    data = ret.ToString();
                }
                catch (System.Exception e)
                {
                    MSDKLog.Log("Wrong MSDKLoginRet data " + data);
                    MSDKLog.Log(e.StackTrace);
                }
            }

            return data;
        }

        /// <summary>
        /// 获取登出回调信息
        /// </summary>
        /// <returns>The logout data.</returns>
        public static string GetLogoutData()
        {
            string filePath = dataFilePath + "LogoutData.txt";
            return GetData(filePath);
        }

        private static string GetData(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            MSDKLog.Log("File Not Exist, " + filePath);
            File.Create(filePath);

            return string.Empty;
        }

        /// <summary>
        /// LoginData.txt 保存当前渠道的登录态
        /// GetLoginRet是从这个文件直接读取当前登录态
        /// </summary>
        /// <param name="data">Data.</param>
        private static void WriteLoginData(string data)
        {
            string filePath = dataFilePath + "LoginData.txt";
            if (!File.Exists(filePath)) File.Create(filePath);
            try
            {
                File.WriteAllText(filePath, data);
            }
            catch (System.Exception e)
            {
                MSDKLog.Log(e.StackTrace);
            }
        }

        const string AndroidConfigPath = "Plugins/Android/assets/MSDKConfig.ini";
        const string iOSConfigPath = "Plugins/iOS/GCloudSDK/MSDKCore/MSDKAppSetting.bundle/MSDKConfig.ini";

        /// <summary>
        /// 获取 MSDKConfig.ini 配置信息
        /// </summary>
        /// <returns>The config.</returns>
        /// <param name="key">Key.</param>
        public static string GetConfig(string key)
        {
            string configPath = Path.Combine(Application.dataPath, AndroidConfigPath);

#if UNITY_ANDROID
            configPath = Path.Combine(Application.dataPath, AndroidConfigPath);
            if (!File.Exists(configPath))
            {
                UnityEngine.Debug.LogError("There is no MSDKConfig.ini in Android SDK, path = " + AndroidConfigPath);
                return string.Empty;
            }
#elif UNITY_IPHONE || UNITY_IOS

            configPath = Path.Combine(Application.dataPath, iOSConfigPath);
            if (!File.Exists(configPath))
            {
                UnityEngine.Debug.LogError("There is no MSDKConfig.ini in iOS SDK, path = " + iOSConfigPath);
                return string.Empty;
            }
#endif
            string[] lines = File.ReadAllLines(configPath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("="))
                {
                    string[] item = lines[i].Split('=');
                    string ckey = item[0].Trim();
                    string cvalue = item[1].Trim();
                    if (ckey.Equals(key))
                    {
                        return cvalue;
                    }
                }
            }
            return string.Empty;
        }
	}
}
