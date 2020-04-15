//------------------------------------------------------------------------------
//
// File: MSDKFriend
// Module: MSDK
// Date: 2020-03-31
// Hash: dd6c0be2fe44dbc63292f6cb3da605c8
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GCloud.MSDK
{
    public class MSDKFriendReqInfo : JsonSerializable
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private int type;

        private string user;

        private string title;

        private string desc;

        private string imagePath;

        private string thumbPath;

        private string mediaPath;

        private string link;

        private string extraJson;

		/// <summary>
		/// 必选，类型：后台静默或者是拉起应用
		/// </summary>
		/// <value>The type.</value>
        [JsonProp("type")]
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

		/// <summary>
		/// 用户，可以是 id 或者是 openid，比如静默分享时需要指定分享给指定的用户
		/// </summary>
		/// <value>The user.</value>
        [JsonProp("user")]
        public string User
        {
            get { return user; }
            set { user = value; }
        }

		/// <summary>
		/// 必填，标题
		/// </summary>
		/// <value>The title.</value>
        [JsonProp("title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

		/// <summary>
		/// 选填，概述，简单描述分享目的
		/// </summary>
		/// <value>The desc.</value>
        [JsonProp("desc")]
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

		/// <summary>
		/// 选填，图片， 可以是本地地址或者是URL，建议使用本地地址
		/// </summary>
		/// <value>The image path.</value>
        [JsonProp("imagePath")]
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
		/// <summary>
		/// 选填，缩略图，一般就是游戏的 icon，可以是 icon 的本地地址或者是icon URL，建议使用本地地址
		/// </summary>
		/// <value>The thumb path.</value>
        [JsonProp("thumbPath")]
        public string ThumbPath
        {
            get { return thumbPath; }
            set { thumbPath = value; }
        }

		/// <summary>
		/// 选填，多媒体地址（音乐或者视频源），本地地址，只支持本地地址
		/// </summary>
		/// <value>The media path.</value>
        [JsonProp("mediaPath")]
        public string MediaPath
        {
            get { return mediaPath; }
            set { mediaPath = value; }
        }

		/// <summary>
		/// 选填，分享链接，可以是图片链接、音乐链接、视频链接或者跳转链接等等
		/// </summary>
		/// <value>The link.</value>
        [JsonProp("link")]
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

		/// <summary>
		/// 选填，扩展字段，一般不需要填
		/// </summary>
		/// <value>The extra json.</value>
        [JsonProp("extraJson")]
        public string ExtraJson
        {
            get { return extraJson; }
            set { extraJson = value; }
        }

        public MSDKFriendReqInfo()
        {
        }

        public MSDKFriendReqInfo(string param) : base(param)
        {
        }

        public MSDKFriendReqInfo(object json) : base(json)
        {
        }
#endif
    }

    public class MSDKFriendRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private List<MSDKPersonInfo> friendInfoList;

		/// <summary>
		/// 好友列表
		/// </summary>
		/// <value>The friend info list.</value>
        [JsonProp("lists")]
        [JsonListProp(typeof(MSDKPersonInfo))]
        public List<MSDKPersonInfo> FriendInfoList
        {
            get { return friendInfoList; }
            set { friendInfoList = value; }
        }

        public MSDKFriendRet()
        {
        }

        public MSDKFriendRet(string param) : base(param)
        {
        }

        public MSDKFriendRet(object json) : base(json)
        {
        }
#endif
    }

    public class MSDKPersonInfo : JsonSerializable
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private string openid;

        private string userName;

        private int gender;

        private string pictureUrl;

        private string country;

        private string province;

        private string city;

        private string language;

		/// <summary>
		/// 用户标识
		/// </summary>
		/// <value>The openid.</value>
        [JsonProp("openid")]
        public string Openid
        {
            get { return openid; }
            set { openid = value; }
        }

		/// <summary>
		/// 昵称
		/// </summary>
		/// <value>The name of the user.</value>
        [JsonProp("user_name")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

		/// <summary>
		///  性别 0未知1男2女,用户未填则默认0
		/// </summary>
		/// <value>The gender.</value>
        [JsonProp("gender")]
        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }
		/// <summary>
		/// 头像链接
		/// </summary>
		/// <value>The picture URL.</value>
        [JsonProp("picture_url")]
        public string PictureUrl
        {
            get { return pictureUrl; }
            set { pictureUrl = value; }
        }

		/// <summary>
		/// 国家
		/// </summary>
		/// <value>The country.</value>
        [JsonProp("country")]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

		/// <summary>
		/// 省份
		/// </summary>
		/// <value>The province.</value>
        [JsonProp("province")]
        public string Province
        {
            get { return province; }
            set { province = value; }
        }

		/// <summary>
		/// 城市
		/// </summary>
		/// <value>The city.</value>
        [JsonProp("city")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

		/// <summary>
		/// 语言
		/// </summary>
		/// <value>The language.</value>
        [JsonProp("language")]
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        public MSDKPersonInfo()
        {
        }

        public MSDKPersonInfo(string param) : base(param)
        {
        }

        public MSDKPersonInfo(object json) : base(json)
        {
        }
#endif
    }

	/// <summary>
	/// 分享类型枚举
	/// </summary>
    public enum FriendReqType
    {
        Friend_REQ_TEXT = 10000, 			//文字分享
        Friend_REQ_LINK, 					//链接分享
        Friend_REQ_IMG, 					//图片分享
        Friend_REQ_INVITE, 					//应用邀请
        Friend_REQ_MUSIC, 					//音乐分享
        Friend_REQ_VIDEO, 					//视频分享
        Friend_REQ_MINI_APP, 				//小程序分享
        FRIEND_REQ_PULL_UP_MINI_APP,        //拉起小程序
        Friend_REQ_ARK,                     //QQ ark分享
        Friend_REQ_OPEN_BUSINESS_VIEW,      //业务功能拉起
        Friend_REQ_WX_GAMELINE,             //微信游戏圈分享

        Friend_REQ_TEXT_SILENT = 20000, 	//文字分享（静默）
        Friend_REQ_LINK_SILENT, 			//链接分享 (静默)
        Friend_REQ_IMG_SILENT, 				//图片分享 （静默）
        Friend_REQ_INVITE_SILENT, 			//应用邀请 (静默）
        Friend_REQ_MUSIC_SILENT, 			//音乐分享 (静默)
        Friend_REQ_VIDEO_SILENT, 			//视频分享 (静默)
        Friend_REQ_MINI_APP_SILENT, 		//小程序分享 (静默)
    }

    #region Friend

    public class MSDKFriend
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName)]
        private static extern void sendMessageAdapter([MarshalAs(UnmanagedType.LPStr)] string infoJson,
            [MarshalAs(UnmanagedType.LPStr)] string channel);

        [DllImport(MSDK.LibName)]
        static extern void shareAdapter([MarshalAs(UnmanagedType.LPStr)] string infoJson,
            [MarshalAs(UnmanagedType.LPStr)] string channel);

        [DllImport(MSDK.LibName)]
		private static extern void queryFriendsAdapter(int page, int count, bool isInGame,
         	[MarshalAs (UnmanagedType.LPStr)] string channel,
	         [MarshalAs(UnmanagedType.LPStr)] string subChannel,
	         [MarshalAs(UnmanagedType.LPStr)] string extraJson);

        [DllImport (MSDK.LibName)]
		private static extern void addFriendAdapter ([MarshalAs (UnmanagedType.LPStr)] string infoJson,
			[MarshalAs (UnmanagedType.LPStr)] string channel);


		/// <summary>
		/// 基本结果的回调
		/// MSDKMethodNameID.MSDK_FRIEND_SHARE
		///	MSDKMethodNameID.MSDK_FRIEND_SEND_MESSAGE
		///	MSDKMethodNameID.MSDK_FRIEND_ADD_FRIEND
		///	MSDKMethodNameID.MSDK_FRIEND_SEND_TO_GAME_CENTER
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKBaseRet> FriendRetEvent;

		/// <summary>
		/// 查找好友的回调
		/// MSDKMethodNameID.MSDK_FRIEND_QUERY_FRIEND
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKFriendRet> QuereyFriendEvent;

        /// <summary>
        /// 发送消息给好友
        /// </summary>
		/// <param name="info">消息结构体，包含需要分享的内容</param>
        /// <param name="channel">发送消息的渠道，比如 weChat、QQ</param>
		public static void SendMessage(MSDKFriendReqInfo info, string channel = "")
        {
            try
            {
                var infoJson = info.ToString();
				MSDKLog.Log("SendMessage infoJson=" + infoJson + " channel="+channel);
#if UNITY_EDITOR

#else
				sendMessageAdapter(infoJson, channel);
#endif
                
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("SendMessage with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 发送消息到朋友圈或者空间等
        /// </summary>
		/// <param name="info">消息结构体，包含需要分享的内容</param>
        /// <param name="channel">渠道，比如 weChat、QQ</param>
		public static void Share(MSDKFriendReqInfo info, string channel = "")
        {
            try
            {
				var infoJson = info.ToString();
				MSDKLog.Log ("Share infoJson=" + infoJson + " channel=" + channel );     
#if UNITY_EDITOR

#else
				shareAdapter(infoJson, channel);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("Share with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 添加好友，目前只有QQ支持该功能
        /// </summary>
		/// <param name="info">消息结构体，包含需要添加的好友的个人信息</param>
        /// <param name="channel">渠道，比如QQ</param>
		public static void AddFriend(MSDKFriendReqInfo info, string channel = "")
        {
            try
            {
				var infoJson = info.ToString();
				MSDKLog.Log ("AddFriend infoJson=" + infoJson + " channel=" + channel );
#if UNITY_EDITOR

#else
				addFriendAdapter(infoJson, channel);
#endif
			}
            catch (Exception ex)
            {
				MSDKLog.LogError("AddFriend with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="page"> 需要获取多少页好友</param>
        /// <param name="count"> 每一页好友的个数 </param>
		/// <param name="isInGame">标识获取的好友是一起玩游戏的好友，还是说普通的好友 </param>
        /// <param name="channel">获取好友的渠道，比如 weChat 、QQ</param>
		/// <param name="subChannel">多渠道时使用，比如 Garena 、乐逗等</param>
        /// <param name="extraJson">扩展字段</param>
		public static void QueryFriends(int page=0, int count=0, bool isInGame=true, string channel="",string subChannel = "", string extraJson = "")
        {
            try
			{
				MSDKLog.Log ("QueryFriends page=" + page + " count=" + count+ " isInGame=" + isInGame + " channel=" + channel + " subChannel=" + subChannel+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				queryFriendsAdapter(page, count, isInGame, channel, subChannel,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("QueryFriends with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        ///-------callback-----------
        internal static void OnFriendMessage(string json)
		{
			MSDKLog.Log ("OnFriendMessage json= " + json);
			if (FriendRetEvent != null)
			{
				var ret = new MSDKBaseRet (json);
				try{
					FriendRetEvent (ret);
				}
				catch(Exception e)
				{
					MSDKLog.LogError (e.StackTrace);
				}
            } else
			{
				MSDKLog.LogError ("No callback for FriendRetEvent !");
			}
        }

        internal static void OnFriendQueryFriend(string json)
		{
			MSDKLog.Log ("OnFriendQueryFriend json= " + json);
			if (QuereyFriendEvent != null)
			{
				var ret = new MSDKFriendRet (json);
				try {
					QuereyFriendEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
            } else
			{
				MSDKLog.LogError ("No callback for QuereyFriendEvent !");
			}
        }
#endif
    }

    #endregion

}