using System;
using System.Text;

namespace GCloud
{
	public delegate void ResultEventHandler(ConnectorResult result);
    public delegate void StateChangedEventHandler(ConnectorState state, ConnectorResult result);
	public delegate void RouteChangedEventHandler(UInt64 serverId);
	public delegate void RecvedDataEventHandler();
	public delegate void RecvedUDPDataEventHandler();
	
	public interface IConnector : IServiceBase
	{
		/// <summary>
		/// Occurs when connect event.
		/// </summary>
		/// result can be:
		///     Success: Success
		///     ConnectFailed: Failed to Connect to Server.
		///     Timeout: as your known, time has been out.
		///     PeerStopSession: Tconnd(Server) stop the session.
		///     StayInQueue: there's too much connection to the server, please try again later.
		///     SvrIsFull: server is full, please try again later.
		///     TokenInvalid: Token is invalid, please Logout(IGCloudAccountService) first and try to Connect again.
		///     TokenSvrError: auth server is error, please Contact to your server administrator.
		///     GcpError: there's some other error
		/// 
		/// you can try to Connect again, when errors below occured:
		///     ConnectFailed,
		///     Timeout,
		///     TokenSvrError,
		/// 
		/// you can show some tips, and be waiting for Server idle, when errors below occured:
		///     StayInQueue: "You'r in queue"
		///     SvrIsFull: "The Server is full, Please try again later"
		///     PeerStopSession: "Server error, please try again later"
		///     GcpError: "Inner Error, please try again later"
		/// 
		/// you must Logout(IGCloudAccountService) first and try to Connect again, when errors below occured:
		///     TokenInvalid,
		/// 
		/// Other errors:
		///     Show tips like "Inner Error, please try again later"
		///
        
        /// <summary>
        /// Callback of Connect
        /// </summary>
		event ResultEventHandler ConnectEvent;
        
        /// <summary>
        /// Callback of Disconnect
        /// </summary>
		event ResultEventHandler DisconnectEvent;
        
        /// <summary>
        /// Callback of RelayConnect
        /// </summary>
        event ResultEventHandler RelayConnectEvent;
        
        /// <summary>
        /// Called when receive TCP/RUDP data
        /// </summary>
        event RecvedDataEventHandler RecvedDataEvent;
        
        /// <summary>
        /// Called when receive UDP data
        /// </summary>
		event RecvedUDPDataEventHandler RecvedUDPDataEvent;
        
        /// <summary>
        /// Called when route information change
        /// </summary>
		event RouteChangedEventHandler RouteChangedEvent;
        
        /// <summary>
        /// Called when state of connection change
        /// </summary>
		event StateChangedEventHandler StateChangedEvent;
		
		bool Connected { get; }
		
        /// <summary>
        /// Initialize connector
        /// </summary>
        /// <returns>The result of initialize</returns>
        /// <param name="initInfo">Information to initialize connector</param>
		ConnectorResult Initialize(ConnectorInitInfo initInfo);
        
        /// <summary>
        /// Star connect
        /// </summary>
        /// <returns>The result of connect</returns>
        /// <param name="channel">platform,such as QQ</param>
        /// <param name="url">url for connect</param>
        /// <param name="clearBuffer">clear buffer or no</param>
		ConnectorResult Connect(ChannelType channel, string url, bool clearBuffer = true);
        
        /// <summary>
        /// Restar connect
        /// </summary>
        /// <returns>The result of connect</returns>
        ConnectorResult RelayConnect();
        
        /// <summary>
        /// Disconnect
        /// </summary>
        /// <returns>The result of disconnect</returns>
        ConnectorResult Disconnect();

        /// <summary>
        /// Send data to service by TCP/RUDP
        /// </summary>
        /// <returns>The result of sending</returns>
        /// <param name="data">data for sending</param>
        /// <param name="dataLen">length of data</param>
        bool Write(byte[] data, int dataLen);
        
        /// <summary>
        /// Send data to service by TCP/RUDP(Cluster mode)
        /// </summary>
        /// <returns>The result of sending</returns>
        /// <param name="data">data for sending</param>
        /// <param name="dataLen">length of data</param>
        /// <param name="routeInfo">information of route</param>
		bool WriteRoute(byte[] data, int dataLen, RouteInfoBase routeInfo);
        
        /// <summary>
        /// Receice TCP/RUDP data
        /// </summary>
        /// <returns>The result of reading</returns>
        /// <param name="data">data of reading</param>
        /// <param name="dataLen">length of data</param>
		bool Read(ref byte[] buffer, ref int dataLength);

        /// <summary>
        /// Send data to service by UDP
        /// </summary>
        /// <returns>The result of sending</returns>
        /// <param name="data">data for sending</param>
        /// <param name="dataLen">length of data</param>
		bool WriteUDP(byte[] data, int dataLen);
        
