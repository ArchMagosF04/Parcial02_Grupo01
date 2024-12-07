using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ExtraInventoryFunctions : MonoBehaviour
{
    private InventoryController playerInventory;
    private ItemCreator itemCreator;
    private int inventoryLimit = 11;

    [SerializeField] private Button addNewItemButton;
    [SerializeField] private Button deleteRandomItemButton;

    private void Awake()
    {
        addNewItemButton.onClick.AddListener(AddNewItem);
        deleteRandomItemButton.onClick.AddListener(DeleteRandomItem);
        itemCreator = GetComponent<ItemCreator>();
        playerInventory = GetComponent<InventoryController>();
    }

    private void AddNewItem() 
    {
        if (playerInventory.inventory.Count < inventoryLimit) 
        {
            Item item = itemCreator.MakeItem();
            playerInventory.inventory.Add(item);
        }
        else { Debug.Log("The inventory is full"); }
        
    }

    private void DeleteRandomItem() 
    {
        if (playerInventory.inventory.Count > 0) 
        {
            int randomIndex = Random.Range(0, playerInventory.inventory.Count - 1);
            Item item = playerInventory.inventory[randomIndex];
            if (item != null)
            {
                Destroy(item.gameObject);
            }
            playerInventory.inventory.RemoveAt(randomIndex);

        }else { return; }
    }
}

