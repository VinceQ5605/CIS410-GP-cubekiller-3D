using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ItemInfoManager : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCost;
    public TextMeshProUGUI playerGold;
    public List<string> shopList = new List<string>();
    public List<bool> isPurchased = new List<bool>();
    public GameObject player;

    public Item currentItem;
    public bool Ispurchased = false;

    private static ItemInfoManager instance;

    private void Start()
    {
        shopList.Add("Knock back Grenade");
        shopList.Add("Fire Grenade");
        shopList.Add("Gravity Grenade");
        shopList.Add("other Grenade");
        shopList.Add("Increase base's health");
        shopList.Add("Increase player's health");
        shopList.Add("Increase explosion radius");
        shopList.Add("Increase player's speed");

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            itemName.text = "";
            itemDescription.text = "";
            itemCost.text = "0";
            playerGold.text = PlayerInventory.gold.ToString();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetUI(Item item)
    {
        instance.currentItem = item;

        instance.itemName.text = item.name;
        instance.itemDescription.text = item.itemDescription;
        instance.itemCost.text = item.itemCost.ToString();
    }

    public void BuyButton()
    {
        if (currentItem.itemCost > PlayerInventory.gold)
        {
            Debug.Log("You can't afford this");
        }
        else
        {
            PlayerInventory.gold -= currentItem.itemCost;
            PlayerInventory.allItems.Add(currentItem);
            playerGold.text = PlayerInventory.gold.ToString();
        }

        foreach (Item item in PlayerInventory.allItems)
        {
            
        }
    }
}
