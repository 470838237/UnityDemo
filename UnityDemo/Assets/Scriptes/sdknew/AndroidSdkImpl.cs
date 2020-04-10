#define AndroidSDK
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：游戏调用Android的接口
    /// </summary>
    class AndroidSdkImpl : IBaseApi
    {

#if AndroidSDK
        protected AndroidJavaObject currentActivity;

        public AndroidSdkImpl()
        {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        public void Init(HonorSDKGameObject gameObject, OnFinish<ResultInit> initListener, string gameResVersion, Dictionary<string, string> configs = null)
        {
            JSONClass json = new JSONClass();
            foreach (KeyValuePair<string, string> kv in configs)
            {
                json.Add(kv.Key, new JSONData(kv.Value));
            }
            string configsStr = json.ToString();
            currentActivity.Call("init", gameObject.name, configs);
        }


        protected virtual void SetGameObjectName(string gameObjectName)
        {
            currentActivity.Call("setGameObjectName", gameObjectName);
        }
        public virtual void Login(OnFinish<UserInfo> loginListener)
        {

            currentActivity.Call("login");
        }

        public virtual void GetAppInfo(OnFinish<AppInfo> appInfoListener)
        {

            currentActivity.Call("getAppInfo");
        }

        public virtual void RegisterNotchScreenInfoListener(OnFinish getNotchInfoListener)
        {

            currentActivity.Call("getNotchScreenInfo");
        }

        public virtual void GetCountryAndLanguage(OnFinish<Locale> getCountryCodeListener)
        {

            currentActivity.Call("getCountryAndLanguage");
        }

        public virtual void ReportError(string errorMsg, string type)
        {

            currentActivity.Call("reportError", errorMsg, type);
        }

        public virtual void GetMemory(OnFinish<MemoryInfo> getMemroyInfoListener)
        {

            currentActivity.Call("getMemory");
        }


        public virtual void GetBattery(OnFinish<BatteryInfo> getBatteryInfoListener)
        {

            currentActivity.Call("getBattery");
        }

        public virtual void PushNotification(string content, int delay, int id)
        {

            currentActivity.Call("pushNotification", content, delay, id);
        }

        public virtual void CleanNotification(int id)
        {

            currentActivity.Call("cleanNotification", id);
        }
        public virtual void CleanAllNotification()
        {

            currentActivity.Call("cleanAllNotification");
        }

        public virtual void SwitchAccount()
        {
            currentActivity.Call("switchAccount");
        }

        public virtual void RegisterSwitchAccountListener(OnFinish<UserInfo> switchAccountListener)
        {

            currentActivity.Call("registerSwitchAccountListener");
        }

        public virtual void StartBind(OnFinish<ResultBind> startBindListener)
        {

            currentActivity.Call("startBind");
        }

        public virtual void Logout(OnFinish<Result> logoutListener)
        {

            currentActivity.Call("logout");
        }

        public virtual bool HasExitDialog()
        {
            return currentActivity.Call<bool>("hasExitDialog");
        }

        public virtual void UploadGameRoleInfo(GameRoleInfo gameRoleInfo)
        {
            JSONClass json = new JSONClass();
            json.Add("type", new JSONData(gameRoleInfo.type));
            json.Add("roleId", new JSONData(gameRoleInfo.roleId));
            json.Add("roleName", new JSONData(gameRoleInfo.roleName));
            json.Add("roleLevel", new JSONData(gameRoleInfo.roleLevel));
            json.Add("roleVip", new JSONData(gameRoleInfo.roleVip));
            json.Add("serverId", new JSONData(gameRoleInfo.serverId));
            json.Add("sex", new JSONData(gameRoleInfo.gender));
            json.Add("balance", new JSONData(gameRoleInfo.balance));
            json.Add("lastUpdate", new JSONData(gameRoleInfo.lastUpdate));
            json.Add("extra", new JSONData(gameRoleInfo.extra));
            currentActivity.Call("uploadGameRoleInfo", json.ToString());
        }

        public virtual void Pay(OrderInfo orderInfo, OnFinish<ResultPay> payListener)
        {
            JSONClass json = new JSONClass();
            json.Add("roleId", new JSONData(orderInfo.roleId));
            json.Add("isFirstPay", new JSONData(orderInfo.isFirstPay));
            json.Add("roleName", new JSONData(orderInfo.roleName));
            json.Add("roleLevel", new JSONData(orderInfo.roleLevel));
            json.Add("count", new JSONData(orderInfo.count));
            json.Add("goodsId", new JSONData(orderInfo.goodsId));
            json.Add("extra", new JSONData(orderInfo.extra));
            json.Add("vipLevel", new JSONData(orderInfo.vipLevel));
            json.Add("balance", new JSONData(orderInfo.balance));
            json.Add("serverId", new JSONData(orderInfo.serverId));
            currentActivity.Call("pay", json.ToString());

        }

        public virtual void Exit(OnFinish<Result> exitListener)
        {

            currentActivity.Call("exit");
        }

        public virtual void GetNoticeList(OnFinish<NoticeList> getNoticeListListener, string serverId, string language, string country, string type)
        {

            currentActivity.Call("getNoticeList", serverId, language, country, type);
        }

        public virtual void GetServerList(OnFinish<ServerList> getServerListListener)
        {

            currentActivity.Call("getServerList");
        }

        public virtual void GetGoodsList(OnFinish<GoodsList> getGoodsListListener, string serverId, string category, string currency)
        {

            currentActivity.Call("getGoodsList", serverId, category, currency);
        }
        public virtual void SetClipboard(string content)
        {

            currentActivity.Call("setClipboard", content);
        }
        public virtual bool IsSupportApi(Api api)
        {

            return currentActivity.Call<bool>("isSupportApi", Convert.ToInt32(api));
        }

        public virtual void ExpandFunction(string functionName, string jsonParameter, string headName, OnFinish<ResultExpand> expandFunctionListener)
        {

            currentActivity.Call("expandFunction", functionName, jsonParameter, headName);
        }

        public virtual void SendGuideFinish()
        {

            currentActivity.Call("sendGuideFinish");
        }

        public virtual void StartNewGame(OnFinish<UserInfo> startNewGameListener)
        {

            currentActivity.Call("startNewGame");
        }


        public virtual void GetNetStateInfo(OnFinish<NetStateInfo> getNetStateInfoListener)
        {

            currentActivity.Call("getNetStateInfo");
        }

        public virtual void SetApplicationLocale(string language, string country = "")
        {

            currentActivity.Call("setApplicationLocale", language, country);
        }

        public NotchScreenInfo GetNotchScreenInfo()
        {
            throw new NotImplementedException();
        }

        public virtual void RestartApp()
        {
            currentActivity.Call("restartApp");
        }
        public virtual DiskInfo GetDiskInfo()
        {
            string result = currentActivity.Call<string>("getDiskInfo");
            DiskInfo info = new DiskInfo();
            JSONNode node = JSONNode.Parse(result);
            info.totalSize = node["totalSize"].AsLong;
            info.availSize = node["availSize"].AsLong;
            return info;
        }
        public virtual void DownloadText(string url, int retry, int timeout, OnFinish<ResultDownloadText> downloadTextListener)
        {
            currentActivity.Call("downloadText", url, retry, timeout);
        }

        public virtual void GetHeadsetState(bool notifyWhenHeadsetChanged, OnFinish<HeadsetStateInfo> getHeadsetStateListener)
        {
            currentActivity.Call("getHeadsetState", notifyWhenHeadsetChanged ? "true" : "false");
        }

#endif
    }
}
