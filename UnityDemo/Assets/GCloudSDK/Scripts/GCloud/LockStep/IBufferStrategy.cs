//------------------------------------------------------------------------------
// 
// File:    IBufferStrategy
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
	public interface IBufferStrategy
	{
		bool OnInput(byte[] data, int len, bool rawUdp, LockStepInputFlag flag);
		FrameInfo OnPopFrame();

		void Update();

		ILockStep Steper { get; set; }
	}

	public interface ILockStep
	{
		bool Write(byte[] data, int len, bool rawUdp, LockStepInputFlag flag);
		bool ReadFrame(FrameInfo frame);
	}

	public class DefaultBufferStrategy : IBufferStrategy
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
		
		FrameInfo frame = new FrameInfo();
		public FrameInfo OnPopFrame()
		{
			if(Steper != null)
			{
                frame.Clear();
                bool ret = Steper.ReadFrame(frame);
				if(ret)
				{
					return frame;
				}
			}
			return null;
		}

		public void Update()
		{
		}
	}

}
