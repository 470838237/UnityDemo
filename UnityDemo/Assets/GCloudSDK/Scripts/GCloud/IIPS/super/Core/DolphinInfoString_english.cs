using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCloud
{
    namespace Dolphin
    {
        public class DolphinInfoStringEnglish : IDolphinInfoStringInterface
        {
            public string strUpdateInfoDefault = "Updating.";
            public string strUpdateInfoGettingAppVersion = "Getting Version Info";
            public string strUpdateInfoGettingSrcVersion = "Getting Version Info";
            public string strUpdateInfoFirstExtract = "Unpacking Game Resources";
            public string strUpdateInfoApkDownloadConfig = "Downloading Configuration File";
            public string strUpdateInfoApkDownDiff = "Downloading Files";
            public string strUpdateInfoApkDownFull = "Downloading Files";
            public string strUpdateInfoApkCheckCompleted = "Checking Files";
            public string strUpdateInfoApkCheckLocal = "Checking Files";
            public string strUpdateInfoApkCheckConfig = "Checking Files";
            public string strUpdateInfoApkCheckDiff = "Checking Files";
            public string strUpdateInfoApkMergeDiff = "Merging Files";
            public string strUpdateInfoApkCheckFull = "Checking Files";

            public string strUpdateInfoSourceDownloadList = "Downloading Configuration File";
            public string strUpdateInfoSourcePrepareUpdate = "Preparing to Update";
            public string strUpdateInfoSourceAnalyseDiff = "Analyzing Files";
            public string strUpdateInfoSourceDownload = "Downloading Resources";
            public string strUpdateInfoSourceExtract = "Unpacking Game Resources";

            public string GetUpdateInfoString(IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, UpdateType updatetype, ref bool isDownloading)
            {
                isDownloading = false;
                switch (curVersionStage)
                {
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_Dolphin_Version:
                        {
                            if (updatetype == UpdateType.UpdateType_Source)
                            {
                                return strUpdateInfoGettingSrcVersion;
                            }
                            else
                            {
                                return strUpdateInfoGettingAppVersion;
                            }
                        }
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_FirstExtract:
                        return strUpdateInfoFirstExtract;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateDownConfig:
                        isDownloading = true;
                        return strUpdateInfoApkDownloadConfig;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateDownDiffFile:
                        isDownloading = true;
                        return strUpdateInfoApkDownDiff;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateDownFullApk:
                        isDownloading = true;
                        return strUpdateInfoApkDownFull;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateCheckCompletedApk:
                        return strUpdateInfoApkCheckCompleted;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateCheckLocalApk:
                        return strUpdateInfoApkCheckLocal;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateCheckConfig:
                        return strUpdateInfoApkCheckConfig;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateCheckDiff:
                        return strUpdateInfoApkCheckDiff;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_ApkUpdateCheckFull:
                        return strUpdateInfoApkCheckFull;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_SourceUpdateDownloadList:
                        isDownloading = true;
                        return strUpdateInfoSourceDownloadList;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_SourcePrepareUpdate:
                        return strUpdateInfoSourcePrepareUpdate;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_SourceAnalyseDiff:
                        return strUpdateInfoSourceAnalyseDiff;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_SourceDownload:
                        isDownloading = true;
                        return strUpdateInfoSourceDownload;
                    case IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE.VS_SourceExtract:
                        return strUpdateInfoSourceExtract;
                    default:
                        return strUpdateInfoDefault;
                }
            }
        }
        
        public class DolphinErrorStringEnglish : IDolphinErrorStringInterface
        {
            public string strUpdateErrorDefault = "Update Failed";
            public string strUpdateErrorNetError = "Connection Error. Please Check your Internet Connection.";
            public string strUpdateErrorNetTimeOut = "Connection Timeout. Please Check your Internet Connection.";
            public string strUpdateErrorNoSpace = "Not Enough Storage Space. Please Make Room for the Update.";
            public string strUpdateErrorOtherSystemError = "System Error. Please Try Again after Rebooting.";
            public string strUpdateErrorOtherError = "System Error. Please Try Again after Rebooting.";
            public string strUpdateErrorNotSupport = "The Current Version is Too Old. Please Download the Latest Version.";
            public string strUpdateErrorNotSure = "Unknown Error. Try Again after Checking Your Internet Connection and Storage Space.";//not exist
            public string strUpdateErrorNetNotSupport = "Connection Error. Please Get the Latest Version of the App.";

            public string GetUpdateErrorString(IIPSMobile.IIPSMobileErrorCodeCheck.error_type errortype)
            {
                switch (errortype)
                {
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_network:
                        return strUpdateErrorNetError;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_net_timeout:
                        return strUpdateErrorNetTimeOut;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_device_hasno_space:
                        return strUpdateErrorNoSpace;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_other_system_error:
                        return strUpdateErrorOtherSystemError;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_other_error:
                        return strUpdateErrorOtherError;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_cur_version_not_support_update:
                        return strUpdateErrorNotSupport;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_can_not_sure:
                        return strUpdateErrorNotSure;
                    case IIPSMobile.IIPSMobileErrorCodeCheck.error_type.error_type_cur_net_not_support_update:
                        return strUpdateErrorNetNotSupport;
                    default:
                        return strUpdateErrorDefault;
                }
            }
        }
    }
}
