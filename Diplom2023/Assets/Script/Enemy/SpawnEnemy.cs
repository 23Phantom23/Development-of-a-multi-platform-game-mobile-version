using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnEnemy;
    [SerializeField]
    private Transform[] spawnPoint;

    public float startSpawnerInterval;
    private float spawnerInterval;

    public int numberOfEnemyes;
    public static int nowTheEnemies;

    private int randEnemy;
    private int randPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnerInterval = startSpawnerInterval;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerInterval <= 0 && nowTheEnemies < numberOfEnemyes)
        {
            randEnemy = UnityEngine.Random.Range(0, spawnEnemy.Length);
            randPoint = UnityEngine.Random.Range(0, spawnPoint.Length);

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TT_RTS_Demo_Character"), spawnPoint[randPoint].transform.position, Quaternion.identity);

            spawnerInterval = startSpawnerInterval;

            nowTheEnemies++;
        }
        else
        {
            spawnerInterval -= Time.deltaTime;
        }
    }
}
