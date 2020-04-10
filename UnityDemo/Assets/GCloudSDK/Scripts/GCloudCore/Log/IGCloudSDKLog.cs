namespace GCloud
{

    public enum ALogPriority
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Event = 3,
        Error = 4,
        None = 5,
    }

    public interface IGCloudSDKLog
    {
        void SetLogLevel(string sdkName, ALogPriority logLevel);
        void SetAllLogLevel(ALogPriority logLevel);
    }


}
