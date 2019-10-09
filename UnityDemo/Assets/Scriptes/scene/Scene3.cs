using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HonorSDK;
using UnityEngine.UI;


public class Scene3 : BaseScene
{
    private Dropdown goodsOptionView;
    private Button payView;
    private string currentGoodsId;
    private static readonly DateTime Jan1st1970 = new DateTime
    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }

    void OnEnable()
    {
        HonorSDKImpl.GetInstance().GetServerList(delegate(ServerList list)
        {
         
        });

        Button switchAccountView = GameObject.Find("SwitchButton").GetComponent<Button>();
        Button bindView = GameObject.Find("BindButton").GetComponent<Button>();
        Button logoutView = GameObject.Find("LogoutButton").GetComponent<Button>();
        payView = GameObject.Find("PaytButton").GetComponent<Button>();
        Button exitView = GameObject.Find("ExitButton").GetComponent<Button>();
        goodsOptionView = GameObject.Find("GoodsOption").GetComponent<Dropdown>();
        
        exitView.onClick.AddListener(delegate ()
        {
            if (HonorSDKImpl.GetInstance().HasExitDialog())
            {
                Exit();
            }
            else
            {
                //创建游戏退出框
                ExitDialog.Inst.show(delegate ()
                {
                    Exit();
                });
            }

        });
        bindView.onClick.AddListener(delegate ()
        {
            Bind();
        });

        switchAccountView.onClick.AddListener(delegate ()
        {
            SwitchAccount();
        });

        logoutView.onClick.AddListener(delegate ()
        {
            Logout();
        });
        payView.enabled = false;
        goodsOptionView.enabled = false;
        getGoodsList();
        payView.onClick.AddListener(delegate ()
        {
            Pay();
        });
        UploadGameRoleInfo();


    }

    private void Bind()
    {
        bool isSupportApi = HonorSDKImpl.GetInstance().IsSupportApi(Api.BIND);
        if (!isSupportApi)
        {
            Debug.Log("HonorSDK:StartBind.IsSupportApi = " + isSupportApi);
            return;
        }

        HonorSDKImpl.GetInstance().StartBind(delegate (ResultBind result)
        {
            Debug.Log("HonorSDK:StartBind.success = " + result.success);
            if (result.success)
            {
                Debug.Log("HonorSDK:StartBind.nickName = " + result.nickName);
            }
            else
            {
                Debug.Log("HonorSDK:StartBind.message = " + result.message);
            }
        });

    }


    public static void Exit()
    {
        HonorSDKImpl.GetInstance().Exit(delegate (Result result)
        {
            Debug.Log("HonorSDK:Exit.success = " + result.success);
            if (!result.success)
                Debug.Log("HonorSDK:Exit.message = " + result.message);
        });
    }

    private void SwitchAccount()
    {
        bool isSupportApi = HonorSDKImpl.GetInstance().IsSupportApi(Api.SWITCH_ACCOUNT);
        if (!isSupportApi)
        {
            Debug.Log("HonorSDK:SwitchAccount.IsSupportApi = " + isSupportApi);
            return;
        }
        //判断是否支持切换账号
        HonorSDKImpl.GetInstance().SwitchAccount(delegate (UserInfo userInfo)
        {
            Debug.Log("HonorSDK:SwitchAccount.success = " + userInfo.success);
            if (userInfo.success)
            {
                Debug.Log("HonorSDK:SwitchAccount.accessToken = " + userInfo.accessToken
                           + ",nickName =" + userInfo.nickName
                           + ",uid =" + userInfo.uid
                           );
                //退出到选服界面
                LoadTest.Instance.ChangeScene(2);
            }
            else
            {
                //切换失败继续游戏
                Debug.Log("HonorSDK:SwitchAccount.message = " + userInfo.message);
            }
        });
    }

    private void Logout()
    {
        bool isSupportApi = HonorSDKImpl.GetInstance().IsSupportApi(Api.LOGOUT);
        if (!isSupportApi)
        {
            Debug.Log("HonorSDK:Logout.IsSupportApi = " + isSupportApi);
            return;
        }
        HonorSDKImpl.GetInstance().Logout(delegate (Result result)
        {
            Debug.Log("HonorSDK:Logout.success = " + result.success);
            if (result.success)
            {
                //注销成功,退出到登陆界面
                LoadTest.Instance.ChangeScene(1);
            }
            else
            {
                //注销失败继续玩游戏
                Debug.Log("HonorSDK:Logout.message = " + result.message);
            }

        });

    }

    private void Pay()
    {
        OrderInfo orderInfo = new OrderInfo();
        orderInfo.count = 1;
        orderInfo.extra = CurrentTimeMillis().ToString();
        orderInfo.serverId = Scene2.currentServerId;
        orderInfo.roleName = "UnityDemoAndorid";
        orderInfo.roleLevel = 1;
        orderInfo.roleId = "1";
        orderInfo.goodsId = currentGoodsId;
        HonorSDKImpl.GetInstance().Pay(orderInfo, delegate (ResultPay result)
        {
            Debug.Log("HonorSDK:Pay.success = " + result.success);

            if (result.success)
            {
                Debug.Log("HonorSDK:Pay.orderId = " + result.orderId);
            }
            else
            {
                Debug.Log("HonorSDK:Pay.message = " + result.message);
            }

        });
    }

    private void UploadGameRoleInfo()
    {

        GameRoleInfo gameRoleInfo = new GameRoleInfo();
        gameRoleInfo.roleId = "1";
        gameRoleInfo.roleLevel = 2;
        gameRoleInfo.roleName = "UnityDemoAndorid";
        gameRoleInfo.roleVip = "1";
        gameRoleInfo.serverId = Scene2.currentServerId;
        gameRoleInfo.type = GameRoleInfo.TYPE_ENTER_GAME;
        gameRoleInfo.extra = "";
        gameRoleInfo.lastUpdate = (int)CurrentTimeMillis();
        HonorSDKImpl.GetInstance().UploadGameRoleInfo(gameRoleInfo);
    }

    private void getGoodsList()
    {
        HonorSDKImpl.GetInstance().GetGoodsList(delegate (GoodsList result)
         {
             Debug.Log("HonorSDK:GetGoodsList.success = " + result.success);
             if (result.success)
             {
                 GetGoodsListSuccess(result);
                 List<GoodsInfo> goodsList = result.goods;
                 foreach (GoodsInfo goodsInfo in goodsList)
                 {
                     Debug.Log("HonorSDK:GetNoticeList.goods.item.category =" + goodsInfo.category
                        + ",count =" + goodsInfo.count
                        + ",currency =" + goodsInfo.currency
                        + ",description =" + goodsInfo.description
                        + ",endTime =" + goodsInfo.endTime
                        + ",gift =" + goodsInfo.gift
                        + ",goodsId =" + goodsInfo.goodsId
                        + ",goodsName =" + goodsInfo.goodsName
                        + ",limitByDay =" + goodsInfo.limitByDay
                        + ",price =" + goodsInfo.price
                        + ",priceDisplay =" + goodsInfo.priceDisplay
                        + ",ratio =" + goodsInfo.ratio
                        + ",startTime =" + goodsInfo.startTime
                        + ",ratio =" + goodsInfo.tag
                        + ",url =" + goodsInfo.url
                    );
                 }


             }
             else
             {
                 Debug.Log("HonorSDK:GetGoodsList.message = " + result.message);
             }
         },Scene2.currentServerId, "", "");

    }

    private void GetGoodsListSuccess(GoodsList result)
    {
        goodsOptionView.enabled = true;
        payView.enabled = true;

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (GoodsInfo goodsInfo in result.goods)
        {
            string goodsId = goodsInfo.goodsId;
            Dropdown.OptionData item = new Dropdown.OptionData();
            item.text = goodsId;
            options.Add(item);
        }
        goodsOptionView.AddOptions(options);
        goodsOptionView.itemText.text = options[0].text;
        currentGoodsId = result.goods[0].goodsId;
        goodsOptionView.onValueChanged.AddListener(delegate (int index)
        {
            currentGoodsId = result.goods[index].goodsId;
            goodsOptionView.itemText.text = result.goods[index].goodsId;

        });

    }

}
