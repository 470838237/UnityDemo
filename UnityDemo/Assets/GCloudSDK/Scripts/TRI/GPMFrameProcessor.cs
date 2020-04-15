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

#region GCloud.GPM definition
namespace GCloud.GPM
{
    public class FrameProcessor : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            GPMAgent.PostFrame();
        }
    }
}
#endregion
