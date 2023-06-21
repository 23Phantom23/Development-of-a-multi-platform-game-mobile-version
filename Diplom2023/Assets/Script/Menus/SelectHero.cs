using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class SelectHero : MonoBehaviourPunCallbacks
{
    public TMP_Text NamePrefabHero;
    public static string IDRoomJoin;
    public static string IDRoomCreate;
    public static string CreateOrConnect;
    public static bool visible = true;
    public static byte countPlayer;

    public void SelectHeroButton()
    {
        if (MenuGame.OnlineOffline == true)
        {
            PlayerManager.YouPerson = NamePrefabHero.text;
            Debug.Log(PlayerManager.YouPerson);

            if (CreateOrConnect == "Create")
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = countPlayer; //(byte)CountPlayers.value;
                roomOptions.IsVisible = visible; //true or false \ visible;
                PhotonNetwork.CreateRoom(IDRoomCreate, roomOptions);
            }
            if (CreateOrConnect == "Connect")
            {
                PhotonNetwork.JoinRoom(IDRoomJoin);
            }
        }
        else
        {
            PlayerManager.YouPerson = NamePrefabHero.text;
            Debug.Log(PlayerManager.YouPerson);
            SceneManager.LoadScene("Game");
        }
    }
}
