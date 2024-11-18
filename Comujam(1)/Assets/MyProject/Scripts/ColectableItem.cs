using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableItem : Interactable
{
    public Item item;
    public bool destruir = true;
    public override void Interact()
    {
        Inventory.SetItem(item);
        Debug.Log("Coletou " + item.itemName);
        if (destruir)
        {
            Destroy(gameObject);
        }
    }
}
