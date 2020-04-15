using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GCloud
{
	public delegate void QueueStatusHandler(Result result, QueueStatusInfo info);
	public delegate void QueueFinishedHandler(Result result, QueueFinishedInfo info);

	public interface IQueueService
	{
		event QueueStatusHandler QueueStatusEvent;
		event QueueFinishedHandler QueueFinishedEvent;

		bool Initialize(QueueInitInfo initInfo);

		bool JoinQueue(string zoneId,string queflag);

		bool ExitQueue();

		void Update();
	}


	public class QueueServiceFactory
	{
		public static IQueueService CreateInstance()
		{
			return QueueService.Instance;
		}
	}


	public class QueueInitInfo : ApolloBufferBase
	{
		public string Url;
		public string AppId;
		public string OpenId;
		public string Token;
		public AuthType AuthType;
		public ChannelType Channel;

		public QueueInitInfo()
		{
			Url = "";
			AppId = "";
			OpenId = "";
			Token = "";
			AuthType = AuthType.None;
			Channel = ChannelType.None;
		}

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(Url);
			writer.Write(AppId);
			writer.Write(OpenId);
			writer.Write(Token);
			writer.Write(AuthType);
			writer.Write(Channel);
		}

		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref Url);
			reader.Read(ref AppId);
			reader.Read(ref OpenId);
			reader.Read(ref Token);
			reader.Read(ref AuthType);
			reader.Read(ref Channel);
		}
	}


	public enum QueueStatusType
	{
		Invalid = 0,
		JoinQueue = 1,
		ExitQueue = 2,
		Status = 3,
	};


	public class QueueStatusInfo : ApolloBufferBase
	{
		public QueueStatusType Type;
		public ErrorCode ErrCode;
		public string ErrMsg;
		public int CurPosition;
		public int TotalCount;
		public int EstimatedTime;

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(Type);
			writer.Write(ErrCode);
			writer.Write(ErrMsg);
			writer.Write(CurPosition);
			writer.Write(TotalCount);
			writer.Write(EstimatedTime);
		}

		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref Type);
			reader.Read(ref ErrCode);
			reader.Read(ref ErrMsg);
			reader.Read(ref CurPosition);
			reader.Read(ref TotalCount);
			reader.Read(ref EstimatedTime);
		}
	}


	public class QueueFinishedInfo : ApolloBufferBase
	{
		public ErrorCode ErrCode;
		public string ErrMsg;
		public string Token;
		public string PassTime;
		public string JoinTime;

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(ErrCode);
			writer.Write(ErrMsg);
			writer.Write(Token);
			writer.Write(PassTime);
			writer.Write(JoinTime);
		}

		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref ErrCode);
			reader.Read(ref ErrMsg);
			reader.Read(ref Token);
			reader.Read(ref PassTime);
			reader.Read(ref JoinTime);
		}
	}

}

