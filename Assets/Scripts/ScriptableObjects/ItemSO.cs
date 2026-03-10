using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
public class ItemSO : ScriptableObject {
    

    public GameObject prefab;
    public Item.ItemType itemType;
    public Sprite itemSprite;
    public int averagePrice;
    public int currentPrice;


}
