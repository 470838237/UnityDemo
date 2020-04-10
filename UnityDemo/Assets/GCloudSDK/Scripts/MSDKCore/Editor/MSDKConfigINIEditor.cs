using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MSDKConfigINI))]
public class MSDKConfigINIEditor : Editor {

    private MSDKConfigINI instance;

    public override void OnInspectorGUI()
    {
        instance = (MSDKConfigINI)target;

        CommonInfoGUI();
    }

    private static string BOOL_CONFIG = 
        "MSDK_DEBUG" +
        "MSDK_AUTOLOGIN_OFFLINE" +
        "MSDK_LIFECYCLE_SCHEME" +
        "WEBVIEW_IS_FULLSCREEN" +
        "WEBVIEW_PORTRAIT_BAR_HIDEABLE" +
        "WEBVIEW_LANDSCAPE_BAR_HIDEABLE" +
        "WEBVIEW_X5_CLOSE_ANDROID" +
        "WEBVIEW_X5_UPLOAD_ANDROID" +
        "WEBVIEW_USED_WKWEBVIEW_IOS" ;

    private void CommonInfoGUI()
    {
        List<string> lines = MSDKConfigINI.configLines;
        for (int i = 0; i < lines.Count;i++)
        {
            string line = lines[i];
            if(line.Contains("="))
            {
                string[] item = line.Split('=');
                string ckey = item[0].Trim();
                string cvalue = item[1].Trim();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(item[0]);
                if(ckey.Contains("ENABLE") || BOOL_CONFIG.Contains(ckey) )
                {
                    bool toggle = cvalue.Equals("1");
                    instance.SetUpLineValue(i, EditorGUILayout.Toggle(toggle));
                }
                else{
                    instance.SetUpLineValue(i, EditorGUILayout.TextField(cvalue));
                }
                EditorGUILayout.EndHorizontal();

            }
            else{
                EditorGUILayout.LabelField(line);
            }
        }
    }
}
