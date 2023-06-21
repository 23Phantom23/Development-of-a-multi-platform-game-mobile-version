using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using System.IO;

public class GameNetworkMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField IDRoomCreate;
    [SerializeField] private InputField IDRoomJoin;
    [SerializeField] private Slider IsVisibleSlider;
    [SerializeField] private Slider CountPlayers;
    [SerializeField] private Text CountPlayersText;
    [SerializeField] private RoomList itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject MenuSelectHero;

    void Start()
    {
        MenuSelectHero.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        MenuSelectHero.SetActive(false);
    }
    private void Update()
    {
        CountPlayersText.text = "" + CountPlayers.value;
    }

    public void CreateRoom()
    {
        MenuSelectHero.SetActive(true);
        SelectHero.CreateOrConnect = "Create";

        SelectHero.visible = true;
        if (IsVisibleSlider.value == 0)
        {
            SelectHero.visible = false;
        }else if (IsVisibleSlider.value == 1)
        {
            SelectHero.visible = true;
        }
        SelectHero.countPlayer = (byte)CountPlayers.value;
        SelectHero.IDRoomCreate = IDRoomCreate.text;
    }
    public void JoinRoom()
    {
        MenuSelectHero.SetActive(true);
        SelectHero.CreateOrConnect = "Connect";
        SelectHero.IDRoomJoin = IDRoomJoin.text;
    }

    public void CloseMenuSelectHero()
    {
        SelectHero.CreateOrConnect = "";
        SelectHero.visible = true;
        SelectHero.countPlayer = 0;
        SelectHero.IDRoomJoin = IDRoomJoin.text;
        SelectHero.IDRoomCreate = "";
        SelectHero.IDRoomJoin = "";
        MenuSelectHero.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }


    public void BackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        //Destroy(GameObject.Find("Room Manager"));
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        /*Debug.Log("On OnDisconnected executed in Game Over Manger class.........");
        base.OnDisconnected(cause);*/
        //PhotonNetwork.LoadLevel("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            RoomList list = Instantiate(itemPrefab, content);
            if(list != null)
            {
                /*if (info.MaxPlayers == 0)
                {
                    if (content != null)
                    {
                        // Перебираем все дочерние объекты
                        foreach (Transform child in content.transform)
                        {
                            // Уничтожаем каждый дочерний объект
                            Destroy(child.gameObject);
                        }
                        list.SetInfo(info);
                    }
                }*/
                list.SetInfo(info);
            }
        }
    }
}
