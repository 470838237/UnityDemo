
using System.Collections.Generic;


namespace HonorSdk
{

    public delegate void OnFinish();
    public delegate void OnFinish<T>(T result);
    public delegate void OnReceiveMsg(string head, string body);

    public class MessageHandler
    {
        const string SET_GAME_OBJECT_NAME_SUCCESS = "set_game_object_name_success";
        //初始化成功
        const string INIT_SUCCESS = "init_success";
        //初始化失败
        const string INIT_FAILED = "init_failed";
        //登录成功
        const string LOGIN_SUCCESS = "login_success";
        //登录失败
        const string LOGIN_FAILED = "login_failed";
        //获取应用信息
        const string GET_APP_INFO = "get_app_info";
        //获取刘海屏信息
        const string GET_NOTCH_SCREEN_INFO = "get_notch_screen_info_success";
        //获取国家码
        const string GET_COUNTRY_CODE = "get_country_code";
        //获取手机内存信息
        const string GET_MEMORY = "get_memory";
        //获取手机电量信息
        const string CURRENT_BATTERY = "current_battery";
        //切换账号成功
        const string SWITCH_ACCOUNT_SUCCESS = "switch_account_success";
        //切换账号失败
        const string SWITCH_ACCOUNT_FAILED = "switch_account_failed";
        //注销成功
        const string LOGOUT_SUCCESS = "logout_success";
        //注销失败
        const string LOGOUT_FAILED = "logout_failed";
        //支付成功
        const string PAY_SUCCESS = "pay_success";
        //支付失败
        const string PAY_FAILED = "pay_failed";
        //退出成功
        const string EXIT_SUCCESS = "exit_success";
        //退出失败
        const string EXIT_FAILED = "exit_failed";
        //获取公告列表成功
        const string GET_NOTICE_SUCCESS = "get_notice_success";
        //获取公告列表失败
        const string GET_NOTICE_FAILED = "get_notice_failed";
        //获取服务器列表成功
        const string GET_SERVER_LIST_SUCCESS = "get_server_list_success";
        //获取服务器列表失败
        const string GET_SERVER_LIST_FAILED = "get_server_list_failed";
        //获取商品列表成功
        const string GET_GOODS_LIST_SUCCESS = "get_goods_list_success";
        //获取商品列表失败
        const string GET_GOODS_LIST_FAILED = "get_goods_list_failed";
        //账号绑定成功
        const string BIND_SUCCESS = "bind_success";
        //账号绑定失败
        const string BIND_FAILED = "bind_failed";
        //获取网络状态
        const string GET_NET_STATE_INFO = "get_net_state_info";
        //下载文本文件成功
        public const string DOWNLOAD_TEXT_SUCCESS = "download_text_success";
        //下载文本文件失败
        public const string DOWNLOAD_TEXT_FAILED = "download_text_failed";
        //获取耳机状态成功
        public const string GET_HEADSET_STATE_SUCCESS = "get_headset_state_success";



        private OnFinish<ResultInit> initListener;
        private OnFinish<UserInfo> loginListener;
        private OnFinish<UserInfo> switchAccountListener;
        private OnFinish<AppInfo> appInfoListener;
        private OnFinish<NotchScreenInfo> getNotchInfoListener;
        private OnFinish<Locale> getCountryAndLanguageListener;
        private OnFinish<MemoryInfo> getMemroyInfoListener;
        private OnFinish<BatteryInfo> getBatteryInfoListener;
        private OnFinish<Result> playVideoListener;
        private OnFinish<Result> logoutListener;
        private OnFinish<ResultPay> payListener;
        private OnFinish<Result> exitListener;
        private OnFinish<NoticeList> getNoticeListListener;
        private OnFinish<ServerList> getServerListListener;
        private OnFinish<GoodsList> getGoodsListListener;
        private OnFinish<ResultBind> startBindListener;
        private OnFinish<NetStateInfo> getNetStateInfoListener;
        private OnFinish<NetStateInfo> networkStateListener;
        private OnFinish<ResultDownloadText> downloadTextListener;
        private OnFinish<HeadsetStateInfo> getHeadsetStateListener;

        private Dictionary<string, OnFinish<ResultExpand>> expandListeners = new Dictionary<string, OnFinish<ResultExpand>>();
        protected SDKManager manager;

        public MessageHandler(SDKManager manager)
        {
            this.manager = manager;
        }

   
        public virtual void RegisterCallback<T>(Api function,OnFinish<T> callback, string headName=null) {
            switch (function)
            {
                case Api.INIT:
                    this.initListener = callback as OnFinish<ResultInit>;
                    break;
                case Api.EXPAND_FUNCTION:
                    if (headName != null && headName != "" && !expandListeners.ContainsKey(headName))
                    {
                        OnFinish<ResultExpand> expandFunctionListener = callback as OnFinish<ResultExpand>;
                        expandListeners[headName]=expandFunctionListener;
                    }              
                    break;
            }
        }

