
//------------------------------------------------------------------------------
//
// File: TGPAGameObject
// Module: APM
// Version: 1.0
// Author: zohnzliu
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCloud.TGPA
{
    // TGPA use this to get callback from system
    #region TGPAGameComponent
    public class TGPAGameComponent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private ITGPACallback tgpaCallback;
        public void setGameCallback(ITGPACallback callback) {
            tgpaCallback = callback;
        }

        public void notifySystemInfo(string json) {
            if (tgpaCallback != null) {
                tgpaCallback.notifySystemInfo(json);
            }
        }

    }
    #endregion
}



