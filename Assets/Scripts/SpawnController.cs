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
    private GameObject destination1;
    private GameObject destination2;


    public void SetValues(GameObject enemyPrefab, GameObject baseObject, List<GameObject> transformationLocations, GameObject coinPrefab)
    {
        this.enemyPrefab = enemyPrefab;
        this.baseObject = baseObject;
        this.transformationLocations = transformationLocations;
        this.coinPrefab = coinPrefab;

        destination1 = transformationLocations[0];
        destination2 = transformationLocations[1];
        Vector3 pos = transform.position + (new Vector3(.1f, 0f, .2f)); // just to make sure not everything is the same distance apart
        Vector3 delta1 = transformationLocations[0].transform.position - pos;
        Vector3 delta2 = transformationLocations[1].transform.position - pos;
        float dot1 = Vector3.Dot(delta1, delta1);
        float dot2 = Vector3.Dot(delta2, delta2);
        if (dot1 > dot2)
        {
            float temp = dot1;
            dot1 = dot2;
            dot2 = temp;
            destination1 = transformationLocations[1];
            destination2 = transformationLocations[0];
        }

        for (int i = 2; i < transformationLocations.Count; i++)
        {
            Vector3 delta = transformationLocations[i].transform.position - pos;
            float dot = Vector3.Dot(delta, delta);
            if (dot < dot1)
            {
                dot2 = dot1;
                destination2 = destination1;
                dot1 = dot;
                destination1 = transformationLocations[i];
            }
            else if (dot < dot2)
            {
                dot2 = dot;
                destination2 = transformationLocations[i];
            }
        }
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
            GameObject destination = destination1;
            if (i % 2 == 0)
            {
                destination = destination2;
            }
            enemyController.SetDestination(destination);
            newEnemy.SetActive(true);
            if (i%5 == 0)
                enemyController.isCarryingCoin = true;
            enemyController.coinPrefab = coinPrefab;
            enemyController.SetBaseObject(baseObject);
            enemyList[i] = newEnemy;
            newEnemy.GetComponent<Rigidbody>().velocity = speed * direction;
            direction = rotation * direction;

            yield return new WaitForSeconds(spawnTimeDelta);
        }
    }
}