        public virtual void OnReceive(string head, string body)
        {
            switch (head)
            {
                case INIT_SUCCESS:
                    InitFinish(true, body);
                    break;
                case INIT_FAILED:
                    InitFinish(false, body);
                    break;
                case GET_APP_INFO:
                    bool success = (body != null) && body.Length != 0;
                    GetAppInfoFinish(success, body);
                    break;
                case GET_NOTCH_SCREEN_INFO:
                    success = (body != null) && body.Length != 0;
                    GetNotchInfoFinish(success, body);
                    break;
                case GET_COUNTRY_CODE:
                    GetCountryAndLanguageFinish(true, body);
                    break;
                case GET_MEMORY:
                    success = (body != null) && body.Length != 0;
                    GetMemoryFinish(success, body);
                    break;
                case CURRENT_BATTERY:
                    success = (body != null) && body.Length != 0;
                    GetBatteryFinish(success, body);
                    break;
                case LOGIN_SUCCESS:
                    LoginFinish(true, body, true);
                    break;
                case LOGIN_FAILED:
                    LoginFinish(false, body, true);
                    break;
                case SWITCH_ACCOUNT_SUCCESS:
                    LoginFinish(true, body, false);
                    break;
                case SWITCH_ACCOUNT_FAILED:
                    LoginFinish(false, body, false);
                    break;
                case BIND_SUCCESS:
                    StartBindFinish(true, body);
                    break;
                case BIND_FAILED:
                    StartBindFinish(false, body);
                    break;
                case LOGOUT_SUCCESS:
                    LogoutFinish(true, body);
                    break;
                case LOGOUT_FAILED:
                    LogoutFinish(false, body);
                    break;
                case PAY_SUCCESS:
                    PayFinish(true, body);
                    break;
                case PAY_FAILED:
                    PayFinish(false, body);
                    break;
                case EXIT_SUCCESS:
                    ExitFinish(true, body);
                    break;
                case EXIT_FAILED:
                    ExitFinish(false, body);
                    break;
                case GET_NOTICE_SUCCESS:
                    GetNoticeListFinish(true, body);
                    break;
                case GET_NOTICE_FAILED:
                    GetNoticeListFinish(false, body);
                    break;
                case GET_SERVER_LIST_SUCCESS:
                    GetServerListFinish(true, body);
                    break;
                case GET_SERVER_LIST_FAILED:
                    GetServerListFinish(false, body);
                    break;
                case GET_GOODS_LIST_SUCCESS:
                    GetGoodsListFinish(true, body);
                    break;
                case GET_GOODS_LIST_FAILED:
                    GetGoodsListFinish(false, body);
                    break;
                case GET_NET_STATE_INFO:
                    GetNetStateInfoFinish(true, body);
                    break;
                case DOWNLOAD_TEXT_SUCCESS:
                    DownloadTextFinish(true, body);
                    break;
                case DOWNLOAD_TEXT_FAILED:
                    DownloadTextFinish(false, body);
                    break;
                case GET_HEADSET_STATE_SUCCESS:
                    GetHeadsetStateFinish(true, body);
                    break;
                default:
                    if (expandListeners.ContainsKey(head))
                    {
                        ExpandFunctionFinish(head, body);
                    }
                    break;
            }
        }
        private void GetHeadsetStateFinish(bool success, string body)
        {
            HeadsetStateInfo result = new HeadsetStateInfo();
            result.success = success;
            int headsetState;
            bool parseResult = int.TryParse(body, out headsetState);
            result.headsetState = headsetState;
            getHeadsetStateListener(result);
        }
        private void DownloadTextFinish(bool success, string body)
        {
            ResultDownloadText result = new ResultDownloadText();
            result.success = success;
            if (success)
            {
                result.content = body;
            }
            else
            {
                result.message = body;
            }
            downloadTextListener(result);
        }
        private void GetNetStateInfoFinish(bool success, string body)
        {
            NetStateInfo result = new NetStateInfo();
            JSONNode node = JSONNode.Parse(body);
            result.success = success;
            result.wifi = node["wifi"].AsBool;
            result.networkConnect = node["networkConnect"].AsBool;
            getNetStateInfoListener(result);
        }


        private void GetCountryAndLanguageFinish(bool success, string body)
        {
            Locale locale = new Locale();
            JSONNode node = JSONNode.Parse(body);
            locale.success = success;
            locale.language = node["language"];
            locale.country = node["country"];
            getCountryAndLanguageListener(locale);
        }

