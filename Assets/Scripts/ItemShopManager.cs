using UnityEngine;

public class ItemShopManager : MonoBehaviour
{
    public Item[] items;
    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    private void Start()
    {
        foreach(Item item in items)
        {
            GameObject go = Instantiate(buttonPrefab);
            go.transform.SetParent(buttonContainer.transform);
            go.GetComponent<ItemButton>().item = item;
            go.GetComponent<ItemButton>().SetUI();
        }
    }
}
