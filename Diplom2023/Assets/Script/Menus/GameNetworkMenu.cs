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

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        CountPlayersText.text = "" + CountPlayers.value;
    }

    public void CreateRoom()
    {
        bool visible = true;
        if (IsVisibleSlider.value == 0)
        {
            visible = false;
        }else if (IsVisibleSlider.value == 1)
        {
            visible = true;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)CountPlayers.value; //(byte)CountPlayers.value;
        roomOptions.IsVisible = visible; //true or false \ visible;
        PhotonNetwork.CreateRoom(IDRoomCreate.text, roomOptions);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(IDRoomJoin.text);
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
