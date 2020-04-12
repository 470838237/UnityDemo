using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class MSDKEditorTools : ScriptableObject
{
    [MenuItem("MSDK/MSDKConfig.ini Settings")]
    public static void setUpMSDKConfig()
    {
        Selection.activeObject = MSDKConfigINI.Instance;
    }

    [MenuItem("MSDK/MSDK SDK Reference")]
    public static void gotoMSDKWiki()
    {
        string url = "http://docs.msdk.qq.com";

        Application.OpenURL(url);
    }
}
