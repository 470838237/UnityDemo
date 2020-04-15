using UnityEngine;
using System;
using System.Collections.Generic;

using GCloud;
namespace GCloud.LockStep
{
	
	public delegate void LoginEventHandler(LockStepResult result);
	public delegate void LogoutEventHandler(LockStepResult result);
	public delegate void ReadyEventHandler(LockStepResult result);
	public delegate void RecvedFrameEventHandler(int numberOfReceivedFrames);
	public delegate void StateChangedEventHandler(LockStepState state, LockStepResult result);
	public delegate void BroadcastEventHandler(FrameCollection frames);
	public abstract class LockSteper : ApolloObject{
		/// <summary>
		/// triggered when error occur.
		/// </summary>
		public abstract event StateChangedEventHandler StateChangedEvent;

		/// <summary>
		/// triggered when other players send broadcast
		/// </summary>
		public abstract event BroadcastEventHandler BroadcastEvent;

		/// <summary>
		/// Get the current PlayerID
		/// </summary>
		/// <value>PlayerID</value>
		public abstract UInt32 PlayerID{get;}

        /// <summary>
        /// Tell GameClient whether the LockStep(Client) has logged into the LockStep Server
        /// </summary>
		public abstract bool HasLogined{get;}

        /// <summary>
        /// Tell GameClient whether the battle has started
        /// </summary>
		public abstract bool HasStarted{get;}

        #region Common
        /// <summary>
        /// Tell the LockStep status.
        /// </summary>
        public abstract LockStepStatus GetStatus();

        /// <summary>
        /// Initialize the specified initInfo.
        /// </summary>
        /// <param name="initInfo">Init info.</param>
        public abstract bool Initialize(LockStepInitializeInfo initInfo);

		/// <summary>
		/// Login to the LockStep Server
		/// </summary>
		/// <param name="accessInfo">Game Server must pass accessInfo, which is created by LockStep Server, to the Game Client</param>
		/// <param name="callback">Callback.</param>
		public abstract void Login(byte[] accessInfo, LoginEventHandler callback);

		/// <summary>
		/// Logout the LockStep System, including clear the cache, and disconnect from LockStep Server
		/// </summary>
		/// <param name="callback">Callback.</param>
		public abstract void Logout(LogoutEventHandler callback);

		/// <summary>
		/// Tell the LockStep Server, you'v been ready to play the game
		/// </summary>
		/// <param name="callback">Callback.</param>
		public abstract void Ready(ReadyEventHandler callback);

        /// <summary>
        /// When automatic reconnection fails, call this function to reconnect manually.
        /// </summary>
        public abstract bool ReconnectManually();
        #endregion

		#region LockStep
		/// <summary>
		/// Send user input to the Lockstep server
		/// </summary>
		/// <param name="data">Data.</param>
		/// <param name="rawUdp">If set to <c>true</c>, it will send through raw UDP, or data will be sent by reliable udp.</param>
		public abstract bool Input(byte[] data, bool rawUdp = true, LockStepInputFlag flag = LockStepInputFlag.None);
		public abstract bool Input(byte[] data, int len, bool rawUdp = true, LockStepInputFlag flag = LockStepInputFlag.None);

		/// <summary>
		/// pop up the next frame data in the cache
		/// </summary>
		/// <returns>a frame info.</returns>
		public abstract  FrameInfo PopFrame();
        public abstract bool ReadFrame(FrameInfo frame);

		#endregion

		#region Broadcast
		/// <summary>
		/// Sends the broadcast.
		/// </summary>
		/// <returns><c>true</c>, if broadcast was sent, <c>false</c> otherwise.</returns>
		/// <param name="data">Data.</param>
		/// <param name="rawUdp">If set to <c>true</c> raw UDP.</param>
		public abstract bool SendBroadcast(byte[] data, int len = -1, bool rawUdp = true, LockStepBroadcastFlag flag = LockStepBroadcastFlag.None);
	

		#endregion


		/// <summary>
		/// Gets the current max frame identifier.
		/// </summary>
		/// <returns>The current max frame identifier.</returns>
		public abstract int GetCurrentMaxFrameId();

		/// <summary>
		/// Sets the start point.
		/// </summary>
		/// <param name="frameId">Frame identifier.</param>
		public abstract void SetStartFrame(int frameId);

		public abstract void GetSpeed(ref int udp, ref int tcp);

        public abstract bool SetUserData(string key, string value);

        /// <summary>
        /// Notify Lockstep SDK that the game is about to start loading resources.
        /// </summary>
        public abstract void NotifyLoadingBegin();

        /// <summary>
        /// Notify Lockstep SDK that the resource loading is complete.
        /// </summary>
        public abstract void NotifyLoadingEnd();

        #region Others
        static private LockSteper instance = null;
		static public LockSteper Instance
		{
			get
			{
				if(instance == null)
				{
					instance = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("GCloud.LockStep.LockStepImpl") as LockSteper;
					
					if(instance == null)
					{
						ADebug.LogError("CreateInstance LockStepImpl instance failed...");
					}
				}
				
				return instance;
			}
		}
		#endregion
	}

