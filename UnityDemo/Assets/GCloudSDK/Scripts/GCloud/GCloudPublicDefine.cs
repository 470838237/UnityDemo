using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GCloud
{
	public delegate void ActionDelegate(Result result, ActionBufferBase action);
	
	// Service Type
	public class ServiceType
	{
		public int Value{ get; private set;}
		protected ServiceType(int value)
		{
			this.Value = value;
		}

		//Common,
		public static readonly ServiceType Account = new ServiceType (0);
		public static readonly ServiceType Pay = new ServiceType (1);
		public static readonly ServiceType Sns = new ServiceType (2);
		public static readonly ServiceType Common = new ServiceType (3);
		public static readonly ServiceType LBS = new ServiceType (4);
		public static readonly ServiceType Notice = new ServiceType (5);
		public static readonly ServiceType Report = new ServiceType (6);
		public static readonly ServiceType QuickLogin = new ServiceType (7);
		
		public static readonly ServiceType Network = new ServiceType (1000);


		
		#region operation
		public static bool operator ==(ServiceType s1, ServiceType s2)
		{
			return s1.Value == s2.Value;
		}

		public static bool operator !=(ServiceType s1, ServiceType s2)
		{
			return s1.Value != s2.Value;
		}

		public override bool Equals (object obj)
		{
			ServiceType s = (obj as ServiceType);
			if(null == s)
			{
				return false;
			}

			return Value == s.Value;
		}

		public override int GetHashCode ()
		{
			return Value;
		}
		#endregion

	};
	
	// Log
	public enum LogPriority
	{
		Debug,
		Info,
		Warning,
		Event,
		Error,
		None,
	};

	
	// Channel
	public enum ChannelType
	{
		None = 0,
		Wechat = 1,
		QQ = 2,
		Guest = 3,
        Facebook,
        GameCenter,
        GooglePlay

	};
	
	public class PluginName
	{
		public const string WTLogin = "WTLogin";
		
		public const string Default = "MSDK";
		public const string Msdk = "MSDK";
		public const string Platform91 = "91";
		public const string _360 = "360";
		public const string UC = "UC";
		
		public const string Netmarble = "Netmarble";

		
		internal const string WinAccount = "WinAccount";
		internal const string Custom = "Custom";
	}
	
	// EncryptMethod
	public enum EncryptMethod
	{
		None = 0,
		Tea  = 1,
		QQ   = 2,
		Aes  = 3,
		Aes2 = 4,
	};
	
	// KeyMaking
	public enum KeyMaking
	{
		None = 0,
		Auth,
		Server,
		RawDH,
		EncDH,
	} ;
	
	// Result
	public enum ErrorCode
	{
		// Common
		Success = 0,
        InnerError = 1,
        NetworkException = 2,
        Timeout = 3,
        InvalidArgument = 4,
        LengthError = 5,
        Unknown = 6,
        Empty = 7,
        NotInitialized = 9,
        NotSupported = 10,
        NotInstalled = 11,
        SystemError = 12,
        NoPermission = 13,
        
        InvalidGameId,
        
        // AccountService, from 100
        InvalidToken = 100,
        NoToken = 101,
        AccessTokenExpired = 102,
        RefreshTokenExpired = 103,
        PayTokenExpired = 104,
        LoginFailed = 105,
        UserCancel = 106,
        UserDenied,
        Checking,
        NeedRealNameAuth,
		
        
        // Connector, from 200
        NoConnection = 200,
        ConnectFailed = 201,
        IsConnecting,
        GcpError,
        PeerCloseConnection,
        PeerStopSession,
        PkgNotCompleted,
        SendError,
        RecvError,
        StayInQueue,
        SvrIsFull,
        TokenSvrError,
        AuthFailed,
        Overflow,
        DNSError,

        // PayService, from 300

		//Webview
		WebviewClosed = 400,
		WebviewPageEvent = 401,

		// TDir from 500
		LeafNotFound = 500,
		PlatformNotFound,

		
		// LBS from 600
		LbsNeedOpenLocation = 600,
		LbsLocateFail,
	
		//Other
		Others = 10000,
	};
	
	
	public partial class Result
	{
		public static Result Success = new Result(ErrorCode.Success);
		public static Result InnerError = new Result(ErrorCode.InnerError);
		public static Result NetworkException = new Result(ErrorCode.NetworkException);
		public static Result Timeout = new Result(ErrorCode.Timeout);
		public static Result InvalidArgument = new Result(ErrorCode.InvalidArgument);
		public static Result LengthError = new Result(ErrorCode.LengthError);
		public static Result Unknown = new Result(ErrorCode.Unknown);
		public static Result Empty = new Result(ErrorCode.Empty);
		public static Result NotInitialized = new Result(ErrorCode.NotInitialized);
		public static Result NotSupported = new Result(ErrorCode.NotSupported);
		public static Result SystemError = new Result(ErrorCode.SystemError);
		public static Result NoPermission = new Result(ErrorCode.NoPermission);
		
		public static Result InvalidGameId = new Result(ErrorCode.InvalidGameId);
		
		// AccountService, from 100
		public static Result InvalidToken = new Result(ErrorCode.InvalidToken);
		public static Result AccessTokenExpired = new Result(ErrorCode.AccessTokenExpired);
		public static Result RefreshTokenExpired = new Result(ErrorCode.RefreshTokenExpired);
		public static Result LoginFailed = new Result(ErrorCode.LoginFailed);
		
		// Connector, from 200
		public static Result NoConnection = new Result(ErrorCode.NoConnection);
		public static Result ConnectFailed = new Result(ErrorCode.ConnectFailed);
		public static Result IsConnecting = new Result(ErrorCode.IsConnecting);
		public static Result GcpError = new Result(ErrorCode.GcpError);
		public static Result PeerCloseConnection = new Result(ErrorCode.PeerCloseConnection);
		public static Result PeerStopSession = new Result(ErrorCode.PeerStopSession);
		public static Result PkgNotCompleted = new Result(ErrorCode.PkgNotCompleted);
		public static Result SendError = new Result(ErrorCode.SendError);
		public static Result RecvError = new Result(ErrorCode.RecvError);
		public static Result StayInQueue = new Result(ErrorCode.StayInQueue);
		public static Result SvrIsFull = new Result(ErrorCode.SvrIsFull);
		public static Result TokenSvrError = new Result(ErrorCode.TokenSvrError);
		public static Result AuthFailed = new Result(ErrorCode.AuthFailed);
		
		// PayService, from 300

		// Webview, from 400
		public static Result WebviewClosed = new Result(ErrorCode.WebviewClosed);
		public static Result WebviewPageEvent = new Result(ErrorCode.WebviewPageEvent);

		// TDir from 500
		public static Result LeafNotFound = new Result(ErrorCode.LeafNotFound);

		//Other
		public static Result Others = new Result(ErrorCode.Others);

	}

	
	public partial class  Result : ApolloBufferBase
	{
		public ErrorCode ErrorCode;
		public string Reason;
		public int Extend;
		public int Extend2;
		
		//
		// rutions
		//
		
		public Result ()
		{
			ErrorCode = ErrorCode.Success;
		}
		
		public Result (ErrorCode errorCode)
		{
			ErrorCode = errorCode;
		}
		
		public Result (ErrorCode errorCode, string reason)
		{
			ErrorCode = errorCode;
			Reason = reason;
		}
		
		public Result (ErrorCode errorCode, int ext, string reason)
		{
			ErrorCode = errorCode;
			Reason = reason;
			Extend = ext;
		}

		public Result (ErrorCode errorCode, int ext, int ext2, string reason)
		{
			ErrorCode = errorCode;
			Reason = reason;
			Extend = ext;
			Extend2 = ext2;
		}

		public bool IsSuccess ()
		{
			return ErrorCode == ErrorCode.Success;
		}

		public static bool operator ==(Result result, ErrorCode errorCode)
		{
			return result.ErrorCode == errorCode;
		}

		public static bool operator != (Result result, ErrorCode errorCode)
		{
			return result.ErrorCode != errorCode;
		}

		public static bool operator ==(Result r1, Result r2)
		{
			return r1.ErrorCode == r2.ErrorCode;
		}
		
		public static bool operator != (Result r1, Result r2)
		{
			return r1.ErrorCode != r2.ErrorCode;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (ErrorCode);
			sb.Append (", ");
			sb.Append (Extend);
			sb.Append (", ");
			sb.Append (Extend2);
			if (!string.IsNullOrEmpty (Reason)) 
			{
				sb.Append(Reason);
			}
			return sb.ToString();
		}

		public override int GetHashCode ()
		{
			return (int)ErrorCode;
		}

		public override bool Equals (object obj)
		{
			Result r = obj as Result;
			if(r == null)
			{
				return false;
			}
			return r.ErrorCode == ErrorCode;
		}

		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (ErrorCode);
			writer.Write (Reason);
			writer.Write (Extend);
			writer.Write (Extend2);
		}
		
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref ErrorCode);
			reader.Read (ref Reason);
			reader.Read (ref Extend);
			reader.Read (ref Extend2);
		}
	}

	// AplloTokenType
	public enum TokenType
	{
		None = 0,
		Access,
		Refresh,
		Pay,
		Pf,
		PfKey,
	};
	
	// Token
	public class Token : ApolloBufferBase
	{
		public TokenType Type;
		
		public string Value;
		
		public Int64 Expire; // seconds
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write (Type);
			writer.Write (Value);
			writer.Write (Expire);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read (ref Type);
			reader.Read (ref Value);
			reader.Read (ref Expire);
		}
	}
	
	public class WaitingInfo : ApolloBufferBase
	{
		public UInt32 Position;
		public UInt32 QueueLen;
		public UInt32 EstimateTime;
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write (Position);
			writer.Write (QueueLen);
			writer.Write (EstimateTime);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read (ref Position);
			reader.Read (ref QueueLen);
			reader.Read (ref EstimateTime);
		}
	};
	
	// AccountInfo
	public class AccountInfo : ApolloBufferBase
	{
		public ChannelType Channel;
		
		public string OpenId;
		public string UserId;
		public UInt64 Uid;

		private List<Token> tokenList = new List<Token>();
		public List<Token> TokenList
		{
			get
			{
				if (tokenList == null)
				{
					tokenList = new List<Token>();
				}
				return tokenList;
			}
		}

		
		public Int32 GetTokenLeftSeconds(TokenType type)
		{
			Token token = GetToken(type);
			if (token != null)
			{
				if(token.Expire == -1)
				{
					return Int32.MaxValue;
				}
				TimeSpan now = DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime();
				
				return (Int32)(token.Expire - (long)now.TotalSeconds);
			}
			return 0;
		}
		
		public Int32 GetPayTokenLeftSeconds()
		{
			if (Channel == ChannelType.Wechat) 
			{
				return GetTokenLeftSeconds (TokenType.Access);
			} 
			else  if(Channel == ChannelType.Guest)
			{
				return GetTokenLeftSeconds(TokenType.Access);
			}
			return GetTokenLeftSeconds(TokenType.Pay);
		}
		
		public Token GetToken(TokenType type)
		{
			foreach (Token token in TokenList)
			{
				if (token.Type == type)
				{
					return token;
				}
			}
			return null;
		}
		
		public void Reset()
		{
			Channel = ChannelType.None;
			OpenId = string.Empty;
			TokenList.Clear();
		}

		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write (Channel);
			writer.Write (OpenId);
			writer.Write (UserId);
			writer.Write (Uid);
			writer.Write (tokenList);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read (ref Channel);
			reader.Read (ref OpenId);
			reader.Read (ref UserId);
			reader.Read (ref Uid);
			reader.Read (ref tokenList);
		}
	};
	
	public class UserInfo : ApolloBufferBase
	{
		int ChannelID;
		string OpenID;

		public UserInfo(int channelID, string openID)
		{
			OpenID = openID;
			ChannelID = channelID;
		}

		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (ChannelID);
			writer.Write (OpenID);

		}
		
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref ChannelID);
			reader.Read (ref OpenID);
		}
	}

	
}
