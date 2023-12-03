using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f; 
    private float timeSinceLastSpawn;

    
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), maxY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    float minX = -5f; 
    float maxX = 5f;  
    float maxY = 7f;  
}
