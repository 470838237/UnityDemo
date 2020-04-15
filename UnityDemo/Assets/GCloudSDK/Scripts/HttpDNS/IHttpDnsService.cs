// ----------------------------------------
//
// Version: 0.1.0
// Module: HttpDns
// Author: zefengwang
//
// ----------------------------------------

namespace GCloud.HttpDns
{
    public interface IHttpDnsService
    {
        #region 初始化
        /// <summary>
        /// 初始化HttpDns：如果接入了MSDK，建议初始化MSDK后再初始化HttpDns
        /// 在Unity主线程调用
        /// </summary>
        /// <param name="appId">应用手Q appId</param>
        /// <param name="debug">sdk日志开关</param>
        /// <param name="timeout">dns请求超时时间</param>
        void Init(string appId, bool debug, int timeout);

        /// <summary>
        /// 设置OpenId，已接入MSDK业务直接传MSDK OpenId，其它业务传"NULL"
        /// 在Unity主线程调用
        /// </summary>
        /// <param name="openId">用户openId</param>
        /// <returns>是否设置成功</returns>
        bool SetOpenId(string openId);
        #endregion

        #region 域名解析
        /// <summary>
        /// 同步域名解析接口
        /// 需先调用Init和SetOpenId接口，耗时接口，建议在Unity子线程中调用
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>解析完成返回最新解析结果，以';'进行分隔，若解析失败返回null</returns>
        string GetAddrByName(string domain);
        #endregion
    }
}