        /// <summary>
        /// receive UDP data
        /// </summary>
        /// <returns>The result of reading</returns>
        /// <param name="buffer">data of reading</param>
        /// <param name="dataLen">length of data</param>
		bool ReadUDP(ref byte[] buffer, ref int dataLen);
        
        /// <summary>
        /// Set player login authentication information
        /// </summary>
        /// <param name="type">type of authentication</param>
        /// <param name="channel">platform</param>
        /// <param name="appid">appId of game</param>
        /// <param name="openid">openId of player</param>
        /// <param name="token">token of player</param>
        void SetAuthInfo(AuthType type, ChannelType channel, string appid, string openid, string token, string extInfo = "");
        
        /// <summary>
        /// Set the Connector and Tconnd communication protocol version
        /// </summary>
        /// <param name="headVersion">version of protocol head</param>
        /// <param name="bodyVersion">version of protocol body</param>
        void SetProtocolVersion(int headVersion, int bodyVersion);
        
        /// <summary>
        /// Set the type of client
        /// </summary>
        /// <param name="type">type of client</param>
		void SetClientType (ClientType type);
        
        /// <summary>
        /// Set the information of route(Cluster mode)
        /// </summary>
        /// <param name="routeInfo">information of route</param>
		void SetRouteInfo (RouteInfoBase routeInfo);
        
        /// <summary>
        /// Set data of start package
        /// </summary>
        /// <param name="reserve">reserve length</param>
        /// <param name="data">data</param>
        /// <param name="len">length of data</param>
		void SetSyncInfo(UInt32 reserve, byte[] data, int len);

        /// <summary>
        /// Get current IP and service id
        /// </summary>
        /// <returns>The result of getting</returns>
        /// <param name="info">information of connection</param>
        bool GetConnectedInfo(ref ConnectedInfo info);
	}
	
