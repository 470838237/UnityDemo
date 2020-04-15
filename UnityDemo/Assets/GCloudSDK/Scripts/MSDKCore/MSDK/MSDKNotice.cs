//------------------------------------------------------------------------------
//
// File: MSDKNotice
// Module: MSDK
// Date: 2020-03-31
// Hash: 62eee2db99f05a5c405e31497e3a9dc2
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GCloud.MSDK
{

	public class MSDKNoticePictureInfo : JsonSerializable
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private string noticePicUrl;

		private string noticePicHashcode;

		private string noticePicTitle;

		private string noticePicSize;

		private int screenDir; //0-未知 1-竖向 2-横向

		[JsonProp ("pic_url")]
		public string NoticePicUrl {
			get { return noticePicUrl; }
			set { noticePicUrl = value; }
		}

		[JsonProp ("pic_hash")]
		public string NoticePicHashcode {
			get { return noticePicHashcode; }
			set { noticePicHashcode = value; }
		}

		[JsonProp ("pic_title")]
		public string NoticePicTitle {
			get { return noticePicTitle; }
			set { noticePicTitle = value; }
		}

		[JsonProp ("pic_size")]
		public string NoticePicSize {
			get { return noticePicSize; }
			set { noticePicSize = value; }
		}

		[JsonProp ("pic_dir")]
		public int ScreenDir {
			get { return screenDir; }
			set { screenDir = value; }
		}

		public MSDKNoticePictureInfo ()
		{
		}

		public MSDKNoticePictureInfo (string param) : base (param)
		{
		}

		public MSDKNoticePictureInfo (object json) : base (json)
		{
		}
#endif
	}

	public class MSDKNoticeTextInfo : JsonSerializable
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private string noticeTitle;

		private string noticeContent;

		private string noticeRedirectUrl;

		[JsonProp ("notice_title")]
		public string NoticeTitle {
			get { return noticeTitle; }
			set { noticeTitle = value; }
		}

		[JsonProp ("notice_content")]
		public string NoticeContent {
			get { return noticeContent; }
			set { noticeContent = value; }
		}

		[JsonProp ("notice_redirect_url")]
		public string NoticeRedirectUrl {
			get { return noticeRedirectUrl; }
			set { noticeRedirectUrl = value; }
		}

		public MSDKNoticeTextInfo ()
		{
		}

		public MSDKNoticeTextInfo (string param) : base (param)
		{
		}

		public MSDKNoticeTextInfo (object json) : base (json)
		{
		}
#endif
	}

	public class MSDKNoticeInfo : JsonSerializable
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private int noticeID;

		//公告类型，1000~1999表示登录前公告;2000~2999表示登录后公告
		private int noticeType;

		//公告分组，游戏自定义使用
		private string noticeGroup;

		private int beginTime;

		private int endTime;

		private int updateTime;

		private int order;

		//1-文本，2-图片，3-网页
		private int contentType;

		//语言类型
		private string language;

		private MSDKNoticeTextInfo textInfo;

		private List<MSDKNoticePictureInfo> picUrlList;

		private string webUrl; //网页公告链接

		private string extraJson;

		[JsonProp ("notice_id")]
		public int NoticeId {
			get { return noticeID; }
			set { noticeID = value; }
		}

		[JsonProp ("notice_type")]
		public int NoticeType {
			get { return noticeType; }
			set { noticeType = value; }
		}

		[JsonProp ("notice_group")]
		public string NoticeGroup {
			get { return noticeGroup; }
			set { noticeGroup = value; }
		}

		[JsonProp ("start_time")]
		public int BeginTime {
			get { return beginTime; }
			set { beginTime = value; }
		}

		[JsonProp ("end_time")]
		public int EndTime {
			get { return endTime; }
			set { endTime = value; }
		}

		[JsonProp ("update_time")]
		public int UpdateTime {
			get { return updateTime; }
			set { updateTime = value; }
		}

		[JsonProp ("order")]
		public int Order {
			get { return order; }
			set { order = value; }
		}

		[JsonProp ("content_type")]
		public int ContentType {
			get { return contentType; }
			set { contentType = value; }
		}

		[JsonProp ("language")]
		public string Language {
			get { return language; }
			set { language = value; }
		}

		[JsonProp ("notice_text_obj")]
		public MSDKNoticeTextInfo TextInfo {
			get { return textInfo; }
			set { textInfo = value; }
		}

		[JsonProp ("notice_pic_list")]
		[JsonListProp (typeof (MSDKNoticePictureInfo))]
		public List<MSDKNoticePictureInfo> PicUrlList {
			get { return picUrlList; }
			set { picUrlList = value; }
		}

		[JsonProp ("notice_web_url")]
		public string WebUrl {
			get { return webUrl; }
			set { webUrl = value; }
		}

		[JsonProp ("extra_data")]
		public string ExtraJson {
			get { return extraJson; }
			set { extraJson = value; }
		}

		public MSDKNoticeInfo ()
		{
		}

		public MSDKNoticeInfo (string param) : base (param)
		{
		}

		public MSDKNoticeInfo (object json) : base (json)
		{
		}
#endif
	}

	public class MSDKNoticeRet : MSDKBaseRet
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private List<MSDKNoticeInfo> noticeInfoList;
        private string reqID;

		[JsonProp ("notice_list")]
		[JsonListProp (typeof (MSDKNoticeInfo))]
		public List<MSDKNoticeInfo> NoticeInfoList {
			get { return noticeInfoList; }
			set { noticeInfoList = value; }
		}

        [JsonProp ("reqID")]
        public string ReqID
        {
            get { return reqID; }
            set { reqID = value; }
        }

		public MSDKNoticeRet ()
		{
		}

		public MSDKNoticeRet (string param) : base (param)
		{
		}

		public MSDKNoticeRet (object json) : base (json)
		{
		}
#endif
	}

	public class MSDKNotice
	{
#if GCLOUD_MSDK_WINDOWS
#else
		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern string loadNoticeDataAdapter ([MarshalAs (UnmanagedType.LPStr)] string noticeGroup,
			[MarshalAs (UnmanagedType.LPStr)] string language,
			int region,
			[MarshalAs (UnmanagedType.LPStr)] string partition,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		/// <summary>
		/// Notice的回调接口
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKNoticeRet>  NoticeRetEvent;

		/// <summary>
		/// 加载公告信息
		/// </summary>
		/// <param name="noticeGroup">公告分组，后台填好公告并且分好组就可以对应拉取公告信息</param>
		/// <param name="language">语种</param>
		/// <param name="region">地区，北美、亚太、南美等等</param>
		/// <param name="partition">游戏大区</param>
		/// <param name="extraJson">扩展字段</param>
		public static string LoadNoticeData (string noticeGroup, string language, int region, string partition,
			string extraJson="")
		{
			try {
				MSDKLog.Log ("LoadNoticeData noticeGroup=" + noticeGroup + " language="+language+ " region=" + region+ " partition=" + partition+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				return loadNoticeDataAdapter (noticeGroup, language, region, partition, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("LoadNoticeData with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}

            return null;
		}


		///-------callback-----------
        internal static void OnNoticeRet (string json)
		{
			MSDKLog.Log ("OnNoticeRet  json= " + json);
			if (NoticeRetEvent != null) {
				var ret = new MSDKNoticeRet (json);
				try {
					NoticeRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for NoticeRetEvent !");
			}
		}
#endif
	}
}