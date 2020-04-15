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
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace GCloud
{
    public class NetworkService : INetworkService
    {
        public event NetworkStateChanged NetworkChangedEvent;

        private NetworkService()
        {
            Tx.Instance.TXNetworkChangedEvent += onNetworkStateChanged;
        }
        public static readonly NetworkService Instance = new NetworkService();


        public NetworkState GetNetworkState()
        {
            return Tx.Instance.GetNetworkState();
        }

        public DetailNetworkInfo GetDetailNetworkInfo()
        {
            return Tx.Instance.GetDetailNetworkInfo();
        }

        private void onNetworkStateChanged(NetworkState state)
        {
            ADebug.Log("C# NetworkService onNetworkStateChanged state:"+state);
            if(null!=NetworkChangedEvent)
            {
                NetworkChangedEvent(state);
            }
        }
    }
}

