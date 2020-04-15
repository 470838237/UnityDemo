using System;
using System.Collections.Generic;
using System.Text;

namespace GCloud
{
    namespace Puffer
    {
        public enum PufferUpdateInitType
        {
            Puffer_UpdateInitType_SeparateRes = 0,//As the default type, the behavior is equivalent to the old protocol, that is, only the latest unassociated puffer version is obtained.
            Puffer_UpdateInitType_Res4DolphinApp = 1,//Get the corresponding version of the puffer associated with the Dolphin app version line
            Puffer_UpdateInitType_Res4DolphinRes = 2,//Get the corresponding version of the puffer associated with the Dolphin res version line
        }
			
        public class PufferConfig
        {
            //Puffer maximum download speed, default is 10M/s
            public System.UInt32 maxDownloadSpeed = 10*1024*1024;

            //The maximum number of concurrent Puffer tasks. The default is 3.
            public System.UInt32 maxDownTask = 3;

            //Puffer single task maximum number of concurrent download links, the default is 3
            public System.UInt32 maxDownloadsPerTask = 3;

            //The local resource directory downloaded by Puffer, the downloaded file will be saved in this path.
            public string resDir = null;

            //Puffer download server address, the same as the puffer page configuration
            public string pufferServerUrl = null;

            //Puffer corresponding ProductId, the same as the puffer page configuration
            public System.UInt32 pufferProductId = 0;

            //(puffer does not need to set this parameter)
            public string pufferGroupMarkId = "";

            //Check the file status ID, whether the file needs to be checked for status
			public bool needCheck = false;

            //Identify whether to repair the resource
			public bool needFileRestore = false;
            
            //Identify whether to remove old files when puffer files are modified and need to be update, true means remove old files, false means remain old files and user need to remove them when necessary.
            public bool removeOldWhenUpdate = true;

            //(puffer does not need to set this parameter)
			public string userId = "";

            //GameId corresponding to Puffer
			public System.Int64 pufferGameId = 0;

            //Puffer download update type, supports three types
			public PufferUpdateInitType pufferUpdateType = PufferUpdateInitType.Puffer_UpdateInitType_SeparateRes;
			
            //The Puffer multi-version line function parameter takes effect when the pufferUpdateType is Puffer_UpdateInitType_Res4DolphinApp or Puffer_UpdateInitType_Res4DolphinRes, and obtains the puffer version information associated with the Dolphin productId.
            public System.UInt32 dolphinProductId = 0;

            //The Puffer multi-version line function parameter takes effect when the pufferUpdateType is Puffer_UpdateInitType_Res4DolphinApp or Puffer_UpdateInitType_Res4DolphinRes, and is used together with the dolphinProductId parameter to obtain the puffer version information of the app version in the puffer version associated with the specific Dolphin productId.      
			public string dolphinAppVersion = "";

            //The Puffer multi-version line function parameter takes effect when the pufferUpdateType is Puffer_UpdateInitType_Res4DolphinRes. It is used together with the dolphinProductId and dolphinAppVersion parameters to obtain the puffer version information of the app version and the res version in the puffer version associated with the specific Dolphin productId.
			public string dolphinResVersion = "";

            // audit version or not
            public bool isAuditVersion = false;
        }

        public enum PufferInitStage
        {
            PIS_Start = 0,
            PIS_DownResSnapshot = 1, 
            PIS_UpdateFileList = 2,
            PIS_GetResURL = 3,
        }
		public enum PufferRestoreStage	
	    {		
            PIS_StartRestore = 0,		
            PIS_LoadLocalFilelist,		
            PIS_CheckFilelistMd5,		
            PIS_RestoreEnd,	
	    }
		public enum PufferBatchDownloadType
		{		
			PBT_BatchTask = 0,	//Indicates that the batch download task callback
			PBT_FileTask,		//Indicates that a single file download task callback in a batch download task
			PBT_FileTask_Retry	//Indicates that the download of a single task in the batch download task fails. You need to try to download again. The progress callback of the batch download may be rolled back. You can prompt the user accordingly.
		}
        public interface IPufferCallBack
        {
            /// <summary>
            /// Initialization callback result, other Puffer operations must be called after the initialization callback is successful, that is, after isSuccess is true, otherwise all calls have no meaning;
            ///  </summary>
            /// <param name="isSuccess">Whether the initialization is successful</param>
            /// <param name="errorCode"> Error code</param>
            void OnInitReturn(bool isSuccess, System.UInt32 errorCode);

            /// <summary>
            /// Initialization progress callback
            /// </summary>
            /// <param name="stage"> stage of Initialization</param>
            /// <param name="nowSize"> Current updated new version of eifs file size</param>
            /// <param name="totalSize"> Total size to update</param>
            void OnInitProgress(PufferInitStage stage, System.UInt32 nowSize, System.UInt32 totalSize);

