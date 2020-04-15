using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIPSMobile
{
    public interface IIPSMobilePufferCallbackInterface
    {
        void OnPufferInitReturn(bool isSuccess, System.UInt32 errorCode);

        void OnPufferInitProgress(System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize);

        void OnPufferDownloadReturn(System.UInt64 taskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode);
		
		void OnPufferDownloadProgress(System.UInt64 taskId, System.UInt64 nowSize, System.UInt64 totalSize);

        void OnPufferRestoreReturn(bool isSuccess, System.UInt32 errorCode);

        void OnPufferRestoreProgress(System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize);

        void OnPufferDownloadBatchReturn(System.UInt64 batchTaskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode, System.UInt32 batchType, string strRet);

        void OnPufferDownloadBatchProgress(System.UInt64 batchTaskId, System.UInt64 nowSize, System.UInt64 totalSize);

    }
    public interface IIPSMobilePufferInterface
    {
        bool PufferPluginInit(IIPSMobilePufferCallbackInterface cb, string config);

        void PufferPluginUninit();

        void PufferPluginUpdate();

        System.UInt32 PufferPluginGetFileId(string filepath);

        bool PufferPluginIsFileReady(System.UInt32 fileId);

        System.UInt32 PufferPluginGetFileSizeCompressed(System.UInt32 fileId);

        System.UInt64 PufferPluginDownloadFile(System.UInt32 fileId, bool forceSync,System.UInt32 priority);

        System.UInt64 PufferPluginStartRestoreFiles(System.UInt32 priority);

        bool PufferPluginRemoveTask(System.UInt64 taskId);

        bool PufferPluginSetImmDLMaxSpeed(System.UInt64 maxSpeed);

        bool PufferPluginSetImmDLMaxTask(System.UInt32 maxTask);

        bool PufferPluginSetTaskPriority(System.UInt64 taskId, System.UInt32 priority);

		System.Double PufferPluginGetCurrentSpeed();

        bool PufferPluginGetBatchDirFileCount(string dir, bool blSubDir, ref System.UInt32 nTotal);

        System.UInt64 PufferPluginDownloadBatchDir(string dir, bool blSubDir, bool forceSync, System.UInt32 priority);

		void PufferPluginPrepare4DownloadBatchList();

		bool PufferPluginAddBatchListItem(System.UInt32 fileId);	
		
        System.UInt64 PufferPluginDownloadBatchList(bool forceSync, System.UInt32 priority);
		
		bool PufferPluginPauseTask(System.UInt64 taskId);
		
		bool PufferPluginResumeTask(System.UInt64 taskId);

    }

    public class IIPSMobilePuffer
    {
        public static IIPSMobilePufferInterface CreateIIPSMobilePufferMgr()
        {
            IIPSMobilePufferPlugin mgr = new IIPSMobilePufferPlugin();
            mgr.CreateCppPufferMgr();
            return mgr;
        }

        public static void DestroyIIPSMobilePufferMgr(IIPSMobilePufferInterface mgr)
        {
            if(mgr != null)
            {
                IIPSMobilePufferPlugin pufferMgr = (IIPSMobilePufferPlugin)mgr;
                if(pufferMgr != null)
                {
                    pufferMgr.DestoryCppPufferMgr();
                }
            }
        }
    }
}
