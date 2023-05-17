using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.IO;

public class MenuGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject MenuPause;
    void Start()
    {
        MenuPause.SetActive(false);

    }

    public void OpenMenuPauseButton()
    {
        MenuPause.SetActive(true);
    }
    public void CloseMenuPauseButton()
    {
        MenuPause.SetActive(false);
    }


    public void DisconnectButton()
    {
        //PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }
    /*public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("GameNetworkMenu");
    }*/
    public override void OnDisconnected(DisconnectCause cause)
    {
        /*Debug.Log("On OnDisconnected executed in Game Over Manger class.........");
        base.OnDisconnected(cause);*/
        SceneManager.LoadScene("MainMenu");
    }
}
