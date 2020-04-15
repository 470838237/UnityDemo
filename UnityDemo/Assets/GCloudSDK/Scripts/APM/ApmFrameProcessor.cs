//------------------------------------------------------------------------------
//
// File: ApmFrameProcessor
// Module: APM
// Version: 1.0
// Author: vincentwgao
//
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

#region GCloud.APM definition
namespace GCloud.APM
{
    public class FrameProcessor : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            ApmAgent.PostFrame();
        }
    }
}
#endregion
