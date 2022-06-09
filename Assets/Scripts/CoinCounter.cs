using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinCounter;
    private int count;
    private int coin;

    // Start is called before the first frame update
    void Start()
    {
        coin = 0;
        SetCoinCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCoinCounter()
    {
        coinCounter.text = coin.ToString();
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
}
