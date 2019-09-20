using honorsdk.SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace honorsdk
{
    public class HonorSDKGameObject : MonoBehaviour
    {

        private OnReceiveMsg receive;

        public void SetOnReceiveListener(OnReceiveMsg receive) {
            this.receive = receive;
        }

        public void OnGetMessage(String message)
        {
            JSONNode rootNode = JSONNode.Parse(message);
            string head = rootNode["head"].Value;
            string body = rootNode["body"].Value;
            receive(head, body);
        }

    }
}
