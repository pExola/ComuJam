using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableItem : Interactable
{
    public Item item;
    public override void Interact()
    {
        Inventory.SetItem(item);
        Debug.Log("Coletou " + item.itemName);
        Destroy(gameObject);
    }
}