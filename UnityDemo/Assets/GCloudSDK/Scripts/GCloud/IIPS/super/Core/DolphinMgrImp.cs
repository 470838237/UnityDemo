using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GCloud
{
    namespace Dolphin
    {
		public enum Filelist_Check_Type
		{
			Filelist_Check_Type_Normal=1,
			Filelist_Check_Type_Part =2,
			Filelist_Check_Type_Full =3,
			Filelist_Check_Type_Scatter =4
		}

		public enum First_Extract_type{
			m_type_extract_all = 1,
			m_type_extract_part_resource,
			m_type_extract_fixToAll
		}
        public class UpdateVariables
        {
            public uint uErr = 0;
            public bool bGetVersion = false;
            public bool bSuccess = false;
            public System.UInt64 progress = 0;
            public System.UInt32 uspeed = 0;
            public bool bNoticeInstall = false;
            public string ApkPath;
            public NewVersionInfo newVerInfo = new NewVersionInfo();
        }
        public class DolphinMgrImp : DolphinMgrInterface, IIPSMobile.IIPSMobileVersionCallBackInterface
        {
            private DolphinCallBackInterface mCallBack = null;
            private DolphinDateInterface mDateMgr = null;
            private UpdateInitInfo mUpdateInitInfo = new UpdateInitInfo();
            private bool mNeedFirstExtract = false;
            private UpdateType mCurrentUpdateType = UpdateType.UpdateType_Program;

            private IIPSMobile.IIPSMobileVersion mVersionFactroy = null;
            private IIPSMobile.IIPSMobileVersionMgrInterface mVersionMgr = null;
            private bool mWaitMessageBox = false;
            private bool mWaitNewVersion = false;
            private bool mShouldStop = false;

            private string mNewVersionStr;
            private UpdateVariables updateVariables = null;

            private IDolphinInfoStringInterface mUpdateInfoString = null;
            private IDolphinErrorStringInterface mUpdateErrorString = null;
            private IIPSMobile.IIPSMobileErrorCodeCheck mErrorCheck = new IIPSMobile.IIPSMobileErrorCodeCheck();
            private DolphinSpeedCounter speedCounter = new DolphinSpeedCounter();
            private IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE mCurVersionStage = IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_Start;

            public UpdateType GetCurrentUpdteType()
            {
                return mCurrentUpdateType;
            }

            public void SetCallBack(DolphinCallBackInterface callback)
            {
                mCallBack = callback;
            }
            public void SetDateMgr(DolphinDateInterface dateMgr)
            {
                mDateMgr = dateMgr;
            }
            public bool InitUpdateMgr(UpdateInitInfo updateInfo, bool needFirstExtract, StringInitType strType = StringInitType.Chinese)
            {
                ADebug.Log(string.Format("[ApolloUpdate] initupdatemgr type:{0},url:{1},channelid:{2},gray:{3}"
                    , updateInfo.updateInitType, updateInfo.gameUpdateUrl, updateInfo.updateChannelId, updateInfo.grayUpdate));

                if(strType == StringInitType.Chinese)
                {
                    mUpdateInfoString = new DolphinInfoString();
                    mUpdateErrorString = new DolphinErrorString();
                }
                else 
                {
                    mUpdateInfoString = new DolphinInfoStringEnglish();
                    mUpdateErrorString = new DolphinErrorStringEnglish();
                }

                mUpdateInitInfo.updateInitType = updateInfo.updateInitType;
                mUpdateInitInfo.gameUpdateUrl = updateInfo.gameUpdateUrl;
				mUpdateInitInfo.versionUrl = updateInfo.versionUrl;
                mUpdateInitInfo.updateChannelId = updateInfo.updateChannelId;
                mNeedFirstExtract = needFirstExtract;
                mUpdateInitInfo.userId = updateInfo.userId;
                mUpdateInitInfo.worldId = updateInfo.worldId;
                mUpdateInitInfo.grayUpdate = updateInfo.grayUpdate;
                mUpdateInitInfo.getChannelConfig = updateInfo.getChannelConfig;
                mUpdateInitInfo.getRegionid = updateInfo.getRegionid;
                mUpdateInitInfo.bFirst_source_in_apk = updateInfo.bFirst_source_in_apk;
                mUpdateInitInfo.bObb_type = updateInfo.bObb_type;
				mUpdateInitInfo.mNeedExtractList = updateInfo.mNeedExtractList;
				mUpdateInitInfo.mCreateDiffFilelist = updateInfo.mCreateDiffFilelist;
                return true;
            }
            public bool UninitUpdateMgr()
            {
                return true;
            }
            public bool StartUpdateService()
            {
                mShouldStop = false;
                if (mCallBack == null || mDateMgr == null)
                {
                    ADebug.LogError("[ApolloUpdate] StartUpdateService failed,mCallBack == null or mDateMgr == null");
                    return false;
                }
//
//                if (mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_OnlySource)
//                {
//                    ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
//                    return StartSourceUpdate();
//                }
//                else if (mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_Normal || mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_OnlyProgram)
//                {
//                    ADebug.Log(string.Format("[ApolloUpdate] StartUpdateService TYPE:{0}", mUpdateInitInfo.updateInitType));
//                    return StartProgramUpdate();
//                }
//				else if (mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_SourceCheckAndSync)
//                {
//                    ADebug.Log("[ApolloUpdate] StartUpdateService for SourceCheckAndSync");
//                    return StartSourceCheckAndSync();
//                }
//                else
//                {
//                    ADebug.LogError("[ApolloUpdate] StartUpdateService failed,unknown update init type");
//                    return false;
//                }

				switch(mUpdateInitInfo.updateInitType)
				{
				case UpdateInitType.UpdateInitType_OnlySource:
					ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
					return StartSourceUpdate();
				case UpdateInitType.UpdateInitType_OnlyProgram:
					ADebug.Log("[ApolloUpdate] StartUpdateService only program update");
					return StartProgramUpdate();
				case UpdateInitType.UpdateInitType_SourceCheckAndSync:
					ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
					return StartSourceCheckAndSync();
				case UpdateInitType.UpdateInitType_SourceCheckAndSync_Optimize_Part:
					ADebug.Log("[ApolloUpdate] StartUpdateService SourceCheckAndSync");
					return StartSourceCheckAndSync_Part();
				case UpdateInitType.UpdateInitType_SourceCheckAndSync_Optimize_Full:
				case UpdateInitType.UpdateInitType_SourceCheckAndSync_Optimize_Full_Scatter:
					ADebug.Log("[ApolloUpdate] StartUpdateService SourceCheckAndSync");
					return StartSourceCheckAndSync_Full();
				case UpdateInitType.UpdateInitType_Only_FirstExtract_All:
					ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
					return StartFirstExtractAll();
				case UpdateInitType.UpdateInitType_Only_FirstExtract_Part:
					ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
					return StartFirstExtractPart();
				case UpdateInitType.UpdateInitType_Only_FirstExtract_Fix:
					ADebug.Log("[ApolloUpdate] StartUpdateService only source update");
					return StartFirstExtractFix();
				case UpdateInitType.UpdateInitType_Normal:
					ADebug.Log("[ApolloUpdate] StartUpdateService reserve type");
					return StartProgramUpdate();
				default:
					return false;
				}



            }
            public bool Continue()
            {
                if (mVersionMgr != null)
                {
                    if (mWaitNewVersion)
                    {
                        mWaitNewVersion = false;
                        mVersionMgr.MgrSetNextStage(true);
                    }
                }
                if (mWaitMessageBox)
                {
                    mWaitMessageBox = false;
                    if (mCurrentUpdateType == UpdateType.UpdateType_Program)
                    {
                        StartProgramUpdate();
                    }
                    else if (mCurrentUpdateType == UpdateType.UpdateType_Source)
                    {
                        StartSourceUpdate();
                    }
                }
                return true;
            }
            public bool StopUpdateService()
            {
                if (mVersionMgr != null)
                {
                    mShouldStop = true;
                    ADebug.Log(string.Format("[ApolloUpdate] StopUpdateService TYPE:{0}", mUpdateInitInfo.updateInitType));
                    mVersionMgr.MgrCancelUpdate();
                    if (mVersionMgr != null)
                    {
                        if (mWaitNewVersion)
                        {
                            mWaitNewVersion = false;
                            mVersionMgr.MgrSetNextStage(false);
                        }
                        if (mWaitMessageBox)
                        {
                            mWaitMessageBox = false;
                        }
                    }
                    mVersionMgr.MgrPoll();
                    mVersionMgr = null;
                    if (mVersionFactroy != null)
                    {
                        mVersionFactroy.DeleteVersionMgr();
                        mVersionFactroy = null;
                    }
                }
                speedCounter.StopSpeedCounter();
                return true;
            }
            public void DriveUpdateService()
            {
                speedCounter.SpeedCounter();
                if (updateVariables == null)
                {
                    return;
                }
                if (mWaitNewVersion || mWaitMessageBox)
                {
                    return;
                }
                bool updateCompleted = false;
                if (mVersionMgr != null && updateVariables.uErr == 0)
                {
                    mVersionMgr.MgrPoll();
                    if (updateVariables.bGetVersion)
                    {
                        updateVariables.bGetVersion = false;
                        mWaitNewVersion = true;
                        mCallBack.OnNoticeNewVersionInfo(updateVariables.newVerInfo);
                    }
                    if (updateVariables.bSuccess)
                    {
                        updateVariables.bSuccess = false;
                        if (mCurrentUpdateType == UpdateType.UpdateType_Program && mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_Normal)
                        {
                            StartSourceUpdate();
                        }
                        else
                        {
                            if (mCurrentUpdateType == UpdateType.UpdateType_Source)
                            {
                                mCallBack.OnNoticeChangeSourceVersion(mNewVersionStr);
                            }
                            updateCompleted = true;
                            mCallBack.OnNoticeUpdateSuccess();
                        }
                    }
                    if (updateVariables.bNoticeInstall)
                    {
#if UNITY_ANDROID
					mCallBack.OnNoticeInstallApk(updateVariables.ApkPath);
#endif
                    }
                }
                if (updateVariables.uErr != 0)
                {
                    if (mVersionMgr != null)
                    {
                        mVersionMgr = null;
                    }
                    if (mVersionFactroy != null)
                    {
                        mVersionFactroy.DeleteVersionMgr();
                        mVersionFactroy = null;
                    }
                    mWaitMessageBox = true;
                    IIPSMobile.IIPSMobileErrorCodeCheck.ErrorCodeInfo errorInfo = mErrorCheck.CheckIIPSErrorCode((int)updateVariables.uErr);
                    string msg = mUpdateErrorString.GetUpdateErrorString((IIPSMobile.IIPSMobileErrorCodeCheck.error_type)errorInfo.m_nErrorType);
                    mCallBack.OnUpdateMessageBoxInfo(msg, MessageBoxType.MessageBoxType_Retry, true, updateVariables.uErr);
                    updateVariables.uErr = 0;
                }
                if (updateCompleted)
                {
                    if (mVersionMgr != null)
                    {
                        mVersionMgr = null;
                    }
                    if (mVersionFactroy != null)
                    {
                        mVersionFactroy.DeleteVersionMgr();
                        mVersionFactroy = null;
                    }
                }
            }
            public System.UInt32 GetCurrentDownSpeed()
            {
                return speedCounter.GetSpeed();
            }

            public void SetNeedFirstExtract(bool needFirstExtract)
            {
                mNeedFirstExtract = needFirstExtract;
            }

            private void InitUpdateParam()
            {
                if (mVersionMgr != null)
                {
                    mVersionMgr = null;
                }
                if (mVersionFactroy != null)
                {
                    mVersionFactroy.DeleteVersionMgr();
                    mVersionFactroy = null;
                }
                updateVariables = new UpdateVariables();
                updateVariables.progress = 0;
                updateVariables.bSuccess = false;
                updateVariables.uErr = 0;
                updateVariables.bNoticeInstall = false;
                updateVariables.bGetVersion = false;
                updateVariables.newVerInfo.isCurrentNewest = false;
                updateVariables.newVerInfo.isForce = false;
                updateVariables.newVerInfo.needDownloadSize = 0;
                updateVariables.newVerInfo.userDefineStr = null;
                updateVariables.newVerInfo.versionStr = null;
            }
			private bool StartSourceCheckAndSync_Part()
			{
				ADebug.Log("[GCloudUpdate] StartSourceCheckAndSync_Part");
				InitUpdateParam();
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}
				string[] mExtractList={""};
				string mExtractListjson = "";
				if (mUpdateInitInfo.mNeedExtractList != null) 
				{
					mExtractList = mUpdateInitInfo.mNeedExtractList.Split (new char[4]{'|',',',';','#'});
					for (int i =0;i<mExtractList.Length;i++) {
						mExtractListjson = mExtractListjson + string.Format ("\"{0}\"",mExtractList[i]);
						if (i != mExtractList.Length - 1)
							mExtractListjson = mExtractListjson + ",";
					}
				} 
				else 
				{
					ADebug.Log ("[GCloudUpdate] StartSourceCheckAndSync_Part mNeedExtractList is empty!");
					return false;
				}

				string basicUpdateInfo = string.Format
					(
						@"
								""m_update_type"":{0},
								""log_debug"":{1},
								""log_error"":{2},
								""log_save_path"":""{3}"",
								""need_down_size"":true,
								""apollo_path"":""{4}"",",
						19,
						logdebug,
						logerror,
						mDateMgr.GetUpdateTempPath(),
						mDateMgr.GetUpdateApolloPath()
					);

				string versionInfo = string.Format
					(
						@"
								""basic_version"":
								{{
									""m_server_url_list"":[""{0}""],
									""gcloud_service_id"":{1},
									""m_cur_app_version_str"" : ""{2}"",
									""m_cur_src_version_str"" : ""{5}"",
									""gcloud_user_id"":""{3}"",
									""gcloud_world_id"":""{4}""
								}},
							",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion()
					);
				string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
				string sourceActionInfo = string.Format
					(
						@"
								""filelist_check"":
								{{
									""m_ifs_save_path"":""{0}"",
									""m_file_extract_path"":""{0}"",
									""m_app_ifs_path"":""{1}"",
									""m_filelist_check_type"":{2},
									""m_need_extract_dir_list"":[{3}]
								}}
							",
						mDateMgr.GetUpdateSourceSavePath(),firstifspath,(int)Filelist_Check_Type.Filelist_Check_Type_Part,mExtractListjson
					);

				string initstr = null;
				initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);

				ADebug.Log(string.Format("[ApolloUpdate] FirstExtract_Part config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part start check update failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_Only_FirstExtract_Part;
				return true;
			}
			private bool StartSourceCheckAndSync_Full()
			{
				ADebug.Log("[GCloudUpdate] StartSourceCheckAndSync_Full");
				InitUpdateParam();
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}
				int filelistCheckType = 0;
				if(mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_SourceCheckAndSync_Optimize_Full)
				{
					filelistCheckType = (int)Filelist_Check_Type.Filelist_Check_Type_Full; 
                }
				else if(mUpdateInitInfo.updateInitType == UpdateInitType.UpdateInitType_SourceCheckAndSync_Optimize_Full_Scatter)
				{
					filelistCheckType = (int)Filelist_Check_Type.Filelist_Check_Type_Scatter;
				}

				string basicUpdateInfo = string.Format
					(
						@"
								""m_update_type"":{0},
								""log_debug"":{1},
								""log_error"":{2},
								""log_save_path"":""{3}"",
								""need_down_size"":true,
								""apollo_path"":""{4}"",",
						19,
						logdebug,
						logerror,
						mDateMgr.GetUpdateTempPath(),
						mDateMgr.GetUpdateApolloPath()
					);

				string versionInfo = string.Format
					(
						@"
								""basic_version"":
								{{
									""m_server_url_list"":[""{0}""],
									""gcloud_service_id"":{1},
									""m_cur_app_version_str"" : ""{2}"",
									""m_cur_src_version_str"" : ""{5}"",
									""gcloud_user_id"":""{3}"",
									""gcloud_world_id"":""{4}""
								}},
							",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion()
					);
				string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
				string sourceActionInfo = string.Format
					(
						@"
								""filelist_check"":
								{{
									""m_ifs_save_path"":""{0}"",
									""m_file_extract_path"":""{0}"",
									""m_app_ifs_path"":""{1}"",
									""m_filelist_check_type"":{2}
								}}
							",
						mDateMgr.GetUpdateSourceSavePath(),firstifspath,filelistCheckType
					);

				string initstr = null;

				initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);

				ADebug.Log(string.Format("[ApolloUpdate] FirstExtract_Full config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Full create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Full start check update failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_Only_FirstExtract_Part;
				return true;
			}
			private bool StartFirstExtractAll()
			{
				ADebug.Log("[ApolloUpdate] StartFirstExtractAll");
				InitUpdateParam();
				if (!mNeedFirstExtract) 
				{
					ADebug.Log("[ApolloUpdate] the second param of InitUpdateMgr should be set true!");
					return false;
				}
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}

				string basicUpdateInfo = string.Format
					(
						@"
                    ""m_update_type"":{0},
				    ""log_debug"":{1},
				    ""log_error"":{2},
				    ""log_save_path"":""{3}"",
                    ""need_down_size"":true,
                    ""apollo_path"":""{4}"",",
						21,
						logdebug,
						logerror,
						mDateMgr.GetUpdateTempPath(),
						mDateMgr.GetUpdateApolloPath()
					);

				string versionInfo = string.Format
					(
						@"
                    ""basic_version"":
                    {{
					    ""m_server_url_list"":[""{0}""],
					    ""gcloud_service_id"":{1},
                        ""m_cur_app_version_str"" : ""{2}"",
                        ""m_cur_src_version_str"" : ""{5}"",
                        ""gcloud_user_id"":""{3}"",
                        ""gcloud_world_id"":""{4}"",
                        ""m_version_url_list"":[""{6}""]
                    }},
                ",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion(),
						mUpdateInitInfo.versionUrl
					);
				string sourceActionInfo = string.Format
					(
						@"
                    ""full_diff"":
					{{
						""m_ifs_save_path"":""{0}"",
						""m_file_extract_path"":""{0}""
					}}
                ",
						mDateMgr.GetUpdateSourceSavePath()
					);

				string initstr = null;
				if (mNeedFirstExtract)
				{
					string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
					string firstextractInfo = string.Format
						(
							@"
                        ""first_extract"":
				        {{
					        ""m_ifs_extract_path"":""{0}"",
					        ""m_ifs_res_save_path"":""{0}"",
							""m_append_source_action"":false,
							""m_extract_type"":{2},
					        ""filelist"":
					        [
		 				        {{
							        ""filepath"":""{1}"",
							        ""filename"":""first_source.ifs""
						        }}
		 			        ]
				        }},
                    ",
							mDateMgr.GetUpdateSourceSavePath(),
							firstifspath,(int)First_Extract_type.m_type_extract_all
						);

					initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, sourceActionInfo);
				}
				else
				{
					initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);
				}

				ADebug.Log(string.Format("[ApolloUpdate] FirstExtract_Part config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part start check update failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_Only_FirstExtract_Part;
				return true;
			}
			private bool StartFirstExtractPart()
			{
				ADebug.Log("[ApolloUpdate] StartFirstExtractPart");
				InitUpdateParam();
				if (!mNeedFirstExtract) 
				{
					ADebug.Log("[ApolloUpdate] the second param of InitUpdateMgr should be set true!");
					return false;
				}
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}

				string[] mExtractList={""};
				string mExtractListjson = "";
				if (mUpdateInitInfo.mNeedExtractList != null) 
				{
					mExtractList = mUpdateInitInfo.mNeedExtractList.Split (new char[4]{'|',',',';','#'});
					for (int i =0;i<mExtractList.Length;i++) {
						mExtractListjson = mExtractListjson + string.Format ("\"{0}\"",mExtractList[i]);
						if (i != mExtractList.Length - 1)
							mExtractListjson = mExtractListjson + ",";
					}
				} 
				else 
				{
					ADebug.Log ("[GCloudUpdate] StartFirstExtractPart mNeedExtractList is empty!");
					return false;
				}


				string basicUpdateInfo = string.Format
					(
						@"
                    ""m_update_type"":{0},
				    ""log_debug"":{1},
				    ""log_error"":{2},
				    ""log_save_path"":""{3}"",
                    ""need_down_size"":true,
                    ""apollo_path"":""{4}"",",
						21,
						logdebug,
						logerror,
						mDateMgr.GetUpdateTempPath(),
						mDateMgr.GetUpdateApolloPath()
					);

				string versionInfo = string.Format
					(
						@"
                    ""basic_version"":
                    {{
					    ""m_server_url_list"":[""{0}""],
					    ""gcloud_service_id"":{1},
                        ""m_cur_app_version_str"" : ""{2}"",
                        ""m_cur_src_version_str"" : ""{5}"",
                        ""gcloud_user_id"":""{3}"",
                        ""gcloud_world_id"":""{4}"",
                        ""m_version_url_list"":[""{6}""]
                    }},
                ",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion(),
						mUpdateInitInfo.versionUrl
					);
				string sourceActionInfo = string.Format
					(
						@"
                    ""full_diff"":
					{{
						""m_ifs_save_path"":""{0}"",
						""m_file_extract_path"":""{0}""
					}}
                ",
						mDateMgr.GetUpdateSourceSavePath()
					);

				string initstr = null;
				if (mNeedFirstExtract)
				{
					string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
					string firstextractInfo = string.Format
						(
							@"
                        ""first_extract"":
				        {{
					        ""m_ifs_extract_path"":""{0}"",
					        ""m_ifs_res_save_path"":""{0}"",
							""m_append_source_action"":false,
							""m_extract_type"":{2},
							""m_need_extract_dir_list"":[{3}],
					        ""filelist"":
					        [
		 				        {{
							        ""filepath"":""{1}"",
							        ""filename"":""first_source.ifs""
									
						        }}
		 			        ]
				        }},
                    ",
							mDateMgr.GetUpdateSourceSavePath(),
							firstifspath,(int)First_Extract_type.m_type_extract_part_resource,mExtractListjson
						);

					initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, sourceActionInfo);
				}
				else
				{
					initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);
				}

				ADebug.Log(string.Format("[ApolloUpdate] FirstExtract_Part config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] FirstExtract_Part failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_Only_FirstExtract_Part;
				return true;
			}
			private bool StartFirstExtractFix()
			{
				ADebug.Log("[ApolloUpdate] StartFirstExtractFix");
				InitUpdateParam();
				if (!mNeedFirstExtract) 
				{
					ADebug.Log("[ApolloUpdate] the second param of InitUpdateMgr should be set true!");
					return false;
				}
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}

				string basicUpdateInfo = string.Format
					(
						@"
                    ""m_update_type"":{0},
				    ""log_debug"":{1},
				    ""log_error"":{2},
				    ""log_save_path"":""{3}"",
                    ""need_down_size"":true,
                    ""apollo_path"":""{4}"",",
						21,
						logdebug,
						logerror,
						mDateMgr.GetUpdateTempPath(),
						mDateMgr.GetUpdateApolloPath()
					);

				string versionInfo = string.Format
					(
						@"
                    ""basic_version"":
                    {{
					    ""m_server_url_list"":[""{0}""],
					    ""gcloud_service_id"":{1},
                        ""m_cur_app_version_str"" : ""{2}"",
                        ""m_cur_src_version_str"" : ""{5}"",
                        ""gcloud_user_id"":""{3}"",
                        ""gcloud_world_id"":""{4}"",
                        ""m_version_url_list"":[""{6}""]
                    }},
                ",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion(),
						mUpdateInitInfo.versionUrl
					);
				string sourceActionInfo = string.Format
					(
						@"
                    ""full_diff"":
					{{
						""m_ifs_save_path"":""{0}"",
						""m_file_extract_path"":""{0}""
					}}
                ",
						mDateMgr.GetUpdateSourceSavePath()
					);

				string initstr = null;
				if (mNeedFirstExtract)
				{
					string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
					string firstextractInfo = string.Format
						(
							@"
                        ""first_extract"":
				        {{
					        ""m_ifs_extract_path"":""{0}"",
					        ""m_ifs_res_save_path"":""{0}"",
							""m_append_source_action"":false,
							""m_extract_type"":{2},
					        ""filelist"":
					        [
		 				        {{
							        ""filepath"":""{1}"",
							        ""filename"":""first_source.ifs""
									
						        }}
		 			        ]
				        }},
                    ",
							mDateMgr.GetUpdateSourceSavePath(),
							firstifspath,(int)First_Extract_type.m_type_extract_fixToAll
						);

					initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, sourceActionInfo);
				}
				else
				{
					initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);
				}

				ADebug.Log(string.Format("[ApolloUpdate] source update config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] source update create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] source update start check update failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_Only_FirstExtract_Fix;
				return true;
			}


            private bool StartSourceUpdate()
            {
                ADebug.Log("[ApolloUpdate] StartSourceUpdate");
                InitUpdateParam();
                mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
                mVersionMgr = null;
				string useDiffFlist = null;
				if (mUpdateInitInfo.mCreateDiffFilelist)
				{
					useDiffFlist = "true";
				} 
				else 
				{
					useDiffFlist = "false";
				}
                string logdebug = "false";
                string logerror = "false";
                if (ADebug.Level == ADebug.LogPriority.None)
                {
                    logdebug = "false";
                    logerror = "false";
                }
                else if (ADebug.Level == ADebug.LogPriority.Info)
                {
                    logdebug = "true";
                    logerror = "true";
                }
                else if (ADebug.Level == ADebug.LogPriority.Error)
                {
                    logdebug = "false";
                    logerror = "true";
                }

                string basicUpdateInfo = string.Format
                   (
                   @"
                    ""m_update_type"":{0},
				    ""log_debug"":{1},
				    ""log_error"":{2},
				    ""log_save_path"":""{3}"",
                    ""need_down_size"":true,
                    ""apollo_path"":""{4}"",",
                       mUpdateInitInfo.grayUpdate ? 23 : 21,
                       logdebug,
                       logerror,
                       mDateMgr.GetUpdateTempPath(),
                       mDateMgr.GetUpdateApolloPath()
                   );

                string versionInfo = string.Format
                    (
                    @"
                    ""basic_version"":
                    {{
					    ""m_server_url_list"":[""{0}""],
					    ""gcloud_service_id"":{1},
                        ""m_cur_app_version_str"" : ""{2}"",
                        ""m_cur_src_version_str"" : ""{5}"",
                        ""gcloud_user_id"":""{3}"",
                        ""gcloud_world_id"":""{4}"",
                        ""m_version_url_list"":[""{6}""],
                        ""m_u32GetChannelConfig"":{7},
                        ""m_u32GetRegionid"":{8}
                    }},
                ",
                        mUpdateInitInfo.gameUpdateUrl,
                        mUpdateInitInfo.updateChannelId,
                        mDateMgr.GetCurrentProgramVersion(),
                        mUpdateInitInfo.userId,
                        mUpdateInitInfo.worldId,
                        mDateMgr.GetCurrentSourceVersion(),
						mUpdateInitInfo.versionUrl,
                        mUpdateInitInfo.getChannelConfig ? 1 : 0,
                        mUpdateInitInfo.getRegionid ? 1 : 0

                    );
                string sourceActionInfo = string.Format
                    (
                    @"
                    ""full_diff"":
					{{
						""m_ifs_save_path"":""{0}"",
						""m_file_extract_path"":""{0}"",
						""UseDiffFlist"":{1}
					}}
                ",
						mDateMgr.GetUpdateSourceSavePath(),useDiffFlist
                    );

                string initstr = null;
                if (mNeedFirstExtract)
                {
                    string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
                    string firstextractInfo = string.Format
                        (
                        @"
                        ""first_extract"":
				        {{
					        ""m_ifs_extract_path"":""{0}"",
					        ""m_ifs_res_save_path"":""{0}"",
					        ""filelist"":
					        [
		 				        {{
							        ""filepath"":""{1}"",
							        ""filename"":""first_source.ifs""
						        }}
		 			        ]
				        }},
                    ",
                            mDateMgr.GetUpdateSourceSavePath(),
                            firstifspath
                        );

                    initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, sourceActionInfo);
                }
                else
                {
                    initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);
                }

                ADebug.Log(string.Format("[ApolloUpdate] source update config:{0}", initstr));
                mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
                if (mVersionMgr == null)
                {
                    ADebug.LogError("[ApolloUpdate] source update create versionmgr failed");
                    return false;
                }
                if (!mVersionMgr.MgrCheckAppUpdate())
                {
                    ADebug.LogError("[ApolloUpdate] source update start check update failed");
                    return false;
                }

                mCurrentUpdateType = UpdateType.UpdateType_Source;
                return true;
            }

            private bool StartProgramUpdate()
            {
                ADebug.Log("[ApolloUpdate] StartProgramUpdate");
                InitUpdateParam();
                mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
                mVersionMgr = null;
                string logdebug = "false";
                string logerror = "false";
                if (ADebug.Level == ADebug.LogPriority.None)
                {
                    logdebug = "false";
                    logerror = "false";
                }
                else if (ADebug.Level == ADebug.LogPriority.Info)
                {
                    logdebug = "true";
                    logerror = "true";
                }
                else if (ADebug.Level == ADebug.LogPriority.Error)
                {
                    logdebug = "false";
                    logerror = "true";
                }

                string curApkPath = DolphinFunction.GetCurrentVersionApkPath();
                string basicUpdateInfo = string.Format
                    (
                    @"
                    ""m_update_type"":{0},
				    ""log_debug"":{1},
				    ""log_error"":{2},
				    ""log_save_path"":""{3}"",
                    ""need_down_size"":true,
                    ""apollo_path"":""{4}"",",
                        mUpdateInitInfo.grayUpdate ? 22 : 20,
                        logdebug,
                        logerror,
                        mDateMgr.GetUpdateTempPath(),
                        mDateMgr.GetUpdateApolloPath()
                    );

                string versionInfo = string.Format
                    (
                    @"
                    ""basic_version"":
                    {{
					    ""m_server_url_list"":[""{0}""],
					    ""gcloud_service_id"":{1},
                        ""m_cur_app_version_str"" : ""{2}"",
                        ""gcloud_user_id"":""{3}"",
                        ""gcloud_world_id"":""{4}"",
                        ""m_version_url_list"":[""{5}""],
                        ""m_u32GetChannelConfig"":{6},
                        ""m_u32GetRegionid"":{7}
                    }},",
                        mUpdateInitInfo.gameUpdateUrl,
                        mUpdateInitInfo.updateChannelId,
                        mDateMgr.GetCurrentProgramVersion(),
                        mUpdateInitInfo.userId,
                        mUpdateInitInfo.worldId,
						mUpdateInitInfo.versionUrl,
                        mUpdateInitInfo.getChannelConfig ? 1 : 0,
                        mUpdateInitInfo.getRegionid ? 1 : 0

                    );

                string programActionInfo = string.Format
                    (
                    @"
                    ""basic_diffupdata"":
                    {{
                        ""m_diff_config_save_path"" : ""{0}"",
                        ""m_diff_temp_path"" : ""{0}"",
                        ""m_nMaxDownloadSpeed"" : 10240000,
                        ""m_apk_abspath"" : ""{1}""
                    }}",
                        mDateMgr.GetUpdateTempPath(),
                        curApkPath
                    );

                string initstr = null;
                if (mNeedFirstExtract)
                {
                    string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk,mUpdateInitInfo.bObb_type);
                    string firstextractInfo = string.Format
                        (
                        @"
                        ""first_extract"":
				        {{
					        ""m_ifs_extract_path"":""{0}"",
					        ""m_ifs_res_save_path"":""{0}"",
					        ""filelist"":
					        [
		 				        {{
							        ""filepath"":""{1}"",
							        ""filename"":""first_source.ifs""
						        }}
		 			        ]
				        }},",
                            mDateMgr.GetUpdateSourceSavePath(),
                            firstifspath
                        );

                    initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, programActionInfo);
                }
                else
                {
                    initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, programActionInfo);
                }

                ADebug.Log(string.Format("[ApolloUpdate] program update config:{0}", initstr));
                mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
                if (mVersionMgr == null)
                {
                    ADebug.LogError("[ApolloUpdate] program update create versionmgr failed");
                    return false;
                }
                if (!mVersionMgr.MgrCheckAppUpdate())
                {
                    ADebug.LogError("[ApolloUpdate] program update start check update failed");
                    return false;
                }

                mCurrentUpdateType = UpdateType.UpdateType_Program;
                return true;
            }

			private bool StartSourceCheckAndSync()
			{
				ADebug.Log("[GCloudUpdate] StartSourceCheckAndSync");
				InitUpdateParam();
				mVersionFactroy = new IIPSMobile.IIPSMobileVersion();
				mVersionMgr = null;
				string logdebug = "false";
				string logerror = "false";
				if (ADebug.Level == ADebug.LogPriority.None)
				{
					logdebug = "false";
					logerror = "false";
				}
				else if (ADebug.Level == ADebug.LogPriority.Info)
				{
					logdebug = "true";
					logerror = "true";
				}
				else if (ADebug.Level == ADebug.LogPriority.Error)
				{
					logdebug = "false";
					logerror = "true";
				}

				string basicUpdateInfo = string.Format
				   (
				   @"
					""m_update_type"":{0},
					""log_debug"":{1},
					""log_error"":{2},
					""log_save_path"":""{3}"",
					""need_down_size"":true,
					""apollo_path"":""{4}"",",
					   19,
					   logdebug,
					   logerror,
					   mDateMgr.GetUpdateTempPath(),
					   mDateMgr.GetUpdateApolloPath()
				   );

				string versionInfo = string.Format
					(
                    @"
					""basic_version"":
					{{
						""m_server_url_list"":[""{0}""],
						""gcloud_service_id"":{1},
						""m_cur_app_version_str"" : ""{2}"",
						""m_cur_src_version_str"" : ""{5}"",
						""gcloud_user_id"":""{3}"",
						""gcloud_world_id"":""{4}"",
                        ""m_u32GetChannelConfig"":{6},
                        ""m_u32GetRegionid"":{7}
					}},
				",
						mUpdateInitInfo.gameUpdateUrl,
						mUpdateInitInfo.updateChannelId,
						mDateMgr.GetCurrentProgramVersion(),
						mUpdateInitInfo.userId,
						mUpdateInitInfo.worldId,
						mDateMgr.GetCurrentSourceVersion(),
                        mUpdateInitInfo.getChannelConfig ? 1 : 0,
                        mUpdateInitInfo.getRegionid ? 1 : 0
					);
				string sourceActionInfo = string.Format
					(
					@"
					""filelist_check"":
					{{
						""m_ifs_save_path"":""{0}"",
						""m_file_extract_path"":""{0}"",
						""m_filelist_check_type"":{1}
					}}
				",
						mDateMgr.GetUpdateSourceSavePath(),(int)Filelist_Check_Type.Filelist_Check_Type_Normal
					);

				string initstr = null;
				if (mNeedFirstExtract)
				{
					string firstifspath = DolphinFunction.GetFirstExtractIfsPath(mUpdateInitInfo.bFirst_source_in_apk, mUpdateInitInfo.bObb_type);
					string firstextractInfo = string.Format
						(
						@"
						""first_extract"":
						{{
							""m_ifs_extract_path"":""{0}"",
							""m_ifs_res_save_path"":""{0}"",
							""filelist"":
							[
								{{
									""filepath"":""{1}"",
									""filename"":""first_source.ifs""
								}}
							]
						}},
					",
							mDateMgr.GetUpdateSourceSavePath(),
							firstifspath
						);

					initstr = string.Format(@"{{{0}{1}{2}{3}}}", basicUpdateInfo, versionInfo, firstextractInfo, sourceActionInfo);
				}
				else
				{
					initstr = string.Format(@"{{{0}{1}{2}}}", basicUpdateInfo, versionInfo, sourceActionInfo);
				}

				ADebug.Log(string.Format("[ApolloUpdate] source update config:{0}", initstr));
				mVersionMgr = mVersionFactroy.CreateVersionMgr(this, initstr);
				if (mVersionMgr == null)
				{
					ADebug.LogError("[ApolloUpdate] source update create versionmgr failed");
					return false;
				}
				if (!mVersionMgr.MgrCheckAppUpdate())
				{
					ADebug.LogError("[ApolloUpdate] source update start check update failed");
					return false;
				}

				mCurrentUpdateType = UpdateType.UpdateType_SourceCheckAndSync;
				return true;
			}


            public System.Byte OnGetNewVersionInfo(IIPSMobile.IIPSMobileVersionCallBack.VERSIONINFO newVersionInfo)
            {
                if (mShouldStop)
                    return 1;
                ADebug.Log("[ApolloUpdate] ApolloUpdateVersionCallBack::OnGetNewVersionInfo");
                ADebug.Log(string.Format("[ApolloUpdate] isAppUpdating:{0}", newVersionInfo.isAppUpdating));
                ADebug.Log(string.Format("[ApolloUpdate] isAppDiffUpdating:{0}", newVersionInfo.isNeedUpdating));
                ADebug.Log(string.Format("[ApolloUpdate] isForcedUpdating:{0}", newVersionInfo.isForcedUpdating));
                ADebug.Log(string.Format("[ApolloUpdate] needDownloadSize:{0}", newVersionInfo.needDownloadSize));
                ADebug.Log(string.Format("[ApolloUpdate] newAppVersion.programmeVersion:{0} {1} {2}", newVersionInfo.newAppVersion.programmeVersion.MajorVersion_Number,
                                         newVersionInfo.newAppVersion.programmeVersion.MinorVersion_Number,
                                         newVersionInfo.newAppVersion.programmeVersion.Revision_Number));

                ADebug.Log(string.Format("[ApolloUpdate] newAppVersion.dataVersion:{0}", newVersionInfo.newAppVersion.dataVersion.DataVersion));

                updateVariables.bGetVersion = true;
                updateVariables.newVerInfo.isForce = newVersionInfo.isForcedUpdating > 0;
                updateVariables.newVerInfo.needDownloadSize = (System.UInt32)newVersionInfo.needDownloadSize;
                updateVariables.newVerInfo.updateType = GetCurrentUpdteType();
                string verStr = string.Format(@"{0}.{1}.{2}.{3}", newVersionInfo.newAppVersion.programmeVersion.MajorVersion_Number,
                    newVersionInfo.newAppVersion.programmeVersion.MinorVersion_Number,
                    newVersionInfo.newAppVersion.programmeVersion.Revision_Number,
                    newVersionInfo.newAppVersion.dataVersion.DataVersion);
                updateVariables.newVerInfo.versionStr = verStr;
                if (newVersionInfo.isNeedUpdating == 0)
                {
                    updateVariables.newVerInfo.isCurrentNewest = true;
                }
                else
                {
                    updateVariables.newVerInfo.isCurrentNewest = false;
                    mNewVersionStr = verStr;
                }

                updateVariables.newVerInfo.isNormalVersion = newVersionInfo.isNormalVersion > 0;
                updateVariables.newVerInfo.isAuditVersion = newVersionInfo.isAuditVersion > 0;
                updateVariables.newVerInfo.isGrayVersion = newVersionInfo.isGrayVersion > 0;

                ADebug.Log(string.Format("[ApolloUpdate] isAuditVersion:{0}", newVersionInfo.isAuditVersion));
                return 1;
            }

            public void OnProgress(IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, System.UInt64 totalSize, System.UInt64 nowSize)
            {
                if (mShouldStop)
                    return;
                ADebug.Log("[ApolloUpdate] ApolloUpdateVersionCallBack::OnProgress");
                ADebug.Log(string.Format("[ApolloUpdate] stage:{0},totalSize:{0} nowsize:{1}", curVersionStage, totalSize, nowSize));
                if (curVersionStage != mCurVersionStage)
                {
                    speedCounter.StopSpeedCounter();
                }
                bool isDownloading = false;
                string msg = mUpdateInfoString.GetUpdateInfoString(curVersionStage, GetCurrentUpdteType(), ref isDownloading);
                if (nowSize <= totalSize && totalSize > 0)
                {
                    if (isDownloading)
                    {
                        msg = msg + " " + GetDownSizeShowString(totalSize, nowSize);
                        if (curVersionStage != mCurVersionStage)
                        {
                            mCurVersionStage = curVersionStage;
                            speedCounter.StartSpeedCounter();
                        }
                        speedCounter.SetSize((uint)nowSize);
                    }
					else
                    {
                        if (curVersionStage != mCurVersionStage)
                        {
                            mCurVersionStage = curVersionStage;
                        }
                    }
                }
                mCallBack.OnUpdateProgressInfo(curVersionStage,msg, (System.UInt32)nowSize, (System.UInt32)totalSize, isDownloading);
            }

            public void OnError(IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, System.UInt32 errorCode)
            {
                if (mShouldStop)
                    return;
                ADebug.Log(string.Format("[ApolloUpdate] ApolloUpdateVersionCallBack::OnError code:{0}", errorCode));
                updateVariables.uErr = errorCode;
            }

            public void OnSuccess()
            {
                if (mShouldStop)
                    return;
                updateVariables.bSuccess = true;
                ADebug.Log("[ApolloUpdate] ApolloUpdateVersionCallBack::OnSuccess");
            }

            public void SaveConfig(System.UInt32 bufferSize, System.IntPtr configBuffer)
            {
                if (mShouldStop)
                    return;
                ADebug.Log("[ApolloUpdate] ApolloUpdateVersionCallBack::SaveConfig");
                //int size = Convert.ToInt32(bufferSize);
                //string text = Marshal.PtrToStringAnsi(configBuffer, size);
            }
            public System.Byte OnNoticeInstallApk(string path)
            {
                if (mShouldStop)
                    return 1;
                ADebug.Log("[ApolloUpdate] ApolloUpdateVersionCallBack::OnNoticeInstallApk");
                updateVariables.bNoticeInstall = true;
                updateVariables.ApkPath = path;
                return 1;
            }

            public System.Byte OnActionMsg(string msg)
            {
                if (mShouldStop)
                    return 1;
                ADebug.Log(string.Format("[ApolloUpdate] ApolloUpdateVersionCallBack::OnActionMsg:{0}", msg));
                if (msg.Equals("{\"first_extract\":\"success\"}"))
                {
                    mNeedFirstExtract = false;
                    mCallBack.OnNoticeFirstExtractSuccess();
                }
                else if (msg.IndexOf("on_get_new_version") != -1)
                {
                    updateVariables.newVerInfo.userDefineStr = msg;
                }
                else
                {
                    mCallBack.OnNoticeServerCfgInfo(msg);
                }
                return 1;
            }

            private string GetDownSizeShowString(System.UInt64 totalSize, System.UInt64 nowSize)
            {
                if (totalSize == 0)
                {
                    return "[-/-]";
                }
                else if (totalSize < 1024 * 1024)
                {
                    float tl = (float)(totalSize) / 1024;
                    float ns = (float)(nowSize) / 1024;
                    return string.Format("[{0:F2}KB/{1:F2}KB]", ns, tl);
                }
                else
                {
                    float tl = (float)(totalSize) / (1024 * 1024);
                    float ns = (float)(nowSize) / (1024 * 1024);
                    return string.Format("[{0:F2}MB/{1:F2}MB]", ns, tl);
                }
            }
        }
    } 
}
