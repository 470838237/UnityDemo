//------------------------------------------------------------------------------
//
// File: MSDKLBS
// Module: MSDK
// Date: 2020-03-31
// Hash: 17e114ea32317f1889ef50443934b8fd
// Author:waylenzhang@tencent.com
//
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GCloud.MSDK
{
    #region MSDKLBSLocationRet
	public class MSDKLBSLocationRet : MSDKBaseRet
    {
#if GCLOUD_MSDK_WINDOWS
#else
		private double longitude;

		private double latitude;

        /// <summary>
		/// 经度
        /// </summary>
		[JsonProp("longitude")]
		public double Longitude
        {
			get { return longitude; }
			set { longitude = value; }
        }

		/// <summary>
		/// 维度
		/// </summary>
		[JsonProp("latitude")]
		public double Latitude
		{
			get { return latitude; }
			set { latitude = value; }
		}

		public MSDKLBSLocationRet()
        {
        }

		public MSDKLBSLocationRet(string param) : base(param)
        {
        }

		public MSDKLBSLocationRet(object json) : base(json)
        {
        }
#endif
    }
    #endregion


	#region MSDKLBSLocationRet

	public class MSDKLBSPersonInfo : MSDKPersonInfo
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private double distance;

		private int isFriend;

		private long timestamp;

		/// <summary>
		/// 距离
		/// </summary>
		[JsonProp("distance")]
		public double Distance
		{
			get { return distance; }
			set { distance = value; }
		}

		/// <summary>
		/// 是否是好友关系
		/// </summary>
		[JsonProp("isFriend")]
		public int IsFriend
		{
			get { return isFriend; }
			set { isFriend = value; }
		}

		/// <summary>
		/// 是否是好友关系
		/// </summary>
		[JsonProp("timestamp")]
		public long TimeStamp
		{
			get { return timestamp; }
			set { timestamp = value; }
		}

		public MSDKLBSPersonInfo()
		{
		}

		public MSDKLBSPersonInfo(string param) : base(param)
		{
		}

		public MSDKLBSPersonInfo(object json) : base(json)
		{
		}
#endif
	}
	#endregion


	#region MSDKLBSRelationRet
	public class MSDKLBSRelationRet : MSDKBaseRet
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private string isLost;

		private List<MSDKLBSPersonInfo> personList;

		/// <summary>
		/// 附近好友列表
		/// </summary>
		/// <value>The friend info list.</value>

		[JsonProp("is_lost")]
		public string IsLost
		{
			get { return isLost; }
			set { isLost = value; }
		}


		[JsonProp("personList")]
		[JsonListProp(typeof(MSDKLBSPersonInfo))]
		public List<MSDKLBSPersonInfo> PersonInfoList
		{
			get { return personList; }
			set { personList = value; }
		}

		public MSDKLBSRelationRet()
		{
		}

		public MSDKLBSRelationRet(string param) : base(param)
		{
		}

		public MSDKLBSRelationRet(object json) : base(json)
		{
		}
#endif
	}
	#endregion

	#region MSDKLBSIPInfoRet
	public class MSDKLBSIPInfoRet : MSDKBaseRet
	{
#if GCLOUD_MSDK_WINDOWS
#else
		private string country;

		private bool isByHeader;

		/// <summary>
		/// 国家
		/// </summary>
		[JsonProp("country")]
		public string Country
		{
			get { return country; }
			set { country = value; }
		}

		/// <summary>
		/// 是否通过Header来判断
		/// </summary>
		[JsonProp("is_query_by_request_header")]
		public bool IsByHeader
		{
			get { return isByHeader; }
			set { isByHeader = value; }
		}

		public MSDKLBSIPInfoRet()
		{
		}

		public MSDKLBSIPInfoRet(string param) : base(param)
		{
		}

		public MSDKLBSIPInfoRet(object json) : base(json)
		{
		}
#endif
	}
	#endregion


    #region MSDKLBS

    public class MSDKLBS
    {
#if GCLOUD_MSDK_WINDOWS
#else
        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void getLocationAdapter();

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void getNearbyAdapter();

        [DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void clearLocationAdapter();

		[DllImport(MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void getIPInfoAdapter();

		/// <summary>
		/// 设置Location回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKLBSLocationRet> LBSLocationRetEvent;

		/// <summary>
		/// 获取附近的人列表回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKLBSRelationRet> LBSRelationRetEvent;

		/// <summary>
		/// 获取IP信息列表
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKLBSIPInfoRet> LBSIPInfoRetEvent;

		/// <summary>
		/// 清除位置信息回调
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKBaseRet> LBSRetEvent;


        /// <summary>
        /// 获取本地GPS信息并设置到服务端
        /// </summary>
		public static void GetLocation()
        {
            try
			{
				MSDKLog.Log ("GetLocation");
#if UNITY_EDITOR

#else
				getLocationAdapter();
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("getLocationAdapter with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 清除GPS信息数据
		/// </summary>
		public static void ClearLocation( )
        {
            try
			{
				MSDKLog.Log ("ClearLocation ");
#if UNITY_EDITOR

#else
			 	clearLocationAdapter();
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("ClearLocation with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
				
        }

		/// <summary>
		/// 获取附近人信息列表
		/// </summary>
		public static void GetNearby( )
        {
            try
			{
				MSDKLog.Log ("GetNearby ");
#if UNITY_EDITOR

#else

				getNearbyAdapter();
#endif
            }
            catch (Exception ex)
            {
				MSDKLog.LogError("GetNearby with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

		/// <summary>
		/// 通过IP信息获取当前国家
		/// </summary>
		public static void GetIPInfo( )
		{
			try
			{
				MSDKLog.Log ("GetIPInfo jsonJsParam=");
#if UNITY_EDITOR

#else

				getIPInfoAdapter();
#endif
			}
			catch (Exception ex)
			{
				MSDKLog.LogError("GetIPInfo with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}



        ///-------callback-----------
		internal static void OnLBSLocationRet(string json)
		{
			MSDKLog.Log ("OnLBSLocationRet  json= " + json);
			if (LBSLocationRetEvent != null)
			{
				var ret = new MSDKLBSLocationRet(json);
				try {
					LBSLocationRetEvent(ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			} else{
				MSDKLog.LogError ("No callback for OnLBSLocationRet !");
			}
		
		}

		internal static void OnLBSRelationRet(string json)
		{
			MSDKLog.Log ("OnLBSRelationRet  json= " + json);
			if (LBSRelationRetEvent != null)
			{
				var ret = new MSDKLBSRelationRet(json);
				try {
					LBSRelationRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			} else{
				MSDKLog.LogError ("No callback for OnLBSRelationRet !");
			}
		}

		internal static void OnLBSIPInfoRet(string json)
		{
			MSDKLog.Log ("OnLBSIPInfoRet  json= " + json);
			if (LBSIPInfoRetEvent != null)
			{
				var ret = new MSDKLBSIPInfoRet(json);
				try {
					LBSIPInfoRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			} else{
				MSDKLog.LogError ("No callback for OnLBSIPInfoRet !");
			}
		}

		internal static void OnLBSBaseRet(string json)
		{
			MSDKLog.Log ("OnLBSBaseRet  json= " + json);
			if (LBSRetEvent != null)
			{
				var ret = new MSDKBaseRet (json);
				try {
					LBSRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			} else{
				MSDKLog.LogError ("No callback for OnLBSBaseRet !");
			}
        }
#endif
    }
    #endregion
}