        private void ExpandFunctionFinish(string head, string body)
        {
            OnFinish<ResultExpand> listener = expandListeners[head];
            if (listener == null)
                return;
            JSONNode node = JSONNode.Parse(body);
            bool success = node["success"].AsBool;
            ResultExpand result = new ResultExpand();
            result.success = success;
            if (success)
            {
                string originResult = node["originResult"].Value;
                result.originResult = originResult;
            }
            else
            {
                string errorMessage = node["errorMessage"].Value;
                result.message = errorMessage;
            }
            listener(result);
        }

        private void StartBindFinish(bool success, string body)
        {
            ResultBind result = new ResultBind();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.nickName = node["nickName"].Value;
                result.message = node["message"].Value;
            }
            else
            {
                result.message = body;
            }
            startBindListener(result);
        }

        private void GetGoodsListFinish(bool success, string body)
        {
            GoodsList result = new GoodsList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                JSONArray jsonArrayGoods = node["list"].AsArray;
                List<GoodsInfo> goods = result.goods;
                foreach (JSONNode item in jsonArrayGoods.Childs)
                {
                    GoodsInfo goodsInfo = new GoodsInfo();
                    goodsInfo.category = item["category"].Value;
                    goodsInfo.count = item["amount"].AsInt;
                    goodsInfo.currency = item["currency"].Value;
                    goodsInfo.description = item["description"].Value;
                    goodsInfo.endTime = item["end_time"].AsLong;
                    goodsInfo.gift = item["gift"].Value;
                    goodsInfo.goodsId = item["pid"].Value;
                    goodsInfo.goodsName = item["name"].Value;
                    goodsInfo.limitByDay = item["limit_by_day"].AsInt;
                    goodsInfo.price = item["price"].AsDouble;
                    goodsInfo.priceDisplay = item["price_display"].Value;
                    goodsInfo.ratio = item["ratio"].Value;
                    goodsInfo.startTime = item["start_time"].AsLong;
                    goodsInfo.url = item["url"].Value;
                    goodsInfo.tag = item["tag"].Value;
                    goodsInfo.localCurrency = item["localCurrency"].Value;
                    goodsInfo.localPrice = item["localPrice"].AsDouble;
                    goodsInfo.localSymbol = item["localSymbol"].Value;
                    goods.Add(goodsInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getGoodsListListener(result);
        }

        private void GetServerListFinish(bool success, string body)
        {
            ServerList result = new ServerList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.tester = node["tester"].AsInt;
                result.time = node["time"].AsLong;
                JSONArray jsonArrayServers = node["list"].AsArray;
                List<ServerInfo> servers = result.servers;
                foreach (JSONNode item in jsonArrayServers.Childs)
                {
                    ServerInfo serverInfo = new ServerInfo();
                    serverInfo.address = item["address"].Value;
                    serverInfo.closeTime = item["close_time"].AsLong;
                    serverInfo.label = item["label"].Value;
                    serverInfo.openTime = item["open_time"].AsLong;
                    serverInfo.serverId = item["sid"].Value;
                    serverInfo.serverName = item["name"].Value;
                    serverInfo.status = item["status"].AsInt;
                    serverInfo.tag = item["tag"].Value;
                    List<GameRoleInfo> roles = serverInfo.roles;
                    JSONArray jsonArrayRoles = node["role"].AsArray;
                    foreach (JSONNode itemRole in jsonArrayRoles.Childs)
                    {
                        GameRoleInfo roleInfo = new GameRoleInfo();
                        roleInfo.extra = itemRole["profile"].Value;
                        roleInfo.lastUpdate = itemRole["last_update"].AsInt;
                        roleInfo.roleId = itemRole["role"].Value;
                        roleInfo.roleLevel = itemRole["level"].AsInt;
                        roleInfo.roleName = itemRole["name"].Value;
                        roleInfo.roleVip = itemRole["vip"].Value;
                        roleInfo.serverId = itemRole["server"].Value;
                        roleInfo.gender = itemRole["gender"].AsInt;
                        roleInfo.balance = itemRole["balance"].AsInt;
                        roles.Add(roleInfo);
                    }
                    servers.Add(serverInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getServerListListener(result);

        }

        private void GetNoticeListFinish(bool success, string body)
        {
            NoticeList result = new NoticeList();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                JSONArray jsonArrayNotices = node["notice"].AsArray;
                List<NoticeInfo> notices = result.notices;
                foreach (JSONNode item in jsonArrayNotices.Childs)
                {
                    NoticeInfo noticeInfo = new NoticeInfo();
                    noticeInfo.id = item["id"].AsInt;
                    noticeInfo.content = item["content"].Value;
                    noticeInfo.endTime = item["end_time"].AsLong;
                    noticeInfo.image = item["image"].Value;
                    noticeInfo.important = item["important"].AsInt;
                    noticeInfo.link = item["link"].Value;
                    noticeInfo.mode = item["mode"].AsInt;
                    noticeInfo.sort = item["sort"].AsInt;
                    noticeInfo.startTime = item["start_time"].AsLong;
                    noticeInfo.status = item["status"].AsInt;
                    noticeInfo.title = item["title"].Value;
                    noticeInfo.type = item["type"].AsInt;
                    noticeInfo.remainTime = item["remain_time"].AsLong;
                    notices.Add(noticeInfo);
                }
            }
            else
            {
                result.message = body;
            }
            getNoticeListListener(result);
        }

        private void ExitFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            exitListener(result);
        }

        private void PayFinish(bool success, string body)
        {
            ResultPay result = new ResultPay();
            result.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                result.orderId = node["orderId"].Value;
            }
            else
            {
                result.message = body;
            }
            payListener(result);
        }

        private void LogoutFinish(bool success, string body)
        {
            Result result = new Result();
            result.success = success;
            if (!success)
                result.message = body;
            logoutListener(result);
        }

        private void InitFinish(bool success, string body)
        {
            Dictionary<string, string> customParams = new Dictionary<string, string>();
            ResultInit result = new ResultInit(customParams);
            result.success = success;
            if (!success)
                result.message = body;
            else
            {
                JSONNode node = JSONNode.Parse(body);
                foreach (KeyValuePair<string, JSONNode> item in node.AsObject)
                {
                    customParams[item.Key] = item.Value.Value;
                }
            }
            initListener(result);
        }
        private NotchScreenInfo notchInfo = new NotchScreenInfo();

        private void GetNotchInfoFinish(bool success, string body)
        {

            notchInfo.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                notchInfo.width = node["width"].AsInt;
                notchInfo.height = node["height"].AsInt;
            }
            getNotchInfoListener(notchInfo); 
        }


