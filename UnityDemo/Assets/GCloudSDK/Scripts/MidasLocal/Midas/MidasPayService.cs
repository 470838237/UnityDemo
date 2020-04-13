using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

//Midas's namespace: MidasPay
namespace MidasPay
{
	
	public class MidasPayService : IMidasPayService
	{
		private static string unityVersion = "1.0.3";
//		private static string UNITY_SDK_VERSION = "V1.0.1";

		private static MidasPayService instance;



		private static CallBackUtils mCallBackUtils;
		private static bool mHasInited = false;

		private static string appExtend;

//		private APMidasBaseRequest mAPMidasBasePayRequest;

		private MidasInitCallback mMidasInitCallback;
		private MidasPayCallback mMidasPayCallback;
		private MidasGetLocalPriceCallback mMidasGetProductCallback;
		private MidasGetIntroPriceCallback mMidasGetIntroPriceCallback;
		private MidasGetInfoCallback mMidasGetInfoCallback;
        private MidasReprovideCallback mMidasReprovidetCallback;
        

#if UNITY_EDITOR

#elif UNITY_ANDROID
        private string helperClass = "com.tencent.midas.api.UnityPayHelper";
        private AndroidJavaClass helper = null;


#elif UNITY_IOS
		[DllImport ("__Internal")]
		private static extern void midasSdkSetProcess(string processName);

        [DllImport ("__Internal")]
		private static extern bool midasSdkInitWithIdcInfo(string idc, string env, string idcInfo, string jsonParams);

		[DllImport ("__Internal")]
		private static extern void midasSdkPay(string bizType, string jsonParams);

		[DllImport ("__Internal")]
		private static extern void midasSdkLogEnable(bool enable);

        [DllImport ("__Internal")]
		private static extern string midasSdkGetVersion();

		[DllImport ("__Internal")]
		private static extern bool midasSdkPayEnable();

        [DllImport ("__Internal")]
		private static extern void midasSdkGetProductInfo(string channel,string products);
		
		[DllImport ("__Internal")]
		private static extern void midasSdkGetInfo(string type, string bizType, string jsonParams);

		[DllImport ("__Internal")]
		private static extern void midasSdkGetIntroPrice(string channel,string products);

		[DllImport ("__Internal")]
		private static extern void midasSdkReprovide();
#endif

        private MidasPayService ()
		{
		}

		public static MidasPayService Instance {
			get {
				if (instance == null) {
					mCallBackUtils = CallBackUtils.Instance;
					instance = new MidasPayService ();
				}

				return instance;
			}
		}

		/// <summary>
		/// 初始化接口
		/// </summary>
		/// 
		/// <param name="processName">ProcessName.</param>
		public void SetProcess (string processName)
		{
#if UNITY_EDITOR
			ULog.Log ("Emulator SetProcess");
#elif UNITY_ANDROID
			ULog.Log ("Android SetProcess does not support");
#elif UNITY_IOS
			midasSdkSetProcess(processName);
#endif
		}

		/// <summary>
		/// 初始化接口
		/// </summary>
		/// 
		/// <param name="idc">Idc.</param>
		/// <param name="env">Env.</param>
		/// <param name="req">Req.</param>
		/// <param name="callback">Callback.</param>
		public void Initialize (string idc, string env, string idcInfo, APMidasBaseRequest req, MidasInitCallback callback)
		{
			if (mHasInited) 
			{
				return;
			}


            mMidasInitCallback = callback;

            try 
			{
#if UNITY_EDITOR
				ULog.Log ("Emulator Initialize");
				ULog.Log ("MidasUnityV"+unityVersion);

#elif UNITY_ANDROID
				//TODO:设置android版本号
				ULog.Log ("MidasUnityV"+unityVersion);
#elif UNITY_IOS
//				req.iapInitExtra.Add("app_reserve_3","MidasUnityV"+unityVersion);
#endif

				string reqString = req.ToString ();
				ULog.Log ("Initialize req json ＝ " + reqString);

#if UNITY_EDITOR
				ULog.Log ("Emulator Initialize");

#elif UNITY_ANDROID
				mHasInited = true;
				helper = new AndroidJavaClass (helperClass);
				if (helper == null)
				{
					ULog.Log("Cannot get Java helper class");
				}
				else
				{
					helper.CallStatic("Initialize",idc, env,idcInfo, reqString);
				}

#elif UNITY_IOS
				if (idc.Length == 0)
				{
					idc = "local";
				}
				mHasInited = midasSdkInitWithIdcInfo(idc,env,idcInfo,reqString);

#endif
            }
            catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}


