using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    //[SerializeField] private Dictionary<string, ItemType> itemType = new Dictionary<string, ItemType>();
    [SerializeField] private List<string> itemsNames = new List<string>();

    [SerializeField] private bool shouldStartWithRandoms;
    [SerializeField] private int startingItemAmount = 6;
    private ItemCreator itemCreator;

    [SerializeField] private Button nameSelectionSortButton;
    [SerializeField] private Button weighSelectionSortButton;
    [SerializeField] private Button typeSelectionSortButton;
    [SerializeField] private Button valueSelectionSortButton;

    [SerializeField] private bool isShop = false;


    private void Awake()
    {
        itemCreator = GetComponent<ItemCreator>();
        nameSelectionSortButton.onClick.AddListener(NameBubbleSort);
        weighSelectionSortButton.onClick.AddListener(WeightSelectionSort);
        typeSelectionSortButton.onClick.AddListener(TypeSelectionSort);
        valueSelectionSortButton.onClick.AddListener(GoldSelectionSort);
    }

    private void Start()
    {
        if (shouldStartWithRandoms)
        {
            for (int i = 0; i < startingItemAmount; i++)
            {
                Item item = itemCreator.MakeItem();
                inventory.Add(item);
                //item.ButtonPref.onClick.AddListener(NameBubbleSort);

                if (isShop)
                {
                    item.gameObject.tag = "ShopItem";
                }
                else
                {
                    item.gameObject.tag = "PlayerItem";
                }
            }
        }
    }

    private void GoldSelectionSort()
    {
        int smallest = 0;

        for (int i = 0; i < inventory.Count; i++)
        {
            smallest = i;

            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].GoldValue_ < inventory[smallest].GoldValue_)
                {
                    smallest = j;
                }
                Swap(i, smallest);
            }
        }
    }

    private void NameBubbleSort()
    {
        int listSize = inventory.Count;
        for (int i = 0; i < listSize - 1; i++)
        {
            for (int j = 0; j < listSize - i - 1; j++)
            {
                // Comparar cadenas alfabéticamente
                if (string.Compare(inventory[j].ItemName_, inventory[j + 1].ItemName_, System.StringComparison.Ordinal) > 0)
                {
                    // Intercambiar si están en el orden incorrecto, el primer index mandado osea (j+1) seria alfabeticamente primero al segundo. 
                    Swap(j+1,j);
                }
            }
        }
    }

    private void WeightSelectionSort()
    {
        int smallest = 0;

        for (int i = 0; i < inventory.Count; i++)
        {
            smallest = i;

            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].Weight < inventory[smallest].Weight)
                {
                    smallest = j;
                }
                Swap(i, smallest);
            }
        }
    }

    private void TypeSelectionSort()
    {
        int smallest = 0;

        for (int i = 0; i < inventory.Count; i++)
        {
            smallest = i;

            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].Rarity < inventory[smallest].Rarity)
                {
                    smallest = j;
                }
                Swap(i, smallest);
            }
        }
    }

    private void Swap(int firstIndex, int secondIndex)
    {
        string tempName = inventory[firstIndex].ItemName_;
        int tempValue = inventory[firstIndex].GoldValue_;
        float tempWeight = inventory[firstIndex].Weight;
        ItemType tempType = inventory[firstIndex].Rarity;

        inventory[firstIndex].SetItemVariables(inventory[secondIndex].ItemName_, inventory[secondIndex].GoldValue_, inventory[secondIndex].Weight, inventory[secondIndex].Rarity);
        inventory[secondIndex].SetItemVariables(tempName, tempValue, tempWeight, tempType);
    }
}
