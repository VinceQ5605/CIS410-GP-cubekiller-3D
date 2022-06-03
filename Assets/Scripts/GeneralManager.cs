using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class GeneralManager : MonoBehaviour
{
    public TextMeshProUGUI base_Health;
    public TextMeshProUGUI player_Health;
    public GameObject winTextObject;
    public GameObject lostTextObject;
    public TextMeshProUGUI coinCounter;
    public TextMeshProUGUI enemy_Counter;

    private int count;
    private int baseHealth;
    private int enemyLeft;
    private int coin;
    private int playerHealth;
    private int totalEnemyCount;
    private int remainingEnemyCount;

    void Start()
    {
        baseHealth = 100;
        playerHealth = 25;
        SetBaseHealth();
        SetPlayerHealth();
        winTextObject.SetActive(false);
        lostTextObject.SetActive(false);
        coin = 0;
        SetCoinCounter();

        //enemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SetEnemyCount();
    }

    void Update()
    {
        
    }

    void SetBaseHealth()
    {
        base_Health.text = "Base Health: " + baseHealth.ToString();
        if (baseHealth < 1)
        {
            lostTextObject.SetActive(true);
        }
        //if (enemyLeft == 0)
        //{
        //    winTextObject.SetActive(true);
        //}
    }

    void SetCoinCounter()
    {
        coinCounter.text = coin.ToString();
    }

    void SetPlayerHealth()
    {
        player_Health.text = "HP: " + playerHealth.ToString();
    }

    public void DamageBase()
    {
        if (baseHealth > 0)
        {
            baseHealth--;

            SetBaseHealth();
        }
        else
        {
            Application.Quit();
        }
        
    }

    public void DamagePlayer()
    {
        if (playerHealth > 0)
        {
            playerHealth--;

            SetPlayerHealth();
        }
        else
        {
            Application.Quit();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Coin"))
        {
            coin++;
            SetCoinCounter();
            other.gameObject.SetActive(false);
        }


    }

    public void SetTotalEnemyCount(int totalEnemyCount)
    {
        this.totalEnemyCount = totalEnemyCount;
        remainingEnemyCount = totalEnemyCount;
        SetEnemyCount();
}

    public void EnemyDeath()
    {
        this.remainingEnemyCount--;
        SetEnemyCount();
    }

    void SetEnemyCount()
    {
        // enemy_Counter.text = "Enemies: " + enemyLeft.ToString();
        enemy_Counter.text = "Enemies: " + remainingEnemyCount.ToString();
    }
}