		/// <summary>
		/// 拉起支付
		/// </summary>
		/// 
		/// <param name="req">Req.</param>
		/// <param name="callback">Callback.</param>
		public void Pay (APMidasBaseRequest req, MidasPayCallback callback)
		{
            if (!mHasInited) 
			{
                Debug.LogError ("you should call Initialize first");
				return;
			}
            try {
				appExtend = req.appExtends;	//保存回调的时候，回传给游戏
				mMidasPayCallback = callback;
//				mAPMidasBasePayRequest = req;
				string bizType = req.GetType().Name;
				ULog.Log ("PayType = " + bizType);
				string reqString = req.ToString ();
				ULog.Log ("Pay with json : " + reqString);
#if UNITY_EDITOR
				ULog.Log ("Simulator Pay");

#elif UNITY_ANDROID
			helper = new AndroidJavaClass (helperClass);
			if (helper == null) 
			{
			ULog.Log("Cannot get Java helper class");
				}
			else
			{
				helper.CallStatic("Pay", bizType, reqString);
			}

#elif UNITY_IOS
			midasSdkPay(bizType,reqString);
#endif
            }
            catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}

		public void GetInfo (string reqType, APMidasBaseRequest req, MidasGetInfoCallback callback)
		{
			if (!mHasInited) {
				Debug.LogError ("you should call Initialize first");
				return;
			}
			try {
				mMidasGetInfoCallback = callback;
//				mAPMidasBasePayRequest = req;
				string bizType = req.GetType ().Name;
				ULog.Log ("PayType = " + bizType);
				string reqString = req.ToString ();
				ULog.Log ("GetInfo with json : " + reqString);

#if UNITY_EDITOR
				ULog.Log ("Simulator GetInfo");

#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) 
				{
					ULog.Log("Cannot get Java helper class");
				}
				else
				{
					helper.CallStatic("GetInfo", reqType,bizType, reqString);
				}

#elif UNITY_IOS
				// IOS bizTy=APMidasGameRequest
				midasSdkGetInfo(reqType, bizType, reqString);
#endif
            }
            catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}

		public void Reprovide (MidasReprovideCallback callback)
		{
			if (!mHasInited) {
				Debug.LogError ("you should call Initialize first");
				return;
			}
			try {
				mMidasReprovidetCallback = callback;
				
#if UNITY_EDITOR
				ULog.Log ("Simulator Reprovide");

#elif UNITY_ANDROID
			helper = new AndroidJavaClass (helperClass);
			if (helper == null) 
			{
			ULog.Log("Cannot get Java helper class");
				}
			else
			{
				helper.CallStatic("Reprovide", "");
			}
	
#elif UNITY_IOS
				midasSdkReprovide();
#endif
			}
			catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}


