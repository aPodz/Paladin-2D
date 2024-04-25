using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;


    public void ButtonPress()
    {
        if (MenuManager.instance.menu.activeInHierarchy)
        {
            MenuManager.instance.itemName.text = itemOnButton.name;
            MenuManager.instance.itemDesc.text = itemOnButton.itemDesc;

            MenuManager.instance.activeItem = itemOnButton;
        }


        if (ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if(ShopManager.instance.buyPanel.activeInHierarchy)
            {
                ShopManager.instance.ActiveBuyItem(itemOnButton);
            }
            else if (ShopManager.instance.sellPanel.activeInHierarchy)
            {
                ShopManager.instance.ActiveSellItem(itemOnButton);
            }
        } 
        
        if (BattleManager.instance.battleItemsMenu.activeInHierarchy)
        {
            BattleManager.instance.SelectedBattleItem(itemOnButton);
        }
        
    }


}
