using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] moduleDropList;
    public Transform player;
    public int score = 0;
    public float spawnRadius;
    public float minSpawnDistance;
    public float spawnInterval;

    [SerializeField] TMP_Text scoreText;

    private readonly KeyCode[] _keyCodes = { KeyCode.A, KeyCode.D,KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, 
        KeyCode.J,KeyCode.K, KeyCode.L, KeyCode.O, KeyCode.P,KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, 
        KeyCode.W,KeyCode.Y};

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
        
        // random module and random key
        GameObject randModuleDrop = moduleDropList[Random.Range(0, moduleDropList.Length - 1)];
        Module md = randModuleDrop.GetComponent<Module>();
        if (md.type != ModuleType.Weapon)
        {
            md.control = _keyCodes[Random.Range(0, _keyCodes.Length-1)];
        }
        enemyPrefab.GetComponent<EnemyController>().modulePrefab = randModuleDrop;
        
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }


    public void scorePlusOne()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }
}

