using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float maxSpawn = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawn);

        InvokeRepeating("Increase", 0f, 30f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject spawnedEnemy = Instantiate(enemy);
        spawnedEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        Invoke("SpawnEnemy", maxSpawn); 

        NextEnemySpawn();
    }

    void NextEnemySpawn()
    {
        float spawn;
        
        if(maxSpawn > 1f)
        {
            spawn = Random.Range(1f, maxSpawn);
        }
        else
            spawn = 1f;
        Invoke("SpawnEnemy", spawn);
     
    }

    void Increase()
    {
        if(maxSpawn > 1f)
        {
            maxSpawn--;;
        }
        if(maxSpawn == 1f)
        {
            CancelInvoke("Increase");
        }
    }
}
