#if UNITY_ANDROID || UNITY_IOS
using System;
namespace GCloud
{

    public interface IGTraceWrapper
    {
        String GetTraceId();
        String CreateContext(String parentContext, String privateType);
        void SpanStart(String context, String name,String caller, String callee);
        void SpanFlush(String context, String key, String value);
        void SpanFinish(String context, String errCode, String errMsg);
    }
}
#endif
