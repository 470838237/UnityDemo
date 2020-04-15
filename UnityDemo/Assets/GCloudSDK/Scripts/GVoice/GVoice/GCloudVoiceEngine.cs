using UnityEngine;
using System.Runtime.InteropServices;
using System;


namespace GCloud
{
	namespace GVoice
	{

        public class GCloudVoice
        {
            /// <summary>
            /// Get the voice engine instance
            /// </summary>
            /// <returns> the voice instance on success, or null on failed.</returns>
            public static IGCloudVoiceEngine GetEngine()
        	{
        		if (instance == null)
        		{
        			instance = new GCloudVoiceEngine();
        #if UNITY_ANDROID
                    GCloudVoiceEngine.PrintLog("GVoice_C# API: Call java from c sharp before");
                    var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    var currentActivity = activity.GetStatic<AndroidJavaObject>("currentActivity");
                    var context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

                    AndroidJavaClass jarVoice = new AndroidJavaClass("com.tencent.gcloud.voice.GCloudVoiceEngine");
                    AndroidJavaObject ins = jarVoice.CallStatic<AndroidJavaObject>("getInstance");
                    ins.Call<int>("init", context, currentActivity);
                    GCloudVoiceEngine.PrintLog("GVoice_C# API: Call java from c sharp after");
        #endif
        		}
        		return instance;
        	
        	}
        	private static  IGCloudVoiceEngine instance = null;
        }

		public class GCloudVoiceEngine : IGCloudVoiceEngine
		{
			#if UNITY_STANDALONE_WIN || UNITY_EDITOR
				public const string LibName = "GVoice";
			#else
				#if UNITY_IPHONE || UNITY_XBOX360
					public const string LibName = "__Internal";
				#else
					public const string LibName = "GVoice";
				#endif
			#endif
			
			enum NoticeMessageType {
                MSG_ON_JOINROOM_COMPLETE = 1,
                MSG_ON_QUITROOM_COMPLETE = 2,
                MSG_ON_UPLOADFILE_COMPLETE = 3,
                MSG_ON_DOWNFILE_COMPLETE = 4,
                MSG_ON_MEMBER_VOICE = 5,
                MSG_ON_APPLY_AUKEY_COMPLETE = 6,
                MSG_ON_PLAYFILE_COMPLETE = 7,
                MSG_ON_SPEECH_TO_TEXT = 8,
                MSG_ON_ROOM_OFFLINE = 9,
                MSG_ON_STREAM_SPEECH_TO_TEXT = 10,
                MSG_ON_ROLE_CHANGED = 11,
                MSG_ON_EVENT_NOTIFY = 12,
                MSG_ON_MUTE_STATE = 13,
                MSG_ON_REPORT_PLAYER = 14,
                MSG_ON_CHECK_REPORTED = 15,
                MSG_ON_UPLOAD_SAVEDATA_COMPLETE = 16,
                MSG_ON_DOWNLOAD_SAVEDATA_COMPLETE = 17,
                MSG_ON_SYNCHROMEMINFO_NOTIFY = 18,
                MSG_ON_SPEECH_TRANSLATE = 19,
                MSG_ON_RSTS = 20,
                MSG_ON_QUERY_WX_MEMBERS = 21,
                MSG_ON_QUERY_USER_INFO = 22,
                MSG_ON_WX_MEMBER_VOICE = 23,
                MSG_ON_UPLOAD_USER_INFO = 24,
                MSG_ON_REPORT_KEY_WORDS = 25,
                MSG_ON_SAVE_REC_FILE = 26,
				MSG_ON_ENABLE_TRANSLATE,
			};

			public class NoticeMessage
			{
				public int what;
				public int intArg1;
				public int intArg2;
				public string strArg;
				public byte[] custom;
				public int datalen;
				public NoticeMessage()
				{
					what = -1;
					intArg1 = 0;
					intArg2 = 0;
					strArg = "";
					datalen = 0;
					custom = new byte[2048];
				}
				public void clear()
				{
					what = -1;
					intArg1 = 0;
					intArg2 = 0;
					strArg = "";
					datalen = 0;
				}
			}
			public override event JoinRoomCompleteEventHandler              OnJoinRoomCompleteEvent;
			public override event QuitRoomCompleteEventHandler              OnQuitRoomCompleteEvent;
			public override event MemberVoiceEventHandler                   OnMemberVoiceEvent;
			public override event ApplyMessageKeyCompleteEventHandler       OnApplyMessageKeyCompleteEvent;
			public override event UploadReccordFileCompleteEventHandler     OnUploadReccordFileCompleteEvent;
			public override event DownloadRecordFileCompleteEventHandler    OnDownloadRecordFileCompleteEvent;
			public override event PlayRecordFilCompleteEventHandler         OnPlayRecordFilCompleteEvent;
			public override event SpeechToTextEventHandler                  OnSpeechToTextEvent;
			public override event StreamSpeechToTextEventHandler            OnStreamSpeechToTextEvent;
	        public override event StatusUpdateEventHandler                  OnStatusUpdateEvent;
	        public override event ChangeRoleCompleteEventHandler            OnRoleChangeCompleteEvent;
	        public override event RoomMemberVoiceEventHandler               OnRoomMemberVoiceEvent;
	        public override event EventUpdateEventHandler                   OnEventUpdateEvent;
            public override event SaveRecFileIndexEventHandler              OnSaveRecFileIndexEvent;
            public override event MuteSwitchResultHandler                   OnMuteSwitchState;
            public override event ReportPlayerHandler                       OnReportPlayer;
			public override event RoomMemberChangedCompleteHandler          OnRoomMemberInfo;
			public override event SpeechTranslateHandler 			        OnSpeechTranslate;
			public override event RSTSHandler 			 			        OnRSTS;
			public override event EnableTranslateHandler 			 		OnEnableTranslate;
			
			private static bool bInit = false;
			private static bool bIsSetAppInfo = false;
			private static bool bPrintLog = true;
			private int pollBufLen = 2048;
			private byte[] pollBuf;
			private NoticeMessage pollMsg = null;
			private int[] memberVoice = null;
			private byte[] roomNameBuf;
			private int membersBufLen = 3072; //maybe save 22 members with max 128len openid
			private byte[] membersBuf;
			
