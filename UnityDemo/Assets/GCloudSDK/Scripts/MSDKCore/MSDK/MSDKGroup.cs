//------------------------------------------------------------------------------
//
// File: MSDKGroup
// Module: MSDK
// Date: 2020-03-20
// Hash: 625428c03b1c9d46f2a9840542ca4482
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GCloud.MSDK
{
    public class MSDKGroupInfo : JsonSerializable
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private string groupID;

        private string groupName;

		private string extraJson;

        /// <summary>
		/// 必填，群 id
        /// </summary>
        /// <value>The group identifier.</value>
        [JsonProp("gc")]
        public string GroupId
        {
            get { return groupID; }
            set { groupID = value; }
        }

        /// <summary>
		/// 必填，群名称
        /// </summary>
        /// <value>The name of the group.</value>
        [JsonProp("group_name")]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        /// <summary>
		/// 选填，扩展字段
        /// </summary>
        /// <value>The extra.</value>
		[JsonProp("extraJson")]
        public string ExtraJson
        {
			get { return extraJson; }
			set { extraJson = value; }
        }

        public MSDKGroupInfo()
        {
        }

        public MSDKGroupInfo(string param) : base(param)
        {
        }

        public MSDKGroupInfo(object json) : base(json)
        {
        }
#endif
    }

	public class MSDKGroupMessage : JsonSerializable
	{
#if GCLOUD_MSDK_WINDOWS
#else
	    private int type;

		private int actionReport;

		private string title;

		private string desc;

		private string link;

		private string extraJson;

		/// <summary>
		/// 【必填】消息类型，1：应用邀请 2：link链接分享
		/// </summary>
		/// <value>The type.</value>
		[JsonProp("type")]
		public int Type
		{
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// 【必填】分享类型，邀请1，炫耀2，赠送3，索要4
		/// </summary>
		/// <value>The action report.</value>
		[JsonProp ("actionReport")]
		public int ActionReport {
			get { return actionReport; }
			set { actionReport = value; }
		}
		/// <summary>
		/// 【必填】，标题
		/// </summary>
		/// <value>The title.</value>
		[JsonProp ("title")]
		public string Title {
			get { return title; }
			set { title = value; }
		}
		/// <summary>
		/// 【必填】，概述，消息的介绍
		/// </summary>
		/// <value>The desc.</value>
		[JsonProp ("desc")]
		public string Desc {
			get { return desc; }
			set { desc = value; }
		}
		/// <summary>
		/// 【选填】，链接地址，type等于1时不需要带；type等于2时必须要带
		/// </summary>
		/// <value>The link.</value>
		[JsonProp ("link")]
		public string Link {
			get { return link; }
			set { link = value; }
		}
		/// <summary>
		/// 【选填】，扩展字段，默认为空
		/// </summary>
		/// <value>The extra json.</value>
		[JsonProp ("extraJson")]
		public string ExtraJson {
			get { return extraJson; }
			set { extraJson = value; }
		}
		public MSDKGroupMessage ()
		{
		}

		public MSDKGroupMessage (string param) : base(param)
        {
		}

		public MSDKGroupMessage (object json) : base(json)
        {
		}
#endif
	};

    public class MSDKGroupRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
        private int status;

        private string groupID;

        private string groupName;

        private string groupOpenID;

        private List<MSDKGroupInfo> groupInfo;

		/// <summary>
		///  1、查询群用户和群的关系
        ///  a、Wechat 时 0：未加群，1：已加群
		///  b、QQ 时 1:群主，2:管理员，3:普通成员，4:非成员 ,-1 查询错误
	    ///  2、查询工会是否绑定微信的群 0：未绑定、1：绑定了
		/// </summary>
		/// <value>The status.</value>
        [JsonProp("status")]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
		/// 工会ID
        /// </summary>
        /// <value>The group identifier.</value>
        [JsonProp("groupID")]
        public string GroupId
        {
            get { return groupID; }
            set { groupID = value; }
        }

        /// <summary>
		/// 工会名称
        /// </summary>
        /// <value>The name of the group.</value>
        [JsonProp("groupName")]
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        /// <summary>
		/// 拉取群信息手Q需要用的，用于发送群消息
        /// </summary>
        /// <value>The openID of the group.</value>
        [JsonProp("groupOpenID")]
        public string GroupOpenID
        {
            get { return groupOpenID; }
            set { groupOpenID = value; }
        }

        /// <summary>
		/// 工会信息列表
        /// </summary>
        /// <value>The group info.</value>
        [JsonProp("group_list")]
        [JsonListProp(typeof(MSDKGroupInfo))]
        public List<MSDKGroupInfo> GroupInfo
        {
            get { return groupInfo; }
            set { groupInfo = value; }
        }

        public MSDKGroupRet()
        {
        }

        public MSDKGroupRet(string param) : base(param)
        {
        }

        public MSDKGroupRet(object json) : base(json)
        {
        }
#endif
    }

    #region Group

    public class MSDKGroup
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void createGroupAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                                [MarshalAs(UnmanagedType.LPStr)] string unionName,
		                                                [MarshalAs (UnmanagedType.LPStr)] string roleName,
		                                                [MarshalAs (UnmanagedType.LPStr)] string zoneID,
		                                                [MarshalAs (UnmanagedType.LPStr)] string roleID,
		                                                [MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void bindGroupAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                              [MarshalAs(UnmanagedType.LPStr)] string zoneID,
		                                              [MarshalAs(UnmanagedType.LPStr)] string roleID,
		                                              [MarshalAs (UnmanagedType.LPStr)] string groupID,
		                                              [MarshalAs (UnmanagedType.LPStr)] string groupName,
		                                              [MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void unbindGroupAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                                [MarshalAs(UnmanagedType.LPStr)] string unionName,
		                                                [MarshalAs (UnmanagedType.LPStr)] string zoneID,
		                                                [MarshalAs (UnmanagedType.LPStr)] string roleID,
		                                                [MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void remindToBindGroupAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                                      [MarshalAs (UnmanagedType.LPStr)] string zoneID,
		                                                      [MarshalAs (UnmanagedType.LPStr)] string roleID,
		                                                      [MarshalAs (UnmanagedType.LPStr)] string roleName,
		                                                      [MarshalAs (UnmanagedType.LPStr)] string leaderOpenID,
		                                                      [MarshalAs (UnmanagedType.LPStr)] string leaderRoleID,
														[MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void getGroupListAdapter();


        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void getGroupStateAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                                  [MarshalAs(UnmanagedType.LPStr)] string zoneID,
		                                                  [MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void getGroupRelationAdapter([MarshalAs(UnmanagedType.LPStr)] string targetID,
		                                                     [MarshalAs(UnmanagedType.LPStr)] string extraJson);


        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void joinGroupAdapter([MarshalAs(UnmanagedType.LPStr)] string unionID,
		                                              [MarshalAs(UnmanagedType.LPStr)] string zoneID,
		                                              [MarshalAs(UnmanagedType.LPStr)] string roleID,
		                                              [MarshalAs (UnmanagedType.LPStr)] string groupID,
		                                              [MarshalAs (UnmanagedType.LPStr)] string extraJson);

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void sendGroupMessageAdapter([MarshalAs(UnmanagedType.LPStr)] string groupMessageJson,
		                                                     [MarshalAs(UnmanagedType.LPStr)] string unionID);

		/// <summary>
		/// 群的回调接口
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKGroupRet> GroupRetEvent;

		/// <summary>
		/// 创建群
		/// </summary>
		/// <param name="unionID">工会 ID</param>
		/// <param name="unionName">工会名称</param>
		/// <param name="roleName">游戏中的角色名称</param>
		/// <param name="zoneID">游戏区服 ID</param>
		/// <param name="roleID">游戏角色 ID</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
		public static void CreateGroup(string unionID, string unionName = "", string roleName = "", string zoneID = "", string roleID = "", string extraJson = "")
        {
            try
            {
				MSDKLog.Log("CreateGroup  unionID=" + unionID + " unionName=" + unionName+ " roleName=" + roleName+ " zoneID=" + zoneID+ " roleID=" + roleID+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				createGroupAdapter(unionID, unionName,roleName,zoneID,roleID,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("CreateGroup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 会长绑定已经存在的群。【qq特有】
		/// </summary>
		/// <param name="unionID">工会 ID</param>
		/// <param name="zoneID">游戏区服 ID</param>
		/// <param name="roleID">游戏角色 ID</param>
		/// <param name="groupID">手Q 群号，创建群时获得</param>
		/// <param name="groupName">手Q 群名，创建群时获得</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
		public static void BindGroup(string unionID, string zoneID, string roleID, string groupID, string groupName, string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("BindGroup  unionID=" + unionID + " zoneID=" + zoneID+ " roleID=" + roleID+ " groupID=" + groupID+ " groupName=" + groupName+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				bindGroupAdapter(unionID, zoneID, roleID,groupID,groupName,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("BindGroup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 解绑群
		/// </summary>
		/// <param name="unionID">工会 ID，微信只需要该参数</param>
		/// <param name="unionName">工会名称</param>
		/// <param name="zoneID">游戏区服 ID</param>
		/// <param name="roleID">游戏角色 ID</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
		public static void UnbindGroup(string unionID, string unionName = "", string zoneID = "", string roleID = "", string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("UnbindGroup  unionID=" + unionID + " unionName=" + unionName+ " zoneID=" + zoneID+ " roleID=" + roleID+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				unbindGroupAdapter(unionID, unionName,zoneID,roleID,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("UnbindGroup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 提醒会长建群【qq特有】
		/// </summary>
		/// <param name="unionID">工会 ID</param>
		/// <param name="zoneID">游戏区服 ID</param>
		/// <param name="roleID">游戏角色 ID</param>
		/// <param name="roleName">游戏角色名称</param>
		/// <param name="leaderOpenID">会长 openID</param>
		/// <param name="leaderRoleID">会长角色 ID</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
		public static void RemindToBindGroup(string unionID, string zoneID, string roleID, string roleName, string leaderOpenID, string leaderRoleID, string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("RemindToBindGroup  unionID=" + unionID + " zoneID=" + zoneID+ " roleID=" + roleID+ " roleName=" + roleName+ " leaderOpenID=" + leaderOpenID+ " leaderRoleID=" + leaderRoleID+ " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				remindToBindGroupAdapter(unionID, zoneID,roleID,roleName,leaderOpenID,leaderRoleID,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("RemindToBindGroup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
		/// <summary>
		/// 获取会长的群列表【qq特有】
		/// </summary>
        public static void GetGroupList()
        {
            try
			{
				MSDKLog.Log ("GetGroupList" );
#if UNITY_EDITOR

#else
				getGroupListAdapter();
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("GetGroupList with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 获取群状态
		/// </summary>
		/// <param name="unionID">工会 ID，微信只需要该参数</param>
		/// <param name="zoneID">游戏中的区服 ID</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
		public static void GetGroupState(string unionID, string zoneID = "", string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("GetGroupState  unionID=" + unionID+ " zoneID=" + zoneID + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				getGroupStateAdapter(unionID, zoneID,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("GetGroupState with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 获取与群的关系
		/// </summary>
		/// <param name="targetID">微信填写 工会ID; 手Q 填写群号</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
     	public static void GetGroupRelation(string targetID, string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("GetGroupRelation  targetID=" + targetID + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				getGroupRelationAdapter(targetID, extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("GetGroupRelation with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 加入群
		/// </summary>
		/// <param name="unionID">工会 ID</param>
		/// <param name="zoneID">游戏区服 ID</param>
		/// <param name="roleID">游戏角色 ID</param>
		/// <param name="groupID">手Q群 ID，手Q使用</param>
		/// <param name="extraJson">扩展字段，默认为空</param>
     	public static void JoinGroup(string unionID, string zoneID = "", string roleID = "", string groupID = "", string extraJson = "")
        {
            try
            {
				MSDKLog.Log ("JoinGroup  unionID=" + unionID + " zoneID=" + zoneID + " roleID=" + roleID + " groupID=" + groupID + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				joinGroupAdapter(unionID, zoneID, roleID,groupID,extraJson);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("JoinGroup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 发送群消息【微信特有】
		/// </summary>
		/// <param name="groupMessage">群消息</param>
		/// <param name="unionID">工会 ID</param>
        public static void SendGroupMessage(MSDKGroupMessage groupMessage, string unionID)
        {
            try
            {
				var groupMessageJson = groupMessage.ToString();
				MSDKLog.Log ("SendGroupMessage  groupMessageJson=" + groupMessageJson + " unionID=" + unionID);
#if UNITY_EDITOR

#else
				sendGroupMessageAdapter(groupMessageJson, unionID);
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("SendGroupMessage with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }


        ///-------callback-----------
        internal static void OnGroupRet(string json)
		{
			MSDKLog.Log ("OnGroupRet json= " + json);
			if (GroupRetEvent != null) {
				var ret = new MSDKGroupRet (json);
				try {
					GroupRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}
			else
			{
				MSDKLog.LogError ("No callback for GroupRet !");
			}
        }
#endif
    }
    #endregion
}