using System;
using System.Text;

namespace IIPSMobile
{
    public class IIPSMobileErrorCodeCheck
    {
        public IIPSMobileErrorCodeCheck()
        {

        }

        public enum first_module
        {
            first_module_init = 0,
            first_module_data = 1,          //datamanager,只使用更新的请忽略
            first_module_version = 2,        //versionmanager,更新模块
        }

        public enum second_module
        {
            second_module_init = 0,
            second_module_datamanager = 1,          //datamanager
            second_module_datadownloader = 2,       //datadownloader
            second_module_dataqueryer = 3,          //dataqueryer
            second_module_datareader = 4,           //datareader

            second_module_versionmanager = 1,       //更新控制
            second_module_version_action = 2,       //连接版本服务器
            second_module_update_action = 3,                //标准ifs更新 对应update_type=1
            second_module_extract_action = 4,               //标准ifs更新，解压ifs 对应update_type=1，next_action="extract_action"
            second_module_apkupdate_action = 5,             //apk更新，对应update_type=3或者4
            second_module_srcupdate_fulldiff_action = 6,    //使用ifs全量资源差异更新，对应update_type=5或者8
            second_module_srcupdate_mergeifs_action = 7,    //标准ifs更新，合并ifs包，对应update_type=1
            second_module_srcupdate_cures_action = 8,       //使用ifs全量资源差异更新，使用cures方案，对应update_type=5或者8
            second_module_srcupdate_filediff_action = 9,    //使用资源差异更新，对应update_type=7
			second_module_apkupdate_allchannel_action = 10, //apk更新，allchannels方案
			second_module_filelist_check_action = 11,		//文件校验更新更新
        }

        //这个用于出现错误，内部与iips项目组确认问题
        public enum error_type_inside
        {
            error_type_inside_init = 0,
            error_type_inside_download_error = 1,          //下载错误
            error_type_inside_system_error = 2,            //系统错误
            error_type_inside_module_error = 3,            //模块内部错误
            error_type_inside_ifs_error = 4,               //IFS错误
            error_type_inside_should_not_happen = 5,       //流程上不能出现的错误
			error_type_inside_other_engine_down_error = 6, //其他下载引擎下载错误
        }

        //这个用于提示用户，出现此类错误如何解决问题。
        public enum error_type
        {
            error_type_init = 0,
            error_type_network = 1,                         //网络错误，提示玩家在网络正常状态重启游戏更新
            error_type_net_timeout = 2,                     //网络超时，游戏侧先重试，提示玩家在网络正常状态重启游戏更新
            error_type_device_hasno_space = 3,              //磁盘空间不足，提示玩家提供充足的sd卡空间，然后再启动游戏
            error_type_other_system_error = 4,              //其他系统错误，没权限，或磁盘问题，提示玩家重启手机
            error_type_other_error = 5,                     //模块其他错误，提示玩家重启手机
            error_type_cur_version_not_support_update = 6,  //当前版本不支持更新，提示玩家市场上下载最新版本
            error_type_can_not_sure = 7,                    //有可能是网络错误，也有可能是磁盘问题，提示玩家检查网络，检查磁盘空间后在重启更新
			error_type_cur_net_not_support_update = 8,		//当前的环境下载的apk异常（运营商缓存），提示玩家到市场下载最新版本
        }

        public struct ErrorCodeInfo
        {
            public bool m_bCheckOk;                         //检查是否成功
            public int m_nErrorType;                        //错误类型，根据此项，提示用户（参见error_type）
            public int m_nFirstModule;                      //一级模块，不必提示用户，记录日志，以备定位，开发阶段调试定位用
            public int m_nSecondModule;                     //二级模块，不必提示用户，记录日志，以备定位，开发阶段调试定位用
            public int m_nInsideErrorType;                  //内部错误类型，不必提示用户，记录日志，以备定位，开发阶段调试定位用
            public int m_nErrorCode;                        //具体错误码，不必提示用户，记录日志，以备定位，开发阶段调试定位用
            public int m_nLastCheckError;                   //onerror回调的具体错误码，不必提示用户，记录日志，以备定位，开发阶段调试定位用
            public ErrorCodeInfo(bool bOk)
            {
                m_bCheckOk = bOk;
                m_nFirstModule = (int)first_module.first_module_init;
                m_nSecondModule = (int)second_module.second_module_init;
                m_nErrorType = (int)error_type.error_type_init;
                m_nInsideErrorType = (int)error_type_inside.error_type_inside_init;
                m_nErrorCode = 0;
                m_nLastCheckError = 0;
            }
        }

        private int m_nLastCheckErrorCode = 0;

