using System;
using System.Collections.Generic;
using System.Text;

namespace IIPSMobile
{
/****************************************************************
**********IIPSMobileDataMgrInterface数据管理接口***************
*****************************************************************/
	public interface IIPSMobileDataMgrInterface
	{
		//反初始化接口，析构前需要调用反初始化
		//@ret:成功、失败
		bool Uninit();
		
		//获取数据读取接口
		//@ret:返回null则为获取失败
		IIPSMobileDataReaderInterface GetDataReader();
		
		//获取数据下载接口
		//@para openProgressCallBack:是否打开进度
		//@ret:返回null则为获取失败
		IIPSMobileDownloaderInterface GetDataDownloader(bool openProgressCallBack = false);
		
		//获取数据查询接口
		//@ret:返回null则为获取失败
		IIPSMobileDataQueryInterface GetDataQuery();
		
		//获取最后一次操作的失败错误码
		//@ret:返回错误码
		System.UInt32 MgrGetDataMgrLastError();
		
		//获取当前数据管理器所占的内存大小
		//@ret:返回所占内存大小（未实现）
		System.UInt64 MgrGetMemorySize();
		
		bool PollCallBack();
		
	}
/****************************************************************
**********IIPSMobileDownloadCallbackInterface下载信息回调******
*****************************************************************/	
	public interface IIPSMobileDownloadCallbackInterface
	{
		//回调接口，下载出错时回调
		//@para/out taskId:出错的任务ID
		//@para errorCode:错误码
		void OnDownloadError(System.UInt32 taskId, System.UInt32 errorCode);
		
		//回调接口，下载完成时回调
		//@para/out taskId:完成的任务ID
		void OnDownloadSuccess(System.UInt32 taskId);
		
		//回调接口，下载完成时回调
		//@para/out taskId:返回进度的任务ID
		//@para/out info:下载信息
		void OnDownloadProgress(System.UInt32 taskId, DataDownloader.DownloadInfo info);
	}
	
	
/****************************************************************
**********IIPSMobileDataReaderInterface数据读取接口******
*****************************************************************/	
	public interface IIPSMobileDataReaderInterface
	{
		//读取接口，从IFS中读取数据
		//@para fileId:需要读取文件的ID
		//@para offset:需要读取的偏移
		//@para buff:用于接受数据的buffer
		//@para/out readlength:要读取的长度/接受读取的长度
		//@ret:成功/失败
		bool Read(System.UInt32 fileId, System.UInt64 offset, byte[] buff, ref System.UInt32 readlength);

		//读取接口，从IFS中读取数据
		//@para fileId:需要读取文件的ID
		//@para offset:需要读取的偏移
		//@para buff:用于接受数据的buffer
		//@para/out readlength:要读取的长度/接受读取的长度
		//@ret:成功/失败
		//bool RestoreFile(System.UInt32 fileId, byte[]  dstPath, bool bOverwrite);
		
		//获取上一个读取错误码的接口
		//@ret:错误码
		System.UInt32 GetLastReaderError();
	}
	
/****************************************************************
**********IIPSMobileDataQueryInterface数据查询接口******
*****************************************************************/	
	public interface IIPSMobileDataQueryInterface
	{
		//根据文件ID获取文件名
		//@para fileId:需要获取文件名的文件ID
		//@ret:文件名
		string GetFileName(System.UInt32 fileId);
		
		//根据文件名获取文件ID
		//@para fileName:需要获取文件id的文件名
		//@ret:文件ID
		System.UInt32 GetFileId(string fileName);
		
		//根据文件ID获取文件长度
		//@para fileId:需要获取文件长度的文件ID
		//@ret:文件长度
		System.UInt32 GetFileSize(System.UInt32 fileId);
		
		//根据文件ID判断文件是否下载完成
		//@para fileId:文件ID
		//@ret:完成/没完成
		bool IsFileReady(System.UInt32 fileId);
		
		//根据文件ID判断当前文件是否为目录
		//@para fileId:文件ID
		//@ret:是/否
		bool IsDirectory(System.UInt32 fileId);
		
