using System;
using System.Collections.Generic;
using System.Text;

namespace IIPSMobile
{
/****************************************************************
**********IIPSMobileDownloadCallbackInterface版本管理回调******
*****************************************************************/
	public interface IIPSMobileVersionCallBackInterface
	{
		//回调接口，获取新版本信息回调
		//@para/out newVersionInfo:新版本信息
		//@ret 返回0时会回调onError
		System.Byte OnGetNewVersionInfo(IIPSMobileVersionCallBack.VERSIONINFO newVersionInfo);
		
		//回调接口，升级进度回调
		//@para curVersionStage:当前升级阶段
		//@para curVersionStage:当前下载文件总大小
		//@para curVersionStage:当前下载文件已下载大小
		void OnProgress(IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, System.UInt64 totalSize, System.UInt64 nowSize);
		
		//回调接口，版本升级出错时回调
		//@para/out curVersionStage:当前版本升级所处的阶段
		//@para errorCode:错误码
		void OnError(IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, System.UInt32 errorCode);
		
		//回调接口，版本升级成功时回调
		void OnSuccess();
		
		//回调接口，保存配置回调
		void SaveConfig(System.UInt32 bufferSize, System.IntPtr configBuffer);
		
		//回调接口，通知安装apk,只在android平台有用
		//@para/out path:apk的path
		//@ret 返回0时会回调onError
		System.Byte OnNoticeInstallApk(string path);
		
		System.Byte OnActionMsg(string msg);
	}
	
	
	public interface IIPSMobileVersionMgrInterface
	{
	//	bool MgrInitVersionManager(ref IIPSMobileVersionCallBack callBack, System.UInt32 bufferSize, byte[] configBuffer);
		
		//反初始化接口，析构前需要调用反初始化
		//@ret:成功、失败
		bool MgrUnitVersionManager();
		
		//进入到下一阶段
		//@para goonWork:是否进入到下一阶段
		//@ret:成功/失败
		bool MgrSetNextStage(bool goonWork);
		
		//检测是否需要升级，返回结果在回调OnGetNewVersionInfo中
		//@ret:执行成功/失败
		bool MgrCheckAppUpdate();
		
		//终止升级
		void MgrCancelUpdate();
		
		//获取当前数据版本
		//@ret:当前数据版本号
		System.Int16 MgrGetCurDataVersion();
		
		//获取最后一次操作的失败错误码
		//@ret:返回错误码
		System.UInt32 MgrGetVersionMgrLastError();
		
		//获取当前数据管理器所占的内存大小
		//@ret:返回所占内存大小（未实现）
		System.UInt64 MgrGetMemorySize();
		
		//获取当前action的下载速度
		//@ret:返回速度大小，单位B/S
		System.UInt32 MgrGetActionDownloadSpeed();
		
		bool MgrPoll();

		#if UNITY_ANDROID
		//安装APK包
		//@para path:apk的路径
		//@ret:成功/失败
		bool InstallApk(string path);
		#endif
		
	}
	
	
	public class IIPSMobileVersion
	{
		public IIPSMobileVersionMgrInterface CreateVersionMgr(IIPSMobileVersionCallBackInterface CallbackImp,string config)
		{
			if(vMgr != null)
			{
				return vMgr;
			}
			vMgr=  new VersionMgr();
			vMgr.CreateCppVersionManager ();
			mCallback = new IIPSMobileVersionCallBack (CallbackImp);
			mCallback.CreateCppVersionInfoCallBack ();
			bool ret = vMgr.MgrInitVersionManager (mCallback, (uint)config.Length, System.Text.Encoding.ASCII.GetBytes (config));
			if(!ret)
			{
				mLastErr = vMgr.MgrGetVersionMgrLastError();
				return null;
			}
			else
			{
				return vMgr;
			}
		}

		public void DeleteVersionMgr()
		{
			if(vMgr != null)
			{
                vMgr.MgrUnitVersionManager();
				vMgr.DeleteCppVersionManager();
				vMgr = null;
			}
			if(mCallback != null)
			{
				mCallback.DeleteCppVersionCallBack();
				mCallback = null;
			}
		}

		public System.UInt32 GetLastErr()
		{
			return mLastErr;
		}

		private IIPSMobileVersionCallBack mCallback = null;
		private VersionMgr vMgr = null;
		private System.UInt32 mLastErr = 0;
	}
}
	

