using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;


    public void ButtonPress()
    {
        MenuManager.instance.itemName.text = itemOnButton.name; 
        MenuManager.instance.itemDesc.text = itemOnButton.itemDesc;

        MenuManager.instance.activeItem = itemOnButton;
    }


}
