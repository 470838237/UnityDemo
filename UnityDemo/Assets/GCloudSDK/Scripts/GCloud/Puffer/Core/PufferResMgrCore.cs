using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCloud
{
    namespace Puffer
    {
        public class PufferResMgrCore : IPufferMgrInterface, IIPSMobile.IIPSMobilePufferCallbackInterface
        {
            private IPufferCallBack pufferCB = null;
            private IIPSMobile.IIPSMobilePufferInterface pufferPlugin = null;
            public bool InitResManager(PufferConfig config, IPufferCallBack cb)
            {
                if (cb == null || config == null)
                {
                    return false;
                }
                pufferPlugin = IIPSMobile.IIPSMobilePuffer.CreateIIPSMobilePufferMgr();
                if(pufferPlugin == null)
                {
                    return false;
                }
                pufferCB = cb;
				string needCheckStr = "";
				string needFileRestoreStr = "";
				string removeOldWhenUpdateStr = "";
				if (config.needCheck)
                {
                    needCheckStr = "\"need_check\":true";
                }
                else
                {
                    needCheckStr = "\"need_check\":false";
                }

                string isAuditVersionStr = "";
                if (config.isAuditVersion)
                {
                    isAuditVersionStr = "\"isAuditVersion\":true";
                }
                else
                {
                    isAuditVersionStr = "\"isAuditVersion\":false";
                }

                if (config.needFileRestore)
                {
                    needFileRestoreStr = "\"need_fileRestore\":true";
                }
                else
                {
                    needFileRestoreStr = "\"need_fileRestore\":false";
                }
				
				if(config.removeOldWhenUpdate)
				{
					removeOldWhenUpdateStr = "\"remove_old_when_update\":true";
				}
				else
				{
					removeOldWhenUpdateStr = "\"remove_old_when_update\":false";
				}

                string configString = string.Format
                  (
                  @"{{""res_dir"":""{0}"",""puffer_server"":""{1}"",""puffer_group_mark_id"":""{2}"",""max_down_speed"":{3},""max_down_task"":{4},""max_download_pertask"":{5},{6},""user_id"":""{7}"",""puffer_product_id"":{8},""puffer_game_id"":{9},""update_type"":{10},""dolphin_product_id"":{11},""dolphin_app_version"":""{12}"",""dolphin_res_version"":""{13}"",{14},{15},{16}}}",
                      config.resDir,
                      config.pufferServerUrl,
                      config.pufferGroupMarkId,
                      config.maxDownloadSpeed,
                      config.maxDownTask,
                      config.maxDownloadsPerTask,
					  needCheckStr,
					  config.userId,
					  config.pufferProductId,
					  config.pufferGameId,
					  (int)config.pufferUpdateType,
					  config.dolphinProductId,
					  config.dolphinAppVersion,
					  config.dolphinResVersion,
					  needFileRestoreStr,
                      isAuditVersionStr,
                      removeOldWhenUpdateStr
                  );
                return pufferPlugin.PufferPluginInit(this, configString);
            }

            public void UnInitResManager()
            {
                if(pufferPlugin != null)
                {
                    pufferPlugin.PufferPluginUninit();
                    IIPSMobile.IIPSMobilePuffer.DestroyIIPSMobilePufferMgr(pufferPlugin);
                    pufferPlugin = null;
                }
            }

            public void Update()
            {
                if (pufferPlugin != null)
                {
                    pufferPlugin.PufferPluginUpdate();
                }
            }

            public System.UInt32 GetFileId(string filePath)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginGetFileId(filePath);
                }
                else
                {
                    return System.UInt32.MaxValue;
                }
            }

            public bool IsFileReady(System.UInt32 fileId)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginIsFileReady(fileId);
                }
                else
                {
                    return false;
                }
            }

            public System.UInt32 GetFileSizeCompressed(System.UInt32 fileId)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginGetFileSizeCompressed(fileId);
                }
                else
                {
                    return 0;
                }
            }

            public System.UInt64 DownloadFile(System.UInt32 fileId, bool forceDownload, System.UInt32 priority)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginDownloadFile(fileId,forceDownload,priority);
                }
                else
                {
                    return System.UInt64.MaxValue;
                }
            }

            public System.UInt64 StartRestoreFiles(System.UInt32 priority)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginStartRestoreFiles(priority);
                }
                else
                {
                    return System.UInt32.MaxValue;
                }
            }

            public bool RemoveTask(System.UInt64 taskId)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginRemoveTask(taskId);
                }
                else
                {
                    return false;
                }
            }

			public bool SetDLMaxSpeed(System.UInt64 maxSpeed)
			{
				if (pufferPlugin != null)
				{
					return pufferPlugin.PufferPluginSetImmDLMaxSpeed(maxSpeed);
				}
				else
				{
					return false;
				}
			}

			public bool SetDLMaxTask(System.UInt32 maxTask)
			{
				if (pufferPlugin != null)
				{
					return pufferPlugin.PufferPluginSetImmDLMaxTask(maxTask);
				}
				else
				{
					return false;
				}
			}

            public bool SetTaskPriority(System.UInt64 taskId, System.UInt32 priority)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginSetTaskPriority(taskId, priority);
                }
                else
                {
                    return false;
                }
            }

			public System.Double GetCurrentSpeed()
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginGetCurrentSpeed();
                }
                else
                {
                    return 0.0;
                }
            }

            public bool GetBatchDirFileCount(string dir, bool blSubDir, ref System.UInt32 nTotal)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginGetBatchDirFileCount(dir, blSubDir, ref nTotal);
                }
                else
                {
                    return false;
                }
            }
			
            public System.UInt64 DownloadBatchDir(string dir, bool blSubDir, bool forceSync, System.UInt32 priority)
            {
                if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginDownloadBatchDir(dir, blSubDir, forceSync, priority);
                }
                else
                {
                    return System.UInt64.MaxValue;
                }
            }

			public System.UInt64 DownloadBatchList(List<string> lst, bool forceSync, System.UInt32 priority)
			{
                if (pufferPlugin != null)
                {
                	if(lst != null && lst.Count > 0)
                	{
                		pufferPlugin.PufferPluginPrepare4DownloadBatchList();
						
						bool blValid = false;
						for (int i=0; i < lst.Count; ++i)
						{
							string file = lst[i];
							System.UInt32 fileId = GetFileId(file);
							if(fileId != System.UInt32.MaxValue)
							{
								blValid = pufferPlugin.PufferPluginAddBatchListItem(fileId) || blValid;
							}
						}

						if(blValid)
						{
							return pufferPlugin.PufferPluginDownloadBatchList(forceSync, priority);
						}
						else
						{
							return System.UInt64.MaxValue;
						}
                	}
					else
					{
						return System.UInt64.MaxValue;
					}	
                }
                else
                {
                    return System.UInt64.MaxValue;
                }

			}
			
			public bool PauseTask(System.UInt64 taskId)
			{
				if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginPauseTask(taskId);
                }
                else
                {
                    return false;
                }
			}
			
			public bool ResumeTask(System.UInt64 taskId)
			{
				if (pufferPlugin != null)
                {
                    return pufferPlugin.PufferPluginResumeTask(taskId);
                }
                else
                {
                    return false;
                }
			}

            public void OnPufferInitReturn(bool isSuccess, System.UInt32 errorCode)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnInitReturn(isSuccess, errorCode);
                }
            }

            public void OnPufferInitProgress(System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnInitProgress((PufferInitStage)stage, nowSize, totalSize);
                }
            }

            public void OnPufferDownloadReturn(System.UInt64 taskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnDownloadReturn(taskId, fileid, isSuccess, errorCode);
                }
            }
			
			public void OnPufferDownloadProgress(System.UInt64 taskId, System.UInt64 nowSize, System.UInt64 totalSize)
			{
				if (pufferCB != null)
                {
                    pufferCB.OnDownloadProgress(taskId, nowSize, totalSize);
                }
			}
            public void OnPufferRestoreReturn(bool isSuccess, System.UInt32 errorCode)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnRestoreReturn(isSuccess, errorCode);
                }
            }

            public void OnPufferRestoreProgress(System.UInt32 stage, System.UInt32 nowSize, System.UInt32 totalSize)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnRestoreProgress((PufferRestoreStage)stage, nowSize, totalSize);
                }
            }

            public void OnPufferDownloadBatchReturn(System.UInt64 batchTaskId, System.UInt32 fileid, bool isSuccess, System.UInt32 errorCode, System.UInt32 batchType, string strRet)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnDownloadBatchReturn(batchTaskId, fileid, isSuccess, errorCode, (PufferBatchDownloadType)batchType, strRet);
                }
            }

            public void OnPufferDownloadBatchProgress(System.UInt64 batchTaskId, System.UInt64 nowSize, System.UInt64 totalSize)
            {
                if (pufferCB != null)
                {
                    pufferCB.OnDownloadBatchProgress(batchTaskId, nowSize, totalSize);
                }
            }
        }
    }
}
