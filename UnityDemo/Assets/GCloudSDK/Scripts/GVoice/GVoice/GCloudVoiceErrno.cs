


namespace GCloud
{

	namespace GVoice 
	{
		/**
         * Mode of voice engine.
         *
         * You should set to one first.
         */
		public enum Mode
		{
			Unknown = -1,
			RealTime = 0,    // realtime mode for TeamRoom, NationalRoom, RangeRoom
			Messages = 1,        // voice message mode
			Translation = 2,     // speach to text mode
			RSTT = 3,            // real-time speach to text mode
			HIGHQUALITY = 4,     // high quality realtime voice mode[deprecated], will cost more network traffic
			RSTS = 5,            // real-time source speach to target text/speach mode
		};

		/**
         * Member's role for National Room.
         */
		public enum MemberRole
		{
			Anchor = 1, // member who can open microphone and say
			Audience = 2,   // member who can only hear anchor's voice
		};

		/**
         * Destination language to translate to.
         */
		public enum Language {
			China       = 0,
			Korean      = 1,
			English     = 2,
			Japanese    = 3,
		};

		/**
         * Mode of voice effects.
         *
         * You should set to one first.
         */
		public enum SoundEffects
		{
			RevbChurch     = 0,
			RevbTheater    = 1,
			Hell           = 2,
			Robot1         = 3,
			MaleToFemale   = 4,
			FemaleToMale   = 5,
			Drunk          = 6,
			PapiJiang      = 7,
			Squirrel       = 8,
			NoEffect       = 9,
		};

		/**
         * Speech Translation languages
         */
		public enum SpeechLanguageType
		{
			SPEECH_LANGUAGE_ZH = 0,		// Chinese
			SPEECH_LANGUAGE_EN = 1,		// English
			SPEECH_LANGUAGE_JA = 2,		// Japanese
			SPEECH_LANGUAGE_KO = 3,		// Korean
			SPEECH_LANGUAGE_DE = 4,		// German
			SPEECH_LANGUAGE_FR = 5,		// French
			SPEECH_LANGUAGE_ES = 6,		// Spanish
			SPEECH_LANGUAGE_IT = 7,		// Italian
			SPEECH_LANGUAGE_TR = 8,		// Turkish
			SPEECH_LANGUAGE_RU = 9,		// Russian
			SPEECH_LANGUAGE_PT = 10,	// Portuguese
			SPEECH_LANGUAGE_VI = 11,	// Vietnamese
			SPEECH_LANGUAGE_ID = 12,	// Indonesian
			SPEECH_LANGUAGE_MS = 13,	// Malaysian
			SPEECH_LANGUAGE_TH = 14,	// Thai
		};

		/**
         * Speech Translation type, pip nodes: Source Speech -> Source Text -> Target Text -> Target Speech
         */
		public enum SpeechTranslateType
		{
			SPEECH_TRANSLATE_STST = 0,	//Source Speech -> Source Text
			SPEECH_TRANSLATE_STTT = 1,  //Source Speech -> Source Text -> Target Text
			SPEECH_TRANSLATE_STTS = 2,	//Source Speech -> Source Text -> Target Text -> Target Speech
		};

	    /**
         * The return error number when call GVoice api.
         */
        public enum ErrorNo
        {
            Succ                    = 0,        //0, no error

            //common base err
            ParamNULL               = 0x1001,	//4097, some param is null
            NeedSetAppInfo          = 0x1002,	//4098, you should call SetAppInfo first before call other api
            InitErr                 = 0x1003,	//4099, Init Erro
            RecordingErr            = 0x1004,	//4100, now is recording, can't do other operator
            PollBuffErr             = 0x1005,	//4101, poll buffer is not enough or null
            ModeStateErr            = 0x1006,	//4102, call some api, but the mode is not correct, maybe you shoud call SetMode first and correct
            ParamInvalid            = 0x1007,	//4103, some param is null or value is invalid for our request, used right param and make sure is value range is correct by our comment
            OpenFileErr             = 0x1008,   //4104, open a file err
            NeedInit                = 0x1009,   //4105, you should call Init before do this operator
            EngineErr               = 0x100A,   //4106, you have not get engine instance, this common in use c# api, but not get gcloudvoice instance first
            PollMsgParseErr         = 0x100B,   //4107, this common in c# api, parse poll msg err
            PollMsgNo               = 0x100C,   //4108, poll, no msg to update


