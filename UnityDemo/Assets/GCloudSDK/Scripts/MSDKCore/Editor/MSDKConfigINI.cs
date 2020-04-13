#define MSDK_ENV

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class MSDKConfigINI : ScriptableObject
{
    public const string MSDKConfigAssetName = "MSDKConfig.ini";

    public static string MSDKConfigPath = "MSDK/Editor/Resources";
    //static string MSDKConfigPath = "GCloudSDK/Editor/MSDKCore/Resources";

    private static MSDKConfigINI _instance;

    public static MSDKConfigINI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load(MSDKConfigAssetName) as MSDKConfigINI;
                if (_instance == null)
                {
                    _instance = CreateInstance<MSDKConfigINI>();

                    string properPath = Path.Combine(Application.dataPath, MSDKConfigPath);
                    if (!Directory.Exists(properPath))
                    {
                        Directory.CreateDirectory(properPath);
                    }

                    string fullPath = Path.Combine(Path.Combine("Assets", MSDKConfigPath), MSDKConfigAssetName + ".asset" );
                    if(!File.Exists(fullPath))
                        AssetDatabase.CreateAsset(_instance, fullPath);
                }
            }
            return _instance;
        }
    }

    void OnEnable()
    {
        string dir = Path.Combine(Application.dataPath, MSDKConfigPath);
        if (!Directory.Exists(dir))
        {
            MSDKConfigPath = "GCloudSDK/Scripts/MSDKCore/Editor/Resources";
            dir = Path.Combine(Application.dataPath, MSDKConfigPath);

            if (!Directory.Exists(dir))
            {
                Debug.LogError("Wrong SDK Directory, please check SDK, or contact with 连线MSDK.");
            }
        }
#if UNITY_5_3_OR_NEWER
        Selection.selectionChanged -= OnSelectionChange;
        Selection.selectionChanged += OnSelectionChange;
#endif
    }

    void OnSelectionChange()
    {
        if(Selection.activeObject !=null && (Selection.activeObject.GetType() == typeof(MSDKConfigINI)))
            ReadConfigFile();
    }

    public static List<string> configLines = new List<string>();

    static bool hasAndroidConfigFile = true;
    static bool hasIOSConfigFile = true;

    const string AndroidConfigPath = "Plugins/Android/assets/MSDKConfig.ini";
    const string iOSConfigPath = "Plugins/iOS/GCloudSDK/MSDKCore/MSDKAppSetting.bundle/MSDKConfig.ini";

    public static void ReadConfigFile()
    {
        configLines.Clear();

        string configPath = Path.Combine(Application.dataPath, AndroidConfigPath);
        if(!File.Exists(configPath))
        {
            UnityEngine.Debug.LogError("There is no MSDKConfig.ini in Android SDK, path = " + AndroidConfigPath);
            hasAndroidConfigFile = false;
        }

        configPath = Path.Combine(Application.dataPath, iOSConfigPath);
        if (!File.Exists(configPath))
        {
            UnityEngine.Debug.LogError("There is no MSDKConfig.ini in iOS SDK, path = " + iOSConfigPath);
            hasIOSConfigFile = false;
        } 

        configPath = Path.Combine(Application.dataPath, AndroidConfigPath);
        string[] lines = File.ReadAllLines(configPath);
        for (int i = 0; i < lines.Length; i++)
        { 
            configLines.Add(lines[i]);
        }
    }

    public void SetUpLineValue(int index, string newValue)
    {
        string line = configLines[index];
        string[] item = line.Split('=');
        newValue = newValue.Trim();
        string cvalue = item[1].Trim();
        if(!cvalue.Equals(newValue))
        {
            configLines[index] = item[0] + " = " + newValue;
            if(hasAndroidConfigFile)
            {
                string fileFullPath = Path.Combine(Application.dataPath, AndroidConfigPath);
                File.WriteAllLines(fileFullPath, configLines.ToArray());
            }
            if(hasIOSConfigFile)
            {
                string fileFullPath = Path.Combine(Application.dataPath, iOSConfigPath);
                File.WriteAllLines(fileFullPath, configLines.ToArray());
            }
        }
    }

    public void SetUpLineValue(int index, bool newValue)
    {
        string newValueStr = newValue ? "1" : "0";
        SetUpLineValue(index, newValueStr);
    }

}