            /// <summary>
            /// The download task callback result .the callback result of the download single file task through the DownloadFile function.
            /// </summary>
            /// <param name="taskId"> Task ID</param>
            /// <param name="fileid"> File ID</param>
            /// <param name="isSuccess"> Whether the download was successful</param>
            /// <param name="errorCode">Error code</param>
            void OnDownloadReturn(System.UInt64 taskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode);
			
            /// <summary>
            /// Download task progress callback, which refers to the progress callback of downloading single file task through DownloadFile operation
            /// </summary>
            /// <param name="taskId">Task ID</param>
            /// <param name="nowSize"> Current download size</param>
            /// <param name="totalSize"> Total size to download</param>
			void OnDownloadProgress(System.UInt64 taskId, System.UInt64 nowSize, System.UInt64 totalSize);

            /// <summary>
            /// The interface is temporarily reserved and will not be called. Checking whether the file needs to be repaired is completed in the initialization step, and the corresponding check result is callback to OnInitReturn.
            /// </summary>
            /// <param name="isSuccess">isSuccess</param>
            /// <param name="errorCode"> Error code</param>
            void OnRestoreReturn(bool isSuccess, System.UInt32 errorCode);

            /// <summary>
            /// The interface is temporarily reserved and will not be called. Checking whether the file needs to be repaired is completed in the initialization step, and the corresponding check progress callback is OnInitReturn.
            /// </summary>
            /// <param name="stage">Current file check status</param>
            /// <param name="nowSize">Current check size</param>
            /// <param name="totalSize"> Total size to check</param>
            void OnRestoreProgress(PufferRestoreStage stage, System.UInt32 nowSize, System.UInt32 totalSize);

            /// <summary>
            /// Download the task callback result, which refers to the callback result of downloading the multi-file task through the DownloadBatchDir/DownloadBatchList operation;
            /// </summary>
            /// <param name="batchTaskId">Task ID, download the task ID returned by multiple files through the DownloadBatchDir/DownloadBatchList interface</param>
            /// param fileid //File handle, when a specified file download fails during the multi-file download process, OnDownloadBatchReturn will return the error information corresponding to the fileid. The fileid is valid only when the batchType is PBT_FileTask = 1, and when the batchType is PBT_FileTask = 0, the file id is invalid.
            ///                When the batchType is PBT_FileTask = 2, it means that the file download failed, and the puffer tries to download the file again.
            /// <param name="isSuccess">whether succeed</param>
            /// <param name="errorCode">Error code</param>
            /// param batchType //callback type (PBT_BatchTask = 0 means that the callback result corresponds to the entire multi-file download task, and PBT_FileTask = 1 or PBT_FileTask = 2 means that the callback result corresponds to a specific specific file in the entire multi-file download task)
            /// param strRet //When batchType is 0, strRet returns a summary of the results of this multi-file download task, such as {"ret":true,"errcode":0,"sucs_num":50,"fail_num":0}
            void OnDownloadBatchReturn(System.UInt64 batchTaskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode, PufferBatchDownloadType batchType, string strRet);

            /// <summary>
            /// Download task progress callback, which refers to the progress callback of downloading multi-file tasks through the DownloadBatchDir/DownloadBatchList operation;
            /// </summary>
            /// <param name="batchTaskId">Task ID, download the task ID returned by multiple files through the DownloadBatchDir/DownloadBatchList interface</param>
            /// <param name="nowSize">Current download size</param>
            /// <param name="totalSize">Total size to download</param>
            void OnDownloadBatchProgress(System.UInt64 batchTaskId, System.UInt64 nowSize, System.UInt64 totalSize);

        }
        public interface IPufferMgrInterface
        {
            /// <summary>
            /// This process will be networked, and will automatically update the new version of eifs on the network (only the part that updates the log file information) to the local puffer_res.eifs. 
            /// After successful initialization, there will be a callback that is successfully initialized.
            /// </summary>
            /// <returns>whether succeed</returns>
            /// <param name="config">Puffer Initial configuration</param>
            /// <param name="cb">The object that implements the IPufferCallBack interface</param>
            bool InitResManager(PufferConfig config, IPufferCallBack cb);

            /// <summary>
            /// Deinitialize, corresponding to InitResManager, release resources
            /// </summary>
            void UnInitResManager();

            /// <summary>
            /// Need to call this interface in every frame of the game Update()
            /// </summary>
            void Update();

            /// <summary>
            /// Get the id of the file through the file path
            /// </summary>
            /// <returns>file ID</returns>
            /// <param name="filePath">file path</param>
            System.UInt32 GetFileId(string filePath);

            /// <summary>
            /// Determine whether the file already exists by the id of the file.
            /// </summary>
            /// <returns>true:exist false:not exist</returns>
            /// <param name="fileId">File ID</param>
            bool IsFileReady(System.UInt32 fileId);

