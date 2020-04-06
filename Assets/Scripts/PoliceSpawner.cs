using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject policePrefab;

    public Transform[] spawnPoints;

    public float spawnTimer = 5f;

    public PoliceStarSystem player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PoliceStarSystem>();
        StartCoroutine(SpawnPolice());
        
    }

    // Update is called once per frame
    public IEnumerator SpawnPolice()
    { 
        int randIndex = Random.Range(0, spawnPoints.Length);
        yield return new WaitForSeconds(spawnTimer);

        if(player.GetCurrentLevelCheck() >= player.level1) {
            Instantiate(policePrefab, spawnPoints[randIndex].position, Quaternion.identity);

            StartCoroutine(SpawnPolice());
        }
    }
}
