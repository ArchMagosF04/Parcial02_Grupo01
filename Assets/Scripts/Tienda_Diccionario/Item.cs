using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Consumable,
    Attachment,
    Armour,
}

public class Item : MonoBehaviour
{
    private string itemName = "";
    public string ItemName_ => itemName;

    private float weight = 1.5f;
    public float Weight => weight;

    private int goldValue = 0;
    public int GoldValue_ => goldValue;

    private ItemType rarity = ItemType.Weapon;
    public ItemType Rarity => rarity;

    [SerializeField] private TextMeshProUGUI textWeight;
    [SerializeField] private TextMeshProUGUI textRarity;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textGoldValue;
    private bool belongsToTheShopScene = true;

    private Button buttonPref;
    public Button ButtonPref => buttonPref;

    public void SetItemVariables(string itemName, int goldValue, float weight, ItemType rarity)
    {
        this.itemName = itemName;
        this.goldValue = goldValue;
        this.weight = weight;
        this.rarity = rarity;

        textName.text = itemName;
        textGoldValue.text = goldValue.ToString("c");
        textWeight.text = weight.ToString("F1") + "g";
        textRarity.text = rarity.ToString();
    }

    public void SetItemVariables(string itemName, int goldValue, float weight, ItemType rarity, bool belongsToTheShopScene)
    {
        this.itemName = itemName;
        this.goldValue = goldValue;
        this.weight = weight;
        this.rarity = rarity;
        this.belongsToTheShopScene = belongsToTheShopScene;

        textName.text = itemName;
        textGoldValue.text = goldValue.ToString("c");
        textWeight.text = weight.ToString("F1") + "g";
        textRarity.text = rarity.ToString();
        SetButtons();
    }

    private void SetButtons()
    {
        if (belongsToTheShopScene)
        {
            buttonPref = GetComponentInChildren<Button>();
            buttonPref.onClick.AddListener(ButtonAction);
        }      
    }

    public void ButtonAction()
    {
        ShopManager.Instance.TradeItem(this);
    }
    

}
