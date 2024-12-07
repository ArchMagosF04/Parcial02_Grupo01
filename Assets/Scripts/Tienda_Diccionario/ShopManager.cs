using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private int currentMoney = 5000;
    [SerializeField] private TextMeshProUGUI goldAmountText;

    [SerializeField] private InventoryController playerInventory;
    [SerializeField] private InventoryController shopInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        UpdateText();
    }

    private void UpdateText()
    {
        goldAmountText.text = "Gold: " + currentMoney;
    }

    public void TradeItem(Item item)
    {
        if(item.gameObject.tag == "PlayerItem")
        {
            SellItem(item);
        } else if(item.gameObject.tag == "ShopItem")
        {
            if(item.GoldValue_ > currentMoney)
            {
                Debug.Log("You can´t afford that");
            }else
            {
                BuyItem(item);
            }
        }
        UpdateText();
    }

    private void SellItem(Item item) 
    {
        playerInventory.inventory.Remove(item);
        shopInventory.inventory.Add(item);
        item.gameObject.tag = "ShopItem";
        currentMoney += item.GoldValue_;
        item.transform.SetParent(shopInventory.transform);
    }

    private void BuyItem(Item item)
    {
        shopInventory.inventory.Remove(item);
        playerInventory.inventory.Add(item);
        item.gameObject.tag = "PlayerItem";
        currentMoney -= item.GoldValue_;
        item.transform.SetParent(playerInventory.transform);
    }
}
