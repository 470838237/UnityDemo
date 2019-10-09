using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HonorSDK;
using UnityEngine.UI;

public class Scene2 : BaseScene
{
    private Dropdown optionView;
    private Button enterGameButton;

    public static string currentServerId {
        set; get; }

    

    void OnEnable()
    {
        GameObject gameObjectOption = transform.Find("Dropdown").gameObject;
        GameObject gameObjectEnterGame = transform.Find("EnterGameButton").gameObject;
        optionView = gameObjectOption.GetComponent<Dropdown>();
       
        enterGameButton = gameObjectEnterGame.GetComponent<Button>();
        optionView.enabled = false;
        enterGameButton.enabled = false;
        GetServerList();

        Debug.Log("HonorSDK:enterGameButton");
        enterGameButton.onClick.AddListener(delegate ()
        {
            LoadTest.Instance.ChangeScene(3);

        });
    }



    private void GetServerListSuccess(ServerList result)
    {
        optionView.enabled = true;
        enterGameButton.enabled = true;

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (ServerInfo server in result.servers)
        {
            string serverName = server.serverName;
            Dropdown.OptionData item = new Dropdown.OptionData();
            item.text = serverName;
            options.Add(item);
        }       
        optionView.AddOptions(options);
        optionView.captionText.text = options[0].text;
        currentServerId = result.servers[0].serverId;
        optionView.onValueChanged.AddListener(delegate(int index)
        {
            optionView.value = index;
            currentServerId = result.servers[index].serverId;
            optionView.captionText.text = result.servers[index].serverName;
        });
        
        
    }

    public void GetServerList()
    {
        HonorSDKImpl.GetInstance().GetServerList(delegate (ServerList result)
        {

            Debug.Log("HonorSDK:GetServerList.success = " + result.success);
            if (result.success)
            {
                GetServerListSuccess(result);
                Debug.Log("HonorSDK:GetServerList.tester = " + result.tester
                    + ",time =" + result.time
                    + ",servers = [...]"
                    );

                List<ServerInfo> servers = result.servers;

                foreach (ServerInfo server in servers)
                {
                    Debug.Log("HonorSDK:GetServerList.servers.item.address =" + server.address
                        + ",closeTime =" + server.closeTime
                        + ",label =" + server.label
                        + ",openTime =" + server.openTime
                        + ",serverId =" + server.serverId
                        + ",serverName =" + server.serverName
                        + ",status =" + server.status
                        + ",tag =" + server.tag
                        + ",roles = [...]"
                    );

                    List<GameRoleInfo> roles = server.roles;

                    foreach (GameRoleInfo role in roles)
                    {
                        Debug.Log("HonorSDK:GetServerList.servers.item.roles.item.lastUpdate =" + role.lastUpdate
                        + ",extra =" + role.extra
                        + ",roleId =" + role.roleId
                        + ",roleLevel =" + role.roleLevel
                        + ",roleName =" + role.roleName
                        + ",roleVip =" + role.roleVip
                        + ",serverId =" + role.serverId
                        + ",roles ={...}"
                        );

                    }                 
                }
                
            }
            else
            {
                Debug.Log("HonorSDK:GetServerList.message = " + result.message);
            }

        });
    }
}
