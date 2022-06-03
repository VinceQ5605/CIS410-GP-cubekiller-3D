using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject baseObject;
    public GameObject enemySpawner;
    public List<GameObject> spawnLocations;
    public List<GameObject> transformationLocations; // destinations
    public GameObject coinPrefab;

    private float time = 0;
    private List<float> timeUntilNextSpawner;
    private List<int> numberOfEnemies;
    private int spawnerIndex = 0;
    private int previousSpawnLocationIndex = -1;
    private GeneralManager generalManager;

    private void Start()
    {
        timeUntilNextSpawner = new List<float> { 5f, 5f, 5f, 20f, 20f, 30f, 30f, 30f, 30f };
        numberOfEnemies = new List<int> { 40, 40, 40, 50, 50, 30, 30, 100, 200 };

        int totalEnemyCount = 0;
        for (int i = 0; i < numberOfEnemies.Count; i++)
        {
            totalEnemyCount += numberOfEnemies[i];
        }
        generalManager = baseObject.transform.GetChild(0).gameObject.GetComponent<GeneralManager>();
        generalManager.SetTotalEnemyCount(totalEnemyCount);
    }

    void Update()
    {
        if (spawnerIndex < timeUntilNextSpawner.Count)
            time += Time.deltaTime;
        if (time >= timeUntilNextSpawner[spawnerIndex])
        {
            time -= timeUntilNextSpawner[spawnerIndex];
            int randomIndex = 0;
            while (true) 
            {
                randomIndex = Random.Range(0, spawnLocations.Count);
                if (randomIndex != previousSpawnLocationIndex)
                    break;
            }
            randomIndex = spawnerIndex%(spawnLocations.Count);
            SpawnController spawnController = Instantiate(enemySpawner, spawnLocations[randomIndex].transform).gameObject.GetComponent<SpawnController>();
            spawnController.SetValues(enemyPrefab, baseObject, transformationLocations, coinPrefab);
            spawnController.Spawn(numberOfEnemies[spawnerIndex]);
            previousSpawnLocationIndex = randomIndex;
            spawnerIndex += 1;
        }
        
    }
}
