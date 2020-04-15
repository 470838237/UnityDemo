using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace IIPSMobile
{

[AttributeUsage (AttributeTargets.Method)]
public sealed class MonoPInvokeCallbackAttribute : Attribute {
	public MonoPInvokeCallbackAttribute (Type t) {}
}

public class IIPSPluginName
{
#if UNITY_EDITOR || UNITY_EDITOR_OSX
    public const string pluginName = "gcloud";
#else
	public const string pluginName = "gcloud";
#endif
}

public class IIPSMobileVersionCallBack {
	[StructLayout(LayoutKind.Sequential)]
	public struct PROGRAMMEVERSION
	{
		public System.UInt16 MajorVersion_Number;
		public System.UInt16 MinorVersion_Number;
		public System.UInt16 Revision_Number;
	}
	
	
	[StructLayout(LayoutKind.Sequential)]
	public struct DATAVERSION
	{
		public System.UInt16 DataVersion;
	}
	
	
	[StructLayout(LayoutKind.Sequential)]
	public struct APPVERSION
	{
		public PROGRAMMEVERSION programmeVersion;
		public DATAVERSION dataVersion;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct VERSIONINFO
	{
		public System.Byte isAppUpdating;
		public System.Byte  isNeedUpdating;
		public System.Byte  isForcedUpdating;
		public APPVERSION    newAppVersion;
		public System.UInt64 needDownloadSize;
        public System.Byte isAuditVersion;
        public System.Byte isGrayVersion;
        public System.Byte isNormalVersion;
    }
	
	public enum VERSIONSTAGE
	{
		VS_Start = 0,
		VS_SelfDataCheck,       // has progress
		VS_SelfDataRepair,      // has progress
		VS_GetVersionInfo,      // has progress
        VS_FirstExtract = 10,	
        VS_Dolphin_Version = 69,
        VS_ApkUpdate = 70,
        VS_ApkUpdateDownConfig,
        VS_ApkUpdateDownDiffFile,
        VS_ApkUpdateDownFullApk,
        VS_ApkUpdateCheckCompletedApk,
        VS_ApkUpdateCheckLocalApk,
        VS_ApkUpdateCheckConfig,
        VS_ApkUpdateCheckDiff,
        VS_ApkUpdateMergeDiff,
        VS_ApkUpdateCheckFull,
        VS_SourceUpdateCures = 90,
        VS_SourceUpdateDownloadList,
        VS_SourcePrepareUpdate,
        VS_SourceAnalyseDiff,
        VS_SourceDownload,
        VS_SourceExtract,
        VS_Success = 99,
        VS_Fail = 100,          //      
	};
	//[UnmanagedFunctionPointer(CallingConvention.StdCall)] 
	internal  delegate System.Byte OnGetNewVersionInfoFunc(System.IntPtr callback,System.IntPtr newVersionInfo);
	
	internal  delegate void OnProgressFunc(System.IntPtr callback,VERSIONSTAGE curVersionStage, System.UInt64 totalSize, System.UInt64 nowSize);
	
	internal  delegate void OnErrorFunc(System.IntPtr callback,VERSIONSTAGE curVersionStage, System.UInt32 errorCode);
	
	internal  delegate void OnSuccessFunc(System.IntPtr callback);
	
	internal  delegate void SaveConfigFunc(System.IntPtr callback,System.UInt32 bufferSize,System.IntPtr configBuffer);
	
	internal delegate System.Byte OnNoticeInstallApkFunc(System.IntPtr callback,[MarshalAs(UnmanagedType.LPStr)]string url);
	internal delegate System.Byte OnActionMsgFunc(System.IntPtr callback,[MarshalAs(UnmanagedType.LPStr)]string url);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName,ExactSpelling = true)]
#endif
	static extern System.IntPtr CreateVersionInfoCallBack (OnGetNewVersionInfoFunc onGetNewVersionInfoFunc, OnProgressFunc onProgressFunc,
	                                                       OnErrorFunc onErrorFunc, OnSuccessFunc onSuccessFunc, SaveConfigFunc saveConfigFunc,
														   OnNoticeInstallApkFunc noticeInstallApk,OnActionMsgFunc msg,System.IntPtr callback);
#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void  DestroyVersionInfoCallBack(System.IntPtr callback);
	

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr  GetCallBackGCHandle(System.IntPtr callback);
	
	
	public IIPSMobileVersionCallBack( IIPSMobileVersionCallBackInterface imp){

		pManagedObject = GCHandle.ToIntPtr(GCHandle.Alloc(imp, GCHandleType.Normal));
		//System.IntPtr pManagedObject = System.IntPtr.Zero;


//        versionFunc = new OnGetNewVersionInfoFunc (OnGetNewVersionInfo);
//		progressFunc = new OnProgressFunc (OnProgress);
//		errFunc = new OnErrorFunc (OnError);
//		succFUnc = new OnSuccessFunc (OnSuccess);
//        saveFUnc = new SaveConfigFunc(SaveConfig);
//		installApk = new OnNoticeInstallApkFunc(OnNoticeInstallApk);
//		actionMsg = new OnActionMsgFunc(OnActionMsg);
        //mCallBack = CreateVersionInfoCallBack(versionFunc, progressFunc, errFunc, succFUnc, saveFUnc, installApk, actionMsg, pManagedObject);

	}
//
//    private OnGetNewVersionInfoFunc versionFunc = null;
//    private OnProgressFunc progressFunc = null;
//    private OnErrorFunc errFunc = null;
//    private OnSuccessFunc succFUnc = null;
//    private SaveConfigFunc saveFUnc = null;
//	private OnNoticeInstallApkFunc installApk = null;
//	private OnActionMsgFunc 	   actionMsg = null;
	~IIPSMobileVersionCallBack()
	{
			//Debug.Log (string.Format ("MYLZK destroy 3"));
		if (mCallBack != System.IntPtr.Zero) {
			//Debug.Log (string.Format ("MYLZK destroy 0"));
			System.IntPtr ptr = GetCallBackGCHandle(mCallBack);
			GCHandle handle = GCHandle.FromIntPtr(ptr);
			handle.Free();
			//Debug.Log (string.Format ("MYLZK destroy 1"));
			DestroyVersionInfoCallBack(mCallBack);
			//Debug.Log (string.Format ("MYLZK destroy 2"));
			mCallBack = System.IntPtr.Zero;
		}
	}
	public void CreateCppVersionInfoCallBack()
	{
		//System.Console.WriteLine("create c++ callback start");
		mCallBack = CreateVersionInfoCallBack(OnGetNewVersionInfo, OnProgress, OnError, OnSuccess, SaveConfig, OnNoticeInstallApk, OnActionMsg, pManagedObject);
		//System.Console.WriteLine("create c++ callback success ,c++ callback:{0}",mCallBack.ToInt32().ToString());
	}
	
	public void DeleteCppVersionCallBack()
	{
		if (mCallBack != System.IntPtr.Zero) 
		{
			System.IntPtr ptr = GetCallBackGCHandle(mCallBack);
			GCHandle handle = GCHandle.FromIntPtr(ptr);
			handle.Free();
			//System.Console.WriteLine("delete c++ callback from c# start");
			DestroyVersionInfoCallBack(mCallBack);
			//System.Console.WriteLine("delete c++ callback from c# end");
			mCallBack = System.IntPtr.Zero;
		}	
	}

	[MonoPInvokeCallback(typeof(OnGetNewVersionInfoFunc))]
	public static System.Byte OnGetNewVersionInfo(System.IntPtr callback,System.IntPtr newVersionInfo)
	{
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			VERSIONINFO info = (VERSIONINFO)Marshal.PtrToStructure(newVersionInfo,typeof(VERSIONINFO));
			return mImpCB.OnGetNewVersionInfo(info);
		}

	}
	
	[MonoPInvokeCallback(typeof(OnNoticeInstallApkFunc))]
	public static System.Byte OnNoticeInstallApk(System.IntPtr callback,[MarshalAs(UnmanagedType.LPStr)]string url)
	{
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			return mImpCB.OnNoticeInstallApk(url);
		}

	}
	
	[MonoPInvokeCallback(typeof(OnActionMsgFunc))]
	public static System.Byte OnActionMsg(System.IntPtr callback,[MarshalAs(UnmanagedType.LPStr)]string url)
	{
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			return mImpCB.OnActionMsg(url);
		}

	}

	[MonoPInvokeCallback(typeof(OnProgressFunc))]
	public static void OnProgress(System.IntPtr callback,VERSIONSTAGE curVersionStage, System.UInt64 totalSize, System.UInt64 nowSize)
	{
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			mImpCB.OnProgress(curVersionStage,totalSize,nowSize);
		}
	}

	[MonoPInvokeCallback(typeof(OnErrorFunc))]
	public static void OnError(System.IntPtr callback,VERSIONSTAGE curVersionStage, System.UInt32 errorCode){
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			mImpCB.OnError(curVersionStage, errorCode);
		}
	}

	[MonoPInvokeCallback(typeof(OnSuccessFunc))]
	public static void OnSuccess(System.IntPtr callback){
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			mImpCB.OnSuccess( );
		}
	}

	[MonoPInvokeCallback(typeof(SaveConfigFunc))]
	public static void SaveConfig(System.IntPtr callback,System.UInt32 bufferSize,System.IntPtr  configBuffer)
	{
	
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mImpCB= (IIPSMobileVersionCallBackInterface)handle.Target;
			mImpCB.SaveConfig(bufferSize, configBuffer);
		}
	}
	
	public System.IntPtr mCallBack = System.IntPtr.Zero;
	private System.IntPtr pManagedObject = System.IntPtr.Zero;
	 static IIPSMobileVersionCallBackInterface mImpCB = null;
}


