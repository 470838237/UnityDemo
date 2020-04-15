 ///
 /// CSharp API for GCloudVoice
 ///
 /// GVoice(Tencent Game Voice) is a voice service that covers diverse game scenes.
 /// GVoice supports multiple voice modes, such as RealTime mode, Messages mode,
 /// Translation mode and RSTT mode.
 ///
 /// In RealTime mode, multiple members can join in the same room to communicate with each other.
 /// There are several different scenes in RealTime mode, they are TeamRoom, NationalRoom, RangeRoom.
 /// The workflow of RealTime mode is like below:
 /// GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->
 /// JoinXxxRoom-->...-->QuitRoom
 ///
 /// In Messages mode, a member can quickly record and send a voice message to other members.
 /// The workflow of Messages mode is like below:
 /// For record side:
 /// GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->
 /// ApplyMessageKey-->StartRecording-->StopRecording-->UploadRecordedFile
 /// Or for play side:
 /// GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->
 /// ApplyMessageKey-->DownloadRecordedFile-->PlayRecordedFile-->StopPlayFile
 ///
 /// In Translation mode, members can translate a recorded voice message to a piece of
 /// text in a specific language.
 /// The workflow of Translation mode is like below:
 /// GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Translation)-->
 /// ApplyMessageKey-->StartRecording-->StopRecording-->SpeechToText
 /// Then you can get the translation result from the event "OnSpeechToText".
 ///
 /// In RSTT mode, members can translate a recorded voice message to a piece of
 /// text in a specific language in realtime.
 /// The workflow of RSTT mode is like below:
 /// GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RSTT)-->
 /// ApplyMessageKey-->StartRecording-->StopRecording
 /// Then you can get the translation result from the event "OnStreamSpeechToText".
 ///
 /// Notice: GVoice SDK uses asynchronous callback mechanism to notify you the result of a
 /// function, so please remember to do the following three things:
 /// 1. Implement the delegates in GCloudVoiceEngineNotify;
 /// 2. Subscrib the events in GCloudVoiceEngineNotify;
 /// 3. Periodically call the Poll method to drive the engine return the callback results.
 ///
 /// For more information, please visit the GVoice official webcite
 /// https://gcloud.qq.com/product/6
 ///

using System;
using System.Runtime.InteropServices;

