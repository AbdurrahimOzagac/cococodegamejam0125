using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool isWaveFinished = false;
    private bool isPoisonBottleReadyToSpawn = false;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject poisonBottle;

    private float minX, maxX, minY, maxY, minZ, maxZ;

    void Start()
    {
        
    }

    void Update()
    {
        SpawnEnemy();
        SpawnPoisonBottle();
    }

    private void TakeBoundaries()
    {
        // initialize boundaries minX

    }

    private Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, y, z);
    }

    private void SpawnEnemy()
    {
        while (!isWaveFinished)
        {
            InvokeRepeating("InstantiateEnemy", 1f, 7f);
        }
    }

    

    private void InstantiateEnemy()
    {
        Instantiate(enemy, GenerateRandomPosition(), enemy.gameObject.transform.rotation);
    }

    public void SetIsWaveFinished(bool isWaveFinished)
    {
        this.isWaveFinished = isWaveFinished;
    }

    public void SetIsPoisonBottleReadyToSpawn(bool isPoisonBottleReadyToSpawn)
    {
        this.isPoisonBottleReadyToSpawn = isPoisonBottleReadyToSpawn;
    }

    private void SpawnPoisonBottle()
    {
        if (isPoisonBottleReadyToSpawn)
        {
            Instantiate(poisonBottle, GenerateRandomPosition(), poisonBottle.gameObject.transform.rotation);
        }
    }
}
