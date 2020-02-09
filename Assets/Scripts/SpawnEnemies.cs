using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject enemyPrefab;
    public int enemiesToSpawn = 3;

    void Start()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(enemyPrefab);
        }

    }
    private void SpawnEnemy(GameObject enemyPrefab)
    {

        int spawnIndex = Random.Range(0, spawnPositions.Length);
        Instantiate(enemyPrefab, spawnPositions[spawnIndex]);

    }
}
