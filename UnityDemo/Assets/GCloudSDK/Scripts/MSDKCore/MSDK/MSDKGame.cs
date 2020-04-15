//------------------------------------------------------------------------------
//
// File: MSDKGame
// Module: MSDK
// Date: 2020-03-31
// Hash: f85b8dae857b6463a736e51517f8bc5d
// Author: mingyiwang@tencent.com
//
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GCloud.MSDK
{
#if GCLOUD_MSDK_WINDOWS

#else
	public class MSDKGame
	{
		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void setupAdapter ([MarshalAs (UnmanagedType.LPStr)] string channel,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void showAchievementAdapter ([MarshalAs (UnmanagedType.LPStr)] string channel,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void showLeaderBoardAdapter ([MarshalAs (UnmanagedType.LPStr)] string board,
			[MarshalAs (UnmanagedType.LPStr)] string channel,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void setScoreAdapter ([MarshalAs (UnmanagedType.LPStr)] string board,
			int score,
			[MarshalAs (UnmanagedType.LPStr)] string channel,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		[DllImport (MSDK.LibName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void unlockAchievementAdapter ([MarshalAs (UnmanagedType.LPStr)] string achieve,
			double count,
			[MarshalAs (UnmanagedType.LPStr)] string channel,
			[MarshalAs (UnmanagedType.LPStr)] string extra);

		/// <summary>
		/// Game的回调接口
		/// </summary>
		public static event OnMSDKRetEventHandler<MSDKBaseRet> GameBaseRetEvent;

		/// <summary>
		/// 游戏排行榜初始化
		/// </summary>
		/// <param name="channel">渠道</param>
		/// <param name="extraJson">扩展字段</param>
		public static void Setup (string channel="", string extraJson="")
		{
			try {
				MSDKLog.Log ("Setup channel=" + channel + " extra=" + extraJson);
#if UNITY_EDITOR

#else
				setupAdapter (channel, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("Setup with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 展示成就榜
		/// </summary>
		/// <param name="channel">Channel.</param>
		/// <param name="extraJson">extraJson.</param>
		public static void ShowAchievement (string channel="", string extraJson="")
		{
			try {
				MSDKLog.Log ("ShowAchievement channel=" + channel + " extra=" + extraJson);
#if UNITY_EDITOR

#else
				showAchievementAdapter (channel, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("ShowAchievement with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 展示排行榜
		/// </summary>
		/// <param name="board">Board.</param>
		/// <param name="channel">Channel.</param>
		/// <param name="extraJson">extraJson.</param>
		public static void ShowLeaderBoard (string board, string channel="", string extraJson="")
		{
			try {
				MSDKLog.Log ("ShowLeaderBoard board=" + board + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				showLeaderBoardAdapter (board, channel, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("ShowLeaderBoard with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 设置分数
		/// </summary>
		/// <param name="board"></param>
		/// <param name="score"></param>
		/// <param name="channel"></param>
		/// <param name="extraJson"></param>
		public static void SetScore (string board, int score, string channel="", string extraJson="")
		{
			try {
				MSDKLog.Log ("SetScore board=" + board + " score=" + score + " channel=" + channel + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				setScoreAdapter (board, score, channel, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("SetScore with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// <summary>
		/// 解锁成就
		/// </summary>
		/// <param name="achieve"></param>
		/// <param name="count"></param>
		/// <param name="channel"></param>
		/// <param name="extraJson"></param>
		public static void UnlockAchievement (string achieve, double count, string channel="", string extraJson="")
		{
			try {
				MSDKLog.Log ("UnlockAchievement board=" + achieve + " score=" + count + " channel=" + channel + " extraJson=" + extraJson);
#if UNITY_EDITOR

#else
				unlockAchievementAdapter (achieve, count, channel, extraJson);
#endif
			} catch (Exception ex) {
				MSDKLog.LogError ("UnlockAchievement with unknown error = \n" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		/// //----callabck---------- 
        internal static void OnGameRet (string json)
		{
			MSDKLog.Log ("OnGameRet  json= " + json);
			if (GameBaseRetEvent != null) {
				var ret = new MSDKBaseRet (json);
				try {
					GameBaseRetEvent (ret);
				} catch (Exception e) {
					MSDKLog.LogError (e.StackTrace);
				}
			}else {
				MSDKLog.LogError ("No callback for GameBaseRetEvent !");
			}
		}
	}
#endif
}
