//using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;


namespace IIPSMobile
{
public class IIPSMobileDataManager :  IIPSMobileDataMgrInterface{

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr CreateDataManager();

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void ReleaseDataManager(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte InitDataManager(System.IntPtr dataManager, System.UInt32 bufferSize, System.IntPtr  configBuffer);
	
	#if UNITY_IPHONE
		[DllImport("__Internal",ExactSpelling = true)]
		#else
		[DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
		#endif
		static extern System.Byte DataMgrPollCallback(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte UnitDataManager(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr GetDataReader(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr GetDataDownloader(System.IntPtr dataManager,byte openProgressCallBack);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr GetDataQuery(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetLastDataMgrError(System.IntPtr dataManager);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt64 GetDataMgrMemorySize(System.IntPtr dataManager);

	protected System.IntPtr mDataManager = System.IntPtr.Zero;
	
	public IIPSMobileDataManager(){
		mDataManager = CreateDataManager ();
	}
	
	~IIPSMobileDataManager()
	{
		if (mDataManager != System.IntPtr.Zero) {
			ReleaseDataManager (mDataManager);
			mDataManager = System.IntPtr.Zero;
		}
	}
	
	public bool Init(System.UInt32 bufferSize, byte[] configBuffer){
		if (mDataManager == System.IntPtr.Zero) {
			return false;
		}
		
		GCHandle pinnedArray = GCHandle.Alloc(configBuffer, GCHandleType.Pinned);
		System.IntPtr configPointer = pinnedArray.AddrOfPinnedObject();
		pinnedArray.Free();
		
		return InitDataManager(mDataManager, bufferSize, configPointer) > 0;
	}
	
	public bool PollCallBack()
	{
		if (mDataManager == System.IntPtr.Zero)
		{
			return false;
		}
		return DataMgrPollCallback (mDataManager)>0;
	}
	
	public bool Uninit(){
		if (mDataManager == System.IntPtr.Zero) {
			return false;
		}
		return UnitDataManager (mDataManager) > 0;
	}
	
	public  IIPSMobileDataReaderInterface GetDataReader()
	{
		if (mDataManager == System.IntPtr.Zero) {
			return null ;
		}
		DataReader dataReader = new DataReader (GetDataReader (mDataManager));
		return dataReader;
	}
	
	public  IIPSMobileDownloaderInterface GetDataDownloader(bool openProgressCallBack = false)
	{
		if (mDataManager == System.IntPtr.Zero) {
			return null ;
		}
		byte progressOpen = 0;
		if(openProgressCallBack)
			progressOpen = 1;
		DataDownloader dataDownloader = new DataDownloader (GetDataDownloader (mDataManager,progressOpen));
		return dataDownloader;
	}
	
	public  IIPSMobileDataQueryInterface GetDataQuery()
	{
		if (mDataManager == System.IntPtr.Zero) {
			return null ;
		}
		DataQuery dataQuery = new DataQuery (GetDataQuery (mDataManager));
		return dataQuery;
	}
	
	
	public System.UInt32 MgrGetDataMgrLastError()
	{
		if (mDataManager == System.IntPtr.Zero) {
			return 0;
		}
		return GetLastDataMgrError (mDataManager);
	}
	
	public System.UInt64 MgrGetMemorySize()
	{
		if (mDataManager == System.IntPtr.Zero) {
			return 0;
		}
		return GetDataMgrMemorySize (mDataManager);
	}
}

public class DataReader :  IIPSMobileDataReaderInterface
{
#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  Read(System.IntPtr dataReader, System.UInt32 fileId, System.UInt64 offset, byte[] buff, ref System.UInt32 readlength);

// add for RestoreFile 
//#if UNITY_IPHONE
	//[DllImport("__Internal",ExactSpelling = true)]
//#else
    //[DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
//#endif
	//static extern System.Byte  RestoreFile(System.IntPtr dataReader,System.UInt32 fileId, byte[] dstPath, bool bOverwrite);


#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetLastReaderError(System.IntPtr reader);
	
	
	public DataReader(System.IntPtr Reader){
		mDataReader = Reader;
	}
	
	public bool Read(System.UInt32 fileId, System.UInt64 offset, byte[] buff, ref System.UInt32 readlength)
	{
		if (mDataReader == System.IntPtr.Zero) {
			return false;
		}
		return Read (mDataReader, fileId, offset, buff, ref readlength) > 0;
	}

	//public bool RestoreFile(System.UInt32 fileId, byte[]  dstPath, bool bOverwrite)
	//{
		//if (mDataReader == System.IntPtr.Zero) {
			//return false;
		//}
		//return RestoreFile (mDataReader, fileId, dstPath, bOverwrite) > 0;

	//}
	
	public System.UInt32 GetLastReaderError(){
		if (mDataReader == System.IntPtr.Zero) {
			return 0;
		}
		return GetLastReaderError (mDataReader);
	}
	
	public System.IntPtr mDataReader = System.IntPtr.Zero;
}

public class DataQuery :  IIPSMobileDataQueryInterface
{
	[StructLayout(LayoutKind.Sequential)]
	public struct IIPS_FIND_FILE_INFO
	{
		public System.UInt32 fileId;
		public System.UInt32 fileSize;
		public System.Byte   isDirectory;
		
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct IIPS_PACKAGE_INFO
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]        
		public System.Byte[] szPackageName;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]        
		public System.Byte[] szPackageFilePath;
		public System.UInt64 currentSize;
		public System.UInt64 totalSize;
	}

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	//[return:MarshalAs(UnmanagedType.LPStr)]
	static extern System.IntPtr GetIFileName(System.IntPtr dataQuery, System.UInt32 fileId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetIFileId(System.IntPtr dataQuery, [MarshalAs(UnmanagedType.LPStr)]string szFileName);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetIFileSize(System.IntPtr dataQuery, System.UInt32 fileId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte IsIFileReady(System.IntPtr dataQuery, System.UInt32 fileId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte IsIFileDir(System.IntPtr dataQuery, System.UInt32 fileId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 IIPSFindFirstFile(System.IntPtr dataQuery, System.UInt32 fileId, ref IIPS_FIND_FILE_INFO pInfo);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte IIPSFindNextFile(System.IntPtr dataQuery, System.UInt32 findHandle, ref IIPS_FIND_FILE_INFO pInfo);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte IIPSFindClose(System.IntPtr dataQuery, System.UInt32 findHandle);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetIfsPackagesInfo(System.IntPtr dataQuery, ref IIPS_PACKAGE_INFO pInfo, System.UInt32 count);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetLastDataQueryError(System.IntPtr dataQuery);
	
	
	public System.IntPtr mDataQuery = System.IntPtr.Zero;
	
	public DataQuery(System.IntPtr Query){
		mDataQuery = Query;
	}
	
	public string GetFileName(System.UInt32 fileId){
		if (mDataQuery == System.IntPtr.Zero) {
			return null;
		}
		System.IntPtr name = GetIFileName (mDataQuery, fileId);
		string strname = Marshal.PtrToStringAnsi(name);
		
		return strname;
	}
	
	public System.UInt32 GetFileId(string fileName){
		if (mDataQuery == System.IntPtr.Zero) {
			return 0;
		}
		
		return GetIFileId (mDataQuery, fileName);
	}
	
	public System.UInt32 GetFileSize(System.UInt32 fileId){
		if (mDataQuery == System.IntPtr.Zero) {
			return 0;
		}
		
		return GetIFileSize (mDataQuery, fileId);
	}
	
	public bool IsFileReady(System.UInt32 fileId){
		if (mDataQuery == System.IntPtr.Zero) {
			return false;
		}
		
		return IsIFileReady (mDataQuery, fileId)>0;
		
	}
	public bool IsDirectory(System.UInt32 fileId){
		if (mDataQuery == System.IntPtr.Zero) {
			return false;
		}
		
		return IsIFileDir (mDataQuery, fileId)>0;
	}
	
	public System.UInt32 IIPSFindFirstFile(System.UInt32 fileId, ref IIPS_FIND_FILE_INFO pInfo){
		if (mDataQuery == System.IntPtr.Zero) {
			return 0;
		}
		
		return IIPSFindFirstFile (mDataQuery, fileId, ref pInfo);
	}
	
	public bool IIPSFindNextFile(System.UInt32 findHandle, ref IIPS_FIND_FILE_INFO pInfo){
		if (mDataQuery == System.IntPtr.Zero) {
			return false;
		}
		
		return IIPSFindNextFile (mDataQuery, findHandle, ref pInfo)>0;
	}
	
	public bool IIPSFindClose(System.UInt32 findHandle){
		if (mDataQuery == System.IntPtr.Zero) {
			return false;
		}
		
		return IIPSFindClose (mDataQuery, findHandle)>0;
	}
	
	public System.UInt32 GetIfsPackagesInfo(ref IIPS_PACKAGE_INFO pInfo, System.UInt32 count){
		if (mDataQuery == System.IntPtr.Zero) {
			return 0;
		}
		
		return GetIfsPackagesInfo (mDataQuery, ref pInfo, count);
	}
	
	public System.UInt32 GetLastDataQueryError(){
		if (mDataQuery == System.IntPtr.Zero) {
			return 0;
		}
		
		return GetLastDataQueryError (mDataQuery);
	}
	
	
}

public class DownloadCallBack{
	internal  delegate void OnDownloadErrorFunc(System.IntPtr callback,System.UInt32 taskId, System.UInt32 errorCode);
	
	internal  delegate void OnDownloadSuccessFunc(System.IntPtr callback,System.UInt32 taskId);
	
	internal  delegate void OnDownloadProgressFunc(System.IntPtr callback,System.UInt32 taskId, DataDownloader.DownloadInfo info);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr CreateDownlaodMgrCallBack(OnDownloadErrorFunc onDownloadError, OnDownloadSuccessFunc onDownloadSuccess, OnDownloadProgressFunc onDownloadProgress,System.IntPtr callback);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern void DestroyDownlaodMgrCallBack(System.IntPtr callback);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.IntPtr GetDownloadCallbackGCHandle(System.IntPtr callback);


	public DownloadCallBack( IIPSMobileDownloadCallbackInterface CBImp)
	{

		System.IntPtr pManagedObject = GCHandle.ToIntPtr(GCHandle.Alloc(CBImp, GCHandleType.Normal));

        errFunc = new OnDownloadErrorFunc(OnDownloadError);
		succFunc = new OnDownloadSuccessFunc (OnDownloadSuccess);
		progressFunc = new OnDownloadProgressFunc(OnDownloadProgress);
        mCallBack = CreateDownlaodMgrCallBack(errFunc,succFunc,progressFunc, pManagedObject);
		//mCBImp = CBImp;
	}

    OnDownloadErrorFunc errFunc = null;
    OnDownloadSuccessFunc  succFunc = null;
	OnDownloadProgressFunc progressFunc = null;
	
	~DownloadCallBack()
	{
		if (mCallBack != System.IntPtr.Zero) {
			System.IntPtr ptr = GetDownloadCallbackGCHandle(mCallBack);
			GCHandle handle = GCHandle.FromIntPtr(ptr);
			handle.Free();

			DestroyDownlaodMgrCallBack(mCallBack);
		}
	}

	[MonoPInvokeCallback (typeof (OnDownloadErrorFunc))]
	public static void OnDownloadError(System.IntPtr callback,System.UInt32 taskId, System.UInt32 errorCode){
	//	if (mCBImp != null)
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mCBImp= (IIPSMobileDownloadCallbackInterface)handle.Target;
			//handle.Free();
			
			mCBImp.OnDownloadError(taskId, errorCode);
		}
	}
	[MonoPInvokeCallback (typeof (OnDownloadProgressFunc))]
	public static void OnDownloadProgress(System.IntPtr callback,System.UInt32 taskId, DataDownloader.DownloadInfo info){
	//	if (mCBImp != null)
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mCBImp= (IIPSMobileDownloadCallbackInterface)handle.Target;
			//handle.Free();
			
			mCBImp.OnDownloadProgress(taskId, info);
		}
	}

	[MonoPInvokeCallback (typeof (OnDownloadSuccessFunc))]
	public static void OnDownloadSuccess(System.IntPtr callback,System.UInt32 taskId){
	//	if (mCBImp != null)
		{
			GCHandle handle = GCHandle.FromIntPtr(callback);
			mCBImp = (IIPSMobileDownloadCallbackInterface)handle.Target;
			//handle.Free();

			mCBImp.OnDownloadSuccess(taskId);
		}
	}
	
	static public  IIPSMobileDownloadCallbackInterface mCBImp = null;
	public System.IntPtr mCallBack = System.IntPtr.Zero;
}

public class DataDownloader :  IIPSMobileDownloaderInterface{
	
	[StructLayout(LayoutKind.Sequential)]
	public struct DownloadInfo
	{
		public System.UInt64 needDownloadSize;
		public System.UInt64 downloadSize;
		public System.UInt64 fileSize;
	}
#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  InitDataDownloader(System.IntPtr dataDownloader, System.IntPtr callback);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  StartDownload(System.IntPtr dataDownloader);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  PauseDownload(System.IntPtr dataDownloader);
	
	#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  ResumeDonload(System.IntPtr dataDownloader);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32  GetDownloadSpeed(System.IntPtr dataDownloader);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  SetDownloadSpeed(System.IntPtr dataDownloader, System.UInt32 downloadSpeed);
	
	//[DllImport(  "__Internal",ExactSpelling = true)]
	//static extern System.Byte  DownloadUserData(System.IntPtr dataDownloader, System.IntPtr fileSystem, System.Byte priority, ref System.UInt32 taskId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  DownloadIfsData(System.IntPtr dataDownloader, System.UInt32 fileId, System.Byte priority, ref System.UInt32 taskId);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
	[DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte DownloadLocalData(System.IntPtr dataDownloader, [MarshalAs(UnmanagedType.LPStr)]string downloadurl,[MarshalAs(UnmanagedType.LPStr)]string filepath, System.Byte priority, ref System.UInt32 TaskID, System.Byte bDoBrokenResume );

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte  GetDownloadTaskInfo(System.IntPtr dataDownloader, System.UInt32 taskId, ref DownloadInfo downloadInfo);

#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.UInt32 GetLastDownloaderError(System.IntPtr dataDownloader);
	
#if UNITY_IPHONE
	[DllImport("__Internal",ExactSpelling = true)]
#else
    [DllImport(IIPSPluginName.pluginName, ExactSpelling = true)]
#endif
	static extern System.Byte CancelDownload(System.IntPtr dataDownloader, System.UInt32 taskId);
	
	public System.IntPtr mDownloader = System.IntPtr.Zero;
	
	public DataDownloader(System.IntPtr downloader){
		mDownloader = downloader;
	}
	

	
	public bool Init(IIPSMobileDownloadCallbackInterface callback){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		mCallback = new DownloadCallBack (callback);
		return InitDataDownloader (mDownloader, mCallback.mCallBack) > 0;
	}
	
	public bool StartDownload(){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return StartDownload (mDownloader) > 0;
	}
	
	public bool PauseDownload(){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return PauseDownload (mDownloader) > 0;
	}
	
	public bool ResumeDownload(){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return ResumeDonload (mDownloader) > 0;
	}
	
	public bool CancelDownload(System.UInt32 taskId){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return CancelDownload (mDownloader, taskId) > 0;
	}
	
	public System.UInt32 GetDownloadSpeed(){
		if (mDownloader == System.IntPtr.Zero) {
			return 0;
		}
		return GetDownloadSpeed (mDownloader);
	}
	
	public bool SetDownloadSpeed(System.UInt32 downloadSpeed){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return SetDownloadSpeed (mDownloader,downloadSpeed) > 0;
	}
	
	// 	public bool DownloadUserData(System.IntPtr fileSystem, System.Byte priority, ref System.UInt32 taskId){
	// 		if (mDownloader == System.IntPtr.Zero) {
	// 			return false;
	// 		}
	// 		return DownloadUserData (mDownloader, fileSystem,priority,ref taskId) > 0;
	// 	}
	
	public bool DownloadIfsData( System.UInt32 fileId, System.Byte priority, ref System.UInt32 taskId){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return DownloadIfsData (mDownloader, fileId,priority,ref taskId) > 0;
	}
	
	public bool GetDownloadTaskInfo(  System.UInt32 taskId, ref DownloadInfo downloadInfo){
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}
		return GetDownloadTaskInfo (mDownloader, taskId,ref downloadInfo) > 0;
	}
	
	public System.UInt32 GetLastError(){
		if (mDownloader == System.IntPtr.Zero) {
			return 0;
		}
		return GetLastDownloaderError(mDownloader);
	}

	public bool DownloadLocalData(string downloadUrl, string savePath, System.Byte priority, ref System.UInt32 taskID, bool bDoBrokenResume)
	{
		if (mDownloader == System.IntPtr.Zero) {
			return false;
		}

		System.Byte brokenResume = 0;
		if(bDoBrokenResume)
		{
			brokenResume = 1;
		}

		return DownloadLocalData (mDownloader, downloadUrl, savePath, priority, ref taskID, brokenResume) > 0;
	}

	private DownloadCallBack mCallback = null;
}

}


