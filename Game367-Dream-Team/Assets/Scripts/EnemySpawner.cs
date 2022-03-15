using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    GameObject currentPoint;

    public GameObject[] enemies;
    public float timeBtwWaves;
    public bool canSpawn;
    public int enemiesInRoom;
    public int enemiesInWave;
    public bool spawnerDone;
    public int maxEnemiesInWave;
    public GameObject spawnerDoneGameObject;


    private void Start()
    {   // calls funtion to start the spawner
        SpawnEnemy();
    }

    private void Update()
    {
        // tracks the waves amount compared to a cap set
        if (enemiesInWave <= maxEnemiesInWave)
        {   
            // sets a timer between waves
            if (canSpawn == true && timeBtwWaves > 0)
            {
                timeBtwWaves -= Time.deltaTime;

                // if the timer hits 0 it calls the spawner to start again
                if (timeBtwWaves <= 0)
                {
                    canSpawn = false;
                    SpawnEnemy();
                }

            }
        }
        
        

    }

    // spawner function
    void SpawnEnemy()
    {
        // for loop that spawns the enemies are spawn points 
        for (int i = 0; i < enemiesInWave; i++)
        {
            currentPoint = spawnPoints[i];

            GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemiesInRoom++;


            if (enemiesInRoom >= enemiesInWave)
            {
                break;
            }
        }
    }

    // reset function, resets some of the variables back to 0 and adds 1 to the enemies in wave 
    public void SpawnerReset()
    {
        if (enemiesInRoom <= 0)
        {
            enemiesInRoom = 0;
            enemiesInWave++;
        }

        canSpawn = true;
        timeBtwWaves = 5f;

    }
}