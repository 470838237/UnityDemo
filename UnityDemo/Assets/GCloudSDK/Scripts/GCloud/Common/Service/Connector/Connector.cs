using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GCloud
{
    internal class GCloudConnector : ApolloObject, IConnector
	{
        public event ResultEventHandler ConnectEvent;
        public event ResultEventHandler DisconnectEvent;
        public event ResultEventHandler RelayConnectEvent;
        public event RecvedDataEventHandler RecvedDataEvent;
        public event RecvedUDPDataEventHandler RecvedUDPDataEvent;
        public event RouteChangedEventHandler RouteChangedEvent;
        public event StateChangedEventHandler StateChangedEvent;

        public bool Connected{get; private set;}

		private ConnectorType connectorType;
		private bool manualUpdate;
		private bool autoReconnect;
		private bool autoLogin;

		public GCloudConnector(ConnectorType type, bool manualUpdate, bool autoReconnect, bool autoLogin)
        {
			this.manualUpdate = manualUpdate;
			this.autoReconnect = autoReconnect;
			this.autoLogin = autoLogin;
			this.connectorType = type;
            Connected = false;
        }

		public ConnectorResult Initialize(ConnectorInitInfo initInfo)
		{
			byte[] data;
			if (!initInfo.Encode (out data)) {
				return new ConnectorResult(ConnectorErrorCode.InvalidArgument);
			}

			return new ConnectorResult(gcloud_connector_Initialize (this.ObjectId, connectorType, manualUpdate, autoReconnect, autoLogin, data, data.Length));
		}

		public ConnectorResult Connect(ChannelType channel, string url, bool clearBuffer = true)
        {
            return new ConnectorResult(gcloud_connector_connect(ObjectId, channel, url, clearBuffer));
        }

        public ConnectorResult RelayConnect()
        {
            return new ConnectorResult(gcloud_connector_relayconnect(ObjectId));
        }

        public ConnectorResult Disconnect()
        {
            return new ConnectorResult(gcloud_connector_disconnect(ObjectId));
        }

		public bool Write(byte[] data, int len)
		{
			if (data == null)
			{
				return false;
			}
			
			if(!Connected)
			{
				ADebug.LogError("WriteData but there's no Connection");
				return false;
			}
			
			if (len > data.Length || len < 0)
			{
				len = data.Length;
			}
			return gcloud_connector_writeData(ObjectId, data, len, false);
		}

        public bool WriteRoute(byte[] data, int dataLen, RouteInfoBase routeInfo)
		{
			if (data == null)
			{
				return false;
			}
			
			if(!Connected)
			{
				ADebug.LogError("WriteRoute but there's no Connection");
				return false;
			}
			
			if (dataLen > data.Length || dataLen < 0)
			{
                dataLen = data.Length;
			}

			byte[] buf;
            routeInfo.Encode(out buf);
            if (buf == null)
            {
                ADebug.LogError("WriteRoute Encode error!");
                return false;
            }
			return gcloud_connector_writeRoute(ObjectId, data, dataLen,  buf, buf.Length);
		}

		public bool WriteUDP(byte[] data, int len = -1)
		{
			if (data == null)
			{
				return false;
			}
			
			if(!Connected)
			{
				ADebug.LogError("WriteData but there's no Connection");
				return false;
			}
			
			if (len == -1)
			{
				len = data.Length;
			}
			return gcloud_connector_writeData(ObjectId, data, len, true);
		}
		
		public bool Read(ref byte[] buffer, ref int realLength)
		{
			if (buffer == null)
			{
				ADebug.LogError("Read Data invalid arguments");
				return false;
			}
			
			int len = buffer.Length;
			bool ret = gcloud_connector_readData (ObjectId, buffer, ref len, false);
			if(ret)
			{
				if(len == 0)
				{
					ADebug.LogError("ReadData empty len==0");
					return false;
				}
				realLength = len;
			}
			return ret;
		}

		public bool ReadUDP(ref byte[] buffer, ref int realLength)
		{
			if (buffer == null)
			{
				ADebug.LogError("Read Data invalid arguments");
				return false;
			}
			
			int len = buffer.Length;
			bool ret = gcloud_connector_readData (ObjectId, buffer, ref len, true);
			if(ret)
			{
				if(len == 0)
				{
					ADebug.LogError("ReadData empty len==0");
					return false;
				}
				realLength = len;
			}
			return ret;
		}

		public void SetProtocolVersion(int headVersion, int bodyVersion)
		{
			gcloud_connector_set_protocol_version(this.ObjectId, headVersion, bodyVersion);
		}
		
        public void SetAuthInfo(AuthType type, ChannelType channel, string appid, string openid, string token, string extInfo = "")
        {
            gcloud_connector_set_authInfo(this.ObjectId, type, appid, channel, openid, token, extInfo);
        }

        public void SetClientType(ClientType type)
		{
			gcloud_connector_set_clientType(ObjectId, type);
		}

        public void SetRouteInfo(RouteInfoBase routeInfo)
        {
            if (routeInfo == null)
            {
                ADebug.LogError("SetRouteInfo: routeInfo is null");
            }

            byte[] buf;
            routeInfo.Encode(out buf);
            if (buf == null)
            {
                ADebug.LogError("SetRouteInfo: routeInfo Encode error");
            }
            else
            {
                gcloud_connector_setRouteInfo(ObjectId, buf, buf.Length);
            }
        }

		public void SetSyncInfo(UInt32 reserve, byte[] data, int len)
        {
            ADebug.Log("SetSyncInfo reserve:" + reserve + ", datalen:" + len);

            gcloud_connector_set_syncInfo(this.ObjectId, reserve, data, len);
        }

		public bool GetConnectedInfo(ref ConnectedInfo info)
		{
            byte[] byteArray = new byte[256];
			bool ret = gcloud_connector_get_connectedInfo (this.ObjectId, byteArray, 256);

			if (ret) 
			{
                return info.Decode(byteArray);
			} 
			else 
			{
				return false;
			}
		}

        #region Observer
        private ConnectorResult convertConnectorResult(byte[] data)
		{
			if (data == null) {
				return new ConnectorResult(ConnectorErrorCode.InvalidArgument);
			}
			ConnectorResult result = new ConnectorResult ();
			result.Decode (data);
			return result;
		}

		void OnConnected(byte[] data)
        {
			ConnectorResult result = convertConnectorResult (data);
            ADebug.Log("c#:OnConnectProc: " + result);

			if (result == ConnectorErrorCode.Success)
            {
                Connected = true;
            }
            else
            {
                Connected = false;
            }

            if (ConnectEvent != null)
            {
                try
                {
					ConnectEvent(result);
                } catch (Exception ex)
                {
                    ADebug.LogException(ex);
                }
            } else
            {
                ADebug.Log("OnConnectProc ConnectEvent is null");
            }
        }

        void OnRelayConnectedProc(byte[] data)
        {
            ConnectorResult result = convertConnectorResult(data);
            ADebug.Log("c#:OnRelayConnectedProc: " + result);

            if (result == ConnectorErrorCode.Success)
            {
                Connected = true;
            }
            else
            {
                Connected = false;
            }

            if (RelayConnectEvent != null)
            {
                try
                {
                    RelayConnectEvent(result);
                }
                catch (Exception ex)
                {
                    ADebug.LogException(ex);
                }
            }
            else
            {
                ADebug.Log("OnRelayConnectedProc RelayConnectEvent is null");
            }
        }

        void OnDisconnectProc(byte[] data)
		{
			ConnectorResult result = convertConnectorResult (data);
			ADebug.Log("c#:OnDisconnectProc: " + result);
			if (result.IsSuccess())
            {
                Connected = false;
            }

            if (DisconnectEvent != null)
            {
                try
                {
                    DisconnectEvent(result);
                } catch (Exception ex)
                {
                    ADebug.LogException(ex);
                }
            }
        }
    
		void OnStateChangedProc(int state, byte[] resultdata)
		{
			ConnectorResult result = convertConnectorResult (resultdata);
			ConnectorState s = (ConnectorState)state;
			ADebug.Log ("OnStateChangedProc state: " + s + " " + result.ToString());
			if (s == ConnectorState.Reconnected && result.IsSuccess()) {
				Connected = true;
			} else {
				Connected = false;
			}
			
			if (StateChangedEvent != null)
			{
				try
				{
					StateChangedEvent(s, result);
				} catch (Exception ex)
				{
					ADebug.LogException(ex);
				}
				
			}
		}

		void OnRouteChangedProc(string msg)
        {
            UInt64 serverId = UInt64.Parse(msg);
			ADebug.Log ("OnStateChangedProc msg: " + msg + ", serverId:" + serverId);
            if (RouteChangedEvent != null)
            {
                try
                {
                    RouteChangedEvent(serverId);
                } catch (Exception ex)
                {
                    ADebug.LogException(ex);
                }
            }
        }
		
		public override void PerformVoidMethodWithId(int methodId)
		{
			ADebug.Log ("PerformVoidMethodWithId");
			if (methodId == 1001) 
			{
				if (RecvedDataEvent != null)
				{
					try
					{
						RecvedDataEvent();
					} catch (Exception ex)
					{
						ADebug.LogException(ex);
					}
					
				}
			}
			else if (methodId == 1002)
			{
				if (RecvedUDPDataEvent != null)
				{
					try
					{
						RecvedUDPDataEvent();
					} catch (Exception ex)
					{
						ADebug.LogException(ex);
					}
					
				}
			}
		}
    	#endregion

    	#region DllImport
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern ConnectorErrorCode gcloud_connector_Initialize(UInt64 objId, ConnectorType type, bool manualUpdate, bool autoReconnect, bool autoLogin, 
		                                                            byte[] initInfo, int size);
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern ConnectorErrorCode gcloud_connector_connect(UInt64 objId, ChannelType channel, string url, bool clear);
    
        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern ConnectorErrorCode gcloud_connector_relayconnect(UInt64 objId);

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
        private static extern ConnectorErrorCode gcloud_connector_disconnect(UInt64 objId);

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_connector_writeData(UInt64 objId, [MarshalAs(UnmanagedType.LPArray)] byte[] buff, int size, bool rawUdp);

        [DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_connector_readData(UInt64 objId, byte[] buff, ref int size, bool rawUdp);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_connector_writeRoute(UInt64 objId, [MarshalAs(UnmanagedType.LPArray)] byte[] dataBuff, int dataSize, [MarshalAs(UnmanagedType.LPArray)] byte[] routeInfo, int infoSize);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_connector_set_authInfo(UInt64 objId, AuthType type, string appid, ChannelType channel, string openid, string token, string extInfo);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_connector_set_protocol_version(UInt64 objId, int headVersion, int bodyVersion);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern ConnectorErrorCode gcloud_connector_set_clientType(UInt64 objId, ClientType type);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern ConnectorErrorCode gcloud_connector_setRouteInfo(UInt64 objId, [MarshalAs(UnmanagedType.LPArray)] byte[] buff, int size);

		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern void gcloud_connector_set_syncInfo(UInt64 objId, UInt32 extInt, [MarshalAs(UnmanagedType.LPArray)] byte[] buff, int size);
	
		[DllImport(GCloudCommon.PluginName, CallingConvention = CallingConvention.Cdecl)]
		private static extern bool gcloud_connector_get_connectedInfo (UInt64 objId, byte[] buff, int size);
    	#endregion
    }
}
