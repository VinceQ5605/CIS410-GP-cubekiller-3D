using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{ 
    public TextMeshProUGUI enemy_Counter;
    private int enemyLeft;
    // Start is called before the first frame update
    void Start()
    {
        enemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SetEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetEnemyCount()
    {
        enemy_Counter.text = enemyLeft.ToString();
    }
}
