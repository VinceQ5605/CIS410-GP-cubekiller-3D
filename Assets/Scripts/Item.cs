using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Asset/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public int itemCost;
}
