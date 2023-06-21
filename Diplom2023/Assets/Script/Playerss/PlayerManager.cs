using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerManager : MonoBehaviour
{

    private PhotonView _photonView;
    public static string YouPerson = "PlayerPrefab";

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", YouPerson), Vector3.zero, Quaternion.identity);
    }
}