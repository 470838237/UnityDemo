using System;

namespace MidasPay
{
	///<summary>
	///CallBack for GetInfo
	///</summary>
	///<remarks>
	///
	///</remarks>
	public class APMidasGetInfoResult : JsonSerializable
	{
		//info type, "mp" or "get_short_openid"
		[JsonProp("type")]
		public string type = "mp";

		//info 
		//[JsonProp("msg")]
		//public string msg;

		//ret code
		[JsonProp("ret")]
		public int ret;

		public APMidasGetInfoResult (string param) : base (param) { }

		public APMidasGetInfoResult (object json) : base (json) { }
	}
}

