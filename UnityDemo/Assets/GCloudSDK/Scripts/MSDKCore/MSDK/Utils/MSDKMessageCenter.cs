//------------------------------------------------------------------------------
//
// File: MSDKMessageCenter
// Module: MSDK
// Date: 2020-03-31
// Hash: e53f22b65110b0c4927f0eccdbc59f95
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;


namespace GCloud.MSDK
{
    public class RetArgsWrapper
    {
        private readonly int methodId;
        private readonly string retJson;

        public int MethodId {
            get { return methodId; }
        }

        public string RetJson {
            get { return retJson; }
        }

        public RetArgsWrapper (int _methodId, string _retJson)
        {
            methodId = _methodId;
            retJson = _retJson;
        }
    }

    #region MSDKMessageCenter

    public class MSDKMessageCenter : MonoBehaviour
    {
#if GCLOUD_MSDK_WINDOWS

#else
        // game not started, call from invited message.
        public void OnWakeLogin(string result)
        {
            MSDKLog.Log("OnWakeLogin, result= " + result);
            string[] args = result.Split(new string[] { "@&@"},StringSplitOptions.None);
            OnMSDKRet(int.Parse(args[0]), args[1]);
        }

		// android UnitySendMessage to this method
		public void OnMSDKMessage(string result)
		{
            // 对UnitySendMessage回来的消息进行BASE64解码，可以单独抽一个函数
            byte[] plainText = Convert.FromBase64String(result);
            result = Encoding.UTF8.GetString(plainText);
            // BASE64解码结束
            MSDKLog.Log("OnMSDKMessage, result= " + result);
			int index = result.IndexOf ("@&@");
			if (index < 0) {
				MSDKLog.LogError ("Error Result, resultJson="+result );
				return; 
			}
			string args1 = result.Substring (0,index);
			string args2 = result.Substring (index+3);
			OnMSDKRet(int.Parse(args1), args2);
		}
#endif
        #region json ret and callback

        private delegate string MSDKRetJsonEventHandler (int methodId, [MarshalAs (UnmanagedType.LPStr)] string jsonstr);


