using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] shopItemObjects; // Reference the ShopItem GameObjects
    private ShopItem[] shopItems; // Array to store ShopItem components

    public int bombCost = 100;
    public int potionCost = 100;
    private int playerMoney;

    private void Start()
    {
        InitializeShopItems();
        UpdateShopUI();
    }

    void InitializeShopItems()
    {
        shopItems = new ShopItem[shopItemObjects.Length];

        // Manual declare items :<
        shopItems[0] = new ShopItem("Bomb", bombCost);
        shopItems[1] = new ShopItem("Potion", potionCost);
    }

    private void Update()
    {
        playerMoney = GameManager.instance.money;
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (playerMoney < shopItems[i].price)
            {
                shopItemObjects[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                shopItemObjects[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    void UpdateShopUI()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            // Change "Cost" children gameobject
            shopItemObjects[i].transform
                .Find("Cost")
                .GetComponent<TextMeshProUGUI>()
                .SetText(shopItems[i].price.ToString());
        }
    }

    public void OnClickBuyBomb()
    {
        if (playerMoney >= bombCost)
        {
            GameManager.instance.money -= bombCost;
            
            GameManager.instance.addGrenade();
            Debug.Log("Bought Bomb");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void OnClickBuyPotion()
    {
        if (playerMoney >= potionCost)
        {
            GameManager.instance.money -= potionCost;
            GameManager.instance.addHealth();
            Debug.Log("Bought Potion");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    // Rest of the script remains similar to previous example
}