		public void LaunchWeb (APMidasBaseRequest req, MidasPayCallback callback)
		{
			if (!mHasInited) {
				Debug.LogError ("you should call Initialize first");
				return;
			}

			try {
				mMidasPayCallback = callback;
//				mAPMidasBasePayRequest = req;
				string reqString = req.ToString ();
				ULog.Log ("LaunchWeb with json : " + reqString);

#if UNITY_EDITOR
				ULog.Log ("Simulator LaunchWeb");

#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) 
				{
					ULog.Log ("Cannot get Java helper class");
				}
				else
				{
					helper.CallStatic ("LaunchWeb", reqString);
				}

#elif UNITY_IOS
				ULog.Log("IOS LaunchWeb does not support");

#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}


		/// <summary>
		/// 返回IOS平台的内购开关是否已打开，Android平台对这个固定返回True
		/// </summary>
		/// 
		/// <returns>The midas SDK version.</returns>
		public bool IsPayEnable ()
		{  
			bool enable = true;

			try {

#if UNITY_EDITOR
				ULog.Log ("simulator IsIAPEnable");

#elif UNITY_ANDROID
				ULog.Log("android IsIAPEnable");
				// Android不作处理，返回默认值True
				enable = true;

#elif UNITY_IOS
				enable = midasSdkPayEnable();

#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}

			return enable;
		}

		public void SetLogEnable (bool enable)
		{  
			try {
				ULog.setLevel (ULog.Level.Log);
				ULog.Log ("SetLogEnable enable:" + enable);

#if UNITY_EDITOR
				ULog.Log ("simulator LogEnable");

#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) 
				{
					ULog.Log("Cannot get Java helper class");
				}
				else
				{
		
					helper.CallStatic("SetLogEnable", enable);
				}

#elif UNITY_IOS
				midasSdkLogEnable(enable);

#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}

		/// <summary>
		/// Gets the midas SDK version.
		/// </summary>
		/// 
		/// <returns>The midas SDK version.</returns>
		public string GetMidasSDKVersion ()
		{  
			string version = "";
			
			try {

#if UNITY_EDITOR
				ULog.Log ("simulator GetMidasSDKVersion");

#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) 
				{
					ULog.Log("Cannot get Java helper class");
				}
				else
				{
					version = helper.CallStatic<string>("GetMidasSDKVersion");
				}

#elif UNITY_IOS
				version = midasSdkGetVersion();

#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}

			return version;
		}



		public void SetPath (string path)
		{
			try {
				#if UNITY_EDITOR
				ULog.Log ("Emulator SetPath");

				#elif UNITY_ANDROID
		helper = new AndroidJavaClass (helperClass);
		if (helper == null)
		{
		ULog.Log("Cannot get Java helper class");
		}
		else
		{
		helper.CallStatic("SetPath", path);
		}
				#elif UNITY_IOS
				#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}

		// type = "wechat"
		public void CouponsRollBack (string s)
		{
			try {
				ULog.Log ("CouponsRollBack s = " + s);

#if UNITY_EDITOR
				ULog.Log ("Simulator CouponsRollBack");

#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) 
				{
					ULog.Log("Cannot get Java helper class");
				}
				else
				{
					helper.CallStatic("CouponsRollBack", s);
				}

#elif UNITY_IOS
#endif
			} catch (System.Exception e) {
				ULog.LogError ("catch exception : " + e.Message);
			}
		}


        public void GetLocalPrice(string channel, List<string> productList, MidasGetLocalPriceCallback callback)
        {
            if (!mHasInited)
            {
                Debug.LogError("you should call Initialize first");
                return;
            }

            mMidasGetProductCallback = callback;

            if (productList == null || productList.Count == 0)
            {
                ULog.LogError("productList is empty");
                return;
            }

            string products = Json.Serialize(productList);
            ULog.Log("productList:" + products);
#if UNITY_EDITOR
            ULog.Log("use simulator");
#elif UNITY_ANDROID
				helper = new AndroidJavaClass (helperClass);
				if (helper == null) {
				}
				else{
					helper.CallStatic("GetProductInfo",channel,products);
				}
#elif UNITY_IOS
				midasSdkGetProductInfo(channel,products);
#endif
        }

		public void GetIntroPrice(string channel, List<string> productList, MidasGetIntroPriceCallback callback)
		{
			if (!mHasInited)
			{
				Debug.LogError("you should call Initialize first");
				return;
			}

			mMidasGetIntroPriceCallback = callback;

			if (productList == null || productList.Count == 0)
			{
				ULog.LogError("productList is empty");
				return;
			}

			string products = Json.Serialize(productList);
			ULog.Log("productList:" + products);
#if UNITY_EDITOR
			ULog.Log("use simulator");
#elif UNITY_ANDROID
			ULog.Log("Android not support");
#elif UNITY_IOS
			midasSdkGetIntroPrice(channel,products);
#endif
		}
		
	
		// for garena
		public void GetGarenaProductInfo(APMidasGameRequest request, MidasGetLocalPriceCallback callback)
		{
			if (!mHasInited)
			{
				ULog.LogError("you should call Initialize first");
				return;
			}

			mMidasGetProductCallback = callback;
			string reqString = request.ToString();
#if UNITY_EDITOR
			ULog.Log("use simulator");
#elif UNITY_ANDROID
			helper = new AndroidJavaClass (helperClass);
			if (helper == null) {
			}
			else{
				helper.CallStatic("GetProductInfo",reqString);
			}
#endif
		}

        // 接收到Java/OC发来的消息后，这个方法会被调用，用以回调C#层的游戏
        public  void MidasPayCallback (string result)
		{
			if (mMidasPayCallback != null) {
				APMidasResponse resp = new APMidasResponse (result);
				resp.appExtends = appExtend;
				mMidasPayCallback.OnMidasPayFinished (resp);
				// 用完即废，防止影响后续逻辑
				mMidasPayCallback = null;
			}
		}

		public  void MidasLoginExpiredCallback ()
		{
			if (mMidasPayCallback != null) {
				mMidasPayCallback.OnMidasLoginExpired ();
				// 用完即废，防止影响后续逻辑
				mMidasPayCallback = null;
			}
		}

        public void MidasInitCallback(string result)
        {
            if (mMidasInitCallback != null)
            {
                Dictionary<string, object> jsonResult = Json.Deserialize(result) as Dictionary<string, object>;
                mMidasInitCallback.OnMidasInitFinished(jsonResult);
            }
        }
        /// <summary>
        /// 回调外发传接来的GetInfo回调接口
        /// </summary>
        /// 
        /// <param name="type">Type.</param>
        /// <param name="result">Result.</param>
        public  void MidasGetInfoFinishCallback (string type, int retCode, string json)
		{
			if (mMidasGetInfoCallback != null) {
				mMidasGetInfoCallback.GetInfoFinished (type, retCode, json);
				// 用完即废，防止影响后续逻辑
				mMidasGetInfoCallback = null;
			}
		}


        /// <summary>
        /// 获取本地价格信息的回调
        /// </summary>
        /// 
        /// <param name="result">Result.</param>
        public void MidasGetLocalPriceCallback(string resul)
        {
            if (mMidasGetProductCallback != null)
            {
                Dictionary<string, object> jsonResult = Json.Deserialize(resul) as Dictionary<string, object>;
                mMidasGetProductCallback.OnMidasGetProdcut(jsonResult);
				mMidasGetProductCallback = null;
            }
        }

		/// <summary>
		/// 获取推介促销价格信息回调
		/// </summary>
		/// 
		/// <param name="result">Result.</param>
		public void MidasGetIntroPriceCallback(string resul)
		{
			if (mMidasGetIntroPriceCallback != null)
			{
				Dictionary<string, object> jsonResult = Json.Deserialize(resul) as Dictionary<string, object>;
				mMidasGetIntroPriceCallback.OnMidasGetIntroPrice(jsonResult);
				mMidasGetIntroPriceCallback = null;
			}
		}


        /// <summary>
        /// 补发的回调
        /// </summary>
        /// 
        /// <param name="result">Result.</param>
        public void MidasReProvidetCallback(string result)
        {
            if (mMidasReprovidetCallback != null)
            {
                Dictionary<string, object> jsonResult = Json.Deserialize(result) as Dictionary<string, object>;
                mMidasReprovidetCallback.OnMidasReprovideFinished(jsonResult);
            }
        }


    }

    // 用于接收来自Java / Object C层SendMessage时传过来的消息
    public class CallBackUtils : Singleton<CallBackUtils>
	{
		public void MidasPayCallback (string result)
		{
			ULog.Log ("CallBackUtils.MidasPayCallback message from Java/OC = " + result);
			// 接收到Java/OC层发送的消息后，调用MidasPayService对应的方法
			MidasPayService.Instance.MidasPayCallback (result);
		}

		public void MidasLoginExpiredCallback ()
		{
			ULog.Log ("Got MidasLoginExpiredCallback message from Java/OC");
			// 接收到Java/OC层发送的消息后，调用MidasPayService对应的方法
			MidasPayService.Instance.MidasLoginExpiredCallback ();
		}

		public void MidasGetLocalPriceCallback (string json)
		{
            MidasPayService.Instance.MidasGetLocalPriceCallback(json);
        }

		public void MidasGetIntroPriceCallback (string json)
		{
			MidasPayService.Instance.MidasGetIntroPriceCallback(json);
		}

        public void MidasInitCallback(string result)
        {
            MidasPayService.Instance.MidasInitCallback(result);
        }
        public void MidasGetInfoCallback(string json)
        {
            ULog.Log("Got MidasGetInfoFinish message from Java/OC, json = " + json);
            // 接收到Java/OC层发送的消息后，调用MidasPayService对应的方法
            APMidasGetInfoResult result2 = new APMidasGetInfoResult(json);
            ULog.Log("CallBackUtils.MidasGetInfoFinish result.type = " + result2.type);
            ULog.Log("CallBackUtils.MidasGetInfoFinish result.ret = " + result2.ret);
//            ULog.Log("CallBackUtils.MidasGetInfoFinish result.msg = " + result2.msg);

            MidasPayService.Instance.MidasGetInfoFinishCallback(result2.type, result2.ret, json);
        }
		public void MidasGetShortOpenidCallback(string json)
		{
			ULog.Log("Got MidasGetInfoFinish message from Java/OC, json = " + json);
			// 接收到Java/OC层发送的消息后，调用MidasPayService对应的方法
			APMidasGetInfoResult result2 = new APMidasGetInfoResult(json);
			result2.type = "get_short_openid";
			//ULog.Log("CallBackUtils.MidasGetInfoFinish result.type = " + result2.type);
			//ULog.Log("CallBackUtils.MidasGetInfoFinish result.ret = " + result2.ret);
			//ULog.Log("CallBackUtils.MidasGetInfoFinish result.msg = " + result2.msg);

			MidasPayService.Instance.MidasGetInfoFinishCallback(result2.type, result2.ret, json);
		}
        public void MidasReProvidetCallback(string result)
        {
            MidasPayService.Instance.MidasReProvidetCallback(result);
        }
    }
	
	
	
}
