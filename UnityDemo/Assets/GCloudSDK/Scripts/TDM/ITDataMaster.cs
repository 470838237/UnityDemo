using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDM
{
	public abstract class ITDataMaster
	{
		protected ITDataMaster()
		{
		}

		public readonly static ITDataMaster Instance = new TDataMaster();


		public abstract void ReleaseInstance();

		public abstract string GetTDMUID();

		public abstract bool Initialize(string appId, string appChannel,bool isTest=false);

		public abstract void EnableReport(bool enable);

		public abstract void SetLogLevel(TLogPriority level);

		public abstract void ReportEvent(int srcId, string eventName, CustomInfo eventInfo);  

		public abstract void ReportBinary(int srcId,string eventName, byte[] data, int len);

        public abstract string GetSessionID();

		public abstract void SetRouterAddress(bool isTest, string url);
    }
}