	public enum ConnectorErrorCode
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
		InvalidGameId = 14,
		
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
	}

	public enum ConnectorType
	{
		TConnd,
		GConnd,

        TConnd_GCP = 11,

        GConnd_GCP = 21,
        GConnd_PRT = 22,
        G6 =23,
	};

	public enum ClientType
	{
		PC = 0,
		Android = 101,
		IOS = 102,
	}

    public enum AuthType
    {
        None = 0,
        MSDKv3 = 0x7fff,	//32767
        MSDKv5 = 0x1000,	//4096
        WeGame = 0x1005,	//4101
        MSDKPC = 0x1006,	//4102
    }

    public enum ConnectorState
	{
		Running,
		Reconnecting, // Reconnecting to the server
		Reconnected,
		StayInQueue, // In queue
		Error, // Error occured
	};
	
	public enum RouteType
	{
		None = 0,
		Zone = 1, 
		Server,
		LoginPosition,
		Name,
	}
	
	public class ConnectorResult : ApolloBufferBase
	{
		public ConnectorErrorCode ErrorCode;//error code
		public string Reason;//reason of error
		public int Extend;//1st extra error message
		public int Extend2;//2nd extra error message
		public Int64 Extend3;//3rd extra error message
		//
		// Construtions
		//
		public ConnectorResult()
		{
			ErrorCode = ConnectorErrorCode.Success;
		}
		
		public ConnectorResult(ConnectorErrorCode errorCode)
		{
			ErrorCode = errorCode;
		}
		
		public ConnectorResult(ConnectorErrorCode errorCode, string reason)
		{
			ErrorCode = errorCode;
			Reason = reason;
		}
		
		public ConnectorResult(ConnectorErrorCode errorCode, int ext, string reason)
		{
			ErrorCode = errorCode;
			Reason = reason;
		}
		
		public ConnectorResult(ConnectorErrorCode errorCode, int ext, int ext2, string reason)
		{
			ErrorCode = errorCode;
			Extend = ext;
			Extend2 = ext2;
		}
		
		//
		// static functions
		//
		public void Reset(ConnectorErrorCode error, string reason = "")
		{
			ErrorCode = error;
			Reason = reason;
			Extend = 0;
			Extend2 = 0;
			Extend3 = 0;
		}
		
		public void Success()
		{
			ErrorCode = ConnectorErrorCode.Success;
			Reason = "";
			Extend = 0;
			Extend2 = 0;
			Extend3 = 0;
		}
		
		public bool IsSuccess()
		{
			return ErrorCode == ConnectorErrorCode.Success;
		}
		
		public static bool operator ==(ConnectorResult result, ConnectorErrorCode errorCode)
		{
			return result.ErrorCode == errorCode;
		}
		
		public static bool operator != (ConnectorResult result, ConnectorErrorCode errorCode)
		{
			return result.ErrorCode != errorCode;
		}
		
		public static bool operator ==(ConnectorResult r1, ConnectorResult r2)
		{
			return r1.ErrorCode == r2.ErrorCode;
		}
		
		public static bool operator != (ConnectorResult r1, ConnectorResult r2)
		{
			return r1.ErrorCode != r2.ErrorCode;
		}

		public override bool Equals(System.Object obj)
		{
			if (obj == null)
			{
				return false;
			}

			ConnectorResult ret = obj as ConnectorResult;
			if ((System.Object)ret == null)
			{
				return false;
			}

			return (ErrorCode == ret.ErrorCode) && (Extend == ret.Extend) && (Extend2 == ret.Extend2) && (Extend3 == ret.Extend3);
		}

		public override int GetHashCode()
		{
			return (int)ErrorCode ^ Extend;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (ErrorCode);
			sb.Append (", ");
			sb.Append (Extend);
			sb.Append (", ");
			sb.Append (Extend2);
			sb.Append (", ");
			sb.Append (Extend3);
			if (!string.IsNullOrEmpty (Reason)) 
			{
				sb.Append(Reason);
			}
			return sb.ToString();
		}
		
		
		public override void WriteTo (ApolloBufferWriter writer)
		{
			writer.Write (ErrorCode);
			writer.Write (Reason);
			writer.Write (Extend);
			writer.Write (Extend2);
			writer.Write (Extend2);
		}
		
		public override void ReadFrom (ApolloBufferReader reader)
		{
			reader.Read (ref ErrorCode);
			reader.Read (ref Reason);
			reader.Read (ref Extend);
			reader.Read (ref Extend2);
			reader.Read (ref Extend3);
		}
	}

	public class ConnectorInitInfo : ApolloBufferBase
	{
        //The buffer size is set according to the maximum data of a single Write or Read
        //(GCloudSDK2.0.7 or higher, it is recommended to use MaxSendMessageSize and MaxRecvMessageSize respectively).
		public UInt32  MaxBufferSize;
        //The send buffer size is set according to the maximum data of a single write (GCloudSDK2.0.7 or higher).
		public UInt32  MaxSendMessageSize;
        //The receive buffer size is set according to the maximum data size of a single Read (GCloudSDK2.0.7 or higher).
		public UInt32  MaxRecvMessageSize;
        //The encryption algorithm needs to be consistent with the backend TConnd configuration.
		public EncryptMethod EncMethod;
        //The key generation method needs to be consistent with the backend TConnd configuration.
		public KeyMaking KeyMakingMethod;
        //The Diffie-Hellman key, obtained from Tconnd, is valid when KeyMaking is RawDH or EncDH.
		public string DH;
        //The Connector thread sending and receiving cycle (milliseconds) controls the period of the internal network messaging of the Connector.
        //The default is 10ms. Pay attention to balance performance and efficiency when adjusting the service.
		public UInt32 Timeout;
        //The Connector thread sending and receiving cycle (milliseconds) controls the period of the internal network messaging of the Connector.
        //The default is 10ms. Pay attention to balance performance and efficiency when adjusting the service.
		public UInt32 LoopInterval;
        //Whether to clear the buffer when reconnecting.
		public bool ClearBufferWhenReconnect;
        protected UInt32 InfoType;//type of info

        public ConnectorInitInfo()
        {
            Timeout = 10;
        }

        public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(MaxBufferSize);
			writer.Write(MaxSendMessageSize);
			writer.Write(MaxRecvMessageSize);
			writer.Write(EncMethod);
			writer.Write(KeyMakingMethod);
			writer.Write(DH);
			writer.Write(Timeout);
			writer.Write(LoopInterval);
			writer.Write(ClearBufferWhenReconnect);
            writer.Write(InfoType);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref MaxBufferSize);
			reader.Read(ref MaxSendMessageSize);
			reader.Read(ref MaxRecvMessageSize);
			reader.Read(ref EncMethod);
			reader.Read(ref KeyMakingMethod);
			reader.Read(ref DH);
			reader.Read(ref Timeout);
			reader.Read(ref LoopInterval);
			reader.Read(ref ClearBufferWhenReconnect);
            reader.Read(ref InfoType);
		}
	}

    public class TconndInitInfo : ConnectorInitInfo
    {
        public TconndInitInfo() : base()
        {
            InfoType = 0;   // Tconnd
        }

        public override void WriteTo(ApolloBufferWriter writer)
        {
            base.WriteTo(writer);
        }

        public override void ReadFrom(ApolloBufferReader reader)
        {
            base.ReadFrom(reader);
        }
    }

    public class G6InitInfo : ConnectorInitInfo
    {
        public G6InitInfo() : base()
        {
            InfoType = 23;   // G6
        }

        public override void WriteTo(ApolloBufferWriter writer)
        {
            base.WriteTo(writer);
        }

        public override void ReadFrom(ApolloBufferReader reader)
        {
            base.ReadFrom(reader);
        }
    }

    public class GConndInitInfo : ConnectorInitInfo
    {
        public UInt64 UnitID;
        public string ServiceName;

        public GConndInitInfo(UInt64 id, string name):base()
        {
            UnitID = id;
            ServiceName = name;
            InfoType = 1;   // GConnd
        }

        public override void WriteTo(ApolloBufferWriter writer)
        {
            base.WriteTo(writer);
            writer.Write(UnitID);
            writer.Write(ServiceName);
        }

        public override void ReadFrom(ApolloBufferReader reader)
        {
            base.ReadFrom(reader);
            reader.Read(ref UnitID);
            reader.Read(ref ServiceName);
        }
    }

    public class ConnectedInfo : ApolloBufferBase
	{
		public string currentIP;
		public UInt64 currentServerID;

		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write(currentIP);
			writer.Write(currentServerID);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref currentIP);
			reader.Read(ref currentServerID);
		}
	};

	public abstract class RouteInfoBase : ApolloBufferBase
	{
		public RouteType RouteType;
		public bool AllowLost;
		
		protected RouteInfoBase(RouteType routeType)
		{
			RouteType = routeType;
			AllowLost = true;
		}
		
		public RouteInfoBase CopyInstance()
		{
			RouteInfoBase instance = onCopyInstance();
			if (instance != null)
			{
				instance.RouteType = this.RouteType;
				instance.AllowLost = this.AllowLost;
			}
			return instance;
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			writer.Write((int)RouteType);
			writer.Write (AllowLost);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			reader.Read(ref RouteType);
			reader.Read(ref AllowLost);
		}
		
		protected abstract RouteInfoBase onCopyInstance();
		
		public static RouteInfoBase CreateRouteInfo(RouteType routeType)
		{
			switch(routeType)
			{
			case RouteType.Server:
			{
				return new ServerRouteInfo();
			}
			case RouteType.Zone:
			{
				return new ZoneRouteInfo();
			}
			case RouteType.Name:
			{
				return new NameRouteInfo();
			}
			default:
			{
				return null;
			}
			}
		}
	}
	
	public class ZoneRouteInfo : RouteInfoBase
	{
		public UInt32 TypeId;
		public UInt32 ZoneId;
		
		public ZoneRouteInfo()
			: base(RouteType.Zone)
		{
			TypeId = 0;
			ZoneId = 0;
		}
		
		public ZoneRouteInfo(UInt32 typeId, UInt32 zoneId)
			: base(RouteType.Zone)
		{
			TypeId = typeId;
			ZoneId = zoneId;
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			base.WriteTo(writer);
			writer.Write(TypeId);
			writer.Write(ZoneId);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			base.ReadFrom(reader);
			reader.Read(ref TypeId);
			reader.Read(ref ZoneId);
		}
		
		protected override RouteInfoBase onCopyInstance()
		{
			ZoneRouteInfo instance = new ZoneRouteInfo();
			instance.TypeId = TypeId;
			instance.ZoneId = ZoneId;
			return instance;
		}
	}
	
	public class ServerRouteInfo : RouteInfoBase
	{
		public UInt64 ServerId;
		
		public ServerRouteInfo()
			: base(RouteType.Server)
		{
			ServerId = 0;
		}
		public ServerRouteInfo(UInt64 serverId)
			: base(RouteType.Server)
		{
			ServerId = serverId;
		}
		
		public override void WriteTo(ApolloBufferWriter writer)
		{
			base.WriteTo(writer);
			writer.Write(ServerId);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			base.ReadFrom(reader);
			reader.Read(ref ServerId);
		}
		
		protected override RouteInfoBase onCopyInstance()
		{
			ServerRouteInfo instance = new ServerRouteInfo();
			instance.ServerId = ServerId;
			return instance;
		}
	}
	
	
	public class NameRouteInfo : RouteInfoBase
	{
		public string ServiceName;
		
		public NameRouteInfo(string serviceName)
			: base(RouteType.Name)
		{
			ServiceName = serviceName;
		}
		
		public NameRouteInfo()
			: base(RouteType.Name)
		{
		}

		public override void WriteTo(ApolloBufferWriter writer)
		{
			base.WriteTo(writer);
			writer.Write(ServiceName);
		}
		
		public override void ReadFrom(ApolloBufferReader reader)
		{
			base.ReadFrom(reader);
			reader.Read(ref ServiceName);
		}
		
		
		protected override RouteInfoBase onCopyInstance()
		{
			NameRouteInfo instance = new NameRouteInfo();
			instance.ServiceName = ServiceName;
			return instance;
		}
	}
}
