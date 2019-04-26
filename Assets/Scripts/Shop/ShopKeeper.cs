using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopPanel;

    //Variable for current Item selected
    public int currentSelectedItem;
    public int currentSelectedItemCost;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player._amountOfDiamonds);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        //0 = flame sword
        //1 = boots of fliht
        //2 = key of castle

        //switch btw item
        //case 0 = flame sword
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(112);
                currentSelectedItem = 0;
                currentSelectedItemCost = 200;
                break;

            case 1:
                UIManager.Instance.UpdateShopSelection(-1);
                currentSelectedItem = 1;
                currentSelectedItemCost = 400;
                break;

            case 2:
                UIManager.Instance.UpdateShopSelection(-110);
                currentSelectedItem = 2;
                currentSelectedItemCost = 100;
                break;
        }
    }

    //BuyItem Method
    //Check if player gems is greater than or equal to itemCost
    //If it is, then awrad item 'subtract cost from player's gem
    //else cancel sale 

    public void BuyItem()
    {
        if (currentSelectedItemCost <= player._amountOfDiamonds)
        {
            //award Item
            //switch if player buy different item
            switch(currentSelectedItem)
            {
                case 0:
                    player.hasFlameSword = true;
                    break;

                case 1:
                    player._jumpforce += 2.0f;
                    break;

                case 2:
                    GameManager.Instance.hasKeyToCasle = true;
                    break;
            }
            player._amountOfDiamonds -= currentSelectedItemCost;
            UIManager.Instance.OpenShop(player._amountOfDiamonds);
            shopPanel.SetActive(false);
        }

        else
        {
            shopPanel.SetActive(false);
        }
    }
}
