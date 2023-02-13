using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
    private int enemiesInScene;
    private int enemiesPerWave = 1;
    public GameObject[] powerupPrefabs;

    private void Start()
    {
        SpawnEnemyWave(enemiesPerWave); //instanciar un nuevo enemigo
    }
    
    private Vector3 RandomSpawnPosition()
    {
        float randX = Random.Range(-spawnRange, spawnRange);
        float randZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randX, 0, randZ);
    }
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomIndex], RandomSpawnPosition(), Quaternion.identity);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
        }
    }
    private void Update()
    {
        enemiesInScene = FindObjectOfType<Enemy>().Length;
        if(enemiesInScene<=0)
        {
            enemiesPerWave++;
            SpawnEnemyWave(enemiesPerWave);
        }
    }
}