	[Flags]
	public enum LockStepManualUpdateOption
	{
        NotSet = 0x0,
		UpdateCallback = 0x01,
		UpdateLoop = 0x02,
		UpdateAll = UpdateCallback | UpdateLoop,
	};

	
	public class LockStepInitializeInfo : ApolloBufferBase
	{
		public int MaxBufferSize;
		public int StartFrameId = 0;
        public LockStepManualUpdateOption ManualUpdateOption;
        public bool IsTcpCritical;
        public int MaxHistorySize;
			
		public LockStepInitializeInfo()
		{
			MaxBufferSize = 1024*100;
            MaxHistorySize = 0;
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(MaxBufferSize);
			writer.Write(StartFrameId);
			writer.Write((int)ManualUpdateOption);
			writer.Write(IsTcpCritical);
            writer.Write(MaxHistorySize);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref MaxBufferSize);
			reader.Read(ref StartFrameId);
			int tmp = 0;
			reader.Read(ref tmp);
			ManualUpdateOption = (LockStepManualUpdateOption)tmp;
			reader.Read(ref IsTcpCritical);
            reader.Read(ref MaxHistorySize);
		}
	}

    // Uninitialized => Idle => Logining => Logined => Logouting => Idle
    public enum LockStepStatus
    {
        Uninitialized = 0,
        Idle = 1,
        Logining = 2,
        Logined = 3,
        Logouting = 4,
    }

	[Flags]
	public enum LockStepInputFlag
	{
		None = 0,
		Subscribe = 0x01,
		DuplicateUpstream = 0x10,
	}
	
	[Flags]
	public enum LockStepBroadcastFlag
	{
		None = 0,
		Subscribe = 0x01,
		GameServerOnly = 0x02,
		DuplicateUpstream = 0x10,
		DuplicateDownstream = 0x20,
		DownstreamReliable = 0x40,
	}


	public enum LockStepErrorCode
	{
		Success,
		ConnectFailed,
		NetowrkException,
		Timeout,
		Unknown,

		SessionStop,
		ServerFull,
		StayInQueue,
		
		RoomNotFound,
		UserNotFound,

		AuthFailed,
        LogoutNotFinished,
        Uninitialized,
        LastLoginNotFinished,
        AlreadyLogged,

		BusinessError = 100,
	}

	public class LockStepResult : ApolloBufferBase
	{
		public LockStepErrorCode Error;
		public string Reason;
		public int ExtCode;
		public int ExtCode2;

		public bool IsSuccess()
		{
			return Error == LockStepErrorCode.Success;
		}

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(Error);
			writer.Write(Reason);
			writer.Write(ExtCode);
			writer.Write(ExtCode2);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref Error);
			reader.Read(ref Reason);
			reader.Read(ref ExtCode);
			reader.Read(ref ExtCode2);
		}

		public override string ToString ()
		{
			return Error + ", " + ExtCode + ", " + ExtCode2 + ", " + Reason;
		}
	}

	[FlagsAttribute]
	public enum RelayDataFlag
	{
		PlayerStatus = 0x01,
	}
	
	public class RelayData : ApolloBufferBase
	{
		public Int32 PlayerId;
		public RelayDataFlag Flag = 0;
        public Int32 DelayMS = 0;
        byte[] data = new byte[1024];
		private int dataLength;
        public Int32 SequenceId;
		
		public RelayData Clone()
		{
			RelayData clone = new RelayData();
			clone.PlayerId = PlayerId;
			clone.Flag = Flag;
            clone.DelayMS = DelayMS;
			clone.dataLength = dataLength;
			Array.Copy(data, clone.data, dataLength);
            clone.SequenceId = SequenceId;
			return clone;
		}

        public void CopyTo(RelayData dest)
        {
            dest.PlayerId = PlayerId;
            dest.Flag = Flag;
            dest.DelayMS = DelayMS;
            dest.dataLength = dataLength;
            Array.Copy(data, dest.data, dataLength);
            dest.SequenceId = SequenceId;
        }
        public int PeekData(ref byte[] buffer)
        {
            if(buffer == null)
            {
                return 0;
            }

            if(dataLength > 0)
            {
                Array.Copy(data, buffer, dataLength);
            }
            return dataLength;
        }

		public bool IsPlayerOnline()
		{
			return (Flag & RelayDataFlag.PlayerStatus) == RelayDataFlag.PlayerStatus;
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
            //throw new Exception("Send with RelayData is not supported");
			writer.Write(PlayerId);
			writer.Write((int)Flag);
			writer.Write(DelayMS);
            if (dataLength == 0)
            {
                writer.Write(null, 0);
            }
            else
            {
                writer.Write(data, dataLength);
            }
			writer.Write(SequenceId);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref PlayerId);
            int temp = 0;
			reader.Read(ref temp);
            Flag = (RelayDataFlag)temp;
			reader.Read(ref DelayMS);
            dataLength = reader.ReadArray(data);
			reader.Read(ref SequenceId);
		}
	};
	
	public class FrameInfo : ApolloBufferBase
	{
		public Int32 FrameId;
		public Int32 RecvTickMS;
        public Int32 ValidDataCount;
		public List<RelayData> DataCollection = new List<RelayData>();

        public void Clear()
        {
            FrameId = 0;
            RecvTickMS = 0;
            ValidDataCount = 0;
            DataCollection.Clear();
        }

		public FrameInfo Clone()
		{
			FrameInfo clone = new FrameInfo();
			clone.FrameId = FrameId;
            clone.RecvTickMS = RecvTickMS;
			for(int i = 0; i < DataCollection.Count; i++)
			{
				RelayData data = DataCollection[i];
				if(data != null)
				{
					clone.DataCollection.Add(data.Clone());
				}
			}
            clone.ValidDataCount = ValidDataCount;
			return clone;
		}

		public void CopyTo(FrameInfo dest)
		{
			if(dest == null)
			{
				return;
			}
			
			dest.FrameId = FrameId;
            dest.RecvTickMS = RecvTickMS;
            dest.ValidDataCount = ValidDataCount;
			
			if(dest.DataCollection == null)
			{
				dest.DataCollection = new List<RelayData>();
			}

            while(ValidDataCount > dest.DataCollection.Count)
            {
                dest.DataCollection.Add(new RelayData());
            }
            for(int i=0; i < ValidDataCount; i++)
			{
				RelayData data = DataCollection[i];

				if(data != null)
				{
                    data.CopyTo(dest.DataCollection[i]);
				}
			}
		}

        public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(FrameId);
            writer.Write(RecvTickMS);
            writer.Write(DataCollection, ValidDataCount);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref FrameId);
            reader.Read(ref RecvTickMS);
            reader.Read(ref ValidDataCount);

            while (ValidDataCount > DataCollection.Count)
            {
                DataCollection.Add(new RelayData());
            }

            int count = reader.ReadList(DataCollection, ValidDataCount);
            ValidDataCount = count < ValidDataCount ? count : ValidDataCount;
		}

	};
	
	
	public class FrameCollection : ApolloBufferBase
	{
		public List<FrameInfo> Frames;
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(Frames);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref Frames);
		}
		
	};

    public class FrameProfile : ApolloBufferBase
    {
        public uint frameID;
        public bool alreadyPeeked;
        public byte inputCount;
        public uint minInputSeqID;
        public uint maxInputSeqID;
        public uint lostInputCount;
        public uint lostFrameCount;

        public int inputIntervalMS;
        public int recvIntervalMS;
        public int peekIntervalMS;

        public UInt16 recvTimeMS;
        public UInt16 peekTimeMS;

        public UInt16 inputQueueLen;
        public UInt16 inputQueueValidLen;
        public UInt16 recvQueueLen;
        public UInt16 recvQueueValidLen;
        public UInt16 peekQueueLen;
        public UInt16 peekQueueValidLen;

        public UInt16 inputSpeedMS;
        public UInt16 recvSpeedMS;

        public override void WriteTo(ApolloBufferWriter writer)
        {
            writer.Write(frameID);
            writer.Write(alreadyPeeked);
            writer.Write(inputCount);
            writer.Write(minInputSeqID);
            writer.Write(maxInputSeqID);
            writer.Write(lostInputCount);
            writer.Write(lostFrameCount);

            writer.Write(inputIntervalMS);
            writer.Write(recvIntervalMS);
            writer.Write(peekIntervalMS);

            writer.Write(recvTimeMS);
            writer.Write(peekTimeMS);

            writer.Write(inputQueueLen);
            writer.Write(inputQueueValidLen);
            writer.Write(recvQueueLen);
            writer.Write(recvQueueValidLen);
            writer.Write(peekQueueLen);
            writer.Write(peekQueueValidLen);

            writer.Write(inputSpeedMS);
            writer.Write(recvSpeedMS);
        }

        public override void ReadFrom(ApolloBufferReader reader)
        {
            reader.Read(ref frameID);
            reader.Read(ref alreadyPeeked);
            reader.Read(ref inputCount);
            reader.Read(ref minInputSeqID);
            reader.Read(ref maxInputSeqID);
            reader.Read(ref lostInputCount);
            reader.Read(ref lostFrameCount);

            reader.Read(ref inputIntervalMS);
            reader.Read(ref recvIntervalMS);
            reader.Read(ref peekIntervalMS);

            reader.Read(ref recvTimeMS);
            reader.Read(ref peekTimeMS);

            reader.Read(ref inputQueueLen);
            reader.Read(ref inputQueueValidLen);
            reader.Read(ref recvQueueLen);
            reader.Read(ref recvQueueValidLen);
            reader.Read(ref peekQueueLen);
            reader.Read(ref peekQueueValidLen);

            reader.Read(ref inputSpeedMS);
            reader.Read(ref recvSpeedMS);
        }
    };

	public enum LockStepState
	{
		Fighting,
		Reconnecting, // Reconnecting to the server
		Reconnected,
		StayInQueue, // In queue
		Error, // Error occured
	};
}
