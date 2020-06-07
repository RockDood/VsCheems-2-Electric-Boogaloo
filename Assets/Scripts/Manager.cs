using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : NetworkLobbyManager
{
    private string[] TeamCats;
    private string[] TeamRats;
    private string[] PlayerList;

    void Update()
    {

    }

    //void OnLobbyServerConnect()
    //{
    //    GetPlayerList();
    //}

    //void GetPlayerList()
    //{
    //    if (NetworkServer.active)
    //    {
    //        PlayerList = NetworkServer.connections.ToString();
    //    }
    //}

    public void StartupHost()
    {
        if (NetworkServer.active)
            return;
        SetPort();
        NetworkLobbyManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkLobbyManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkLobbyManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkLobbyManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetupMenuSceneButtons();
        }
        else
        {
            SetupOtherSceneButtons();
        }
    }

    void SetupMenuSceneButtons()
    {
        //GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        //GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkLobbyManager.singleton.StopHost);
    }
}
