//------------------------------------------------------------------------------
// 
// File:    JitterBufferStrategy
// Module:  LockStep
// Version: 1.0    
// Author:  vforkkcai
// 
//------------------------------------------------------------------------------


using UnityEngine;
using System;
using System.Collections.Generic;

using GCloud;
namespace GCloud.LockStep
{
	public class JitterBufferStrategy : IBufferStrategy
	{
		public ILockStep Steper { get; set; }
		public bool OnInput(byte[] data, int len, bool rawUdp, LockStepInputFlag flag)
		{
			if(Steper != null)
			{
				return Steper.Write(data, len, rawUdp, flag);
			}
			return false;
		}

		public FrameInfo OnPopFrame()
		{
			if(Steper != null)
			{
				FrameInfo frame = new FrameInfo();
				bool ret = Steper.ReadFrame(frame);
				if(ret)
				{
					return frame;
				}
			}
			return null;
		}

		//DateTime lastUpate = DateTime.MinValue;
		public void Update()
		{
		}
	}

}
