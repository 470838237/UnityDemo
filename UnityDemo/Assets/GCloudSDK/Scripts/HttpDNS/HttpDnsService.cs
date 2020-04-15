// ----------------------------------------
//
// Version: 0.1.0
// Module: HttpDns
// Author: zefengwang
//
// ----------------------------------------

using System;

namespace GCloud.HttpDns
{
    public class HttpDnsService : IHttpDnsService
    {
        void IHttpDnsService.Init(string appId, bool debug, int timeout)
        {
            HttpDns.Init(appId, debug, timeout);
        }

        bool IHttpDnsService.SetOpenId(string openId)
        {
            return HttpDns.SetOpenId(openId);
        }

        string IHttpDnsService.GetAddrByName(string domain)
        {
            return HttpDns.GetAddrByName(domain);
        }
    }
}