        private void GetAppInfoFinish(bool success, string body)
        {
            AppInfo appInfo = new AppInfo();
            appInfo.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                appInfo.appId = node["appId"].Value;
                appInfo.deviceId = node["deviceId"].Value;
                appInfo.appName = node["appName"].Value;
                appInfo.platform = node["platform"].Value;
                appInfo.version = node["version"].Value;
            }
            else
            {
                appInfo.message = body;
            }
            appInfoListener(appInfo);
        }

        private void GetBatteryFinish(bool success, string body)
        {
            BatteryInfo info = new BatteryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.scale = node["scale"].AsInt;
                info.level = node["level"].AsInt;
                info.temperature = node["temperature"].AsInt;
                info.isCharge = node["isCharge"].AsBool;
            }
            getBatteryInfoListener(info);
        }

        private void GetMemoryFinish(bool success, string body)
        {
            MemoryInfo info = new MemoryInfo();
            info.success = success;
            if (success)
            {
                JSONNode node = JSONNode.Parse(body);
                info.totalMem = node["totalMem"].AsLong;
                info.availMem = node["availMem"].AsLong;
                info.pssmemory = node["pssmemory"].AsLong;
            }
            getMemroyInfoListener(info);
        }

        private void LoginFinish(bool success, string body, bool login)
        {
            Dictionary<string, string> extra = new Dictionary<string, string>();
            UserInfo userInfo = new UserInfo(extra);
            userInfo.success = success;
            if (!success)
                userInfo.message = body;
            else
            {
                JSONNode node = JSONNode.Parse(body);
                JSONClass extraNode = node["extra"].AsObject;
                foreach (KeyValuePair<string, JSONNode> item in extraNode)
                {
                    extra[item.Key] = item.Value.Value;
                }
                userInfo.message = node["message"].Value;
                userInfo.uid = node["uid"].Value;
                userInfo.accessToken = node["accessToken"].Value;
                userInfo.nickName = node["nickname"].Value;
                JSONArray arrayBindStates = node["bindStates"].AsArray;
                //List<BindState> bindStates = userInfo.bindStates;
                foreach (JSONNode item in arrayBindStates.Childs)
                {
                    BindState bindState = new BindState();
                    bindState.bindState = item["bindState"].AsInt;
                    bindState.platform = item["platform"].Value;
                    //bindStates.Add(bindState);
                }
            }
            if (login)
                loginListener(userInfo);
            else
                switchAccountListener(userInfo);
        }
    }


}
