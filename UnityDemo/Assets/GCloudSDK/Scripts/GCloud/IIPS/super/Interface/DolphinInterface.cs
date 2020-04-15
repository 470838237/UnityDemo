using System;
using System.Collections.Generic;
using System.Text;

namespace GCloud
{
    namespace Dolphin
    {
        //--------------------------------  UpdateType  --------------------------------//
        public enum UpdateType
        {
            //Resource Update
            UpdateType_Source = 1,
            //Program Update
            UpdateType_Program =2,
            UpdateType_SourceCheckAndSync,//find the files which is deleted or modified and then download it
			UpdateType_SourceCheckAndSync_Optimize_Full,//find the files which is deleted or modified and check the deleted file whether exist or newest in app' first ifs,Final decide to download the files or not. 
			UpdateType_SourceCheckAndSync_Optimize_Part,//use with the UpdateType_Only_FirstExtract_Part ,beside the above consituation,when the file is partly extracted,it's function is the same as  'UpdateType_SourceCheckAndSync'
            UpdateType_SourceCheckAndSync_Optimize_Full_Scatter,//if the resource in app is a scattered type,another word without IFS file.use this type can do the same as UpdateType_SourceCheckAndSync_Optimize_Full
			UpdateType_Only_FirstExtract_All,//All file will be extracted
			UpdateType_Only_FirstExtract_Part,//some files which specially delivered by user that the verbose 'mNeedExtractList' in the init strcut 'UpdateInitInfo' will be extracted
			UpdateType_Only_FirstExtract_Fix,//Extract the files which is in app'ifs and which is the newest now to fill up the outside resource.
            UpdateType_Normal,
        }

        //--------------------------------  NewVersionInfo  --------------------------------//
        public struct NewVersionInfo
        {
            public string versionStr;//New version
            public System.UInt32 needDownloadSize;//Need to download size
            public bool isForce;//Whether to force to update
            public UpdateType updateType;//Update type
            public string userDefineStr;//User-defined string
            public bool isCurrentNewest;//Is the current local version up to date
            public bool isAuditVersion; // is audit version
            public bool isGrayVersion;// is gray veresion
            public bool isNormalVersion;// is normal versioin
        }

        public enum MessageBoxType
        {
            MessageBoxType_Normal = 1,
            MessageBoxType_Retry = 2,
        }

        //--------------------------------  DolphinCallBackInterface  --------------------------------//
        public interface DolphinCallBackInterface
        {
            /// <summary>
            /// After the new version information is obtained, the callback is triggered and the update process enters the wait phase.
            /// Need to implement the interface, manually call the mDolphin.Continue () interface to continue to perform the update, newVersionInfo is the new version information.
            /// </summary>
            /// <param name="newVersionInfo">New version information</param>
            void OnNoticeNewVersionInfo(NewVersionInfo newVersionInfo);

            /// <summary>
            /// Update process progress is obtained through this callback interface during the process of updating or repairing resources.
            /// </summary>
            /// <param name="curVersionStage"> The stage at which the current version is upgraded</param>
            /// <param name="msg">Message about Current State</param>
            /// <param name="nowSize">Currently downloaded size</param>
            /// <param name="totalSize">Total size that needs to be downloaded</param>
            /// <param name="isDownloading">Whether downloading</param>
            void OnUpdateProgressInfo(IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage,string msg, System.UInt32 nowSize, System.UInt32 totalSize, bool isDownloading);
            
            /// <summary>
            /// Information in the process of updating or repairing resources. And the update process enters the waiting phase.
            /// </summary>
            /// <param name="msg">Alert Information during updating</param>
            /// <param name="msgBoxType"> Message box type</param>
            /// <param name="isError"> Whether occur Error</param>
            /// <param name="errorCode"> Error code</param>
            void OnUpdateMessageBoxInfo(string msg, MessageBoxType msgBoxType, bool isError, System.UInt32 errorCode);
            
