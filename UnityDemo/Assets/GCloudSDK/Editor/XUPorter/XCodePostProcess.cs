using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using GCloudSDKEditor.XCodeEditor;

// // Add AssociatedDomains
// #if UNITY_2017_1_OR_NEWER && UNITY_IOS
// using UnityEditor.iOS.Xcode;
// #endif

#endif


using System.IO;
namespace GCloudSDKEditor
{

#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build;

class MyCustomBuildProcessor : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        string[] cmetas = Directory.GetFiles(Application.dataPath, "Crashlytics.framework.meta", SearchOption.AllDirectories);
        foreach (string file in cmetas)
        {
            XClass xclass = new XClass(file);
            xclass.Replace("AddToEmbeddedBinaries: true", "AddToEmbeddedBinaries: false");
            UnityEngine.Debug.Log("framework.meta File: " + file);
        }

        string[] fmetas = Directory.GetFiles(Application.dataPath, "Fabric.framework.meta", SearchOption.AllDirectories);
        foreach (string file in fmetas)
        {
            XClass xclass = new XClass(file);
            xclass.Replace("AddToEmbeddedBinaries: true", "AddToEmbeddedBinaries: false");
            UnityEngine.Debug.Log("framework.meta File: " + file);
        }
        AssetDatabase.Refresh();
    }
}
#endif

public static class XCodePostProcess
{
	static List<string> pngs = new List<string> ();

