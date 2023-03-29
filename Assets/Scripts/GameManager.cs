using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius;
    public float minSpawnDistance;
    public float spawnInterval;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector2 randomPosition = Vector2.zero;
        bool foundValidPosition = false;
        while (!foundValidPosition)
        {
            randomPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            if (Vector2.Distance(randomPosition, player.position) >= minSpawnDistance)
            {
                foundValidPosition = true;
            }
        }

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}