            //realtime err
            RealtimeStateErr        = 0x2001,   // 8193, call some realtime api, but state err, such as OpenMic but you have not Join Room first
            JoinErr                 = 0x2002,   // 8194, join room failed
            QuitRoomNameErr         = 0x2003,   // 8195, quit room err, the quit roomname not equal join roomname
            OpenMicNotAnchorErr     = 0x2004,   // 8196, open mic in bigroom,but not anchor role
            CreateRoomErr           = 0x2005,   // 8197, create room error
            NoRoom                  = 0x2006,   // 8198, no such room
            QuitRoomErr             = 0x2007,   // 8199, quit room error
            AlreadyInTheRoom        = 0x2008,   // 8200, already in the room which in JoinXxxxRoom

            //message err
            AuthKeyErr              = 0x3001,   // 12289, apply authkey api error
            PathAccessErr           = 0x3002,   // 12290, the path can not access ,may be path file not exists or deny to access
            PermissionMicErr        = 0x3003,	// 12291, you have not right to access micphone in android
            NeedAuthKey             = 0x3004,	// 12292,you have not get authkey, call ApplyMessageKey first
            UploadErr               = 0x3005,	// 12293, upload file err
            HttpBusy                = 0x3006,	// 12294, http is busy,maybe the last upload/download not finish.
            DownloadErr             = 0x3007,	// 12295, download file err
            SpeakerErr              = 0x3008,   // 12296, open or close speaker tve error
            TVEPlaySoundErr         = 0x3009,   // 12297, tve play file error
            Authing                 = 0x300a,   // 12298, Already in applying auth key processing
            Limit                   = 0x300b,   // 12299, upload limit
            NothingToReport         = 0x300c,   // 12300, no sound to report

            InternalTVEErr          = 0x5001,	// 20481, internal TVE err, our used
            InternalVisitErr        = 0x5002,	// 20482, internal Not TVE err, out used
            InternalUsed            = 0x5003,   // 20483, internal used, you should not get this err num
            
            BadServer               = 0x06001,  // 24577, bad server address,should be "udp://capi.xxx.xxx.com"
            
            STTing                  = 0x07001,  // 28673, Already in speach to text processing
            
            ChangeRole              = 0x08001,  // 32769, change role error
            ChangingRole            = 0x08002,  // 32770, is in changing role
            NotInRoom               = 0x08003,  // 32771, no in room
            Coordinate              = 0x09001,  // 36865, sync coordinate error
            SmallRoomName           = 0x09002,  // 36866, query with a small roomname
	        CoordinateRoomNameErr   = 0x09003,  // 36867, update coordinate in a non-exist room

            SaveDataDownloading     = 0x0A001,  // 40961, dowloading file for lgame save voice data, need no nothing, just let userinterface know.
            SaveDataIndexNotFound   = 0x0A002,  // 40962, this file index not found in file map ,may not set ,have not in this video
        };


	    /**
         * The complete code number in the recall method of GVoice.
         */
        public enum CompleteCode
        {
            // common code
            NetErr                       = 0x1001,   // 4097，network error, maybe can't connect to network
            Unknown                      = 0x1002,   // 4098


