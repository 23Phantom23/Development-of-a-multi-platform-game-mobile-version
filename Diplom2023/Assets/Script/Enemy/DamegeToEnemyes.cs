using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DamegeToEnemyes : MonoBehaviour
{
    public int hp;
    public Slider healthBar;
    public GameObject Mob;
    public Collider sword;
    void Start()
    {
        hp = 100;
    }

    private void Update()
    {
        healthBar.value = hp;
        if (hp <= 0)
        {
            PhotonNetwork.Destroy(Mob);
            SpawnEnemy.nowTheEnemies--;
        } 
       if (ChaseBehaviour.numer == 2)
        {


            sword.enabled = true;

        }
        if (ChaseBehaviour.numer == 3)
        {


            sword.enabled = false;

        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
       
        // ��������� ������������
        Debug.Log("���������� ������������!");

        // ������ �������������� ���������:
        // ���� ������, � ������� �����������, ����� ��� "Player", ��������� �����-�� ��������
        if (other.CompareTag("Weapon"))
        {
            hp -= 10;
            // ��������, ����������� ��� ������������ � �������
            Debug.Log("������������ � �������!");
        }
    }
}