            /// <summary>
            /// This callback interface informs the installation apk, after receiving this callback, the game can implement the apk installation logic.
            /// </summary>
            /// <param name="apkPath"> Apk path</param>
            void OnNoticeInstallApk(string apkPath);

            /// <summary>
            /// Tell the update or resource repair success with this callback
            /// </summary>
            void OnNoticeUpdateSuccess();

            /// <summary>
            /// After this callback informs that the local resource has been updated, the game needs to update the local resource version number.
            /// </summary>
            /// <param name="newVersionStr">The version of the new resource</param>
            void OnNoticeChangeSourceVersion(string newVersionStr);

            /// <summary>
            /// Tell the First Extract success with this callback
            /// </summary>
            void OnNoticeFirstExtractSuccess();

            /// <summary>
            /// get channel config from server
            /// </summary>
            void OnNoticeServerCfgInfo(string config);
        }

        //--------------------------------  DolphinDateInterface  --------------------------------//
        public interface DolphinDateInterface
        {
            /// <summary>
            /// Implement the interface, return the storage path related to the update process, do not delete and modify this directory, the directory must exist and can be written
            /// </summary>
            /// <returns>Storage path during updating</returns>
            string GetUpdateTempPath();

            /// <summary>
            /// Implement the interface, return the storage path after the resource is decompressed, the game can read the game resources from this directory, the directory must exist and can be written
            /// </summary>
            /// <returns>Resource storage path</returns>
            string GetUpdateSourceSavePath();

            /// <summary>
            /// The interface is currently left unused and can be ignored
            /// </summary>
            /// <returns>Temporarily return any string</returns>
            string GetUserDateString();

            /// <summary>
            /// Implement the interface, return the current resource version number, the form x.x.x.x, belongs to the resource version number set created on GCloud server.It can returns "" if it is program update.
            /// </summary>
            /// <returns>Current source version</returns>
            string GetCurrentSourceVersion();

            /// <summary>
            /// Implement the interface, return the current program version number, the form x.x.x.x, belongs to the program version number set created on GCloud server.
            /// </summary>
            /// <returns>Current program version</returns>
            string GetCurrentProgramVersion();

            /// <summary>
            /// Implement the interface, return the path used to store information related to the update module, do not delete the contents of the modified directory, the directory must exist and can be written
            /// </summary>
            /// <returns> The path to store information related to the update module</returns>
            string GetUpdateApolloPath();
        }


        //--------------------------------   UpdateInitType  --------------------------------//
        public enum UpdateInitType
        {
            UpdateInitType_OnlyProgram = 1,
            UpdateInitType_OnlySource,
            UpdateInitType_SourceCheckAndSync,
			UpdateInitType_SourceCheckAndSync_Optimize_Full,
			UpdateInitType_SourceCheckAndSync_Optimize_Part,
            UpdateInitType_SourceCheckAndSync_Optimize_Full_Scatter,
			UpdateInitType_Only_FirstExtract_All,
			UpdateInitType_Only_FirstExtract_Part,
			UpdateInitType_Only_FirstExtract_Fix,
			UpdateInitType_Normal,
        }

        //--------------------------------  StringInitType  --------------------------------//
        public enum StringInitType
        {
            Chinese = 1,
            English,
        }

        //--------------------------------  UpdateInitInfo  --------------------------------//
        public class UpdateInitInfo
        {
            public UpdateInitType updateInitType = UpdateInitType.UpdateInitType_Normal; //UpdateInitType
            public string gameUpdateUrl = null; //GCloud server url
            public string versionUrl = null; //Reserved field, not ope, Ignorable
            public System.UInt32 updateChannelId = 0; //Update channel ID
            public string userId = ""; //user`s open ID , for gray update)
            public string worldId = ""; //user`s world id,for gray update
            public bool grayUpdate = false;//gray update or not
			public bool getChannelConfig = false;// whether to get channel config
			public bool getRegionid = false; // whether to get region id
            public bool bFirst_source_in_apk = true;//true: first_source.png in APK;false:first_source.png in obb(for google play);
            public bool bObb_type =true; //obb two type: true:main,   false:patch
            public string mNeedExtractList="";//List of files that need to be extracted
            public bool m_append_source_action=true;//Whether to update the resource immediately after First Extract
            public bool mCreateDiffFilelist = false;//Whether create diff file list after updating the resource
        }


