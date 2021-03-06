//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace GCloud
{
    public delegate void NetworkStateChanged(NetworkState state);

    public interface INetworkService : IServiceBase
    {
        event NetworkStateChanged NetworkChangedEvent;

        NetworkState GetNetworkState();

        DetailNetworkInfo GetDetailNetworkInfo();
    }

	
	
	// Network State
	public enum NetworkState
	{
		NotReachable,//none
		ReachableViaWWAN,//mobile
		ReachableViaWiFi,//wifi
        ReachableViaOthers
    };

    // Detail Network State
    public enum DetailNetworkState
    {
        NotReachable,
        Reserve1,
        ReachableViaWiFi,
        ReachableViaOthers,
        ReachableViaWWAN_UNKNOWN,
        ReachableViaWWAN_2G,
        ReachableViaWWAN_3G,
        ReachableViaWWAN_4G
    };

    public enum Carrier
    {
        None,
        Unknown,
        ChinaMobile,
        ChinaUnicom,
        ChinaTelecom,
        ChinaSpacecom
    };

    public class DetailNetworkInfo : ApolloBufferBase
    {
        public DetailNetworkState DetailState;
        public Carrier Carrier;
        public String CarrierCode;
        public String SSID;
        public String BSSID;
        public String CurrentAPN;

        public override void WriteTo(ApolloBufferWriter writer)
        {
            writer.Write(DetailState);
            writer.Write(Carrier);
            writer.Write(CarrierCode);
            writer.Write(SSID);
            writer.Write(BSSID);
            writer.Write(CurrentAPN);
        }

        public override void ReadFrom(ApolloBufferReader reader)
        {
            reader.Read(ref DetailState);
            reader.Read(ref Carrier);
            reader.Read(ref CarrierCode);
            reader.Read(ref SSID);
            reader.Read(ref BSSID);
            reader.Read(ref CurrentAPN);
        }
    }
}