			#region DllImport 
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_CreateInstance();	
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetServerInfo([MarshalAs(UnmanagedType.LPArray)] string URL);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetAppInfo([MarshalAs(UnmanagedType.LPArray)] string appID, [MarshalAs(UnmanagedType.LPArray)]string appKey,[MarshalAs(UnmanagedType.LPArray)] string openID);	
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_Init();	
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetMode(int mode);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_Poll(byte[] buf, int length);		

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_Pause();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_Resume();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_JoinTeamRoom([MarshalAs(UnmanagedType.LPArray)] string roomName, int msTimeout);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_QueryRoomName(byte[] buf, int length, int index);

	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_JoinRangeRoom([MarshalAs(UnmanagedType.LPArray)] string roomName, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_JoinNationalRoom([MarshalAs(UnmanagedType.LPArray)] string roomName, int role, int msTimeout);
	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_ChangeRole(int role, [MarshalAs(UnmanagedType.LPArray)] string roomName);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_JoinFMRoom([MarshalAs(UnmanagedType.LPArray)] string roomName, int msTimeout);		
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_QuitRoom([MarshalAs(UnmanagedType.LPArray)] string roomName, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_OpenMic();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_CloseMic();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_OpenSpeaker();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_CloseSpeaker();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableRoomMicrophone([MarshalAs(UnmanagedType.LPArray)] string roomName, bool enable);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableRoomSpeaker([MarshalAs(UnmanagedType.LPArray)] string roomName, bool enable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableMultiRoom(bool enable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_UpdateCoordinate([MarshalAs(UnmanagedType.LPArray)] string roomName, long x, long y, long z, long r);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_ApplyMessageKey(int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetMaxMessageLength(int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StartRecording([MarshalAs(UnmanagedType.LPArray)] string filePath, bool bOptimized);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StopRecording();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_UploadRecordedFile([MarshalAs(UnmanagedType.LPArray)] string filePath, int msTimeout, bool bPermanent);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_DownloadRecordedFile([MarshalAs(UnmanagedType.LPArray)] string fileID, [MarshalAs(UnmanagedType.LPArray)] string downloadFilePath, int msTimeout, bool bPermanent);	
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_PlayRecordedFile([MarshalAs(UnmanagedType.LPArray)] string downloadFilePath);	
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StopPlayFile();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SpeechToText([MarshalAs(UnmanagedType.LPArray)] string fileID, int language, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_ForbidMemberVoice(int member, bool bEnable, [MarshalAs(UnmanagedType.LPArray)] string roomName);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableLog(bool enable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetDataFree(bool enable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetMicLevel(bool bFadeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetSpeakerLevel();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetSpeakerVolume(int vol);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_TestMic() ;
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetFileParam([MarshalAs(UnmanagedType.LPArray)] string filepath, int [] bytes, float []seconds);		
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_invoke( uint nCmd, uint nParam1, uint nParam2, [MarshalAs(UnmanagedType.LPArray)] uint [] pOutput );
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_JoinNationalRoom_Token([MarshalAs(UnmanagedType.LPArray)] string roomName, int role, [MarshalAs(UnmanagedType.LPArray)] string token, int timestamp, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_JoinTeamRoom_Token( [MarshalAs(UnmanagedType.LPArray)] string roomName,  [MarshalAs(UnmanagedType.LPArray)] string token, int timestamp, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_ApplyMessageKey_Token([MarshalAs(UnmanagedType.LPArray)] string token, int timestamp, int msTimeout);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SpeechToText_Token([MarshalAs(UnmanagedType.LPArray)] string fileID, [MarshalAs(UnmanagedType.LPArray)] string token, int timestamp, int msTimeout, int language);								

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableSpeakerOn(bool enable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetMicVol(int vol);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetAudience([MarshalAs(UnmanagedType.LPArray)] int [] audience, int count, [MarshalAs(UnmanagedType.LPArray)] string path);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetMicState();
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetSpeakerState();
            [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
            private static extern int GVoice_CaptureMicrophoneData(bool bCapture);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableBluetoothSCO(bool enable);
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_CheckDeviceMuteState();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr GVoice_GetInstance();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetVoiceEffects(int mode);
			
	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_IsSpeaking();
	        
	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableReverb(bool bEnable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetReverbMode(int mode);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetVoiceIdentify();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetBGMPath([MarshalAs(UnmanagedType.LPArray)] string path);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StartBGMPlay();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StopBGMPlay();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_PauseBGMPlay();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_ResumeBGMPlay();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetBGMPlayState();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetBGMVol(int vol);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableNativeBGMPlay(bool bEnable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetBitRate(int bitrate);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetHwState();

	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetAudioDeviceConnectionState();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetMuteResult();

	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_SetReportBufferTime(int nTimeSec);
	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_SetReportedPlayerInfo([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] cszOpenID, int[] nMemberID, int nCount);
	        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
	        private static extern int GVoice_ReportPlayer([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)]string[] cszOpenID, int nCount, [MarshalAs(UnmanagedType.LPArray)] string strInfo);	

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StartSaveVoice();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StopSaveVoice();
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetRecSaveTs(int ts);
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetPlayFileIndex([MarshalAs(UnmanagedType.LPArray)] string fileid, int fileindex);
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StartPlaySaveVoiceTs(int ts);
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_StopPlaySaveVoice();
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]	
			private static extern int GVoice_DelAllSaveVoiceFile([MarshalAs(UnmanagedType.LPArray)] string fileid, int fileindex);
			
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetRoomMembers([MarshalAs(UnmanagedType.LPArray)] string roomName, byte[] buf, int len);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableCivilVoice(bool bEnable);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SpeechTranslate([MarshalAs(UnmanagedType.LPArray)] string fileID, int srcLang, int targetLang, int transType, int nTimeoutMS);


			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableTranslate([MarshalAs(UnmanagedType.LPArray)] string roomName, bool enable, int myLang);
		
			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_RSTSStartRecording(int srcLang, int[] pTargetLangs, int nTargetLangCnt, int transType, int nTimeoutMS);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_RSTSStopRecording();

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetCivilBinPath([MarshalAs(UnmanagedType.LPArray)] string path);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_SetPlayerVolume([MarshalAs(UnmanagedType.LPArray)] string playerid, uint nVol);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_GetPlayerVolume([MarshalAs(UnmanagedType.LPArray)] string playerid);

			[DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
			private static extern int GVoice_EnableKeyWordsDetect(bool bEnable);
			#endregion 
			
			public GCloudVoiceEngine()
			{
				int ret = GVoice_CreateInstance();
				if(ret != 0)
				{
					PrintLog("Create GCloudVoiceInstance failed!");
				}
				pollBuf = new byte[pollBufLen];
				pollMsg = new NoticeMessage();
				roomNameBuf = new byte[256];
				memberVoice = new int[1024];
                membersBuf = new byte[membersBufLen];
			}

			public static void PrintLog(object logMsg)
			{
				if (null == logMsg)
				{
					return;
				}
				
				if (bPrintLog)
				{
					Debug.Log(logMsg);
				}
			}
			
			public override ErrorNo SetAppInfo(string appID, string appKey, string openID)
			{
				int ret = GVoice_SetAppInfo(appID, appKey, openID);
				if(ret == 0)
				{
					bIsSetAppInfo = true;
				}
				return (ErrorNo)ret;
			}

			public override ErrorNo SetServerInfo(string URL)
			{
				int ret = GVoice_SetServerInfo(URL);
				return (ErrorNo)ret;
			}
			
			public override ErrorNo Init()
			{
				if(!bIsSetAppInfo)
				{
					PrintLog("please set appinfo first");
					return ErrorNo.NeedSetAppInfo;
				}
				if(!bInit)
				{
					int ret = GVoice_Init();
					if(ret != 0)
					{
						PrintLog("Init GCloudVoice failed!");
						return (ErrorNo)ret;
					}
					bInit = true;
				} 
				return ErrorNo.Succ;         
			}
			
			public override  ErrorNo SetMode(Mode nMode)
			{
				PrintLog("GVoice_C# API: _SetMode");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				return (ErrorNo) GVoice_SetMode((int)nMode);
			}
			
			public NoticeMessage NoticeMessage_ParseFrom(byte[] buf, int buflen)
			{
				int guard = 0;
				if (buflen - guard < sizeof(Int32)) {
					PrintLog("notifymsg,parse error, buf small then sizeof(int)");
					return null;
				}
				pollMsg.what = BitConverter.ToInt32 (buf, guard);
				guard += sizeof(UInt32);
				pollMsg.intArg1 = BitConverter.ToInt32 (buf, guard);
				guard += sizeof(UInt32);
				pollMsg.intArg2 = BitConverter.ToInt32 (buf, guard);
				guard += sizeof(UInt32);
				int strlen = BitConverter.ToInt32 (buf, guard);
				guard += sizeof(UInt32);
				if (strlen == 0) {
					pollMsg.strArg = "";
				} else {
					byte[] bstr = new byte[strlen];
					Array.Copy(buf, guard, bstr, 0, strlen);	
					pollMsg.strArg = System.Text.Encoding.Default.GetString ( bstr );
				}
				guard += strlen;
				pollMsg.datalen = BitConverter.ToInt32 (buf, guard);
				guard += sizeof(UInt32);
				if (pollMsg.datalen > 0) {
					Array.Copy(buf, guard, pollMsg.custom, 0, pollMsg.datalen);
				}
				return pollMsg;
				
			}
			
			public  override  ErrorNo Poll()
			{	
				int ret = GVoice_Poll(pollBuf, pollBufLen);
				if (ret != 0) {
					if(ret == (int)ErrorNo.PollMsgNo)
					{
						//poll no msg, return succ
						return 0;
					}
					return (ErrorNo)ret;
				}
				pollMsg.clear ();
				NoticeMessage msg = NoticeMessage_ParseFrom(pollBuf, pollBufLen);
				if (msg == null) {
					return ErrorNo.PollMsgParseErr;
				}

				switch (msg.what)
				{
				case (int)NoticeMessageType.MSG_ON_JOINROOM_COMPLETE:
					if (OnJoinRoomCompleteEvent != null) {
						PrintLog ("c# poll callback OnJoinRoomComplete not null");
						OnJoinRoomCompleteEvent ((CompleteCode)msg.intArg1, msg.strArg, msg.intArg2);
					}
					break;
				case (int)NoticeMessageType.MSG_ON_QUITROOM_COMPLETE:
					if (OnQuitRoomCompleteEvent != null) {
						OnQuitRoomCompleteEvent ((CompleteCode)msg.intArg1, msg.strArg, msg.intArg2);
					}
					break;
					case (int)NoticeMessageType.MSG_ON_APPLY_AUKEY_COMPLETE:
						if (OnApplyMessageKeyCompleteEvent != null) {
							OnApplyMessageKeyCompleteEvent ((CompleteCode)msg.intArg1);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_UPLOADFILE_COMPLETE:
						if (OnUploadReccordFileCompleteEvent != null) {
							OnUploadReccordFileCompleteEvent ((CompleteCode)msg.intArg1, msg.strArg, msg.custom != null ? System.Text.Encoding.Default.GetString (msg.custom, 0, msg.datalen) : "");
						}
						break;
					case (int)NoticeMessageType.MSG_ON_DOWNFILE_COMPLETE:
						if (OnDownloadRecordFileCompleteEvent != null) {
							OnDownloadRecordFileCompleteEvent ((CompleteCode)msg.intArg1, msg.strArg, msg.custom != null ? System.Text.Encoding.Default.GetString (msg.custom, 0, msg.datalen) : "");
						}
						break;
					case (int)NoticeMessageType.MSG_ON_PLAYFILE_COMPLETE:
						if (OnPlayRecordFilCompleteEvent != null) {
							OnPlayRecordFilCompleteEvent ((CompleteCode)msg.intArg1, msg.strArg);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_SPEECH_TO_TEXT:
						if (OnSpeechToTextEvent != null) {
							OnSpeechToTextEvent ((CompleteCode)msg.intArg1, msg.strArg, msg.custom != null ? System.Text.Encoding.UTF8.GetString (msg.custom, 0, msg.datalen) : "");
						}
						break;
					case (int)NoticeMessageType.MSG_ON_STREAM_SPEECH_TO_TEXT:
						if (OnStreamSpeechToTextEvent != null) {
							OnStreamSpeechToTextEvent ((CompleteCode)msg.intArg1, msg.intArg2, msg.custom != null ? System.Text.Encoding.UTF8.GetString (msg.custom, 0, msg.datalen) : "", msg.strArg);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_MEMBER_VOICE:
						Array.Clear (memberVoice, 0, memberVoice.Length);
						int memcount = msg.intArg1;
                        if (msg.intArg2 == 1) { // for signal room
                            for (int i = 0; i < memcount; i++) {
                                memberVoice [2 * i] = System.BitConverter.ToInt32 (pollMsg.custom, 2 * i * 4);
                                memberVoice [2 * i + 1] = System.BitConverter.ToInt32 (pollMsg.custom, (2 * i + 1) * 4);
                            }
                            if (OnMemberVoiceEvent != null) {
                                OnMemberVoiceEvent(memberVoice, memcount);
                            }

                        } else if (msg.intArg2 == 3) { // for multiroom
                            string[] rooms = pollMsg.strArg.Split('$');
                            int guard = 0;
                            for (int i = 0; i < rooms.Length/2; i++) {
                                string room = rooms[2*i];
                                int count = Convert.ToInt32(rooms[2*i+1]);
                                for (int j=0;j<count;j++) {
                                    int memID = System.BitConverter.ToInt32 (pollMsg.custom, 2 * guard * 4);
                                    int status = System.BitConverter.ToInt32 (pollMsg.custom, (2 * guard + 1) * 4);
                                    guard++;
                                    if (OnRoomMemberVoiceEvent != null) {
                                        OnRoomMemberVoiceEvent(room, memID, status);
                                    }
                                }
                            }
                        }
						break;
					case (int)NoticeMessageType.MSG_ON_ROOM_OFFLINE:
						if (OnStatusUpdateEvent != null) {
							OnStatusUpdateEvent ((CompleteCode)msg.intArg1,  msg.strArg, msg.intArg2);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_ROLE_CHANGED:
						if (OnRoleChangeCompleteEvent != null)
						{
							OnRoleChangeCompleteEvent((CompleteCode)msg.intArg1, msg.strArg, msg.intArg2, msg.custom[0]);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_EVENT_NOTIFY:
						if (OnEventUpdateEvent != null)
						{
							OnEventUpdateEvent((Event)msg.intArg1, msg.strArg);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_MUTE_STATE:
						if(OnMuteSwitchState != null){
							OnMuteSwitchState(msg.intArg1);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_REPORT_PLAYER:
						if (OnReportPlayer != null)
						{
							OnReportPlayer((CompleteCode)msg.intArg1, msg.strArg);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_UPLOAD_SAVEDATA_COMPLETE:
						if(OnSaveRecFileIndexEvent != null)
						{
							OnSaveRecFileIndexEvent((CompleteCode)msg.intArg1, msg.strArg, msg.intArg2);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_SYNCHROMEMINFO_NOTIFY:
						if(OnRoomMemberInfo != null)
						{
							OnRoomMemberInfo((CompleteCode)msg.intArg1, msg.strArg, msg.intArg2, msg.custom != null ? System.Text.Encoding.Default.GetString (msg.custom, 0, msg.datalen) : "");
						}
						break;
					case (int)NoticeMessageType.MSG_ON_SPEECH_TRANSLATE:
						if (OnSpeechTranslate != null)
						{
							string strTargetText = "";
							string strTargetFileID = "";
							int nDurationMS = 0;
							if(msg.custom != null){
								string strArgs = System.Text.Encoding.UTF8.GetString(msg.custom, 0, msg.datalen);
								string[] args = strArgs.Split('|');
								if(args.Length >= 3){
									nDurationMS = Convert.ToInt32(args[0]);
									strTargetFileID = args[1];
									strTargetText = args[2];
								}
							}
							OnSpeechTranslate((CompleteCode)msg.intArg1, msg.strArg, strTargetText, strTargetFileID, nDurationMS);
						}
						break;
					case (int)NoticeMessageType.MSG_ON_RSTS:
						if (OnRSTS != null)
						{
							string strTargetText = "";
							string strTargetFileID = "";
							int nDurationMS = 0;
							SpeechLanguageType srcLang = SpeechLanguageType.SPEECH_LANGUAGE_ZH;
							SpeechLanguageType targetLang = SpeechLanguageType.SPEECH_LANGUAGE_ZH;

							if(msg.custom != null){
								string strArgs = System.Text.Encoding.UTF8.GetString(msg.custom, 0, msg.datalen);
								string[] args = strArgs.Split('|');
								if(args.Length >= 5){
									srcLang = (SpeechLanguageType)Convert.ToInt32(args[0]);
									targetLang = (SpeechLanguageType)Convert.ToInt32(args[1]);
									nDurationMS = Convert.ToInt32(args[2]);
									strTargetFileID = args[3];
									strTargetText = args[4];
								}
							}
							OnRSTS((CompleteCode)msg.intArg1, srcLang, targetLang, msg.strArg, strTargetText, strTargetFileID, nDurationMS);
						}
						break;
                    case (int)NoticeMessageType.MSG_ON_REPORT_KEY_WORDS:
                    {
                        if(OnReportPlayer != null)
                        {
                            OnReportPlayer((CompleteCode)msg.intArg1,msg.strArg);
                        }
                    }
                    break;
	            	case (int)NoticeMessageType.MSG_ON_ENABLE_TRANSLATE:
                		if(OnEnableTranslate != null)
						{
                    		OnEnableTranslate((CompleteCode)msg.intArg1, msg.strArg, Convert.ToBoolean(msg.intArg2));
                		}
						break;
				}

				return ErrorNo.Succ;
			}
			
			public override  ErrorNo Pause()
			{
				PrintLog("GVoice_C# API: _Pause");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_Pause();
				PrintLog("GVoice_C# API: _Pause nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public  override ErrorNo Resume()
			{
				PrintLog("GVoice_C# API: _Resume");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_Resume();
				PrintLog("GVoice_C# API: _Resume nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo JoinTeamRoom(string roomName, int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinTeamRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_JoinTeamRoom(roomName, msTimeout);
				PrintLog("GVoice_C# API: JoinTeamRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

			public override ErrorNo JoinRangeRoom(string roomName, int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinRangeRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_JoinRangeRoom(roomName, msTimeout);
				PrintLog("GVoice_C# API: JoinRangeRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}


			public override ErrorNo JoinTeamRoom(string roomName, string token, int timestamp, int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinTeamRoom"+" mstimeout:"+msTimeout);
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_JoinTeamRoom_Token(roomName, token, timestamp, msTimeout);
				PrintLog("GVoice_C# API: JoinTeamRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}		
			
			public override ErrorNo JoinNationalRoom(string roomName, MemberRole role, int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinNationalRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_JoinNationalRoom(roomName, (int)role, msTimeout);
				PrintLog("GVoice_C# API: JoinNationalRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

	        public override ErrorNo ChangeRole(MemberRole role, string roomName="")
	        {
	            PrintLog("GVoice_C# API: ChangeRole");
	            if (!bInit)
	            {
	                return ErrorNo.NeedSetAppInfo;
	            }
	            int nRet = GVoice_ChangeRole((int)role, roomName);
	            PrintLog("GVoice_C# API: GVoice_ChangeRole  nRet=" + nRet);
	            return (ErrorNo)nRet;
	        }

			public override ErrorNo JoinFMRoom(string roomID,int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinFMID");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = 0;
				return (ErrorNo)nRet;
			}

			public override int SetAudience(int[] audience, string roomName = "")
			{
				int nRet = GVoice_SetAudience(audience, audience.Length, roomName);
				PrintLog("GVoice_C# API: SetAudience  nRet=" + nRet);
				return nRet;
			}

			public override ErrorNo JoinNationalRoom(string roomName, string token, int timestamp, MemberRole role, int msTimeout)
			{
				PrintLog("GVoice_C# API: JoinNationalRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_JoinNationalRoom_Token(roomName, (int)role, token, timestamp, msTimeout);
				PrintLog("GVoice_C# API: JoinNationalRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}		
			
			public override ErrorNo QuitRoom(string roomName, int msTimeout)
			{
				PrintLog("GVoice_C# API: QuitRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_QuitRoom(roomName, msTimeout);
				PrintLog("GVoice_C# API: QuitRoom  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo OpenMic()
			{
				PrintLog("GVoice_C# API: OpenMic");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_OpenMic();
				PrintLog("GVoice_C# API: OpenMic  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo CloseMic()
			{
				PrintLog("GVoice_C# API: CloseMic");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_CloseMic();
				PrintLog("GVoice_C# API: CloseMic  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo OpenSpeaker()
			{
				PrintLog("GVoice_C# API: OpenSpeaker");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_OpenSpeaker();
				PrintLog("GVoice_C# API: OpenSpeaker  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo CloseSpeaker()
			{
				PrintLog("GVoice_C# API: CloseSpeaker");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_CloseSpeaker();
				PrintLog("GVoice_C# API: CloseSpeaker  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			

			public override ErrorNo EnableRoomMicrophone(string roomName, bool enable)
			{
				PrintLog("GVoice_C# API: EnableRoomMicrophone");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableRoomMicrophone(roomName, enable);
				PrintLog("GVoice_C# API: EnableRoomMicrophone  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

			public override ErrorNo EnableRoomSpeaker(string roomName, bool enable)
			{
				PrintLog("GVoice_C# API: EnableRoomSpeaker");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableRoomSpeaker(roomName, enable);
				PrintLog("GVoice_C# API: EnableRoomSpeaker  nRet=" + nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo EnableMultiRoom(bool enable)
			{
				PrintLog("GVoice_C# API: EnableMultiRoom");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableMultiRoom(enable);
				PrintLog("GVoice_C# API: EnableMultiRoom  nRet=" + nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo UpdateCoordinate (string roomName, long x, long y, long z, long r)
			{
				PrintLog("GVoice_C# API: UpdateCoordinate");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_UpdateCoordinate (roomName, x, y, z, r);
				PrintLog("GVoice_C# API: UpdateCoordinate  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo ApplyMessageKey(int msTimeout)
			{
				PrintLog("GVoice_C# API: ApplyMessageKey");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_ApplyMessageKey(msTimeout);
				PrintLog("GVoice_C# API: ApplyMessageKey  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

			public override ErrorNo ApplyMessageKey(string token, int timestamp, int msTimeout)
			{
				PrintLog("GVoice_C# API: ApplyMessageKey");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_ApplyMessageKey_Token(token, timestamp, msTimeout);
				PrintLog("GVoice_C# API: ApplyMessageKey  nRet=" + nRet);
				return (ErrorNo)nRet;
			}		
			
			public override ErrorNo SetMaxMessageLength(int msTime)
			{
				PrintLog("GVoice_C# API: SetMaxMessageLength");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetMaxMessageLength(msTime);
				PrintLog("GVoice_C# API: SetMaxMessageLength  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo StartRecording(string filePath)
			{
				PrintLog("GVoice_C# API: StartRecording");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StartRecording(filePath, false);
				PrintLog("GVoice_C# API: StartRecording  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo StopRecording ()
			{
				PrintLog("GVoice_C# API: StopRecording");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StopRecording();
				PrintLog("GVoice_C# API: StopRecording  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

	#if UNITY_IPHONE
	        public override ErrorNo StartRecording(string filePath, bool bOptimized)
			{
				PrintLog("GVoice_C# API: StartRecording");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StartRecording(filePath, bOptimized);
				PrintLog("GVoice_C# API: StartRecording  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
	#endif
			
			public override ErrorNo UploadRecordedFile(string filePath, int msTimeout)
			{
				PrintLog("GVoice_C# API: UploadRecordedFile");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_UploadRecordedFile(filePath, msTimeout, false);
				PrintLog("GVoice_C# API: UploadRecordedFile  nRet=" + nRet);
				return (ErrorNo)nRet;
			}	
			
			public override ErrorNo DownloadRecordedFile(string fileID, string downloadFilePath, int msTimeout)
			{
				PrintLog("GVoice_C# API: DownloadRecordedFile");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_DownloadRecordedFile(fileID, downloadFilePath, msTimeout, false);
				PrintLog("GVoice_C# API: DownloadRecordedFile  nRet=" + nRet);
				return (ErrorNo)nRet;
			}

	        //overidload
	        public override ErrorNo UploadRecordedFile(string filePath, int msTimeout, bool bPermanent)
	        {
	            PrintLog("GVoice_C# API: UploadRecordedFile");
	            if (!bInit)
	            {
	                return ErrorNo.NeedSetAppInfo;
	            }

	            int nRet = GVoice_UploadRecordedFile(filePath, msTimeout, bPermanent);
	            PrintLog("GVoice_C# API: UploadRecordedFile  nRet=" + nRet);
	            return (ErrorNo)nRet;
	        }

	        public override ErrorNo DownloadRecordedFile(string fileID, string downloadFilePath, int msTimeout, bool bPermanent)
	        {
	            PrintLog("GVoice_C# API: DownloadRecordedFile");
	            if (!bInit)
	            {
	                return ErrorNo.NeedSetAppInfo;
	            }

	            int nRet = GVoice_DownloadRecordedFile(fileID, downloadFilePath, msTimeout, bPermanent);
	            PrintLog("GVoice_C# API: DownloadRecordedFile  nRet=" + nRet);
	            return (ErrorNo)nRet;
	        }	
			
			public override ErrorNo PlayRecordedFile(string downloadFilePath)
			{
				PrintLog("GVoice_C# API: PlayRecordedFile");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_PlayRecordedFile(downloadFilePath);
				PrintLog("GVoice_C# API: PlayRecordedFile  nRet=" + nRet);
				return (ErrorNo)nRet;
			}		
			
			public override ErrorNo StopPlayFile()
			{
				PrintLog("GVoice_C# API: StopPlayFile");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StopPlayFile();
				PrintLog("GVoice_C# API: StopPlayFile  nRet=" + nRet);
				return (ErrorNo)nRet;
			}	
			
			public override ErrorNo SpeechToText(string fileID, Language language = Language.China, int msTimeout = 6000)
			{
				PrintLog("GVoice_C# API: SpeechToText");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SpeechToText(fileID,(int)language, msTimeout);
				PrintLog("GVoice_C# API: SpeechToText  nRet=" + nRet);
				return (ErrorNo)nRet;
			}	

			public override ErrorNo SpeechToText(string fileID, string token, int timestamp, int language = 0, int msTimeout = 6000)
			{
				PrintLog("GVoice_C# API: SpeechToText");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SpeechToText_Token(fileID, token, timestamp, language, msTimeout);
				PrintLog("GVoice_C# API: SpeechToText  nRet=" + nRet);
				return (ErrorNo)nRet;
			}			
			
			public override ErrorNo ForbidMemberVoice(int member, bool bEnable, string roomName="")
			{
				PrintLog("GVoice_C# API: ForbidMemberVoice");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_ForbidMemberVoice(member, bEnable, roomName);
				PrintLog("GVoice_C# API: ForbidMemberVoice  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo EnableLog(bool enable)
			{
				PrintLog("GVoice_C# API: EnableLog");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableLog(enable);
				PrintLog("GVoice_C# API: EnableLog  nRet=" + nRet);
                bPrintLog = enable;
				return (ErrorNo)nRet;
			}

			public override ErrorNo SetDataFree(bool enable)
			{
				PrintLog("GVoice_C# API: SetDataFree");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetDataFree(enable);
				PrintLog("GVoice_C# API: SetDataFree  nRet=" + nRet);
				return (ErrorNo)nRet;

			}
			
			public override int GetMicLevel()
			{
				PrintLog("GVoice_C# API: GetMicLevel");

				
				int nRet = GVoice_GetMicLevel(true);
				PrintLog("GVoice_C# API: GetMicLevel  nRet=" + nRet);
				return nRet;
			}

	        public override int GetMicLevel(bool bFadeOut)
	        {
	            PrintLog("GVoice_C# API: GetMicLevel");


	            int nRet = GVoice_GetMicLevel(bFadeOut);
	            PrintLog("GVoice_C# API: GetMicLevel  nRet=" + nRet);
	            return nRet;
	        }
			
			public override int GetSpeakerLevel()
			{
				PrintLog("GVoice_C# API: GetSpeakerLevel");

				
				int nRet = GVoice_GetSpeakerLevel();
				PrintLog("GVoice_C# API: GetSpeakerLevel  nRet=" + nRet);
				return nRet;
			}

			public override int GetMicState()
			{
				int nRet = GVoice_GetMicState();
				PrintLog("GVoice_C# API: GetMicState  nRet=" + nRet);
				return nRet;
			}

			public override int GetSpeakerState()
			{
				int nRet = GVoice_GetSpeakerState();
				PrintLog("GVoice_C# API: GetSpeakerState  nRet=" + nRet);
				return nRet;
			}
   
           public override int CaptureMicrophoneData(bool bCapture)
           {
               if (!bInit)
               {
                   return (int)ErrorNo.NeedInit;
               }

               PrintLog("GVoice_C# API: CaptureMicrophoneData");
               return GVoice_CaptureMicrophoneData(bCapture);
           }

			public override ErrorNo SetSpeakerVolume(int vol)
			{
				PrintLog("GVoice_C# API: SetSpeakerVolume");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetSpeakerVolume(vol);
				PrintLog("GVoice_C# API: SetSpeakerVolume  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo TestMic()
			{
				PrintLog("GVoice_C# API: TestMic");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_TestMic();
				PrintLog("GVoice_C# API: TestMic  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
			public override ErrorNo GetFileParam(string filepath, int [] bytes, float [] seconds)
			{
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_GetFileParam(filepath, bytes, seconds);
				return (ErrorNo)nRet;
			}
			
			public override int invoke( uint nCmd, uint nParam1, uint nParam2, uint [] pOutput )
			{
				PrintLog("GVoice_C# API: invoke");

				
				int nRet = GVoice_invoke(nCmd, nParam1, nParam2, pOutput);
				PrintLog("GVoice_C# API: invoke  nRet=" + nRet);
				return nRet;
			}
			public override ErrorNo EnableSpeakerOn(bool bEnable)
			{
				PrintLog("GVoice_C# API: EnableSpeakerOn");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableSpeakerOn(bEnable);
				PrintLog("GVoice_C# API: GVoice_EnableSpeakerOn  nRet=" + nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo SetMicVolume(int vol)
			{
				PrintLog("GVoice_C# API: SetMicVol");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetMicVol(vol);
				PrintLog("GVoice_C# API: GVoice_SetMicVol  nRet=" + nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo SetVoiceEffects(SoundEffects mode)
			{
				PrintLog("GVoice_C# API: SetVoiceEffects");
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetVoiceEffects((int)mode);
				PrintLog("GVoice_C# API: SetVoiceEffects  nRet=" + nRet);
				return (ErrorNo)nRet;
			}
			
	        public override int IsSpeaking()
	        {
	            if (!bInit)
	            {
	                return 0;
	            }

	            return GVoice_IsSpeaking();            
	        }

			public override void EnableBluetoothSCO(bool enable)
			{
				GVoice_EnableBluetoothSCO(enable);
			}


			public override ErrorNo EnableReverb(bool bEnable)
			{
				PrintLog("GVoice_C# API: EnableReverb");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableReverb(bEnable);
				PrintLog("GVoice_C# API: GVoice_EnableReverb  nRet=" + nRet);
				return (ErrorNo)nRet;	
			}	

			public override IntPtr GetExtensionPluginContext()
	        {
	             return GVoice_GetInstance();
	        }

				

			public override ErrorNo SetReverbMode(int mode)
			{
				PrintLog("GVoice_C# API: SetReverbMode");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetReverbMode(mode);
				PrintLog("GVoice_C# API: GVoice_SetReverbMode  nRet=" + nRet);
				return (ErrorNo)nRet;

			}
			public override int GetVoiceIdentify()
			{
				PrintLog("GVoice_C# API: GetVoiceIdentify");
				
				
				int nRet = GVoice_GetVoiceIdentify();
				PrintLog("GVoice_C# API: GVoice_GetVoiceIdentify nRet = "+ nRet);
				return nRet;

			}

			public override ErrorNo SetBGMPath(string path)
			{
				PrintLog("GVoice_C# API: SetBGMPath");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetBGMPath(path);
				PrintLog("GVoice_C# API: GVoice_SetBGMPath nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo StartBGMPlay()
			{
				PrintLog("GVoice_C# API: StartBGMPlay");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StartBGMPlay();
				PrintLog("GVoice_C# API: GVoice_StartBGMPlay nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo StopBGMPlay()
			{
				PrintLog("GVoice_C# API: StopBGMPlay");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_StopBGMPlay();
				PrintLog("GVoice_C# API: GVoice_StopBGMPlay nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo PauseBGMPlay()
			{
				PrintLog("GVoice_C# API: PauseBGMPlay");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_PauseBGMPlay();
				PrintLog("GVoice_C# API: GVoice_PauseBGMPlay nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo ResumeBGMPlay()
			{
				PrintLog("GVoice_C# API: ResumeBGMPlay");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_ResumeBGMPlay();
				PrintLog("GVoice_C# API: GVoice_ResumeBGMPlay nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo SetBGMVol(int vol)
			{
				PrintLog("GVoice_C# API: SetBGMVol");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetBGMVol(vol);
				PrintLog("GVoice_C# API: GVoice_SetBGMVol nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override int GetBGMPlayState()
			{
				
				int nRet = GVoice_GetBGMPlayState();
				return nRet;

			}

			public override ErrorNo EnableNativeBGMPlay(bool bEnable)
			{
				PrintLog("GVoice_C# API: GetVoiceIdentify");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_EnableNativeBGMPlay(bEnable);
				PrintLog("GVoice_C# API: GVoice_EnableNativeBGMPlay nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override ErrorNo SetBitRate(int bitrate)
			{
				PrintLog("GVoice_C# API: SetBitRate");
				
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
				
				int nRet = GVoice_SetBitRate(bitrate);
				PrintLog("GVoice_C# API: GVoice_SetBitRate nRet = "+ nRet);
				return (ErrorNo)nRet;

			}

			public override int GetHwState()
			{
				int nRet = GVoice_GetHwState();
				
				return nRet;
			}

	        public override DeviceState GetAudioDeviceConnectionState()
			{
				PrintLog("GVoice_C# API: getAudioDeviceConnectionState");
				if (!bInit)
				{
					return DeviceState.Unconnected;
				}
				
				int nRet = GVoice_GetAudioDeviceConnectionState();
				PrintLog("GVoice_C# API: getAudioDeviceConnectionState  nRet=" + nRet);
				return (DeviceState)nRet;
			}

			public override int CheckDeviceMuteState()
			{
				int nRet = GVoice_CheckDeviceMuteState();
				PrintLog("GVoice_C# API: CheckDeviceMuteState  nRet=" + nRet);
				return nRet;
			}

			public override ErrorNo StartSaveVoice(){
				PrintLog("GVoice_C# API: StartSaveVoice");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_StartSaveVoice();
				PrintLog("GVoice_C# API: StartSaveVoice nRet = "+ nRet);
				return (ErrorNo)nRet;
			}

            public override ErrorNo StopSaveVoice(){
				PrintLog("GVoice_C# API: StopSaveVoice");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_StopSaveVoice();
				PrintLog("GVoice_C# API: StopSaveVoice nRet = "+ nRet);
				return (ErrorNo)nRet;
            }

            public override ErrorNo SetRecSaveTs(int ts){
				PrintLog("GVoice_C# API: SetRecSaveTs");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_SetRecSaveTs(ts);
				PrintLog("GVoice_C# API: SetRecSaveTs nRet = "+ nRet);
				return (ErrorNo)nRet;
            }

            public override ErrorNo SetPlayFileIndex(string fileid, int fileindex){
				PrintLog("GVoice_C# API: SetPlayFileIndex");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_SetPlayFileIndex(fileid, fileindex);
				PrintLog("GVoice_C# API: SetPlayFileIndex nRet = "+ nRet);
				return (ErrorNo)nRet;
            }
            
            public override ErrorNo StartPlaySaveVoiceTs(int ts){
				PrintLog("GVoice_C# API: StartPlaySaveVoiceTs");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_StartPlaySaveVoiceTs(ts);
				PrintLog("GVoice_C# API: StartPlaySaveVoiceTs nRet = "+ nRet);
				return (ErrorNo)nRet;
            }
            
            public override ErrorNo StopPlaySaveVoice(){
				PrintLog("GVoice_C# API: StopPlaySaveVoice");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_StopPlaySaveVoice();
				PrintLog("GVoice_C# API: StopPlaySaveVoice nRet = "+ nRet);
				return (ErrorNo)nRet;
            }
            
            public override ErrorNo DelAllSaveVoiceFile(string fileid, int fileindex){
				PrintLog("GVoice_C# API: DelAllSaveVoiceFile");

				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				int nRet = GVoice_DelAllSaveVoiceFile(fileid, fileindex);
				PrintLog("GVoice_C# API: DelAllSaveVoiceFile nRet = "+ nRet);
				return (ErrorNo)nRet;
            }

            public override int GetMuteResult(){
            	PrintLog("GVoice_C# API: GetMuteResult");

            	if (!bInit)
	            {
	                return (int)ErrorNo.NeedInit;
	            }

	            int ret = GVoice_GetMuteResult();
	            PrintLog("GVoice_C# API: GetMuteResult ret = "+ ret);
	            return ret;
            }
            

         	public override ErrorNo SetReportBufferTime(int nTimeSec){
         		PrintLog("GVoice_C# API: SetReportBufferTime");

            	if (!bInit)
	            {
	                return ErrorNo.NeedInit;
	            }

	            int ret = GVoice_SetReportBufferTime(nTimeSec);
	            PrintLog("GVoice_C# API: SetReportBufferTime ret = "+ ret);
	            return (ErrorNo)ret;
	        }

        	public override ErrorNo SetReportedPlayerInfo(string[] cszOpenID, int[] nMemberID, int nCount)
	        {
	        	PrintLog("GVoice_C# API: SetReportedPlayerInfo");

	            if (!bInit)
	            {
	                return ErrorNo.NeedInit;;
	            }

	            int ret = GVoice_SetReportedPlayerInfo(cszOpenID, nMemberID, nCount);
	            PrintLog("GVoice_C# API: SetReportedPlayerInfo ret = "+ ret);
	            return (ErrorNo)ret;
	        }

	        public override ErrorNo ReportPlayer(string[] cszOpenID, int nCount, string strInfo)
	        {
	        	PrintLog("GVoice_C# API: ReportPlayer");

	            if (!bInit)
	            {
	                return ErrorNo.NeedInit;
	            }

	            int ret = GVoice_ReportPlayer(cszOpenID, nCount, strInfo);
	            PrintLog("GVoice_C# API: ReportPlayer nRet = "+ ret);
	            return (ErrorNo)ret;
	        }

			public override int GetRoomMembers(string roomName, RoomMembers[] members, int len)
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}
				//return room members num
				if (members == null || len == -1)
				{
					return GVoice_GetRoomMembers(roomName, null, -1);
				}
				int retNum = GVoice_GetRoomMembers(roomName, membersBuf, membersBufLen);
				int usedbuflen = 0;
				int guard = 0;
				int openidlen = 0;
				usedbuflen = BitConverter.ToInt32(membersBuf, guard);
				guard += sizeof(Int32);
				for(int i = 0; i < retNum && i < len && guard < membersBufLen && guard <=usedbuflen; i++)
				{
					members[i].memberid = BitConverter.ToInt32(membersBuf, guard);
					guard += sizeof(Int32);
					openidlen = BitConverter.ToInt32(membersBuf, guard);
					guard += sizeof(Int32);
					members[i].openid = System.Text.Encoding.Default.GetString(membersBuf, guard, openidlen);
					guard += openidlen;
				}
				return retNum;
			}

			public override int EnableCivilVoice(bool bEnable)
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: EnableCivilVoice");
				return GVoice_EnableCivilVoice(bEnable);
			}

			public override int SpeechTranslate(string fileID, SpeechLanguageType srcLang, SpeechLanguageType targetLang, SpeechTranslateType transType, int nTimeoutMS)
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: SpeechTranslate");
				return GVoice_SpeechTranslate(fileID, (int)srcLang, (int)targetLang, (int)transType, nTimeoutMS);
			}

			public override int RSTSStartRecording(SpeechLanguageType srcLang, SpeechLanguageType[] pTargetLangs, int nTargetLangCnt, SpeechTranslateType transType, int nTimeoutMS)
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: RSTSStartRecording");
				int[] nTargetLangs = Array.ConvertAll(pTargetLangs, value => (int) value);
				return GVoice_RSTSStartRecording((int)srcLang, nTargetLangs, nTargetLangCnt, (int)transType, nTimeoutMS);
			}

			public override int RSTSStopRecording()
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: RSTSStopRecording");
				return GVoice_RSTSStopRecording();
			}
			
			public override int EnableTranslate(string roomname, bool enable, SpeechLanguageType myLang)
			{
				if (!bInit)
            	{
            	    return (int)ErrorNo.NeedInit;
           	 	}
				return GVoice_EnableTranslate(roomname, enable, (int)myLang);
			}
		
			public override ErrorNo SetCivilBinPath(string path)
			{		
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
		
				int nRet = GVoice_SetCivilBinPath(path);
				PrintLog("GVoice_C# API: SetCivilBinPath nRet = "+ nRet);
				return (ErrorNo)nRet;
		
			}
					
			public override ErrorNo SetPlayerVolume(string playerid, uint vol)
			{
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: SetPlayerVolume");
				int nRet = GVoice_SetPlayerVolume(playerid, vol);
				return (ErrorNo)nRet;
			}
		
			public override int GetPlayerVolume(string playerid)
			{
				if (!bInit)
				{
					return (int)ErrorNo.NeedInit;
				}

				PrintLog("GVoice_C# API: GetPlayerVolume");
				int nRet = GVoice_GetPlayerVolume(playerid);
				return nRet;
			}

			public override ErrorNo EnableKeyWordsDetect(bool bEnable)
			{		
				if (!bInit)
				{
					return ErrorNo.NeedInit;
				}
		
				int nRet = GVoice_EnableKeyWordsDetect(bEnable);
				PrintLog("GVoice_C# API: EnableKeyWordsDetect nRet = "+ nRet);
				return (ErrorNo)nRet;
			}

			///////////////////////////////////////////////////////////////////////////////
			
		}
	}// endof GVoice 
}//endof GCloud