		//目录遍历接口，获取目录下的第一个文件
		//@para fileId:目录的文件ID
		//@para/out pInfo:输出的第一个文件的文件信息
		//@ret:返回的findhanle，用于接下来的遍历
		System.UInt32 IIPSFindFirstFile(System.UInt32 fileId, ref DataQuery.IIPS_FIND_FILE_INFO pInfo);
		
		//目录遍历接口，根据findhanle获取下一个文件，不会查询子目录
		//@para findHandle:IIPSFindFirstFile中返回的findhandle
		//@para/out pInfo:输出的下一个文件的文件信息
		//@ret:成功/失败，失败则说明查询完成
		bool IIPSFindNextFile(System.UInt32 findHandle, ref DataQuery.IIPS_FIND_FILE_INFO pInfo);
		
		//关闭目录遍历，关闭handle
		//@para findHandle:IIPSFindFirstFile中返回的findhandle
		//@ret:成功/失败
		bool IIPSFindClose(System.UInt32 findHandle);
		
		//获取IFS包信息
		//@para/out pInfo:返回的信息
		//@para count:包的id
		//@ret:成功/失败
		System.UInt32 GetIfsPackagesInfo(ref DataQuery.IIPS_PACKAGE_INFO pInfo, System.UInt32 count);
		
		//获取上一个操作的错误码
		//@ret:错误码
		System.UInt32 GetLastDataQueryError();
	}
	
/****************************************************************
**********IIPSMobileDownloaderInterface数据下载接口******
*****************************************************************/		
	public interface IIPSMobileDownloaderInterface
	{
		//初始化下载器
		//@para callback:下载回调
		//@ret:成功/失败
		bool Init(IIPSMobileDownloadCallbackInterface callback);
		
		//开始下载
		//@ret:成功/失败
		bool StartDownload();
		
		//暂停下载
		//@ret:成功/失败
		bool PauseDownload();
		
		//恢复下载
		//@ret:成功/失败
		bool ResumeDownload();
		
		//退出下载
		//@para taskId:下载任务id
		//@ret:成功/失败
		bool CancelDownload(System.UInt32 taskId);
		
		//获取当前下载速度
		//@ret:下载速度，单位是b/s
		System.UInt32 GetDownloadSpeed();
		
		//设置最高下载速度
		//@para downloadSpeed:需要设置的限速，单位是kb/s
		//@ret:成功/失败
		bool SetDownloadSpeed(System.UInt32 downloadSpeed);
		
		
		//下载IFS包内的数据
		//@para fileId:需要下载的文件ID
		//@para priority:下载优先级0~101，越小优先级越高
		//@para/out taskId:返回的任务ID
		//@ret:成功/失败
		bool DownloadIfsData(System.UInt32 fileId, System.Byte priority, ref System.UInt32 taskId);

		//下载本地的数据
		//@para downloadUrl:需要下载的文件url
		//@para savePath:存取的文件名
		//@para priority:下载优先级0~101，越小优先级越高
		//@para/out taskId:返回的任务ID
		//@para bDoBrokenResume:是否需要断点续传
		//@ret:成功/失败
		bool DownloadLocalData(string downloadUrl, string savePath, System.Byte priority, ref System.UInt32 taskID, bool bDoBrokenResume);
		
		//获取下载任务信息
		//@para taskId:需要获取信息的任务ID
		//@para/out downloadInfo:返回的下载信息
		//@para priority:下载优先级0~101，越小优先级越高
		//@ret:成功/失败
		bool GetDownloadTaskInfo(System.UInt32 taskId, ref DataDownloader.DownloadInfo downloadInfo);
		
		//获取上一个操作的错误码
		//@ret:错误码
		System.UInt32 GetLastError();
	}
	

/****************************************************************
**********IIPSMobileData数据管理生成工厂**********************
*****************************************************************/	
	public class IIPSMobileData
	{
		public IIPSMobileDataMgrInterface CreateDataMgr(string config)
		{
			IIPSMobileDataManager dMgr=  new IIPSMobileDataManager();
			bool ret = dMgr.Init ((uint)config.Length, System.Text.Encoding.ASCII.GetBytes (config));
			if(!ret)
			{
				return null;
			}
			else
			{
				return dMgr;
			}
		}
	}
}