            // realtime code
            JoinRoomSucc                 = 0x2001,   // 8193, join room success
            JoinRoomTimeout              = 0x2002,   // 8194, join room timeout
            JoinRoomSVRErr               = 0x2003,   // 8195, communication with svr meets some error, such as wrong data received from svr
            JoinRoomUnknown              = 0x2004,   // 8196, reserved, GVoice internal unknown error
            JoinRoomRetryFail            = 0x2005,   // 8197, join room try again fail
            QuitRoomSucc                 = 0x2006,   // 8198, quitroom success, if you have joined room success first, quit room will alway return success
            RoomOffline                  = 0x2007,   // 8199, dropped from the room
            RoleSucc                     = 0x2008,   // 8200, change role success
            RoleTimeout                  = 0x2009,   // 8201, change role timeout
            RoleMaxAnchor                = 0x2010,   // 8202, too many anchors, no more than 5 anchors in the same time are allowed in a national room
            RoleNoChange                 = 0x2011,   // 8203, the same role as before
            RoleSvrErr                   = 0x2012,   // 8204, server's error in change role
            
            
            // message mode
            MessageKeyAppliedSucc        = 0x3001,   // 12289, apply message authkey succ
            MessageKeyAppliedTimeout     = 0x3002,   // 12290, apply message authkey timeout
            MessageKeyAppliedSVRErr      = 0x3003,   // 12291, communication with svr meets some error, such as wrong data received,
            MessageKeyAppliedUnknown     = 0x3004,   // 12292, reserved, GVoice internal unknown error
            UploadRecordDone             = 0x3005,   // 12293, upload record file success
            UploadRecordError            = 0x3006,   // 12294, upload record file meets some error
            DownloadRecordDone           = 0x3007,   // 12295, download record file success
            DownloadRecordError          = 0x3008,   // 12296, download record file meets some error
            PlayFileDone                 = 0x3009,   // 12297, the record file have played to the end

            
            // translate mode
            STTSucc                      = 0x4001,   // 16385, speech to text success
            STTTimeout                   = 0x4002,   // 16386, speech to text timeout
            STTAPIErr                    = 0x4003,   // 16387, server's error
            
            
            // rstt mode
            RSTTSucc                     = 0x5001,   // 20481, stream speech to text success
            RSTTTimeout                  = 0x5002,   // 20482, stream speech to text timeout
            RSTTAPIErr                   = 0x5003,   // 20483, server's error in stream speech to text
            RSTTRetry                    = 0x5004,   // 20484, need retry stt

            
            // voice report
            ReportSucc                   = 0x6001,   // 24577, report other player succ
            DataError                    = 0x6002,   // 24578, receive illegal or invalid data from serve
            Punished                     = 0x6003,   // 24579, the player is punished because of being reported
            NotPunished                  = 0x6004,   // 24580, the player
            KeyDeleted                   = 0x6005,
            
            // for LGame
            SaveDataSucc                 = 0x7001,   // 28673, LGAME Save RecData


            // for member info
	        RoomMemberInRoom             = 0x8001,   // 32769, member join or in room
	        RoomMemberOutRoom            = 0x8002,   // 32770, member out of room


            // for civil voice
            UploadReportInfoErr          = 0x9001,   // 36865, civilized voice reporting error
	        UploadReportInfoTimeout      = 0x9002,   // 36866, civilized voice reporting timeout


            // for speech translate
            StSucc                       = 0xA001,   // 40961, speech translate success
	        StHttpErr                    = 0xA002,   // 40962, http failed
	        StServerErr                  = 0xA003,   // 40963, server error
	        StInvalidJson                = 0xA004,   // 40964, parse rsp json faild
			
			//for realtime translate
			RtTranslateSucc              = 0xC001,   // 49153, realtime enable translate ok
			RtTranslateServerErr         = 0xC002,   // 49154, realtime enable translate server error
        };
    
        /**
         * Event of GCloudVoice.
         *
         */
        public enum Event
        {
            NoDeviceConnected             = 0,      // not any device is connected
            HeadsetDisconnected           = 10,     // a headset device is connected
            HeadsetConnected              = 11,     // a headset device is disconnected
            BluetoothHeadsetDisconnected  = 20,     // a bluetooth device is connected
            BluetoothHeadsetConnected     = 21,     // a bluetooth device is disconnected
            MicStateOpenSucc              = 30,     // open microphone
            MicStateOpenErr               = 31,     // open microphone
            MicStateNoOpen                = 32,     // close micrphone
	        MicStateOccupancy             = 33,     //indicates the microphone has been occupancyed by others
            SpeakerStateOpenSucc          = 40,     // open speaker
            SpeakerStateOpenErr           = 41,     // open speaker error
            SpeakerStateNoOpen            = 42,     // close speaker
	        AudioInterruptBegin           = 50,     //audio device begin to be interrupted
	        AudioInterruptEnd             = 51,     //audio device end to be interrupted
	        AudioRecorderException        = 52,     //indicates the recorder thread throws a exception, maybe you can resume the audio
	        AudioRenderException          = 53,     //indicates the render thread throws a exception, maybe you can resume the audio
	        PhoneCallPickUp               = 54,     //indicates that you picked up the phone
	        PhoneCallHangUp               = 55,     //indicates that you hanged up the phone
        };
    
        /**
         * Event of GCloudVoice.
         *
         */
        public enum DeviceState
        {
            Unconnected             = 0, // not any audio device is connected
            WriteHeadsetConnected   = 1, // have a wiredheadset device is connected
            BluetoothConnected      = 2, // have a bluetooth device is disconnected
        };
	}
}
