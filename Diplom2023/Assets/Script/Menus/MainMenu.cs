using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject Mainmenu;
    [SerializeField] private GameObject GameSettingsmenu;
    [SerializeField] private GameObject AccountsSettingsmenu;
    [SerializeField] private GameObject GameSolomenu;
    [SerializeField] private GameObject SelectHeroMenu;

    void Start()
    {
        Mainmenu.SetActive(true);
        SelectHeroMenu.SetActive(false);
        GameSettingsmenu.SetActive(false);
        AccountsSettingsmenu.SetActive(false);
        GameSolomenu.SetActive(false);
    }

    public void GameSettingsMenuButton()
    {
        Mainmenu.SetActive(false);
        GameSettingsmenu.SetActive(true);
        AccountsSettingsmenu.SetActive(false);
        GameSolomenu.SetActive(false);
    }
    public void AccountsSettingsMenuButton()  
    {
        Mainmenu.SetActive(false);
        GameSettingsmenu.SetActive(false);
        AccountsSettingsmenu.SetActive(true);
        GameSolomenu.SetActive(false);
    }
    public void GameSoloMenuButton()
    {
        MenuGame.OnlineOffline = false;
        SelectHeroMenu.SetActive(true);
    }
    public void CloseGameSoloMenuButton()
    {
        SelectHeroMenu.SetActive(false);
    }
    public void GameNetworkButton()
    {
        MenuGame.OnlineOffline = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To PUN Server");
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("GameNetworkMenu");
    }

    public void BackMainMenuButton()
    {
        Mainmenu.SetActive(true);
        GameSettingsmenu.SetActive(false);
        AccountsSettingsmenu.SetActive(false);
        GameSolomenu.SetActive(false);
    }
    public void ExitAccountButton()
    {
        SceneManager.LoadScene("StartAccount");
    }
}