        [MonoPInvokeCallback (typeof (MSDKRetJsonEventHandler))]
        public static string OnMSDKRet (int methodId, string ret)
        {
            var argsWrapper = new RetArgsWrapper (methodId, ret);
            MSDKLog.Log ("OnMSDKRet, the methodId is ( " + methodId + " )  ret=\n " + ret);
#if GCLOUD_MSDK_WINDOWS

#else
            if(methodId == (int)MSDKMethodNameID.MSDK_WEBVIEW_JS_CALL)  //JS回调在webview线程中，不走unity主线程
            {
                ParaseDelegate (argsWrapper);
                return "";
            }
            if(methodId == (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_MESSAGE ||
                methodId == (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_DATA)  
            {
                string result = SynchronousDelegate (argsWrapper);
                return result;
            }
            if (methodId == (int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT) { // GetLoginResult 改为同步接口，不会出现这种回调
                MSDKLog.LogError ("MSDK_LOGIN_GETLOGINRESULT");
                return "";
            }
            if(methodId == (int)MSDKMethodNameID.MSDK_LOGIN_WAKEUP)
            {
                wakeUpLoginRet = ret;
            }
#endif
            lock (queueLock) {
                resultQueue.Enqueue (argsWrapper);
            }
            return "";
        }

        [DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void setUnityCallback (MSDKRetJsonEventHandler eventHandler);

        #endregion

        /// <summary>
        /// 游戏主动获取wakeup的回调
        /// </summary>
        /// <returns>The wake up login ret.</returns>
#if GCLOUD_MSDK_WINDOWS

#else
        public MSDKLoginRet GetWakeUpLoginRet()
        {
            MSDKLog.Log("GetWakeUpLoginRet, wakeUpLoginRet= " + wakeUpLoginRet);
            if (!string.IsNullOrEmpty (wakeUpLoginRet)) {
                return new MSDKLoginRet(wakeUpLoginRet);
            }
            return null;
        }
        public void ClearWakeUpLoginRet()
        {
            wakeUpLoginRet = "";
        }
#endif
        static string wakeUpLoginRet;
        static MSDKMessageCenter instance;

        public static MSDKMessageCenter Instance {
            get {
                if (instance != null) return instance;
                var bridgeGameObject = new GameObject { name = "MSDKMessageCenter" };
                DontDestroyOnLoad (bridgeGameObject);
                instance = bridgeGameObject.AddComponent (typeof (MSDKMessageCenter)) as MSDKMessageCenter;
                MSDKLog.Log ("MSDKMessageCenter  instance="+instance);
                return instance;
            }
        }

        public void Init ()
        {
#if GCLOUD_MSDK_WINDOWS
            setUnityCallback(OnMSDKRet);
#elif !(UNITY_EDITOR || UNITY_STANDALONE)

            setUnityCallback(OnMSDKRet);

#endif
            MSDKLog.Log ("MSDK Init, set unity callback");
        }

        private static readonly object queueLock = new object ();
        private static readonly Queue resultQueue = new Queue (10);

        private void Update ()
        {
            lock (queueLock) {
                if (resultQueue.Count <= 0) return;
                try {
                    var arg = resultQueue.Dequeue ();
                    ParaseDelegate (arg);
                } catch (Exception e) {
                    MSDKLog.LogError (e.Message + "   \n" + e.StackTrace);
                }
            }
        }

        private static void ParaseDelegate (object arg)
        {
            var argsWrapper = (RetArgsWrapper)arg;
            var methodId = argsWrapper.MethodId;
            var json = argsWrapper.RetJson;

            MSDKLog.Log ("the methodId is ( " + methodId + " ) and json=\n " + json);
            var className = "";
            var methodName = "";
            switch (methodId) {
            case (int)MSDKMethodNameID.MSDK_LOGIN_AUTOLOGIN:
            case (int)MSDKMethodNameID.MSDK_LOGIN_LOGIN:
            case (int)MSDKMethodNameID.MSDK_LOGIN_BIND:
            case (int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT:
            case (int)MSDKMethodNameID.MSDK_LOGIN_SWITCHUSER:
            case (int)MSDKMethodNameID.MSDK_LOGIN_QUERYUSERINFO:
            case (int)MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE:
            case (int)MSDKMethodNameID.MSDK_LOGIN_LOGINUI:
            case (int)MSDKMethodNameID.MSDK_LOGIN_BINDUI:
                className = "GCloud.MSDK.MSDKLogin";
                methodName = "OnLoginRet";
                MSDKLogin.OnLoginRet(json);
                break;

			case (int)MSDKMethodNameID.MSDK_LOGIN_CONNECT:
			case (int)MSDKMethodNameID.MSDK_LOGIN_GETCONNECTRESULT:
				className = "GCloud.MSDK.MSDKLogin";
				methodName = "OnConnectRet";
				MSDKLogin.OnConnectRet(json);
				break;
            case (int)MSDKMethodNameID.MSDK_LOGIN_LOGOUT:
            case (int)MSDKMethodNameID.MSDK_LOGIN_WAKEUP:
            case (int)MSDKMethodNameID.MSDK_LOGIN_SCHEME:
            case (int)MSDKMethodNameID.MSDK_LOGIN_RESETGUEST:
                className = "GCloud.MSDK.MSDKLogin";
                methodName = "OnLoginBaseRet";
                MSDKLogin.OnLoginBaseRet(json);
                break;
#if GCLOUD_MSDK_WINDOWS
            case (int)MSDKMethodNameID.MSDK_LOGIN_QRCODE:
                className = "GCloud.MSDK.MSDKLogin";
                methodName = "OnQrCodeRet";
                MSDKLogin.OnQrCodeRet(json);
                break;
#else
            /// account
            case (int)MSDKMethodNameID.MSDK_ACCOUNT_VERIFY_CODE:
            case (int)MSDKMethodNameID.MSDK_ACCOUNT_RESET_PASSWORD:
            case (int) MSDKMethodNameID.MSDK_ACCOUNT_MODIFY:
            case (int) MSDKMethodNameID.MSDK_ACCOUNT_REGISTER_STATUS:
                case (int) MSDKMethodNameID.MSDK_ACCOUNT_VERIFY_CODE_STATUS:
                className = "GCloud.MSDK.MSDKAccount";
                methodName = "OnAccountRet";
                MSDKAccount.OnAccountRet(json);
                break;
            /// friend
            case (int)MSDKMethodNameID.MSDK_FRIEND_SHARE:
            case (int)MSDKMethodNameID.MSDK_FRIEND_SEND_MESSAGE:
            case (int)MSDKMethodNameID.MSDK_FRIEND_ADD_FRIEND:
                className = "GCloud.MSDK.MSDKFriend";
                methodName = "OnFriendMessage";
                MSDKFriend.OnFriendMessage(json);
                break;
            case (int)MSDKMethodNameID.MSDK_FRIEND_QUERY_FRIEND:
                className = "GCloud.MSDK.MSDKFriend";
                methodName = "OnFriendQueryFriend";
                MSDKFriend.OnFriendQueryFriend(json);
                break;

            //group
            case (int)MSDKMethodNameID.MSDK_GROUP_CREATE:
            case (int)MSDKMethodNameID.MSDK_GROUP_BIND:
            case (int)MSDKMethodNameID.MSDK_GROUP_GET_GROUP_LIST:
            case (int)MSDKMethodNameID.MSDK_GROUP_GET_GROUP_STATE:
            case (int)MSDKMethodNameID.MSDK_GROUP_JOIN:
            case (int)MSDKMethodNameID.MSDK_GROUP_UNBIND:
            case (int)MSDKMethodNameID.MSDK_GROUP_REMIND_TO_BIND:
            case (int)MSDKMethodNameID.MSDK_GROUP_SEND_GROUP_MESSAGE:
            case (int)MSDKMethodNameID.MSDK_GROUP_GET_GROUP_RELATION:
                className = "GCloud.MSDK.MSDKGroup";
                methodName = "OnGroupRet";
                MSDKGroup.OnGroupRet(json);
                break;

            // webview
            case (int)MSDKMethodNameID.MSDK_WEBVIEW_CLOSE:
            case (int)MSDKMethodNameID.MSDK_WEBVIEW_GET_ENCODE_URL:
            case (int)MSDKMethodNameID.MSDK_WEBVIEW_JS_CALL:
            case (int)MSDKMethodNameID.MSDK_WEBVIEW_JS_SHARE:
			case (int)MSDKMethodNameID.MSDK_WEBVIEW_JS_SEND_MESSAGE:
                className = "GCloud.MSDK.MSDKWebView";
                methodName = "OnWebViewRet";
                MSDKWebView.OnWebViewRet(json);
                break;

            case (int)MSDKMethodNameID.MSDK_PUSH_REGISTER_PUSH:
            case (int)MSDKMethodNameID.MSDK_PUSH_UNREGISTER_PUSH:
            case (int)MSDKMethodNameID.MSDK_PUSH_SET_TAG:
            case (int)MSDKMethodNameID.MSDK_PUSH_DELETE_TAG:
            case (int)MSDKMethodNameID.MSDK_PUSH_SET_ACCOUNT:
            case (int)MSDKMethodNameID.MSDK_PUSH_DELETE_ACCOUNT:
                className = "GCloud.MSDK.MSDKPush";
                methodName = "OnPushBaseRet";
                MSDKPush.OnPushBaseRet(json);
                break;

            case (int)MSDKMethodNameID.MSDK_PUSH_ADD_LOCAL_NOTIFICATION:
            case (int)MSDKMethodNameID.MSDK_PUSH_CLEAR_LOCAL_NOTIFICATION:
            case (int)MSDKMethodNameID.MSDK_PUSH_NOTIFICAITON_CALLBACK:
            case (int)MSDKMethodNameID.MSDK_PUSH_NOTIFICATION_SHOW:
            case (int)MSDKMethodNameID.MSDK_PUSH_NOTIFICATION_CLICK:
                className = "GCloud.MSDK.MSDKPush";
                methodName = "OnPushRet";
                MSDKPush.OnPushRet(json);
                break;

            case (int)MSDKMethodNameID.MSDK_NOTICE_LOAD_DATA:
                className = "GCloud.MSDK.MSDKNotice";
                methodName = "OnNoticeRet";
                MSDKNotice.OnNoticeRet(json);
                break;

            case (int)MSDKMethodNameID.MSDK_GAME_SETUP:
            case (int)MSDKMethodNameID.MSDK_GAME_SHOW_LEADER_BOARD:
            case (int)MSDKMethodNameID.MSDK_GAME_SET_SCORE:
            case (int)MSDKMethodNameID.MSDK_GAME_SHOW_ACHIEVEMENT:
            case (int)MSDKMethodNameID.MSDK_GAME_UNLOCK_ACHIEVE:
                className = "GCloud.MSDK.MSDKGame";
                methodName = "OnGameRet";
                MSDKGame.OnGameRet(json);
                break;
			case (int)MSDKMethodNameID.MSDK_TOOLS_FREE_FLOW:
				className = "GCloud.MSDK.MSDKTools";
				methodName = "OnToolsFreeFlowRet";
				MSDKTools.OnToolsFreeFlowRet(json);
				break;
			case (int)MSDKMethodNameID.MSDK_TOOLS_OPEN_DEEPLINK:
                className = "GCloud.MSDK.MSDKTools";
				methodName = "OnToolsRet";
				MSDKTools.OnToolsRet(json);
                break;
			case (int)MSDKMethodNameID.MSDK_EXTEND:
				className = "GCloud.MSDK.MSDKExtend";
				methodName = "OnExtendRet";
				MSDKExtend.OnExtendRet (json);
                break;

			case (int)MSDKMethodNameID.MSDK_LBS_GETIPINFO:
				className = "GCloud.MSDK.MSDKLBS";
				methodName = "OnLBSIPInfoRet";
				MSDKLBS.OnLBSIPInfoRet (json);
				break;

			case (int)MSDKMethodNameID.MSDK_LBS_GETLOCATION:
				className = "GCloud.MSDK.MSDKLBS";
				methodName = "OnLBSLocationRet";
				MSDKLBS.OnLBSLocationRet (json);
				break;

			case (int)MSDKMethodNameID.MSDK_LBS_GETNEARBY:
				className = "GCloud.MSDK.MSDKLBS";
				methodName = "OnLBSRelationRet";
				MSDKLBS.OnLBSRelationRet (json);
				break;

			case (int)MSDKMethodNameID.MSKD_LBS_CLEARLOCATION:
				className = "GCloud.MSDK.MSDKLBS";
				methodName = "OnLBSBaseRet";
				MSDKLBS.OnLBSBaseRet (json);
				break;
#endif
            }

#if USING_ITOP
           className = className.Replace("MSDK", "ITOP");
            MSDKLog.Log ("className=" + className + "  methodName=" + methodName);

            if (string.IsNullOrEmpty (className) || string.IsNullOrEmpty (methodName)) {
                MSDKLog.LogError ("something wrong with the plugins, className = " + className + "  methodName = " + methodName);
                return;
            }
            try {
                
                var assembly = Assembly.GetExecutingAssembly ();
                var type = assembly.GetType (className, true, true);
                if (type != null) {
                    var methodInfo = type.GetMethod (methodName, BindingFlags.NonPublic | BindingFlags.Static, null,new [] { typeof (string)}, null);
                    if (methodInfo != null)
                        methodInfo.Invoke (null, new object [] { json });
                    else
                        MSDKLog.LogError ("cannot get this method, methodName=" + methodName);
                } else {
                    MSDKLog.LogError ("current className is wrong,className=" + className);
                }
            } catch (Exception e) {
                MSDKLog.LogError ("call method error\n" + e.StackTrace);
            }
#endif
        }

        static string SynchronousDelegate(object arg){
            var argsWrapper = (RetArgsWrapper)arg;
            var methodId = argsWrapper.MethodId;
            var json = argsWrapper.RetJson;

            MSDKLog.Log ("the methodId is ( " + methodId + " ) and json=\n " + json);
            var className = "";
            var methodName = "";
            switch (methodId) {
#if GCLOUD_MSDK_WINDOWS
#else
            case (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_MESSAGE:
                className = "GCloud.MSDK.MSDKCrash";
                methodName = "OnCrashCallbackMessage";
                return MSDKCrash.OnCrashCallbackMessage(json);
                break;
            case (int)MSDKMethodNameID.MSDK_CRASH_CALLBACK_EXTRA_DATA:
                className = "GCloud.MSDK.MSDKCrash";
                methodName = "OnCrashCallbackData";
                return MSDKCrash.OnCrashCallbackData(json);
                break;
#endif
            }
#if USING_ITOP
           className = className.Replace("MSDK", "ITOP");

            MSDKLog.Log ("className=" + className + "  methodName=" + methodName);

            if (string.IsNullOrEmpty (className) || string.IsNullOrEmpty (methodName)) {
                MSDKLog.LogError ("something wrong with the plugins, className = " + className + "  methodName = " + methodName);
                return "";
            }

            try {

                var assembly = Assembly.GetExecutingAssembly ();
                var type = assembly.GetType (className, true, true);
                if (type != null) {
                    var methodInfo = type.GetMethod (methodName, BindingFlags.NonPublic | BindingFlags.Static, null,new [] { typeof (string)}, null);
                    if (methodInfo != null){
                        object res = methodInfo.Invoke (null,new object [] { json });
                        return res.ToString();
                    }
                    else
                        MSDKLog.LogError ("cannot get this method, methodName=" + methodName);
                } else {
                    MSDKLog.LogError ("current className is wrong,className=" + className);
                }
            } catch (Exception e) {
                MSDKLog.LogError ("call method error\n" + e.StackTrace);
            }
#endif
            return "";
        }


        #endregion
    }
}