public class VersionMgr :  IIPSMobileVersionMgrInterface
{
#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr CreateVersionManager ();

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void  ReleaseVersionManager(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  InitVersionMgr(System.IntPtr versionMgr, System.IntPtr callback, System.UInt32 bufferSize, System.IntPtr configBuffer);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  UnitVersionMgr(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  SetNextStage(System.IntPtr versionMgr, System.Byte goonWork);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  CheckAppUpdate(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void  CancelUpdate(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Int16  GetCurDataVersion(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetVersionMgrLastError(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt64 GetMemorySize(System.IntPtr versionMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetActionDownloadSpeed(System.IntPtr versionMgr);
	
#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	[DllImport("__Internal",ExactSpelling = true)]
#else
	[DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void PoolVersionManager(System.IntPtr versionMgr);
	
	public VersionMgr()
	{
		//System.Console.WriteLine("create c# VersionMgr start");
		mVersionMgr = System.IntPtr.Zero;
		//System.Console.WriteLine("create c# VersionMgr end c++ mgr:{0}",mVersionMgr.ToInt32().ToString());
	}
	
	public void CreateCppVersionManager()
	{
		//System.Console.WriteLine("create c++ VersionMgr start");
		mVersionMgr = CreateVersionManager ();
		//System.Console.WriteLine("create c++ VersionMgr end c++ mgr:{0}",mVersionMgr.ToInt32().ToString());
	}
	
	public void DeleteCppVersionManager()
	{
		//System.Console.WriteLine("delete c++ VersionMgr start");
		if (mVersionMgr != System.IntPtr.Zero) {
			//System.Console.WriteLine("delete c++ VersionMgr start mgr:{0}",mVersionMgr.ToInt32().ToString());
			ReleaseVersionManager(mVersionMgr);
			//System.Console.WriteLine("delete c++ VersionMgr end mgr:{0}",mVersionMgr.ToInt32().ToString());
			mVersionMgr = System.IntPtr.Zero;
		}
	}
	
	~VersionMgr()
	{
		//System.Console.WriteLine("delete c# VersionMgr start");
		mVersionMgr = System.IntPtr.Zero;
		//System.Console.WriteLine("delete c# VersionMgr end");
	}
		
	
	public bool MgrInitVersionManager(/*System.IntPtr callback*/IIPSMobileVersionCallBack callBack, System.UInt32 bufferSize, byte[] configBuffer/*System.IntPtr configBuffer*/)
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return false;
		}
		
		
		GCHandle pinnedArray = GCHandle.Alloc(configBuffer, GCHandleType.Pinned);
		System.IntPtr configPointer = pinnedArray.AddrOfPinnedObject();
		pinnedArray.Free();
		return InitVersionMgr(mVersionMgr, callBack.mCallBack, bufferSize, configPointer) > 0;
	}
	
	public bool MgrUnitVersionManager()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return false;
		}
		
		bool ret =  UnitVersionMgr (mVersionMgr) > 0;
			//ReleaseVersionManager (mVersionMgr);
			//mVersionMgr = System.IntPtr.Zero;
			//System.Console.WriteLine("MYLZK release");
		return ret;
	}
	
	public bool MgrSetNextStage(bool goonWork)
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return false;
		}
		System.Byte IsGoOn = 0;
		if (goonWork) {
			IsGoOn = 1;
		}
		return SetNextStage (mVersionMgr,IsGoOn) > 0;
	}
	
	public bool MgrPoll()
	{
			if (mVersionMgr == System.IntPtr.Zero) {
				return false;
			}
			PoolVersionManager (mVersionMgr);
			return true;
	}
	
	public bool MgrCheckAppUpdate()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return false;
		}
		return CheckAppUpdate (mVersionMgr) > 0;
	}
	
