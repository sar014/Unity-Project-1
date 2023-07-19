using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject powerUpPrefab;
    private Vector3 spawnPos = new Vector3(25,0,0);
    private Vector3 spawnPowerUPpos = new Vector3(50,0,0);
    private PlayerController playerControllerScript;

    private float startDelay = 2;
    private float repeateRate = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle",startDelay,repeateRate);
        InvokeRepeating("SpawnPowerUp",startDelay,10);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        int obsIndex = Random.Range(0,obstaclePrefab.Length);
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[obsIndex],spawnPos,obstaclePrefab[obsIndex].transform.rotation);
        }
    }

    void SpawnPowerUp()
    {
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(powerUpPrefab,spawnPowerUPpos,powerUpPrefab.transform.rotation);
        }

    }
}
