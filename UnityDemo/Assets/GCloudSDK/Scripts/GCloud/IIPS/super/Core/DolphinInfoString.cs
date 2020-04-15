using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCloud
{
    namespace Dolphin
    {
        public interface IDolphinInfoStringInterface
        {
            string GetUpdateInfoString(IIPSMobile.IIPSMobileVersionCallBack.VERSIONSTAGE curVersionStage, UpdateType updatetype, ref bool isDownloading);
        }
        class DolphinInfoString : IDolphinInfoStringInterface
        {
            public string strUpdateInfoDefault = "正在更新中";
            public string strUpdateInfoGettingAppVersion = "获取程序版本信息";
            public string strUpdateInfoGettingSrcVersion = "获取资源版本信息";
            public string strUpdateInfoFirstExtract = "释放资源";
            public string strUpdateInfoApkDownloadConfig = "下载配置文件";
            public string strUpdateInfoApkDownDiff = "下载差异文件";
            public string strUpdateInfoApkDownFull = "下载文件";
            public string strUpdateInfoApkCheckCompleted = "校验文件";
            public string strUpdateInfoApkCheckLocal = "校验文件";
            public string strUpdateInfoApkCheckConfig = "校验文件";
            public string strUpdateInfoApkCheckDiff = "校验文件";
            public string strUpdateInfoApkMergeDiff = "合并差异文件";
            public string strUpdateInfoApkCheckFull = "校验文件";

            public string strUpdateInfoSourceDownloadList = "下载配置文件";
            public string strUpdateInfoSourcePrepareUpdate = "准备更新";
            public string strUpdateInfoSourceAnalyseDiff = "分析差异";
            public string strUpdateInfoSourceDownload = "下载资源";
            public string strUpdateInfoSourceExtract = "解压资源";

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
        public interface IDolphinErrorStringInterface
        {
            string GetUpdateErrorString(IIPSMobile.IIPSMobileErrorCodeCheck.error_type errortype);
        }
        class DolphinErrorString : IDolphinErrorStringInterface
        {
            public string strUpdateErrorDefault = "更新失败";
            public string strUpdateErrorNetError = "网络错误，请确认网络状态后重试";
            public string strUpdateErrorNetTimeOut = "网络超时，请确认网络状态后重试";
            public string strUpdateErrorNoSpace = "磁盘空间不足，请清理空间后重试";
            public string strUpdateErrorOtherSystemError = "系统错误，请重启手机";
            public string strUpdateErrorOtherError = "系统错误，请重启手机";
            public string strUpdateErrorNotSupport = "当前版本不支持更新，请到市场上下载最新版本";
            public string strUpdateErrorNotSure = "未知错误，检查网络，检查磁盘空间后重启更新";//不存在
            public string strUpdateErrorNetNotSupport = "当前的环境下载异常，请到市场下载最新版本";

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
