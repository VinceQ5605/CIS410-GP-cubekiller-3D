using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float speed;
    public float rotationDelta;
    public float spawnTimeDelta;

    private GameObject enemyPrefab;
    private GameObject baseObject;
    private List<GameObject> transformationLocations;
    private GameObject coinPrefab;


    public void SetValues(GameObject enemyPrefab, GameObject baseObject, List<GameObject> transformationLocations, GameObject coinPrefab)
    {
        this.enemyPrefab = enemyPrefab;
        this.baseObject = baseObject;
        this.transformationLocations = transformationLocations;
        this.coinPrefab = coinPrefab;
    }

    public void Spawn(int numberOfEnemies)
    {
        StartCoroutine(Spawner360(transform.position, numberOfEnemies)); // set this EnemySpawner in the right location before calling Spawn
    }

    private IEnumerator Spawner360(Vector3 position, int numberOfEnemies)
    {
        GameObject[] enemyList = new GameObject[numberOfEnemies];

        Vector3 direction = transform.forward;
        Quaternion rotation = Quaternion.AngleAxis(rotationDelta, Vector3.up);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.SetDestination(transformationLocations[0]);
            newEnemy.SetActive(true);
            if (i%5 == 0)
                enemyController.isCarryingCoin = true;
            enemyController.coinPrefab = coinPrefab;
            enemyList[i] = newEnemy;
            newEnemy.GetComponent<Rigidbody>().velocity = speed * direction;
            direction = rotation * direction;

            yield return new WaitForSeconds(spawnTimeDelta);
        }
    }
}