            /// <summary>
            /// Get the compressed size of the file by the id of the file
            /// </summary>
            /// <returns> File compressed size</returns>
            /// <param name="fileId">File ID</param>
            System.UInt32 GetFileSizeCompressed(System.UInt32 fileId);

            /// <summary>
            /// Download a single file by file id
            /// </summary>
            /// <returns>The id of the download task</returns>
            /// <param name="fileId">File ID</param>
            /// param forceDownload //Whether to force the update, if it is true, it will force the download. If it is false, it will check if it needs to be updated (equivalent to calling IsFileReady first), then decide whether to download.
            /// <param name="priority">Task priority</param>
            System.UInt64 DownloadFile(System.UInt32 fileId, bool forceDownload, System.UInt32 priority);

            /// <summary>
            /// Start performing resource repair
            /// </summary>
            /// <returns>Task ID</returns>
            /// <param name="priority"> task priority</param>
            System.UInt64 StartRestoreFiles(System.UInt32 priority);

            /// <summary>
            /// Remove the task. Starting the download file will get the task id to perform the operation, and then generally remove this task in the callback after downloading the file.
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="taskId"> task id</param>
            bool RemoveTask(System.UInt64 taskId);

            /// <summary>
            /// Dynamically set the maximum download speed in b/s
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="maxSpeed"> Maximum speed in b/s</param>
			bool SetDLMaxSpeed(System.UInt64 maxSpeed);

            /// <summary>
            /// Dynamically set the maximum number of download tasks
            /// </summary>
            /// <returns>whether succeed</returns>
            /// <param name="maxTask"> Max task number</param>
			bool SetDLMaxTask(System.UInt32 maxTask);
			
            /// <summary>
            /// Dynamically adjust task priority
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="taskId"> task ID</param>
            /// <param name="priority"> task priority</param>
		    bool SetTaskPriority(System.UInt64 taskId, System.UInt32 priority);

            /// <summary>
            /// Get current download speed
            /// </summary>
            /// <returns> current speed</returns>
			System.Double GetCurrentSpeed();

            /// <summary>
            /// Get the number of files in the specified directory of eifs
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="dir"> file directory</param>
            /// <param name="blSubDir"> whether to traverse subdirectories</param>
            /// <param name="nTotal"> total number of files obtained after executing this method</param>
		    bool GetBatchDirFileCount(string dir, bool blSubDir, ref System.UInt32 nTotal);

            /// <summary>
            /// Download the files in the specified directory
            /// </summary>
            /// <returns> download task ID</returns>
            /// <param name="dir"> Specify the directory to download</param>
            /// <param name="blSubDir"> whether to traverse subdirectories</param>
            /// param forceSync // Whether to force the update, if it is true, all files will be forced to download. If it is false, each file will check whether it needs to be updated before deciding whether to download.
            /// <param name="priority"> task priority</param>
            System.UInt64 DownloadBatchDir(string dir, bool blSubDir, bool forceSync, System.UInt32 priority);

            /// <summary>
            /// Downloads the batch list.
            /// </summary>
            /// <returns> download task ID</returns>
            /// <param name="lst"> List of files to download</param>
            /// param forceSync // Whether to force the update, if it is true, all files will be forced to download. If it is false, each file will check whether it needs to be updated before deciding whether to download. 
            /// <param name="priority"> task priority</param>
			System.UInt64 DownloadBatchList(List<string> lst, bool forceSync, System.UInt32 priority);
			
            /// <summary>
            /// Pause a download task
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="taskId"> task ID</param>
			bool PauseTask(System.UInt64 taskId);
			
            /// <summary>
            /// Resume a download task
            /// </summary>
            /// <returns> whether succeed</returns>
            /// <param name="taskId"> task ID</param>
			bool ResumeTask(System.UInt64 taskId);
        }

        public class PufferFactory
        {
            public const System.UInt32 InvalidFileId = System.UInt32.MaxValue;
            public const System.UInt64 InvalidTaskId = System.UInt64.MaxValue;

            private volatile static PufferFactory _instance = null;
            private static readonly object lockHelper = new object();
            private PufferFactory() { }
            public static PufferFactory GetInstance()
            {
                if (_instance == null)
                {
                    lock (lockHelper)
                    {
                        if (_instance == null)
                            _instance = new PufferFactory();
                    }
                }
                return _instance;
            }

            private IPufferMgrInterface resManager = null;

            public void CreatePufferMgr()
            {
                if(resManager == null)
                {
                    resManager = new PufferResMgrCore();
                }
            }

            public IPufferMgrInterface GetPufferMgr()
            {
                return resManager;
            }

            public void ReleasePufferMgr()
            {
                resManager = null;
            }
        }
    }
}