        //--------------------------------  DolphinMgrInterface  --------------------------------//
        public interface DolphinMgrInterface
        {
            /// <summary>
            /// Initialize UpdateMgr, program update, resource update, resource repair or first package decompression need to be initialized first, to inform the update related information.
            /// </summary>
            /// <returns>Whether the initialization was successful</returns>
            /// <param name="updateInfo"> Update related initialization information</param>
            /// <param name="needFirstExtract"> Whether to open the First Extract</param>
            /// <param name="strType"> The language of the message during updating,chinese or english</param>
            bool InitUpdateMgr(UpdateInitInfo updateInfo, bool needFirstExtract,StringInitType strType = StringInitType.Chinese);
            
            /// <summary>
            /// </summary>
            /// <returns> Whether uninit successful</returns>
            bool UninitUpdateMgr();

            /// <summary>
            /// Call this interface to starts the update service.
            /// </summary>
            /// <returns> Whether the update service was started successfully</returns>
            bool StartUpdateService();

            /// <summary>
            /// When receiving some callbacks from DolphinCallBackInterface, the update service will be blocked, and you need to call Continue() to continue the update.
            /// </summary>
            /// <returns> Whether continue to update successfully</returns>
            bool Continue();

            /// <summary>
            /// Call this interface to stops the update service.
            /// </summary>
            /// <returns> Whether stop the update service successfully</returns>
            bool StopUpdateService();

            /// <summary>
            /// In the game frame loop Update(), you need to call mgr.DriveUpdateService() to refresh the update process.
            /// </summary>
            void DriveUpdateService();

            /// <summary>
            /// Call this interface to gets the current download speed.
            /// </summary>
            /// <returns> The current download speed</returns>
            System.UInt32 GetCurrentDownSpeed();

            /// <summary>
            /// This interface can be ignored for the time being. 
            /// It`s used to set whether need First Extract , this setting has been done in the interface InitUpdateMgr    
            /// </summary>
            /// <param name="needFirstExtract"> whether need First Extract</param>
            void SetNeedFirstExtract(bool needFirstExtract);
        }

        //--------------------------------  DolphinFactory  --------------------------------//
        public class DolphinFactory
        {
            private DolphinMgrImp mDolphinMgr = null;
            private DolphinCallBackInterface mCallBackImp = null;
            private DolphinDateInterface mDateMgr = null;

            /// <summary>
            /// This method is used to create the implementation class DolphinMgrImp of DolphinMgrInterface, 
            /// Through the instance of DolphinMgrImp，Perform different update processes.
            /// </summary>
            /// <returns>An instance of DolphinMgrImp</returns>
            /// <param name="callBackImp">DolphinCallBackInterface</param>
            /// <param name="dateMgr">DolphinDateInterface</param>
            public DolphinMgrInterface CreateDolphinMgr(DolphinCallBackInterface callBackImp, DolphinDateInterface dateMgr)
            {
                if (callBackImp == null || dateMgr == null)
                {
                    return null;
                }
                if (mDolphinMgr != null)
                {
                    return mDolphinMgr;
                }
                mDolphinMgr = new DolphinMgrImp();
                mCallBackImp = callBackImp;
                mDolphinMgr.SetCallBack(mCallBackImp);
                mDateMgr = dateMgr;
                mDolphinMgr.SetDateMgr(mDateMgr);
                return mDolphinMgr;
            }

            public DolphinMgrInterface GetDolphinMgr()
            {
                return mDolphinMgr;
            }
        }
    }
}
