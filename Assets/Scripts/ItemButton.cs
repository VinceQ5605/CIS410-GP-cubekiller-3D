using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Item item;

    public void SetUI()
    {
        buttonText.text = item.itemName;
    }

    public void ButtonPressed()
    {
        ItemInfoManager.SetUI(item);
    }
}
