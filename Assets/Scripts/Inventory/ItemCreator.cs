using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    private Dictionary<string, ItemType> itemType = new Dictionary<string, ItemType>();
    private List<string> itemsNames = new List<string>();
    [SerializeField] private Item itemSlotPrefab;
    [SerializeField] private bool belongsToTheShopScene = true;

    private void Awake()
    {
        ManualItemName();
    }

    public Item MakeItem() 
    {
        Item item = Instantiate(itemSlotPrefab, transform);
        int randomGold = Random.Range(1, 1001);
        float randomWeight = Random.Range(0.5f, 36.0f);
        string randomName = ItemNameCollection();
        item.SetItemVariables(randomName, randomGold, randomWeight, itemType[randomName], belongsToTheShopScene);
        return item;
    }
    private string ItemNameCollection()
    {
        int randomKey = Random.Range(0, itemsNames.Count - 1);
        string randomName = itemsNames[randomKey];
        return randomName;
    }
    private void ManualItemName()
    {
        itemType.Add("Axe", ItemType.Weapon);
        itemType.Add("Apple Pie", ItemType.Consumable);
        itemType.Add("Breastplate", ItemType.Armour);
        itemType.Add("Boots", ItemType.Armour);
        itemType.Add("Cape", ItemType.Attachment);
        itemType.Add("Gauntlets", ItemType.Armour);
        itemType.Add("Crown", ItemType.Attachment);
        itemType.Add("Claymore", ItemType.Weapon);
        itemType.Add("Hammer", ItemType.Weapon);
        itemType.Add("Helmet", ItemType.Armour);
        itemType.Add("Health potion", ItemType.Consumable);
        itemType.Add("Iron Ingot", ItemType.Attachment);
        itemType.Add("Night vision\n potion", ItemType.Consumable);
        itemType.Add("Resurrection\n potion", ItemType.Consumable);
        itemType.Add("Ring", ItemType.Attachment);
        itemType.Add("Red Hood", ItemType.Attachment);
        itemType.Add("Sword", ItemType.Weapon);
        itemType.Add("Shield", ItemType.Armour);
        itemType.Add("Speelbook:\nNecromancy", ItemType.Weapon);
        itemType.Add("Speelbook:\nElementalism", ItemType.Weapon);
        itemType.Add("Speelbook:\nRadiant", ItemType.Weapon);
        itemType.Add("Speelbook:\nIllusion", ItemType.Weapon);
        itemType.Add("Speelbook:\nPsionic", ItemType.Weapon);
        itemType.Add("Talisman", ItemType.Attachment);
        //agrega las keys del diccionario a una lista que guarda el nombre de los items
        foreach (var item in itemType)
        {
            itemsNames.Add(item.Key);
        }
    }
}