namespace GCloud
{
    namespace GVoice 
    {
	    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi,Pack = 1)]
	    public struct RoomMembers
	    {
		    public int memberid;
		    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		    public string openid;
	    }

        public abstract class IGCloudVoiceEngine
        {

        #region Delegate
	        /*************************************************************
	        *                  Real-Time Voice Callbacks
	        *************************************************************/
	        /// <summary>
	        /// Callback after you called JoinXxxRoom, you can get the result of JoinXxxRoom from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="roomName">Name of the room which you joined, it is the one you set in JoinXxxRoom method.</param>
	        /// <param name="memberID">If success, returns the player's ID in this room.</param>
	        /// <see cref="JoinTeamRoom"/>
	        /// <see cref="JoinNationalRoom"/>
	        /// <see cref="JoinRangeRoom"/>
	        /// <see cref="CompleteCode"/>
            public delegate void JoinRoomCompleteEventHandler(CompleteCode code, string roomName, int memberID) ;

	        /// <summary>
	        /// Deprecate from GVoice 1.1.14
	        /// Callback when someone in the same room changes saying status, such as begining saying from silence or stopping saying.
	        /// </summary>
	        ///
	        /// <param name="members">An int array composed of [memberid_0, status, memberid_1, status ... memberid_2*count, status],
	        /// here, status could be 0, 1, 2. 0 means being silence from saying, 1 means begining saying from silence
	        /// and 2 means continue saying.</param>
	        /// <param name="count">The count of members who's status has changed.</param>
	        public delegate void MemberVoiceEventHandler(int[] members, int count) ;

	        /// <summary>
	        /// Callback when someone in the same room changes saying status, such as begining saying from silence or stopping saying.
	        /// </summary>
	        ///
	        /// <param name="roomName">Name of the room which you joined.</param>
	        /// <param name="member">The ID of the member who's status has changed.</param>
	        /// <param name="status">Status could be 0, 1, 2. 0 means being silence from saying, 1 means begining saying from silence
	        /// and 2 means continue saying.</param>
	        /// <see cref="CompleteCode"/>
	        public delegate void RoomMemberVoiceEventHandler(string roomName, int member, int status) ;

	        /// <summary>
	        /// Callback after you called ChangeRole, you can get the result of ChangeRole from the parameters.
	        /// </summary>
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="roomName">Name of the room which the member joined.</param>
	        /// <param name="memberID">The ID of the member who changed role.</param>
	        /// <param name="role">Current role of the member, Anchor or Audience.</param>
	        /// <see cref="ChangeRole"/>
	        /// <see cref="CompleteCode"/>
            public delegate void ChangeRoleCompleteEventHandler(CompleteCode code, string roomName, int memberID, int role);

	        /// <summary>
	        /// Callback when dropped from the room. When a member be offline more than 1min, he will be dropped from the room.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="roomName">Name of the room which the member joined.</param>
	        /// <param name="memberID">If success, return the ID of the mermber who has been dropped from the room.</param>
	        /// <see cref="CompleteCode"/>
	        public delegate void StatusUpdateEventHandler(CompleteCode code, string roomName, int memberID) ;

	        /// <summary>
	        /// Callback when some room members in or out room
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. for member had join or quit room .</param>
	        /// <param name="roomName">Name of the room which members changed.</param>
	        /// <param name="memid">the changed members memberid.</param>
	        /// <param name="openID">the changed members openid.</param>
	        /// <see cref="CompleteCode"/>
	        public delegate void RoomMemberChangedCompleteHandler(CompleteCode code, string roomName, int memid, string openID);

	        /// <summary>
	        /// Callback after you called QuitRoom, you can get the result of QuitRoom from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="roomName">Name of the room which you quited.</param>
	        /// <param name="memberID">If success, return the ID of the mermber who has quitted from the room.</param>
	        /// <see cref="QuitRoom"/>
	        /// <see cref="CompleteCode"/>
            public delegate void QuitRoomCompleteEventHandler(CompleteCode code, string roomName, int memberID) ;


	        /*************************************************************
             *                  Voice Messages Callbacks
             *************************************************************/
	        /// <summary>
	        /// Callback after you called ApplyMessageKey, you can get the result of ApplyMessageKey from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <see cref="ApplyMessageKey"/>
	        /// <see cref="CompleteCode"/>
            public delegate void ApplyMessageKeyCompleteEventHandler(CompleteCode code);

	        /// <summary>
	        /// Callback after you called UploadRecordedFile, you can get the result of UploadRecordedFile from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="filepath">The path of the voice file uploaded.</param>
	        /// <param name="fileid">If success, return the ID of the file.</param>
	        /// <see cref="UploadRecordedFile"/>
	        /// <see cref="CompleteCode"/>
            public delegate void UploadReccordFileCompleteEventHandler(CompleteCode code, string filepath, string fileid) ;

	        /// <summary>
	        /// Callback after you called DownloadRecordedFile, you can get the result of DownloadRecordedFile from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="filepath">The path of the file which the voice download to.</param>
	        /// <param name="fileid">If success,return the ID of the file.</param>
	        /// <see cref="DownloadRecordedFile"/>
	        /// <see cref="CompleteCode"/>
            public delegate void DownloadRecordFileCompleteEventHandler(CompleteCode code, string filepath, string fileid) ;

	        /// <summary>
	        /// Callback after you called PlayRecordedFile and the voice file has been played to the end, you can get the result of PlayRecordedFile from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="filepath">The path of the file which had been played.</param>
	        /// <see cref="PlayRecordedFile"/>
	        /// <see cref="CompleteCode"/>
            public delegate void PlayRecordFilCompleteEventHandler(CompleteCode code, string filepath) ;


	        /*************************************************************
	         *                  Translation Callbacks
	         *************************************************************/
	        /// <summary>
	        /// Callback after you called SpeechToText, you can get the result of SpeechToText from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="fileID">The ID of the file which had been translated.</param>
	        /// <param name="result">If success, return the translation result, which is a piece of text in a specific language.</param>
	        /// <see cref="SpeechToText"/>
	        /// <see cref="CompleteCode"/>
	        public delegate void SpeechToTextEventHandler(CompleteCode code, string fileID, string result) ;

	        /// <summary>
	        /// Callback after you called StopRecording in RSTT mode, you can get the result of stream speech to text from the parameters.
	        /// </summary>
	        ///
	        /// <param name="code">A CompleteCode code. You should check this first the get the result of successful or not.</param>
	        /// <param name="error">An error code for internal use, you can ignore it.</param>
	        /// <param name="result">If success, return the translation result, which is a piece of text in a specific language.</param>
	        /// <param name="voicePath">The path of the voice file.</param>
	        /// <see cref="CompleteCode"/>
	        public delegate void StreamSpeechToTextEventHandler(CompleteCode code, int error, string result, string voicePath) ;

	        /// <summary>
	        /// Event Callback. e.g. the device connect Event, the device disconcet Event
	        /// </summary>
	        ///
	        /// <param name="code">A event code.</param>
	        /// <param name="info">The event info.</param>
	        /// <see cref="Event"/>
	        public delegate void EventUpdateEventHandler(Event code, string info) ;

	        /// <summary>
	        /// Callback after you called CheckDeviceMuteState, you can get the result of CheckDeviceMuteState from the parameters.
	        /// </summary>
	        ///
	        /// <param name="result">Mute state flag. Non-zero means mute state.</param>
	        /// <see cref="CompleteCode"/>
            public delegate void MuteSwitchResultHandler(int result) ;
            
	        /// <summary>
	        /// callback function @see ReportPlayer
	        /// </summary>
	        /// <param name="nCode">the reported result, 0 means server receive your reporter succ</param>
	        /// <param name="strInfo">the information send to server, json string description, jwt
	        /// <returns></returns>
            public delegate void ReportPlayerHandler(CompleteCode nCode, string strInfo);
            
            /// <summary>
            /// Callback when rec and upload the fileindex for LGameVideo Voice
            /// </summary>
            ///
            public delegate void SaveRecFileIndexEventHandler(CompleteCode code, string fileid, int fileindex);

	        /// @brief Callback function for speech translate
	        ///
	        /// @param nCode, this operation's result @enum CompleteCode.
	        /// @param srcText, text that the source speech file translate to.
	        /// @param targetText, target text that translated from source text.
	        /// @param targetFileID, ID of the target speech file.
	        /// @param srcFileDuration, duration of the source speech file, the unit is milliseconds.
	        public delegate void SpeechTranslateHandler(CompleteCode nCode, string srcText, string targetText, string targetFileID, int srcFileDuration);

	        /// @brief Callback function for speech translate
	        ///
	        /// @param nCode, this operation's result @enum CompleteCode.
	        /// @param srcLang, the speech language of the recorder.
	        /// @param targetLang, target language that we want to translate to.
	        /// @param srcText, text that the source speech file translate to.
	        /// @param targetText, target text that translated from source text.
	        /// @param targetFileID, ID of the target speech file.
	        /// @param srcFileDuration, duration of the source speech file, the unit is milliseconds.
	        public delegate void RSTSHandler(CompleteCode nCode, SpeechLanguageType srcLang, SpeechLanguageType targetLang, string srcText, string targetText, string targetFileID, int srcFileDuration);
            
	        /// @brief Callback function for EnableTranslate
	        ///
	        /// @param code, this operation's result @enum CompleteCode.
	        /// @param roomName, name of the room .
	        /// @param enable, refer to EnableTranslate.
            public delegate void EnableTranslateHandler(CompleteCode code, string roomName, bool enable);
	
        	public abstract event JoinRoomCompleteEventHandler              OnJoinRoomCompleteEvent;
        	public abstract event QuitRoomCompleteEventHandler              OnQuitRoomCompleteEvent;
        	public abstract event MemberVoiceEventHandler                   OnMemberVoiceEvent;
        	public abstract event ApplyMessageKeyCompleteEventHandler       OnApplyMessageKeyCompleteEvent;
        	public abstract event UploadReccordFileCompleteEventHandler     OnUploadReccordFileCompleteEvent;
        	public abstract event DownloadRecordFileCompleteEventHandler    OnDownloadRecordFileCompleteEvent;
        	public abstract event PlayRecordFilCompleteEventHandler         OnPlayRecordFilCompleteEvent;
            public abstract event SpeechToTextEventHandler                  OnSpeechToTextEvent;
            public abstract event StreamSpeechToTextEventHandler            OnStreamSpeechToTextEvent;
        	public abstract event StatusUpdateEventHandler                  OnStatusUpdateEvent;
            public abstract event ChangeRoleCompleteEventHandler            OnRoleChangeCompleteEvent;
            public abstract event RoomMemberVoiceEventHandler               OnRoomMemberVoiceEvent;
            public abstract event EventUpdateEventHandler                   OnEventUpdateEvent;
            public abstract event MuteSwitchResultHandler                   OnMuteSwitchState;
            public abstract event ReportPlayerHandler                       OnReportPlayer;
            public abstract event SaveRecFileIndexEventHandler              OnSaveRecFileIndexEvent;
	        public abstract event RoomMemberChangedCompleteHandler          OnRoomMemberInfo;
	        public abstract event SpeechTranslateHandler                    OnSpeechTranslate;
	        public abstract event RSTSHandler                               OnRSTS;
            public abstract event EnableTranslateHandler                    OnEnableTranslate;
        #endregion

        #region BaseAPI

	        /*************************************************************
	         *                  Basic common APIs
	         *************************************************************/
	        /// <summary>
	        /// Set your app's info such as appID/appKey.
	        ///
	        /// SetAppInfo method should be called after you have gotten the voice engine by GetEngine.
	        /// e.g. GetEngine-->SetAppInfo
	        /// </summary>
	        ///
	        /// <param name="appID">Your game ID from gcloud.qq.com after you have registered.</param>
	        /// <param name="appKey">Your game key from gcloud.qq.com after you have registered.</param>
	        /// <param name="openID">A unique user ID, such as QQ, Wechat or other string which can uniquely identify a user.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo SetAppInfo(string appID, string appKey, string openID);

	        /// <summary>
	        /// Initialize the GCloudVoice engine.
	        ///
	        /// Init method should be called after you have set the app information by SetAppInfo.
	        /// e.g. GetEngine-->SetAppInfo-->Init
	        /// </summary>
	        ///
	        /// <returns> If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo Init();

	        /// <summary>
	        /// Set the mode for the voice engine.
	        ///
	        /// SetMode method should be called after you have initialized the voice engine by Init.
	        /// You should choose an appropriate mode for you application according to the function you need.
	        /// </summary>
	        ///
	        /// <param name="mode">Mode to set. <see cref="Mode"/>
	        /// RealTime:    realtime mode for TeamRoom or NationalRoom
	        /// Messages:    voice message mode
	        /// Translation: speach to text mode</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="Mode"/>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetMode(Mode mode);

	        /// <summary>
	        /// Trigger engine's callback.
	        /// You should invoke poll on your loop periodically, such as Update() in Unity.
	        ///
	        /// Poll method should be called after you have initialized the voice engine by Init.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract  ErrorNo Poll();

	        /// <summary>
	        /// The Application's Pause.
	        /// When your app pause such as goto backend you should invoke this.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo Pause();

	        /// <summary>
	        /// The Application's Resume.
	        /// When your app reuse such as come back from  backend you should invoke this
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo Resume();
        #endregion


	    #region Real-Time Voice APIs
	      /*************************************************************
		   *                  Real-Time Voice APIs
		   *************************************************************/
	        /// <summary>
	        /// Join in a team room.
	        /// Team room function allows no more than 20 members join in the same room to communicate freely.
	        ///
	        /// JoinTeamRoom method should be called after you have set the engine mode to RealTime.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinTeamRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of joining room successful or not can be obtained by the event OnJoinRoom.
	        /// <see cref="OnJoinRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The room to join, should be less than 127byte, composed by alpha.</param>
	        /// <param name="msTimeout">The length of the timeout for joining, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo JoinTeamRoom(string roomName, int msTimeout);

	        /// <summary>
	        /// Join in a LBS team room.
	        /// RangeRoom function allows user to join a LBS room.
	        /// After joined a RangeRoom, the member can hear the members' voice within a specific range.
	        ///
	        /// JoinRangeRoom method should be called after you have set the engine mode to RealTime.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinRangeRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of joining room successful or not can be obtained by the event OnJoinRoom.
	        /// <see cref="OnJoinRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The room to join, should be less than 127byte, composed by alpha.</param>
	        /// <param name="msTimeout">The length of the timeout for joining, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo JoinRangeRoom(string roomName, int msTimeout);

	        /// <summary>
	        /// Join in a national room.
	        /// National room function allows more than 20 members to join in the same room, and they can choose two different roles to be.
	        /// The Anchor role can open microphone to speak and open speaker to listen.
	        /// The Audience role can only open the speaker to listen.
	        ///
	        /// JoinNationalRoom method should be called after you have set the engine mode to RealTime.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinNationalRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of joining room successful or not can be obtained by the event OnJoinRoom.
	        /// <see cref="OnJoinRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The room to join, should be less than 127byte, composed by alpha.</param>
	        /// <param name="role">A MemberRole value illustrates wheather the player can send voice data or not.</param>
	        /// <param name="msTimeout">The length of the timeout for joining, it is a micro second, value range[5000, 60000]</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo JoinNationalRoom(string roomName, MemberRole role, int msTimeout);

	        /// <summary>
	        /// Update your coordinate.
	        ///
	        /// UpdateCoordinate method should be called after the member has joined a RangeRoom successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinRangeRoom
	        /// -->UpdateCoordinate-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <param name="roomName">The room to update, should be less than 127byte, composed by alpha.</param>
	        /// <param name="x">The x coordinate.</param>
	        /// <param name="y">The y coordinate.</param>
	        /// <param name="z">The z coordinate.</param>
	        /// <param name="r">The r coordinate.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo UpdateCoordinate (string roomName, long x, long y, long z, long r);

	        /// <summary>
	        /// Change the member's role in a national room.
	        /// ChangeRole is a function in NationalRoom, so this method should be called after the member has
	        /// joined a NationalRoom successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinNationalRoom
	        /// -->.....-->ChangeRole-->.....-->QuitRoom
	        ///
	        /// The result of changing role successful or not can be obtained by the event OnRoleChanged.
	        /// <see cref="OnRoleChanged"/>
	        /// </summary>
	        ///
	        /// <param name="role">The member's role want to change to.</param>
	        /// <param name="roomName">The name of The room to change role, it should be an exist national room name.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="GCloudVoiceRole"/>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo ChangeRole(MemberRole role, string roomName="");

	        /// <summary>
	        /// Open the player's microphone and begin to send the player's voice data.
	        ///
	        /// OpenMic method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->.....-->OpenMic-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <returns> If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo OpenMic();

	        /// <summary>
	        /// Close the players's microphone and stop sending the player's voice data.
	        ///
	        /// CloseMic method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->.....-->OpenMic-->CloseMic-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <returns> If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo CloseMic();

	        /// <summary>
	        /// Open the player's speaker and begin to recvie voice data from the network.
	        ///
	        /// OpenSpeaker method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->.....-->OpenSpeaker-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo OpenSpeaker();

	        /// <summary>
	        /// Close player's speaker and stop reciving voice data from the network.
	        ///
	        /// CloseSpeaker method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->.....-->OpenSpeaker-->CloseSpeaker-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo CloseSpeaker();

	        /// <summary>
	        /// Quit the voice room.
	        ///
	        /// QuitRoom method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of quiting room successful or not can be obtained by the event OnQuitRoom
	        /// <see cref="OnQuitRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The name of The room to quit, it should be composed by 0-9A-Za-Z._- and less than 127 bytes
	        /// and should be an exist room names.</param>
	        /// <param name="msTimeout">The length of the timeout for quiting, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo QuitRoom(string roomName, int msTimeout);
	    #endregion


	    #region Messages Voice APIs
	        /*************************************************************
             *                 Messages Voice APIs
             *************************************************************/
	        /// <summary>
	        /// Apply the key for voice message.
	        /// In Messages, Translation and RSTT mode, you should first apply the message key before you use the functions.
	        ///
	        /// ApplyMessageKey method should be called after you have set the voice mode to Messages, Translation or RSTT.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey-->...
	        ///
	        /// The result of applying message key successful or not can be obtained by the event OnApplyMessageKey.
	        /// <see cref="OnApplyMessageKey"/>
	        /// </summary>
	        ///
	        /// <param name="msTimeout">The length of the timeout for applying, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo ApplyMessageKey(int msTimeout);

	        /// <summary>
	        /// Limit the maximum last time of a voice message.
	        ///
	        /// SetMaxMessageLength method should be called after you have initialized the voice engine and it is
	        /// a function in Messages, Translation or RSTT mode.
	        /// </summary>
	        ///
	        /// <param name="msTime">The maximum last time of a voice message in a micro second, value range[1000, 2*60*1000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetMaxMessageLength(int msTime);

	        /// <summary>
	        /// Open the player's microphone and record the player's voice.
	        ///
	        /// StartRecording method should be called in Messages, Translation or RSTT mode, and after you have
	        /// applied the message key successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->...
	        /// </summary>
	        ///
	        /// <param name="filePath">The path of the file to store the voice data, the filePath should like:"your_dir/your_file_name"</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo StartRecording(string filePath);

	        /// <summary>
	        /// Stop the player's microphone and stop record the player's voice.
	        ///
	        /// StopRecording method should be called in Messages, Translation or RSTT mode, and after you have
	        /// applied the message key successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->...
	        /// </summary>
	        ///
	        ///<returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo StopRecording();

	        /// <summary>
	        /// Upload the player's voice message file to the network.
	        ///
	        /// UploadRecordedFile method should be called in Messages or Translation mode, and after you have
	        /// recorded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->UploadRecordedFile-->...
	        ///
	        /// The result of uploading recorded file successful or not can be obtained by the event OnUploadFile.
	        /// <see cref="OnUploadFile"/>
	        /// </summary>
	        ///
	        /// <param name="filePath">The path of the voice file to upload, the filePath should like:"your_dir/your_file_name".</param>
	        /// <param name="msTimeout">The length of the timeout for uploading, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo UploadRecordedFile(string filePath, int msTimeout);

	        /// <summary>
	        /// Download other players' voice message from the network.
	        ///
	        /// DownloadRecordedFile method should be called in Messages mode, and after the other member has
	        /// uploaded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->...-->DownloadRecordedFile-->...
	        ///
	        /// The result of downloading recorded file successful or not can be obtained by the event OnDownloadFile.
	        /// <see cref="OnDownloadFile"/>
	        /// </summary>
	        ///
	        /// <param name="fileID">The ID of the file to be downloaded. FileID can be obtained from the callback method OnUploadFile.</param>
	        /// <param name="downloadFilePath">The path of the voice file to download, the filePath should like:"your_dir/your_file_name"</param>
	        /// <param name="msTimeout">The length of the timeout for downloading, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo DownloadRecordedFile(string fileID, string downloadFilePath, int msTimeout);

	        /// <summary>
	        /// Play local voice message file.
	        ///
	        /// PlayRecordedFile method should be called in Messages mode, and after you have
	        /// recorded a voice message successfully or downloaded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->PlayRecordedFile-->...
	        /// or GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->DownloadRecordedFile-->PlayRecordedFile-->...
	        ///
	        /// If the voice file has been played to the end normally, the event OnPlayRecordedFile will be called.
	        /// And if you called StopPlayFile method before the end of the voice file, OnPlayRecordedFile will not be called.
	        /// <see cref="OnPlayRecordedFile"/>
	        /// </summary>
	        ///
	        /// <param name="downloadFilePath">The path of the voice file to play, the filePath should like:"your_dir/your_file_name".</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo PlayRecordedFile (string downloadFilePath);

	        /// <summary>
	        /// Stop playing the voice file.
	        ///
	        /// StopPlayFile method should be called in Messages mode, and before the voice message has been played to the end.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->...-->PlayRecordedFile-->StopPlayFile
	        ///
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo StopPlayFile();


	        /*************************************************************
		     *                 Translation APIs
		     *************************************************************/
	        /// <summary>
	        /// Translate the voice data to a piece of text in a specific language, the default language is Chinese.
	        ///
	        /// SpeechToText method should be called in Translation mode, and after you have
	        /// uploaded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Translation)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->SpeechToText-->...
	        ///
	        /// The result of translating successful or not can be obtained by the event OnSpeechToText.
	        /// <see cref="OnSpeechToText"/>
	        /// </summary>
	        ///
	        /// <param name="fileID">The ID of the file to be translated. FileID can be obtained from the callback method OnUploadFile.</param>
	        /// <param name="language">The specific language to be translated to.</param>
	        /// <param name="msTimeout">The length of the timeout for translating, it is a micro second, value range[5000, 60000].</param>
	        /// <returns> If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="Language"/>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SpeechToText(string fileID, Language language = 0, int msTimeout = 6000);


	        /*************************************************************
		     *                  Token related APIs
		     * Deprecated APIs, please move to the APIs ends with token
		     * in IGCloudVoiceExtension class
		     *************************************************************/
	        /// <summary>
	        /// Join in a team room with token.
	        /// Team room function allows no more than 20 members join in the same room to communicate freely.
	        ///
	        /// JoinTeamRoom method should be called after you have set the engine mode to RealTime.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinTeamRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of joining room successful or not can be obtained by the event OnJoinRoom.
	        /// <see cref="OnJoinRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The name of The room to join, it should be composed by 0-9A-Za-Z._- and less than 127 bytes.</param>
	        /// <param name="token"></param>
	        /// <param name="timestamp"></param>
	        /// <param name="msTimeout">The length of the timeout for joining, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>return GCLOUD_VOICE_SUCC</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo JoinTeamRoom (string roomName, string token, int timestamp, int msTimeout);

	        /// <summary>
	        /// Join in a national room with token.
	        /// National room function allows more than 20 members to join in the same room, and they can choose two different roles to be.
	        /// The Anchor role can open microphone to speak and open speaker to listen.
	        /// The Audience role can only open the speaker to listen.
	        ///
	        /// JoinNationalRoom method should be called after you have set the engine mode to RealTime.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinNationalRoom
	        /// -->.....-->QuitRoom
	        ///
	        /// The result of joining room successful or not can be obtained by the event OnJoinRoom.
	        /// <see cref="OnJoinRoom"/>
	        /// </summary>
	        ///
	        /// <param name="roomName">The room to join, should be less than 127byte, composed by alpha.</param>
	        /// <param name="token"></param>
	        /// <param name="timestamp"></param>
	        /// <param name="role">A MemberRole value illustrates wheather the player can send voice data or not.</param>
	        /// <param name="msTimeout">The length of the timeout for joining, it is a micro second, value range[5000, 60000]</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo JoinNationalRoom (string roomName, string token, int timestamp, MemberRole role, int msTimeout);

	        /// <summary>
	        /// Apply the key for voice message with token.
	        /// In Messages, Translation and RSTT mode, you should first apply the message key before you use the functions.
	        ///
	        /// ApplyMessageKey method should be called after you have set the voice mode to Messages, Translation or RSTT.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey-->...
	        ///
	        /// The result of applying message key successful or not can be obtained by the event OnApplyMessageKey.
	        /// <see cref="OnApplyMessageKey"/>
	        /// </summary>
	        ///
	        /// <param name="token"></param>
	        /// <param name="timestamp"></param>
	        /// <param name="msTimeout">The length of the timeout for applying, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo ApplyMessageKey (string token, int timestamp, int msTimeout);

	        /// <summary>
	        /// Translate the voice data to a piece of text in a specific language with token, the default language is Chinese.
	        ///
	        /// SpeechToText method should be called in Translation mode, and after you have
	        /// uploaded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Translation)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->SpeechToText-->...
	        ///
	        /// The result of translating successful or not can be obtained by the event OnSpeechToText.
	        /// <see cref="OnSpeechToText"/>
	        /// </summary>
	        ///
	        /// <param name="fileID">The ID of the file to be translated. FileID can be obtained from the callback method OnUploadFile.</param>
	        /// <param name="token"></param>
	        /// <param name="timestamp"></param>
	        /// <param name="language">The specific language to be translated to.</param>
	        /// <param name="msTimeout">The length of the timeout for translating, it is a micro second, value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="Language"/>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SpeechToText (string fileID, string token, int timestamp, int language = 0, int msTimeout = 6000);


	        /*************************************************************************
             *                  Multiroom related APIs
             *
             * Multiroom is a function in GVoice real-time mode, it allows a member to join
             * 1~16 room(s) at the same time.
             *
             * The workflow of the Multiroom function:
             * GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->
             * EnableMultiRoom-->JoinTeamRoom/JoinRangeRoom-->EnableRoomMicrophone/
             * EnableRoomSpeaker-->...-->QuitRoom
             *************************************************************************/
	        /// <summary>
	        /// Enable a member to join in multi rooms. Notice that this may cause higher bitrate.
	        ///
	        /// EnableMultiRoom method should be called after you have set the mode to RealTime
	        /// and before you call the JoinXxxRoom method.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->EnableMultiRoom
	        /// -->JoinXxxRoom-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <param name="enable">Enable joining in multi rooms if it is ture, and disable joining in multi rooms if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableMultiRoom(bool enable);

	        /// <summary>
	        /// Open or close the microphone in a specific room in MultiRoom mode.
	        ///
	        /// EnableRoomMicrophone method should be called after the member has joined a voice room in MultiRoom mode successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->EnableMultiRoom(true)
	        /// -->JoinXxxRoom-->EnableRoomMicrophone-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <param name="roomName">The name of The room to enable microphone, it should be an exist room name.</param>
	        /// <param name="enable">Open the microphone in The room if it is true, close the microphone in The room if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableRoomMicrophone(string roomName, bool enable);

	        /// <summary>
	        /// Open or Close the speaker in a specific room in MultiRoom mode.
	        ///
	        /// EnableRoomSpeaker method should be called after the member has joined a voice room in MultiRoom mode successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->EnableMultiRoom(true)
	        /// -->JoinXxxRoom-->EnableRoomMicrophone-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <param name="roomName">The name of The room to enable speaker, it should be an exist room name.</param>
	        /// <param name="enable">Open the speaker in The room if it is true, close the speaker if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableRoomSpeaker(string roomName, bool enable);


	        /*************************************************************************
             *                  BGM related APIs
             *
             * GVoice supports mp3 format background music.
             * The workflow of the BGM function:
             * getInstance-->SetAppInfo-->Init-->EnableNativeBGMPlay-->SetBGMPath
             * -->StartBGMPlay-->PauseBGMPlay-->ResumeBGMPlay-->StopBGMPlay
             *************************************************************************/
	        /// <summary>
	        /// Set The path to a BGM file.
	        /// SetBGMPath method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="path">The path of the BGM file.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetBGMPath(string path);

	        /// <summary>
	        /// Enable or disable the native play mode.
	        /// EnableNativeBGMPlay method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="bEnable">Enable the native play mode if it is true, and disable the native play mode if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableNativeBGMPlay(bool bEnable);

	        /// <summary>
	        /// Start playing the BGM.
	        /// StartBGMPlay method should be called after you have set The path of the BGM file by SetBGMPath method.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo StartBGMPlay();

	        /// <summary>
	        /// Set the play volume of the BGM.
	        /// SetBGMVol method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="vol">The play volume of the BGM, which should be an integer between 0~800.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetBGMVol(int vol);

	        /// <summary>
	        /// Pause the BGM.
	        /// When you want to pause the playing of BGM or when the application paused, you can call PauseBGMPlay
	        /// method to pause the BGM.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo PauseBGMPlay();

	        /// <summary>
	        /// Resume the BGM.
	        /// When you want to resume the playing of BGM after paused it or when the application resumed, you can call
	        /// ResumeBGMPlay method to resume the BGM.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo ResumeBGMPlay();

	        /// <summary>
	        /// Get the state of the BGM.
	        /// If you want to get the playing state of the BGM, you can call GetBGMPlayState method.
	        /// GetBGMPlayState method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract int GetBGMPlayState();

	        /// <summary>
	        /// Stop the BGM.
	        /// StopBGMPlay method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo StopBGMPlay();


	        /*************************************************************
	         *                  Micphone or speaker related APIs
	         *************************************************************/
	        /// <summary>
	        /// Open or close the speaker.
	        /// EnableSpeakerOn method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="bEnable">Open the speaker if it is true and close the speaker if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableSpeakerOn(bool bEnable);

	        /// <summary>
	        /// Set the volume of microphone.
	        /// SetMicVolume method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="vol">The volume to set, for windows platform, the vol should in -1000～1000,
	        /// and in other platforms, the vol should in -150～150.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetMicVolume(int vol);

	        /// <summary>
	        /// Set the sepaker's volume.
	        /// SetSpeakerVolume method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="vol">The volume to set, for windows platform, the vol should in 0～100, and in other platforms,
	        /// the vol should in 0～150, the real volume is equals to (the vol / 100 * the original voice volume).
	        /// If you set the vol to 120, then the real vol is (1.2*the original voice volume).</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetSpeakerVolume(int vol) ;

	        /// <summary>
	        /// Get the microphone's volume.
	        /// GetMicLevel method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>The microphone's volume, if return value>0, means you have said something captured by microphone.</returns>
	        public abstract int GetMicLevel();

	        /// <summary>
	        /// Get the microphone's volume.
	        /// GetMicLevel method should be called after you have initialized the voice engine.
	        /// </summary>
	        /// TODO
	        /// <param name="bFadeOut"></param>
	        /// <returns>The microphone's volume, if return value>0, means you have said something captured by microphone.</returns>
	        public abstract int GetMicLevel(bool bFadeOut);

	        /// <summary>
	        /// Get the speaker's volume.
	        /// GetSpeakerLevel method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>The speaker's volume, the value is equal to the parameter when you call SetSpeakerVolume method.
	        /// </returns>
	        public abstract int GetSpeakerLevel();

	        /// <summary>
	        /// Get the microphone's state, open microphone success, failed or be occupied.
	        /// </summary>
	        ///
	        /// <returns>The microphone's state. -1: microphone is closed; 0: open microphone failed;
	        /// 1: open microphone success; 2: microphone has been occupied.
	        /// </returns>
	        public abstract int GetMicState();

	        /// <summary>
	        /// Get the speaker's state, open speaker success, failed or be occupied.
	        /// </summary>
	        ///
	        /// <returns>The speaker's state. -1: speaker is closed; 0: open speaker failed;
	        /// 1: open speaker success; 2: speaker has been occupied.
	        /// </returns>
	        public abstract int GetSpeakerState();

	        /// <summary>
		    /// Capture microphone audio data.
		    /// </summary>
		    ///
		    /// <param name="bCapture">Start capturing audio data if it is true, stop capturing audio data if it is false.</param>
		    ///
		    public abstract int CaptureMicrophoneData(bool bCapture);

	        /// <summary>
	        /// Test wheather the microphone is available or not.
	        /// Before you want to open microphone, call TestMic method to check whether the microphone is available or not.
	        /// TestMic method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>If microphone device is available, returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo TestMic() ;

	        /// <summary>
	        /// Detect whether the member is speaking or just keep microphone opened.
	        /// IsSpeaking method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>If the member is speaking, returns true, otherwise returns false.</returns>
	        public abstract int IsSpeaking();

	        /// <summary>
	        /// Enable or disable the bluetooth SCO mode. When you want to capture the voice via bluetooth, you can call EnableBluetoothSCO(true).
	        /// EnableBluetoothSCO method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="enable">Enable the bluetooth SCO mode if it is true, and disable the bluetooth SCO mode if it is false.</param>
	        public abstract void EnableBluetoothSCO(bool enable);

	        /// <summary>
	        /// Identify that whether there is any device connected or not.
	        /// </summary>
	        ///
	        /// <returns>0: no audio device connected; 1: a wiredheadset device connected; 2: a bluetooth device connected.</returns>
	        /// <see cref="DeviceState"/>
	        public abstract DeviceState GetAudioDeviceConnectionState();

	        /// <summary>
	        /// Check mute switch state; iPhone is valiable; iOS simulator and android will return non-mute.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="event MuteSwitchResultHandler OnMuteSwitchState"/>
	        /// <see cref="ErrorNo"/>
	        public abstract int CheckDeviceMuteState();

	        /// <summary>
	        /// Get the mute state of the device.
	        /// </summary>
	        ///
	        /// <returns> The device is muted or not. non-zero:mute state; 0: not in mute state; -1:error.
	        /// </returns>
	        public abstract int GetMuteResult();

	    #endregion


	    #region Voice algorithm related APIs
	        /*************************************************************
	         *                  Voice algorithm related APIs
	         *************************************************************/
	        /// <summary>
	        /// This method supports setting sound effect mode.
	        /// SetVoiceEffects method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="mode">The path of the BGM file.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            /// <summary>
            /// Join in a FM room.
            /// </summary>
            /// <param name="roomID">the room to join, should be less than 127byte, composed by alpha.</param>
            /// <param name="msTimeout">time for join, it is micro second. value range[5000, 60000]</param>
            /// <returns>if success return GCLOUD_VOICE_SUCC, failed return other errno @see ErrorNono</returns>
	        public abstract ErrorNo SetVoiceEffects(SoundEffects mode);

	        /// <summary>
	        /// This method supports enabling sound reverb function.
	        /// EnableReverb method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="bEnable">Enable the sound reverb if it is true, and disable the sound reverb if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableReverb(bool bEnable);

	        /// <summary>
	        /// This method supports setting sound reverb mode.
	        /// SetReverbMode method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="mode">The reverb mode which you want to set, the value should in 0~5, and default is 0.
	        /// 0: strong vocal; 1: vocal; 2: small room; 3: large room; 4: church; 5: theater</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetReverbMode(int mode);

	        /// <summary>
	        /// Identify the type of the voice.
	        /// GetVoiceIdentify method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>0: boy's sound; 1: girl's sound; 2: non human sound; -1: error.</returns>
	        public abstract int GetVoiceIdentify();

	        /// <summary>
	        /// Indentify whether there has howling or not.
	        /// GetHwState method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <returns>0: no howling; 1: have howling.</returns>
	        public abstract int GetHwState();
	    #endregion


	    #region Other APIs
	        /*************************************************************
	         *                  Other APIs
	         *************************************************************/
	        /// <summary>
	        /// Set the server's address, only needed for games which published in foreign contries, such as Korea, Europe...
	        ///
	        /// SetServerInfo method should be called before JoinXxxRoom in RealTime mode
	        /// or ApplyMessageKey in Messages, Translation and RSTT mode.
	        /// </summary>
	        ///
	        /// <param name="URL">url of server, you can get the url from gcloud.qq.com after you have registered.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetServerInfo(string URL);

	        /// <summary>
	        /// Set the bit rate of the voice code.
	        /// When you want to change the voice's bit rate, you can call this method.
	        /// SetBitRate method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="bitrate">The bit rate you want to set, it should be an integer between 8~256K.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetBitRate(int bitrate);

	        /// <summary>
	        /// Set if it is datafree.
	        /// SetDataFree method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="enable">Enable datafree if it is true, and disable datafree if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo SetDataFree(bool enable);

	        /// <summary>
	        /// Open Voice Engine's logcat.
	        /// EnableLog method should be called after you have initialized the voice engine.
	        /// </summary>
	        ///
	        /// <param name="enable">Open logcat if it is true, and disable logcate if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo EnableLog(bool enable);

	        /// <summary>
	        /// Join in a FM room.
	        /// </summary>
	        ///
	        /// <param name="roomID">The room to join, should be less than 127byte, composed by alpha.</param>
	        /// <param name="msTimeout">The leangth of timeout for joining, it is a micro second. value range[5000, 60000].</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo JoinFMRoom(string roomID, int msTimeout);

	        /// <summary>
	        /// Set the audience list who can hear, that is, members not in this list can not hear the voice from the members in the same room.
	        /// </summary>
	        ///
	        /// <param name="audience">The IDs of the members who can hear the voice.</param>
	        /// <param name="roomName">The room to set the audience list.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        /// </summary>
	        public abstract int SetAudience(int []audience, string roomName ="" );

	        /// <summary>
	        /// Don't play the member's voice.
	        ///
	        /// ForbidMemberVoice method should be called after the member has joined a voice room successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(RealTime)-->JoinXxxRoom
	        /// -->ForbidMemberVoice-->.....-->QuitRoom
	        /// </summary>
	        ///
	        /// <param name="member">The ID of the member who you want to forbid his voice.</param>
	        /// <param name="bEnable">Forbid the member's voice if it is true, and listen the member's voice if it is false.</param>
	        /// <param name="roomName">The name of The room to forbid member's voice, it should be an exist room name.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo ForbidMemberVoice(int member, bool bEnable, string roomName="");

        #if UNITY_IPHONE    
	        /// <summary>
	        /// Open the player's microphone and record the player's voice.
	        ///
	        /// StartRecording method should be called in Messages, Translation or RSTT mode, and after you have
	        /// applied the message key successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->...
	        /// </summary>
	        ///
	        /// <param name="filePath">The path of the file to store the voice data, the filePath should like:"your_dir/your_file_name".</param>
	        /// <param name="bOptimized">Optimization flag, it means record audio not enter voip mode if it is true, and it means appears the same as before if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo StartRecording(string filePath, bool bOptimized);
        #endif

	        /// <summary>
	        /// Get the voice message's file size and last time.
	        /// </summary>
	        ///
	        /// <param name="filepath">The path of the voice file to get infomation, the filePath should like:"your_dir/your_file_name".</param>
	        /// <param name="bytes">For returning the file's size.</param>
	        /// <param name="seconds">For returning the voice's length.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract ErrorNo GetFileParam(string filepath, int [] bytes, float [] seconds);

	        /// <summary>
	        /// It is not recommended to call this method.
	        /// If you want to use this, please contact with the GVoice team.
	        /// </summary>
	        public abstract int invoke( uint nCmd, uint nParam1, uint nParam2, uint [] pOutput );

	        /// <summary>
	        /// Get the GVoice engine pointer for GFM.
	        /// </summary>
	        ///
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
	        public abstract IntPtr GetExtensionPluginContext();
	    #endregion

	    #region overload
	        /// <summary>
	        /// Upload the player's voice message file to the network.
	        /// It is not recommended to call this method.
	        ///
	        /// UploadRecordedFile method should be called in Messages or Translation mode, and after you have
	        /// recorded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->StartRecording-->StopRecording-->UploadRecordedFile-->...
	        ///
	        /// The result of uploading recorded file successful or not can be obtained by the event OnUploadFile.
	        /// <see cref="OnUploadFile"/>
	        /// </summary>
	        ///
	        /// <param name="filePath">The path of the voice file to upload, the filePath should like:"your_dir/your_file_name".</param>
	        /// <param name="msTimeout">The length of the timeout for uploading, it is a micro second, value range[5000, 60000].</param>
	        /// <param name="bPermanent">Upload a permanent voice file if it is true, and upload a normal voice file if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo UploadRecordedFile(string filePath, int msTimeout, bool bPermanent);

	        /// <summary>
	        /// Download other players' voice message from the network.
	        /// It is not recommended to call this method.
	        ///
	        /// DownloadRecordedFile method should be called in Messages mode, and after the other member has
	        /// uploaded a voice message successfully.
	        /// e.g. GetEngine-->SetAppInfo-->Init-->Poll-->SetMode(Messages)-->ApplyMessageKey
	        /// -->...-->DownloadRecordedFile-->...
	        ///
	        /// The result of downloading recorded file successful or not can be obtained by the event OnDownloadFile.
	        /// <see cref="OnDownloadFile"/>
	        /// </summary>
	        ///
	        /// <param name="fileID">The ID of the file to be downloaded. FileID can be obtained from the callback method OnUploadFile.</param>
	        /// <param name="downloadFilePath">The path of the voice file to download, the filePath should like:"your_dir/your_file_name".</param>
	        /// <param name="msTimeout">The length of the timeout for downloading, it is a micro second, value range[5000, 60000].</param>
	        /// <param name="bPermanent">Download a permanent voice file if it is true, and download a normal voice file if it is false.</param>
	        /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
	        /// <see cref="ErrorNo"/>
            public abstract ErrorNo DownloadRecordedFile(string fileID, string downloadFilePath, int msTimeout, bool bPermanent);
	    #endregion


	    #region Voice report related APIs
	        /*************************************************************
             *                  Voice report related APIs
             *************************************************************/
            /// Set the buffered voice size in GVoice when use voice report function, the default value is 20s.
            ///
            /// <param name="nTimeSec">How many seconds you want GVoice to buffer</param>
            public abstract ErrorNo SetReportBufferTime(int nTimeSec);

            /// @brief set players information that may be reported by yourself
            ///
            /// @param[in] nResult the reported result, 0 means server receive your reporter succ
            /// @brief Report a uncivilized player in your game
            ///
            /// <param name="cszOpenID">All players openid you may report</param>
            /// <param name="nMemberID">All players memberid you may report</param>
            /// <param name="nCount">The count of members you may report, it should be equals to the length of cszOpenID and nMemberID</param>
            ///
            /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
            /// <see cref="ErrorNo"/>
            public abstract ErrorNo SetReportedPlayerInfo(string[] cszOpenID, int[] nMemberID, int nCount);

            /// @brief Report uncivilized players in your game
            ///
            /// <param name="cszOpenID">All players openid you may report, null will report all the players you set @see SetReportedPlayerInfo</param>
            /// <param name="nCount">Element count in array, 0 will report all the players you set @see SetReportedPlayerInfo</param>
            /// <param name="strInfo">Information will be send to server</param>
            ///
            /// <returns>If success returns GCLOUD_VOICE_SUCC, otherwise returns other errno.</returns>
            /// <see cref="ErrorNo"/>
            public abstract ErrorNo ReportPlayer(string[] cszOpenID, int nCount, string strInfo);
	    #endregion


	    #region LGame rec related APIs
	        /*************************************************************
             *                  LGame rec related APIs
             *************************************************************/
            public abstract ErrorNo StartSaveVoice();

            public abstract ErrorNo StopSaveVoice();

            public abstract ErrorNo SetRecSaveTs(int ts);

            public abstract ErrorNo SetPlayFileIndex(string fileid, int fileindex);

            public abstract ErrorNo StartPlaySaveVoiceTs(int ts);

            public abstract ErrorNo StopPlaySaveVoice();
            
            public abstract ErrorNo DelAllSaveVoiceFile(string fileid, int fileindex);
	    #endregion


		#region Other APIs
	    /*************************************************************
         *                  Other APIs
         *************************************************************/
		/*
		*set civil voice source path
		*param:[in]path :bin absolutive path
		*return:<see cref="ErrorNo"/>
		*/
		public abstract ErrorNo SetCivilBinPath(string path);
		
		/*
		*set Player's volume by playerid which value set by SetAppInfo's Openid
		*param:[in]playerid :the playerid who you want to set his volume
		*param:[in]vol: the volume value range[0-100]
		*return:<see cref="ErrorNo"/>
		*/
		public abstract ErrorNo SetPlayerVolume(string playerid, uint vol);
		
		/*
		*Get Player's volume
		*param:[in]playerid :the playerid's volume value,which set by SetPlayerVolume API, default value is 100.
		*return:the playerid's volume value [0-100], default value is 100.
		*/
		public abstract int GetPlayerVolume(string playerid);

		/*
		*enable key words detect
		*param:[in]bEnable : true enable,false disable
		*return:<see cref="ErrorNo"/>
		*/
		public abstract ErrorNo EnableKeyWordsDetect(bool bEnable);
	     
	    #endregion


	        public abstract int GetRoomMembers(string roomName, RoomMembers[] members, int len);

	        public abstract int EnableCivilVoice(bool bEnable);


	    #region Speech translate related APIs
	        /*************************************************************
             *                  Speech translate related APIs
             *************************************************************/
	        public abstract int SpeechTranslate(string fileID, SpeechLanguageType srcLang, SpeechLanguageType targetLang, SpeechTranslateType transType, int nTimeoutMS=10000);

	        public abstract int RSTSStartRecording(SpeechLanguageType srcLang, SpeechLanguageType[] pTargetLangs, int nTargetLangCnt, SpeechTranslateType transType, int nTimeoutMS = 5000);

	        public abstract int RSTSStopRecording();
            public abstract int EnableTranslate(string roomname, bool enable, SpeechLanguageType srcLang);
	    #endregion
        }

    } // endof GVoice
}//end GCloud