        public ErrorCodeInfo CheckIIPSErrorCode(int nErrorCode)
        {
            m_nLastCheckErrorCode = nErrorCode;
            int nFirstModule = GetFirstModuleType();
            int nSecondModule = GetSecondModuleType();
            int nErrorInsideType = GetErrorCodeType();
            int nRealErrorCode = GetRealErrorCode();
            int nErrorType = (int)error_type.error_type_init;

            if (nErrorInsideType == (int)error_type_inside.error_type_inside_download_error
				||nErrorInsideType == (int)error_type_inside.error_type_inside_other_engine_down_error)
            {
                //下载错误
                int nDownloadErrorType = GetDownloadErrorType(nRealErrorCode);
                int realdownerror = GetReadDownloadErrorCode(nRealErrorCode);
                if (nDownloadErrorType == 5)
                {
                    //磁盘满错误码
                    if (realdownerror == 112||realdownerror==39||realdownerror == 28)
                    {
                        nErrorType = (int)error_type.error_type_device_hasno_space;
                    }
                    else
                    {
                        nErrorType = (int)error_type.error_type_other_system_error;
                    }
                }
                else if (nDownloadErrorType == 1)
                {
                    nErrorType = (int)error_type.error_type_net_timeout;
                    realdownerror = nRealErrorCode;
                }
                else if (nDownloadErrorType == 2)
                {
                    nErrorType = (int)error_type.error_type_network;
                }
                else
                {
                    nErrorType = (int)error_type.error_type_other_error;
                }
                nRealErrorCode = realdownerror;
            }
            else if (nErrorInsideType == (int)error_type_inside.error_type_inside_system_error)
            {
                //系统错误
                //磁盘满错误码
                if (nRealErrorCode == 112 || nRealErrorCode == 39 || nRealErrorCode == 28)
                {
                    nErrorType = (int)error_type.error_type_device_hasno_space;
                }
                else
                {
                    nErrorType = (int)error_type.error_type_other_system_error;
                }
            }
            else if (nErrorInsideType == (int)error_type_inside.error_type_inside_module_error)
            {
                //各模块错误
                if (nFirstModule == (int)first_module.first_module_data)
                {
                    //datamanager 暂时全部其他错误
                    nErrorType = (int)error_type.error_type_other_error;
                }
                else if (nFirstModule == (int)first_module.first_module_version)
                {
                    nErrorType = GetSecondModuleNoticeErrorType(nSecondModule, nRealErrorCode);
                }
                else
                {
                    nErrorType = (int)error_type.error_type_other_error;
                }
            }
            else if (nErrorInsideType == (int)error_type_inside.error_type_inside_ifs_error)
            {
                //系统错误
                //磁盘满错误码
                if (nRealErrorCode == 112 || nRealErrorCode == 39 || nRealErrorCode == 28)
                {
                    nErrorType = (int)error_type.error_type_device_hasno_space;
                }
                else
                {
                    nErrorType = (int)error_type.error_type_other_system_error;
                }
            }
            else if (nErrorInsideType == (int)error_type_inside.error_type_inside_should_not_happen)
            {
                nErrorType = (int)error_type.error_type_other_error;
            }
            else
            {
                nErrorType = (int)error_type.error_type_other_error;
            }

            ErrorCodeInfo stInfo = new ErrorCodeInfo(false);
            stInfo.m_bCheckOk = true;
            stInfo.m_nErrorType = nErrorType;
            stInfo.m_nFirstModule = nFirstModule;
            stInfo.m_nSecondModule = nSecondModule;
            stInfo.m_nInsideErrorType = nErrorInsideType;
            stInfo.m_nErrorCode = nRealErrorCode;
            stInfo.m_nLastCheckError = m_nLastCheckErrorCode;
            return stInfo;
        }

        //获取错误码对应一级模块
        private int GetFirstModuleType()
        {
            int nTemp = m_nLastCheckErrorCode >> 23;
            nTemp = nTemp & 0x07;
            return nTemp;
        }

        //获取错误码对应二级模块
        private int GetSecondModuleType()
        {
            int nTemp = m_nLastCheckErrorCode >> 26;
            nTemp = nTemp & 0x0f;
            return nTemp;
        }

        //获取错误码类型
        private int GetErrorCodeType()
        {
            int nTemp = m_nLastCheckErrorCode >> 20;
            nTemp = nTemp & 0x07;
            return nTemp;
        }

        //获取具体错误码
        private int GetRealErrorCode()
        {
            int nTemp = m_nLastCheckErrorCode & 0xfffff;
            return nTemp;
        }

        private int GetDownloadErrorType(int downloaderror)
        {
            int nTemp = downloaderror >> 16;
            nTemp = nTemp & 0x0f;
            return nTemp;
        }

        private int GetReadDownloadErrorCode(int downloaderror)
        {
            int nTemp = downloaderror & 0xffff;
            return nTemp;
        }

        private int GetSecondModuleNoticeErrorType(int secondModule, int errorcode)
        {
            int nTemp = 0;
            if (secondModule == (int)second_module.second_module_versionmanager)
            {
                nTemp = (int)error_type.error_type_other_error;
            }
            else if (secondModule == (int)second_module.second_module_version_action)
            {
                if (errorcode ==15||errorcode == 16||errorcode ==17||errorcode==22)
                {
                    nTemp = (int)error_type.error_type_cur_version_not_support_update;
                }
                else
                {
                    nTemp = (int)error_type.error_type_network;
                }
            }
            else if (secondModule == (int)second_module.second_module_apkupdate_action)
            {
                if (errorcode == 4)
                {
                    nTemp = (int)error_type.error_type_other_system_error;
                }
				else if(errorcode == 4006)
				{
					nTemp = (int)error_type.error_type_cur_net_not_support_update;
				}
                else
                {
                    nTemp = (int)error_type.error_type_other_error;
                }
            }
            else if (secondModule == (int)second_module.second_module_srcupdate_fulldiff_action)
            {
                nTemp = (int)error_type.error_type_can_not_sure;
            }
			else if (secondModule == (int)second_module.second_module_apkupdate_allchannel_action)
			{
				if (errorcode == 7 || errorcode == 14 || errorcode == 15)
				{
					nTemp = (int)error_type.error_type_cur_net_not_support_update;
				}
				else
				{
					nTemp = (int)error_type.error_type_other_error;
				}
			}
            else
            {
                nTemp = (int)error_type.error_type_other_error;
            }
            return nTemp;
        }
    }
}
