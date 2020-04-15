using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace IIPSMobile
{
    public class IIPSPufferPluginName
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX
        public const string pluginName = "gcloud";
#else
	    public const string pluginName = "gcloud";
#endif
    }

    public class IIPSPufferPluginCallBack
    {
        internal delegate void OnInitReturnFunc(System.IntPtr callback, bool isSuccess, System.UInt32 errorCode);

        internal delegate void OnInitProgresFunc(System.IntPtr callback, System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize);

        internal delegate void OnDownloadReturnFunc(System.IntPtr callback, System.UInt64 taskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode);
		
		internal delegate void OnDownloadProgressFunc(System.IntPtr callback, System.UInt64 taskId, System.UInt64 nowSize, System.UInt64 totalSize);

        internal delegate void OnRestoreReturnFunc(System.IntPtr callback, bool isSuccess, System.UInt32 errorCode);

        internal delegate void OnRestoreProgresFunc(System.IntPtr callback, System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize);

        internal delegate void OnDownloadBatchReturnFunc(System.IntPtr callback, System.UInt64 batchTaskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode, System.UInt32 batchType, string strRet);

        internal delegate void OnDownloadBatchProgressFunc(System.IntPtr callback, System.UInt64 batchTaskId, System.UInt64 nowSize, System.UInt64 totalSize);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.IntPtr CreatePufferCallBack(OnInitReturnFunc onInitReturnFunc, OnInitProgresFunc onInitProgresFunc,
                                                               OnDownloadReturnFunc onDownloadReturnFunc, OnDownloadProgressFunc onDownloadProgressFunc,
                                                                        OnRestoreReturnFunc onRestoreReturnFunc, OnRestoreProgresFunc onRestoreProgresFunc,
                                                                                OnDownloadBatchReturnFunc onDownloadBatchReturnFunc, OnDownloadBatchProgressFunc onDownloadBatchProgressFunc, System.IntPtr callback);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern void DestroyPufferCallBack(System.IntPtr callback);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.IntPtr GetPufferCallBackGCHandle(System.IntPtr callback);

        public System.IntPtr mCallBack = System.IntPtr.Zero;
        private System.IntPtr pManagedObject = System.IntPtr.Zero;
        static IIPSMobilePufferCallbackInterface mImpCB = null;
        public IIPSPufferPluginCallBack(IIPSMobilePufferCallbackInterface imp)
        {
		    pManagedObject = GCHandle.ToIntPtr(GCHandle.Alloc(imp, GCHandleType.Normal));
	    }

        public void CreateCppPufferCallBack()
        {
            mCallBack = CreatePufferCallBack(OnInitReturn, OnInitProgress, OnDownloadReturn, OnDownloadProgress, OnRestoreReturn, OnRestoreProgress, OnDownloadBatchReturn, OnDownloadBatchProgress, pManagedObject);
        }

        public void DeleteCppVersionCallBack()
        {
            if (mCallBack != System.IntPtr.Zero)
            {
                System.IntPtr ptr = GetPufferCallBackGCHandle(mCallBack);
                GCHandle handle = GCHandle.FromIntPtr(ptr);
                handle.Free();
                DestroyPufferCallBack(mCallBack);
                mCallBack = System.IntPtr.Zero;
            }
        }

        [MonoPInvokeCallback(typeof(OnInitReturnFunc))]
        public static void OnInitReturn(System.IntPtr callback, bool isSuccess, System.UInt32 errorCode)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferInitReturn(isSuccess,errorCode);
        }

        [MonoPInvokeCallback(typeof(OnInitProgresFunc))]
        public static void OnInitProgress(System.IntPtr callback, System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferInitProgress(stage, nowSize,totalSize);
        }

        [MonoPInvokeCallback(typeof(OnDownloadReturnFunc))]
        public static void OnDownloadReturn(System.IntPtr callback, System.UInt64 taskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferDownloadReturn(taskId,fileid,isSuccess,errorCode);
        }
		
		[MonoPInvokeCallback(typeof(OnDownloadProgressFunc))]
        public static void OnDownloadProgress(System.IntPtr callback, System.UInt64 taskId, System.UInt64 nowSize, System.UInt64 totalSize)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferDownloadProgress(taskId,nowSize,totalSize);
        }
       
        [MonoPInvokeCallback(typeof(OnRestoreReturnFunc))]
        public static void OnRestoreReturn(System.IntPtr callback, bool isSuccess, System.UInt32 errorCode)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferRestoreReturn(isSuccess, errorCode);
        }

        [MonoPInvokeCallback(typeof(OnRestoreProgresFunc))]
        public static void OnRestoreProgress(System.IntPtr callback, System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferRestoreProgress(stage, nowSize, totalSize);
        }
        
        [MonoPInvokeCallback(typeof(OnDownloadBatchReturnFunc))]
        public static void OnDownloadBatchReturn(System.IntPtr callback, System.UInt64 batchTaskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode, System.UInt32 batchType, string strRet)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferDownloadBatchReturn(batchTaskId, fileid, isSuccess, errorCode, batchType, strRet);
        }

        [MonoPInvokeCallback(typeof(OnDownloadBatchProgressFunc))]
        public static void OnDownloadBatchProgress(System.IntPtr callback, System.UInt64 batchTaskId, System.UInt64 nowSize, System.UInt64 totalSize)
        {
            GCHandle handle = GCHandle.FromIntPtr(callback);
            mImpCB = (IIPSMobilePufferCallbackInterface)handle.Target;
            mImpCB.OnPufferDownloadBatchProgress(batchTaskId, nowSize, totalSize);
        }
    }

    public class IIPSMobilePufferPlugin : IIPSMobilePufferInterface
    {
#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.IntPtr CreatePufferManager();

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern void DestoryPufferManager(System.IntPtr pufferMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool InitPufferManager(System.IntPtr pufferMgr, System.IntPtr callback, System.UInt32 bufferSize, System.IntPtr configBuffer);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern void UninitPufferManager(System.IntPtr pufferMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern void PufferManagerUpdate(System.IntPtr pufferMgr);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt32 PufferManagerGetFileId(System.IntPtr pufferMgr, [MarshalAs(UnmanagedType.LPStr)]string szFileName);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerIsFileReady(System.IntPtr pufferMgr, System.UInt32 fileID);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt32 PufferManagerGetFileSizeCompressed(System.IntPtr pufferMgr, System.UInt32 fileId);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt64 PufferManagerDownloadFile(System.IntPtr pufferMgr, System.UInt32 fileID, bool forceSync,System.UInt32 priority);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt64 PufferManagerStartRestoreFiles(System.IntPtr pufferMgr, System.UInt32 priority);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerRemoveTask(System.IntPtr pufferMgr, System.UInt64 taskID);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
		[DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
		static extern bool PufferManagerSetImmDLMaxSpeed(System.IntPtr pufferMgr, System.UInt64 maxSpeed);

#if UNITY_IPHONE && ! (UNITY_EDITOR || UNITY_EDITOR_OSX)
		[DllImport("__Internal",ExactSpelling = true)]
#else
		[DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerSetImmDLMaxTask(System.IntPtr pufferMgr, System.UInt32 maxTask);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerSetTaskPriority(System.IntPtr pufferMgr, System.UInt64 taskId, System.UInt32 priority);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.Double PufferManagerGetCurrentSpeed(System.IntPtr pufferMgr);


#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerGetBatchDirFileCount(System.IntPtr pufferMgr, string dir, bool blSubDir, ref System.UInt32 nTotal);


#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt64 PufferManagerDownloadBatchDir(System.IntPtr pufferMgr, string dir, bool blSubDir, bool forceSync, System.UInt32 priority);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern void PufferManagerPrepare4DownloadBatchList(System.IntPtr pufferMgr);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerAddBatchListItem(System.IntPtr pufferMgr, System.UInt32 fileID);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern System.UInt64 PufferManagerDownloadBatchList(System.IntPtr pufferMgr, bool forceSync, System.UInt32 priority);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerPauseTask(System.IntPtr pufferMgr, System.UInt64 taskID);

#if UNITY_IPHONE && !(UNITY_EDITOR || UNITY_EDITOR_OSX)
	    [DllImport("__Internal",ExactSpelling = true)]
#else
        [DllImport(IIPSPufferPluginName.pluginName, ExactSpelling = true)]
#endif
        static extern bool PufferManagerResumeTask(System.IntPtr pufferMgr, System.UInt64 taskID);
		
        private System.IntPtr mCppPufferMgr = System.IntPtr.Zero;

        private IIPSPufferPluginCallBack mCallback = null;

        public void CreateCppPufferMgr()
        {
            mCppPufferMgr = CreatePufferManager();
        }

        public void DestoryCppPufferMgr()
        {
            DestoryPufferManager(mCppPufferMgr);
        }

        public bool PufferPluginInit(IIPSMobilePufferCallbackInterface cb, string config)
        {
            mCallback = new IIPSPufferPluginCallBack(cb);
            mCallback.CreateCppPufferCallBack();
            if (mCppPufferMgr == System.IntPtr.Zero)
            {
                return false;
            }
            byte[] configBuffer = System.Text.Encoding.ASCII.GetBytes(config);
            GCHandle pinnedArray = GCHandle.Alloc(configBuffer, GCHandleType.Pinned);
            System.IntPtr configPointer = pinnedArray.AddrOfPinnedObject();
            pinnedArray.Free();
            return InitPufferManager(mCppPufferMgr, mCallback.mCallBack, (System.UInt32)config.Length, configPointer);
        }

        public void PufferPluginUninit()
        {
            if (mCppPufferMgr == System.IntPtr.Zero)
            {
                return;
            }
            UninitPufferManager(mCppPufferMgr);
            if(mCallback != null)
            {
                mCallback.DeleteCppVersionCallBack();
                mCallback = null;
            }
        }

        public void PufferPluginUpdate()
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                PufferManagerUpdate(mCppPufferMgr);
            }
        }

        public System.UInt32 PufferPluginGetFileId(string filepath)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerGetFileId(mCppPufferMgr,filepath);
            }
            else
            {
                return System.UInt32.MaxValue;
            }
        }

        public bool PufferPluginIsFileReady(System.UInt32 fileId)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerIsFileReady(mCppPufferMgr, fileId);
            }
            else
            {
                return false;
            }
        }

        public System.UInt32 PufferPluginGetFileSizeCompressed(System.UInt32 fileId)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerGetFileSizeCompressed(mCppPufferMgr, fileId);
            }
            else
            {
                return 0;
            }
        }

        public System.UInt64 PufferPluginDownloadFile(System.UInt32 fileId, bool forceSync, System.UInt32 priority)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerDownloadFile(mCppPufferMgr, fileId,forceSync,priority);
            }
            else
            {
                return System.UInt32.MaxValue;
            }
        }

        public System.UInt64 PufferPluginStartRestoreFiles(System.UInt32 priority)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerStartRestoreFiles(mCppPufferMgr, priority);
            }
            else
            {
                return System.UInt32.MaxValue;
            }
        }

        public bool PufferPluginRemoveTask(System.UInt64 taskId)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerRemoveTask(mCppPufferMgr, taskId);
            }
            else
            {
                return false;
            }
        }

        public bool PufferPluginSetImmDLMaxSpeed(System.UInt64 maxSpeed)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerSetImmDLMaxSpeed(mCppPufferMgr, maxSpeed);
            }
            else
            {
                return false;
            }
        }

        public bool PufferPluginSetImmDLMaxTask(System.UInt32 maxTask)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerSetImmDLMaxTask(mCppPufferMgr, maxTask);
            }
            else
            {
                return false;
            }
        }

        public bool PufferPluginSetTaskPriority(System.UInt64 taskId, System.UInt32 priority)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerSetTaskPriority(mCppPufferMgr, taskId, priority);
            }
            else
            {
                return false;
            }
        }

		public System.Double PufferPluginGetCurrentSpeed()
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerGetCurrentSpeed(mCppPufferMgr);
            }
            else
            {
                return 0.0;
            }
        }

        public bool PufferPluginGetBatchDirFileCount(string dir, bool blSubDir, ref System.UInt32 nTotal)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerGetBatchDirFileCount(mCppPufferMgr, dir, blSubDir, ref nTotal);
            }
            else
            {
                return false;
            }
        }

        public System.UInt64 PufferPluginDownloadBatchDir(string dir, bool blSubDir, bool forceSync, System.UInt32 priority)
        {
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerDownloadBatchDir(mCppPufferMgr, dir, blSubDir, forceSync, priority);
            }
            else
            {
                return System.UInt32.MaxValue;
            }
        }

		public void PufferPluginPrepare4DownloadBatchList()
		{
            if (mCppPufferMgr != System.IntPtr.Zero)
            {
                PufferManagerPrepare4DownloadBatchList(mCppPufferMgr);
            }
		}

		public bool PufferPluginAddBatchListItem(System.UInt32 fileID)
		{
		    if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerAddBatchListItem(mCppPufferMgr, fileID);
            }
			else
			{
				return false;
			}
		}

		public System.UInt64 PufferPluginDownloadBatchList(bool forceSync, System.UInt32 priority)
		{
		    if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerDownloadBatchList(mCppPufferMgr, forceSync, priority);
            }
			else
			{
                return System.UInt32.MaxValue;
			}
		}
		
		public bool PufferPluginPauseTask(System.UInt64 taskId)
		{
			if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerPauseTask(mCppPufferMgr, taskId);
            }
            else
            {
                return false;
            }
		}
		
		public bool PufferPluginResumeTask(System.UInt64 taskId)
		{
			if (mCppPufferMgr != System.IntPtr.Zero)
            {
                return PufferManagerResumeTask(mCppPufferMgr, taskId);
            }
            else
            {
                return false;
            }
		}

    }
}
