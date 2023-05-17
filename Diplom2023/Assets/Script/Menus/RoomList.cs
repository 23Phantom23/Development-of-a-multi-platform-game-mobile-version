using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomList : MonoBehaviour
{
    [SerializeField] private Text NameRoom;
    [SerializeField] private Text CountPlayers;

    public void SetInfo(RoomInfo info)
    {
        NameRoom.text = info.Name;
        CountPlayers.text = info.PlayerCount + "/" + info.MaxPlayers;

    }

    public void SelectJoinRoomButton()
    {
        Text selectedRooms = GetComponentInChildren<Text>();

        GameObject textPole = GameObject.Find("IDRoomJoin_Input");
        InputField selectedRoom = textPole.GetComponent<InputField>();
        selectedRoom.text = "" + selectedRooms.text;
    }
}

//RoomName_Text CountPlayers_Text
