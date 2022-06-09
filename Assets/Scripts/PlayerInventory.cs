using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static int gold = 1000;
    public static List<Item> allItems = new List<Item>();

    public static void PrintInventory()
    {
        foreach(Item item in allItems)
        {
            Debug.Log(item.itemName);
        }
    }
}