	public void MgrCancelUpdate()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return;
		}
		CancelUpdate (mVersionMgr);
		return;
	}
	
	public System.Int16 MgrGetCurDataVersion()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return 0;
		}
		return GetCurDataVersion (mVersionMgr);
	}
	
	public System.UInt32 MgrGetVersionMgrLastError()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return 1;
		}
		return GetVersionMgrLastError (mVersionMgr);
	}
	
	public System.UInt64 MgrGetMemorySize()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return 0;
		}
		return GetMemorySize (mVersionMgr);
	}
	
	public System.UInt32 MgrGetActionDownloadSpeed()
	{
		if (mVersionMgr == System.IntPtr.Zero) {
			return 0;
		}
		return GetActionDownloadSpeed (mVersionMgr);
	}
#if UNITY_ANDROID
	public bool InstallApk(string path)
	{
		AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"); 
		if(jc == null)
		{
			return false;
		}
		AndroidJavaObject m_jo = jc.GetStatic<AndroidJavaObject> ("currentActivity"); 
		if(m_jo == null)
		{
			return false;
		}
		
		AndroidJavaClass clazz = new AndroidJavaClass("com.tencent.gcloud.dolphin.CuIIPSMobile");
		if(clazz == null)
		{
			return false;
		}
		int result = clazz.CallStatic<int>("installAPK",path,m_jo);

		if (result != 0)
		{
			//System.Console.WriteLine ("installapk failed: {0}", result);
			return false;
		}
		else
		{
			//System.Console.WriteLine ("installapk success");
			return true;
		}
	}
#endif


	
	
	private System.IntPtr mVersionMgr = System.IntPtr.Zero; 
}
}



