using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MidasPay {

    ///<summary>
    ///callback for payment
    ///</summary>
    public interface MidasPayCallback
    {
        void OnMidasLoginExpired();
		void OnMidasPayFinished(APMidasResponse result);
    }
    
	///<summary>
	///Callback for GetInfo
	///</summary>
	public interface MidasGetInfoCallback
	{
		//param type:"mp","short_openid"
		//param retCode: 0 is success; other is failed
		//param json: the market and product info
		void GetInfoFinished (string type, int retCode, string json);
	}


    ///<summary>
    ///初始化的回调。如果涉及到补发货，会通过此回调通知app补发货的信息
    ///</summary>
    public interface MidasInitCallback
    {
        void OnMidasInitFinished(Dictionary<string, object> result);
    }

    ///<summary>
    ///补发货的回调。业务主动调用补发货接口的情况下，会通过此回调返回结果。
    ///</summary>
    public interface MidasReprovideCallback
    {
        void OnMidasReprovideFinished(Dictionary<string, object> result);
    }

    ///<summary>
    ///获取物品信息
    ///</summary>
    public interface MidasGetLocalPriceCallback
    {
        void OnMidasGetProdcut(Dictionary<string, object> result);
    }

	///<summary>
	///获取推介物品信息
	///</summary>
	public interface MidasGetIntroPriceCallback
	{
		void OnMidasGetIntroPrice(Dictionary<string, object> result);
	}
	



    /// <summary>
    /// midas payment interface
    /// </summary>
    public interface IMidasPayService
	{
		//Only for iOS
		//param procesName:sea/local, default value is 'local';
		//attention, The 'SetProcess' must be called before all midas interface method.
		void SetProcess (string processName);

		//is enable log, sugest to set false when after fully tested.
        void SetLogEnable (bool enable);

		//param env:test: in sandbox environment; release: in payment release environment;Please use release environment only after fully tested in test environment.
		//param req:Request payment base object
		void Initialize (string idc, string env, string idcInfo, APMidasBaseRequest req, MidasInitCallback callback);

		//param req:A specific request payment object, such as APMidasGameRequest, APMidasGoodsRequest, APMidasMonthRequest or APMidasSubscribeRequest
		void Pay (APMidasBaseRequest req, MidasPayCallback callback);

		//is payment allowed, this just valid for ios ,android is always return true
		bool IsPayEnable ();

		//get market info and productInfo from midas server:
		//param reqType:"mp",get market info;"short_openid",get the short openid
		//param req:A specific request payment object, such as APMidasGameRequest, APMidasGoodsRequest, APMidasMonthRequest or APMidasSubscribeRequest
		//param callback: the callback of getInfo
		void GetInfo (string reqType, APMidasBaseRequest req, MidasGetInfoCallback callback);

		//check if it have undeliveried orders and do reprovide 
		//param callback:you can get the callback only when it have undeliveried orders
		void Reprovide (MidasReprovideCallback callback);
		
		


        ///*************************************       optional interfaces below       ********************************************************************************///


        //return sdk version
        //[optional]
        string GetMidasSDKVersion ();

		//only for android
		//[optional]
		void LaunchWeb (APMidasBaseRequest req, MidasPayCallback callback);

		//only for android
		//[optional]
		void SetPath (string path);

		//only for android
		//[optional]
		void CouponsRollBack (string s);

		//only for iap and googlewallet
		//[optional]
		void GetLocalPrice(string channel, List<string> productList, MidasGetLocalPriceCallback callback);

		//only for iap
		//[optional]
		void GetIntroPrice(string channel, List<string> productList, MidasGetIntroPriceCallback callback);
		
		// only for garena
		void GetGarenaProductInfo(APMidasGameRequest request, MidasGetLocalPriceCallback callback);
    }
}
