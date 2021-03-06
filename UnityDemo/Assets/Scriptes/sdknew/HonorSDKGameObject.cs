﻿using System;
using UnityEngine;

namespace HonorSdk
{

    /// <summary>
    /// 类说明：游戏消息接收器
    /// </summary>
    public class HonorSDKGameObject : MonoBehaviour {

        private OnReceiveMsg receive;

        public void SetOnReceiveListener(OnReceiveMsg receive) {
            this.receive = receive;
        }

        public void OnGetMessage(string message) {         
            JSONNode rootNode = JSONNode.Parse(message);
            string head = rootNode["head"].Value;
            string body = rootNode["body"].Value;
            if (body == null || body == "")
            {
                body = "{}";
            }
            receive(head, body);
       
        }

        public void OnDownloadTextSuccess(string message)
        {
            receive(MessageHandler.DOWNLOAD_TEXT_SUCCESS, message);
        }

        public void OnDownloadTextFailed(string message)
        {
            receive(MessageHandler.DOWNLOAD_TEXT_FAILED, message);
        }
    }
  

}
