using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class BaseHealth : MonoBehaviour
{
    public TextMeshProUGUI base_Health;
    public GameObject winTextObject;
    public GameObject lostTextObject;


    private int count;
    private int baseHealth;
    private int enemyLeft;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = 10;
        SetBaseHealth();
        winTextObject.SetActive(false);
        lostTextObject.SetActive(false);

        enemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
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
        if (enemyLeft == 0)
        {
            winTextObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
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
            other.gameObject.SetActive(false);
        }
        
    }
}
