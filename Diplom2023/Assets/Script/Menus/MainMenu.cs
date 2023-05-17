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

    void Start()
    {
        Mainmenu.SetActive(true);
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
        Mainmenu.SetActive(false);
        GameSettingsmenu.SetActive(false);
        AccountsSettingsmenu.SetActive(false);
        GameSolomenu.SetActive(true);
    }
    public void GameNetworkButton()
    {
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