using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform[] SpawnPoints;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        PhotonNetwork.Instantiate(playerPrefab.name,SpawnPoints[Random.Range(0,SpawnPoints.Length)].position + new Vector3(0,1,0), Quaternion.identity);
        Instantiate(enemyPrefab, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
