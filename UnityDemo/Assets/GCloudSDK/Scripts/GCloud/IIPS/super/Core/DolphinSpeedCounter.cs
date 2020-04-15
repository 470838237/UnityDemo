using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GCloud
{
    namespace Dolphin
    {
        class DolphinSpeedCounter
        {
            private uint currentSize = 0;
            private uint lastCurrentSize = 0;
            private LinkedList<uint> mSpeedCountList = new LinkedList<uint>();
            private uint speed = 0;
            private bool doTimer = false;
            public float timer = 1.0f;
            public void StartSpeedCounter()
            {
                doTimer = true;
                mSpeedCountList.Clear();
                speed = 0;
            }

            public void StopSpeedCounter()
            {
                doTimer = false;
                mSpeedCountList.Clear();
                speed = 0;
            }

            public void SetSize(uint size)
            {
                currentSize = size;
            }

            public void SpeedCounter()
            {
                if (!doTimer)
                    return;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 1.0f;
                    uint thisSecondSize = currentSize - lastCurrentSize;
                    lastCurrentSize = currentSize;
                    if (mSpeedCountList.Count >= 5)
                    {
                        mSpeedCountList.RemoveFirst();
                        mSpeedCountList.AddLast(thisSecondSize);
                    }
                    else
                    {
                        mSpeedCountList.AddLast(thisSecondSize);
                    }
                    speed = thisSecondSize;
                }
            }

            public uint GetSpeed()
            {
                uint sum = 1;
                uint total = 0;
                System.UInt64 totalsize = 0;
                foreach (uint s in mSpeedCountList)
                {
                    totalsize += s * sum * sum;
                    total += sum * sum;
                    sum++;
                }
                uint speed1 = (uint)(totalsize / (total > 0 ? total : 1));
                return speed1;
            }

            public uint GetCurrentSpeed()
            {
                return speed;
            }
        }
    }
}
