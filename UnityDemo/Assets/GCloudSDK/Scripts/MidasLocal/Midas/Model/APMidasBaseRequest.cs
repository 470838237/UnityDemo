using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MidasPay {
	
	///<summary>
	///Request payment base object
	///</summary>
	///<remarks>
	///
	///</remarks>
	[System.Serializable]
	public class APMidasBaseRequest : JsonSerializable {

        public const string WECHAT = "wechat";
        public const string BANK = "bank";
        public const string QQWALLET = "qqwallet";
        public const string GOOGLEPLAY = "googleplay";  //
        public const string APPLEIAP = "APPLEIAP";  //


        ///<summary>
        ///appId offered by midas。
        ///[required]
        ///</summary>
        [JsonProp("offerId")]
		public string offerId;


		///<summary>
		///Player’s ID。
		/// [required]
		///</summary>
		[JsonProp("openId")]
		public string openId;

		///<summary>
		///Payment token for player。
		/// For Mobile QQ, this value can be retrieved with: LoginRet.getTokenByType(TokenType.eToken_QQ_Pay)
		/// For WeChat, this value can be retrieved with: LoginRet.getTokenByType(TokenType.eToken_WX_Access)
		/// For Guest, this value can be a random value
		/// [required]
		///</summary>
		[JsonProp("openKey")]
		public string openKey;

		///<summary>
		///account zone ID from midas server configrated. If game don’t have multiple zones, this value is by default zoneId ="1".
		/// [required]
		///</summary>
		[JsonProp("zoneId")]
		public string zoneId;


		///<summary>
		/// login Type，"sessionId" and "sessionType" used in pairs.
		/// Mobile QQ：sessionId=“openid”，sessionType=“kp_actoken”
		/// WeChat：sessionId="hy_gameid"，sessionType=“wc_actoken”
		/// Guest：sessionId="hy_gameid"，sessionType=“st_dummy”
		/// [required]
		///</summary>
		[JsonProp("sessionId")]
		public string sessionId;

		///<summary>
		/// login Type，"sessionId" and "sessionType" used in pairs.
		/// Mobile QQ：sessionId=“openid”，sessionType=“kp_actoken”;
		/// WeChat：sessionId="hy_gameid"，sessionType=“wc_actoken”;
		/// Guest：sessionId="hy_gameid"，sessionType=“st_dummy”;
		/// [required]
		///</summary>
		[JsonProp("sessionType")]
		public string sessionType;

		///<summary>
		///Game can retrieve this value with WGPlatform.WGGetPf().
		/// [required]
		///</summary>
		[JsonProp("pf")]
		public string pf;

		///<summary>
		///Game can retrieve this value with MSDKAPI:WGPlatform.WGGetPfKey().
		/// [required]
		///</summary>
		[JsonProp("pfKey")]
		public string pfKey;



        ///<summary>
        ///only for iOS：now only the key "app_extra"
        /// must pass in IDIP's partition as the value for the key "app_extra" when called Initialize().
        /// [required]
        ///</summary>
        [JsonProp("goodsZoneId")]
        public string goodsZoneId;

        ///<summary>
        ///item id for iOS or android SubscribeRequest
        /// [required]
        ///</summary>
        [JsonProp("productId")]
        public string productId;

        ///<summary>
        ///only for iOS：Goods，“productId*price(unit：jiao)*quantity”；Subscribe，“days”；Game，“quantity”
        /// [required]
        ///</summary>
        [JsonProp("payItem")]
        public string payItem;

        ///<summary>
        ///only for iOS：now only the key "app_extra"
        /// must pass in IDIP's partition as the value for the key "app_extra" when called Initialize().
        /// [required]
        ///</summary>
        //[JsonProp("iapInitExtra")]
        //public Dictionary<string, object> iapInitExtra = new Dictionary<string, object>();


        ///<summary>
        /// buy quantity. if the value is null or "", it will show midas mall page.
        /// [optional]
        ///</summary>
        [JsonProp("saveValue")]
        public string saveValue;

        ///<summary>
        /// extend object
        /// [optional]
        ///</summary
        //[JsonProp("extendInfo")]
        //public APMidasExtendInfo extendInfo = new APMidasExtendInfo ();

        ///<summary>
        /// discount object
        /// [optional]
        ///</summary>
        //[JsonProp("mpInfo")]
        //public APMidasMPInfo mpInfo = new APMidasMPInfo ();

  

        ///<summary>
        /// extend field to pass some unnormal params
        /// such as "remark": extend="remark=xxx"
        /// [optional]
        ///</summary>
        [JsonProp("appExtends")]
		public string appExtends; // remark=xxx&a=x&b＝x&drmInfo=xxx


        ///<summary>
        ///一些支付渠道需要的特殊字段，比如roleid，rolename等。
        ///</summary>
        [JsonProp("channelExtras")]
        public string channelExtras;


        ///<summary>
        ///物品列表，garena测拉物品时可选性传入
        ///</summary>
        [JsonProp("productList")]
        public List<string> productList;


        ///<summary>
        ///指定的货币类型。米大师后台会根据货币类型来过滤物品。
        ///</summary>
        [JsonProp("currencyType")]
        public string currencyType;

        ///<summary>
        ///指定的国家。米大师后台会根据国家来过滤支付渠道。
        ///</summary>
        [JsonProp("country")]
        public string country;


        ///<summary>
        ///指定支付渠道。
        ///</summary>
        [JsonProp("payChannel")]
        public string payChannel;

        ///<summary>
        ///account type。
        /// [optional]
        ///</summary>
        //[JsonProp("acctType")]
        private string acctType = "common";

        ///<summary>
        ///quantity of game coin is enable to change in the sdk,only use for midas mall.
        /// [optional]
        ///</summary>
        //[JsonProp("isCanChange")]
        //public bool isCanChange;

        ///<summary>
        ///mall logo resource id,eg:R.id.xxxx
        /// [optional]
        ///</summary>
        //[JsonProp("mallLogo")]
        //public string mallLogo;

        ///<summary>
        ///mall item icon，need base64 encoding.
        /// [optional]
        ///</summary>
        //[JsonProp("resData")]
        //public string resData = "";

        ///<summary>
        ///mall item icon resource id,only for android.eg: R.id.xxxxx
        /// [optional]
        ///</summary>
        //[JsonProp("resId")]
        //public string resId;


        ///<summary>
        ///extras for custom configuration。The format of this field is json。
        //{
        //  "uiconfig":
        //  {
        //     "unit":"ge",
        //     "isShowNum":false,
        //     "isShowListOtherNum":true,
        //     "isCanChange":true,
        //     "extras":"",
        //     "resData":"",
        //     "resId":0,
        //     "mallLogo":0
        //  },
        //  "drmConfig":
        //  {
        //     "discountType":"",
        //     "discountUrl":"",
        //     "discoutId":""
        //     "drmInfo":""
        //  }
        //  "others"
        //  {
        //      "mallType":"",
        //      "h5Url":""
        //  }
        //}
        ///
        /// 
        /// [optional]
        ///</summary>
        [JsonProp("extras")]
            public string extras;

    }
}