	#if UNITY_EDITOR
	[PostProcessBuild (999)]
	public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)
	{
		Debug.Log("OnPostProcessBuild pathToBuiltProject: " + pathToBuiltProject);
		#if UNITY_5_3_OR_NEWER
		if (target != BuildTarget.iOS) {
			Debug.LogWarning ("Target is not iPhone. XCodePostProcess will not run");
			return;
		}
		#else
		if (target != BuildTarget.iPhone) {
			Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
			return;
		}
		#endif
		// Create a new project object from build target
		XCProject project = new XCProject (pathToBuiltProject);

		// Find and run through all projmods files to patch the project.
		// Please pay attention that ALL projmods files in your project folder will be excuted!
		// string[] files = Directory.GetFiles( Application.dataPath, "*.projmods", SearchOption.AllDirectories );
		string modsRootFolderPath = System.IO.Path.Combine (Application.dataPath, "GCloudSDK");
		if(!Directory.Exists (modsRootFolderPath)) {
			modsRootFolderPath = Application.dataPath;
		}
		Debug.Log ("modsRootFolderPath is " + modsRootFolderPath);
		string[] files = Directory.GetFiles( modsRootFolderPath, "*.projmods", SearchOption.AllDirectories ); //隔离，只扫描GCloudSDK目录下的projmods
		foreach( string file in files ) {
			UnityEngine.Debug.Log("ProjMod File: "+file);
			project.ApplyMod( file );
		}

		// edit code
        EditorCode(pathToBuiltProject);

		// Finally save the xcode project
		project.Save();

		// //调整Xcode 9的 Scheme中GPU Scheme capture为open GL
		// String xml = File.ReadAllText (pathToBuiltProject + "/Unity-iPhone.xcodeproj/xcshareddata/xcschemes/Unity-iPhone.xcscheme");
		// XmlDocument document = new XmlDocument ();
		// document.LoadXml (xml);
		// XmlNode node = document.SelectSingleNode ("Scheme/LaunchAction");
		// XmlNode attr = document.CreateNode(XmlNodeType.Attribute, "enableGPUFrameCaptureMode", "");
		// attr.Value = "2";
		// node.Attributes.SetNamedItem(attr);
		// document.Save (pathToBuiltProject + "/Unity-iPhone.xcodeproj/xcshareddata/xcschemes/Unity-iPhone.xcscheme");

		// 注释, 删除测试用的png
		// #if UNITY_5_3_OR_NEWER
		// #if UNITY_IOS
		// //Unity5时使用Unity提供的API删除
		// string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		// UnityEditor.iOS.Xcode.PBXProject proj = new UnityEditor.iOS.Xcode.PBXProject ();
		// proj.ReadFromString (File.ReadAllText (projectPath));
		// List<string> cutPngs = cutPath(pngs,projectPath);

		// foreach (string cutPng in cutPngs) {
		// 	string guid = proj.FindFileGuidByProjectPath (cutPng);
		// 	proj.RemoveFile (guid);
		// }

		 
		// File.WriteAllText (projectPath, proj.WriteToString ());
	
		// #endif
		// #endif

		// Add AssociatedDomains // 需要Unity 2017以上版本
        //AddAssociatedDomains(pathToBuiltProject);
        
	}
	public static List<string>  cutPath (List<string> pngPaths, string pathProject)
	{
		List<string> list = new List<string> ();
		foreach (string pngPath in pngPaths) {
			string subPngPath = pngPath.Substring (pathProject.Length + 1);
			list.Add (subPngPath);
		}
		return list;
	}
	public static void GetPngFileNames (string pathname)
	{
		if (Directory.Exists (pathname)) {
			string[] tmp = Directory.GetFileSystemEntries (pathname);//获取源文件夹中的目录及文件路径，存入字符串
			for (int i = 0; i < tmp.Length; i++) {
				if (Path.GetExtension (tmp [i]).Equals (".png")) {//如果是文件则存入FileList
					pngs.Add (tmp [i]);
				} else {
					GetPngFileNames (tmp [i]);
				}
			}
		} else {
			Debug.Log (pathname + " is not exsit");
		}
	}
	#endif
	public static void Log (string message)
	{
		UnityEngine.Debug.Log ("PostProcess: " + message);
	}

	private static void EditorCode(string filePath) {
        // load UnityAppController.mm
        UnityEngine.Debug.Log("EditorCode: " + filePath);
        XClass unityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        // add codes
        string codes = "#include \"AllLifecycleRegister.h\"";//add_flag
        unityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"", codes);

        if (!unityAppController.isExist("continueUserActivity:")) {
            #if (UNITY_5 || UNITY_5_3_OR_NEWER)
			unityAppController.WriteBelow ("SensorsCleanup();\n}", "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler\r{\r    return YES;\r}");
            #else
            unityAppController.WriteBelow ("UnityCleanup();\n}", "- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler\r{\r    return YES;\r}");
            #endif
        }
    }
	
 //    // Add AssociatedDomains
 //    private static void AddAssociatedDomains(string pathToBuiltProject) {
 //        #if UNITY_2017_1_OR_NEWER && UNITY_IOS
 //        string projPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(pathToBuiltProject);
 //        UnityEditor.iOS.Xcode.PBXProject proj = new UnityEditor.iOS.Xcode.PBXProject();
 //        proj.ReadFromString(File.ReadAllText(projPath));
 //        // 获取当前项目名字  
 //        string target_1 = proj.TargetGuidByName(UnityEditor.iOS.Xcode.PBXProject.GetUnityTargetName());
 //        // // 对所有的编译配置设置选项  
 //        // proj.SetBuildProperty(target_1, "ENABLE_BITCODE", "NO");
 //        // proj.SetBuildProperty(target_1, "CODE_SIGN_IDENTITY", "iPhone Developer: Xiaochen Song (4LF6UN27XS)"); // 签名证书
 //        // proj.SetBuildProperty(target_1, "PROVISIONING_PROFILE", "bfbf3f78-e9eb-40ab-8924-aeb3d05d7e8d");
 //        // proj.SetBuildProperty(target_1, "PROVISIONING_PROFILE_SPECIFIER", "MSDKV3-DEV");  // 签名描述文件
 //        // proj.SetBuildProperty(target_1, "DEVELOPMENT_TEAM", "JBS4AWYMFX");
 //        // proj.SetBuildProperty(target_1, "CODE_SIGN_STYLE", "Manual");

 //        // Need create entitlements
 //        string relativeEntitlementFilePath = "Unity-iPhone/gcloudsdk.entitlements";
 //        Debug.Log("entitlementsPath : " + relativeEntitlementFilePath);
 //        string absoluteEntitlementFilePath = pathToBuiltProject + "/" + relativeEntitlementFilePath;

 //        PlistDocument tempEntitlements = new PlistDocument();
 //        string key_associatedDomains = "com.apple.developer.associated-domains";
 //        var arr = (tempEntitlements.root[key_associatedDomains] = new PlistElementArray()) as PlistElementArray;
 //        arr.values.Add(new PlistElementString("applinks:wiki.ssl.msdk.qq.com")); // 替换成游戏的links
 //        tempEntitlements.WriteToFile(absoluteEntitlementFilePath);

 //        // Add Capability 
 //        proj.AddCapability(target_1, PBXCapabilityType.AssociatedDomains, relativeEntitlementFilePath);

 //        string projPath_1 = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(pathToBuiltProject);
 //        File.WriteAllText(projPath_1, proj.WriteToString());
        
 //        // 保存工程  
 //        proj.WriteToFile(projPath);
 //        #endif
 //    }
}
}