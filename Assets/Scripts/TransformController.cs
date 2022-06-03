using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public GameObject baseObj;
    public GameObject player;
    public GameObject fireball; // for type 2 enemies
    public GameObject enemyPrefab;
    public int timer = 20000;
    GameObject newEnemy;

    public List<GameObject> boximonEnemies;
    public List<float> boximonTypeDistributionWeight; // high number means it's more likely for the boximon to be this type

    private float distributionWeightSum = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < boximonTypeDistributionWeight.Count; i++)
        {
            distributionWeightSum += boximonTypeDistributionWeight[i];
        }
        /*//newEnemy = Instantiate(boximonEnemies[0], player.transform.position + 5 * Vector3.forward, Quaternion.identity);
        newEnemy = Instantiate(enemyPrefab, player.transform.position + 5 * Vector3.forward, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetDestination(baseObj);
        newEnemy.SetActive(true);*/
    }


    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
        if (enemyController != null && !enemyController.IsBoximon())
        {
            Transformation(enemyController);
        }
    }

    public void Transformation(EnemyController oldEnemyController)
    {
        // transform this enemy into a random boximon
        float randomizer = Random.Range(0, distributionWeightSum);
        int boximonTypeIndex = 0;
        // determine which type of boximon
        for (int i = 0; i < boximonTypeDistributionWeight.Count; i++)
        {
            if (randomizer < boximonTypeDistributionWeight[i])
            {
                boximonTypeIndex = i;
            }
            else
            {
                randomizer -= boximonTypeDistributionWeight[i];
            }
        }

        GameObject newBoximon = Instantiate(boximonEnemies[boximonTypeIndex], oldEnemyController.gameObject.transform.position, Quaternion.identity);
        newBoximon.SetActive(true);
        BoximonEnemyController boximonController = newBoximon.GetComponent<BoximonEnemyController>();
        EnemyController newEnemyController = newBoximon.GetComponent<EnemyController>();
        boximonController.SetValues(player, baseObj, fireball);
        newEnemyController.SetDestination(baseObj);
        newEnemyController.currentHealth = oldEnemyController.currentHealth;
        newEnemyController.coinPrefab = oldEnemyController.coinPrefab;
        Destroy(oldEnemyController.gameObject);
    }
}
