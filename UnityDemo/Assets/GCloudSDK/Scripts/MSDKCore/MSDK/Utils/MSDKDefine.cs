//------------------------------------------------------------------------------
//
// File: MSDKDefine
// Module: MSDK
// Date: 2020-03-20
// Hash: 7f74cedddb74cd611d0a85fabc23d84c
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

namespace GCloud.MSDK
{
    public enum MSDKMethodNameID
    {
        MSDK_METHODNAMEID_UNDEFINE = 000,

        // Login
        MSDK_LOGIN_AUTOLOGIN = 111,
        MSDK_LOGIN_LOGIN = 112,
        MSDK_LOGIN_BIND = 113,
        MSDK_LOGIN_GETLOGINRESULT = 114,
        MSDK_LOGIN_SWITCHUSER = 115,
        MSDK_LOGIN_QUERYUSERINFO = 116,
        MSDK_LOGIN_LOGOUT = 117,
        MSDK_LOGIN_LOGINWITHCONFIRMCODE = 118,
        MSDK_LOGIN_WAKEUP = 119,
        MSDK_LOGIN_SCHEME = 120,
        MSDK_LOGIN_RESETGUEST = 121,

        MSDK_LOGIN_QRCODE = 122,
        MSDK_LOGIN_LOGINUI = 123,
		//Connect
		MSDK_LOGIN_CONNECT = 124,
        MSDK_LOGIN_UNCONNECT = 125,
		MSDK_LOGIN_GETCONNECTRESULT = 126,
        MSDK_LOGIN_BINDUI = 127,


		// Friend
		MSDK_FRIEND_SHARE = 211,
		MSDK_FRIEND_SEND_MESSAGE = 212,
		MSDK_FRIEND_QUERY_FRIEND = 213,
		MSDK_FRIEND_ADD_FRIEND = 214,

		MSDK_GROUP_CREATE = 311,
		MSDK_GROUP_BIND = 312,
		MSDK_GROUP_GET_GROUP_LIST = 313,
		MSDK_GROUP_GET_GROUP_STATE = 314,
		MSDK_GROUP_JOIN = 315,
		MSDK_GROUP_UNBIND = 316,
		MSDK_GROUP_REMIND_TO_BIND = 317,
		MSDK_GROUP_SEND_GROUP_MESSAGE = 318,
		MSDK_GROUP_GET_GROUP_RELATION = 319,

		MSDK_WEBVIEW_CLOSE = 411,
		MSDK_WEBVIEW_GET_ENCODE_URL = 412,
		MSDK_WEBVIEW_JS_CALL = 413,
		MSDK_WEBVIEW_JS_SHARE = 414,
		MSDK_WEBVIEW_JS_SEND_MESSAGE = 415,


		MSDK_PUSH_REGISTER_PUSH = 511,
		MSDK_PUSH_UNREGISTER_PUSH = 512,
		MSDK_PUSH_SET_TAG = 513,
		MSDK_PUSH_DELETE_TAG = 514,
		MSDK_PUSH_ADD_LOCAL_NOTIFICATION = 515,
		MSDK_PUSH_CLEAR_LOCAL_NOTIFICATION = 516,
		MSDK_PUSH_NOTIFICAITON_CALLBACK = 517,
		MSDK_PUSH_NOTIFICATION_SHOW = 518,
		MSDK_PUSH_NOTIFICATION_CLICK = 519,
		MSDK_PUSH_SET_ACCOUNT = 520,
		MSDK_PUSH_DELETE_ACCOUNT = 521,

        MSDK_NOTICE_LOAD_DATA = 611,

        MSDK_GAME_SETUP = 711,
        MSDK_GAME_SHOW_LEADER_BOARD = 712,
        MSDK_GAME_SET_SCORE = 713,
        MSDK_GAME_SHOW_ACHIEVEMENT = 714,
        MSDK_GAME_UNLOCK_ACHIEVE = 715,

		MSDK_TOOLS_OPEN_DEEPLINK = 911,
		MSDK_TOOLS_FREE_FLOW = 913,

		MSDK_CRASH_CALLBACK_EXTRA_DATA           = 1011,
		MSDK_CRASH_CALLBACK_EXTRA_MESSAGE        = 1012,

        MSDK_EXTEND = 1111,

		MSDK_LBS_GETLOCATION           = 1211,
		MSKD_LBS_CLEARLOCATION         = 1212,
		MSDK_LBS_GETNEARBY             = 1213,
		MSDK_LBS_GETIPINFO	           = 1214,
        
        MSDK_ACCOUNT_VERIFY_CODE           = 1311,
        MSDK_ACCOUNT_RESET_PASSWORD        = 1312,
        MSDK_ACCOUNT_MODIFY                = 1313,
        MSDK_ACCOUNT_LOGIN_WITH_CODE       = 1314,
        MSDK_ACCOUNT_REGISTER_STATUS       = 1315,
        MSDK_ACCOUNT_VERIFY_CODE_STATUS	   = 1316,
    }

    #region MSDKBaseRet 

    public class MSDKBaseRet : JsonSerializable
    {
        private int methodNameID;

        private int retCode;

        private string retMsg;

        private int thirdCode;

        private string thirdMsg;

        private string extraJson;

        // 标记是从哪个方法过来
        [JsonProp("methodNameID")]
        public int MethodNameId
        {
            get { return methodNameID; }
            set { methodNameID = value; }
        }

        [JsonProp("retCode")]
        public int RetCode
        {
            get { return retCode; }
            set { retCode = value; }
        }

        [JsonProp("retMsg")]
        public string RetMsg
        {
            get { return retMsg; }
            set { retMsg = value; }
        }

        [JsonProp("ret")]
        public int ThirdCode
        {
            get { return thirdCode; }
            set { thirdCode = value; }
        }

        [JsonProp("msg")]
        public string ThirdMsg
        {
            get { return thirdMsg; }
            set { thirdMsg = value; }
        }

        [JsonProp("extraJson")]
        public string ExtraJson
        {
            get { return extraJson; }
            set { extraJson = value; }
        }

        public MSDKBaseRet()
        {
        }

        public MSDKBaseRet(string param) : base(param)
        {
        }

        public MSDKBaseRet(object json) : base(json)
        {
        }
    }

    public class MSDKChannel
    {
        public const string WeChat     = "WeChat";
        public const string QQ         = "QQ";
        public const string Guest      = "Guest";
        public const string Facebook   = "Facebook";
        public const string GameCenter = "GameCenter";
        public const string Google     = "Google";
        public const string Twitter    = "Twitter";

        public const string XG         = "XG";
        public const string Firebase   = "Firebase";
        public const string Adjust     = "Adjust";
        public const string AppsFlyer  = "AppsFlyer";
        public const string Beacon     = "Beacon";
        public const string Bugly      = "Bugly";
        public const string TDM        = "TDM";
    }



    #endregion
}