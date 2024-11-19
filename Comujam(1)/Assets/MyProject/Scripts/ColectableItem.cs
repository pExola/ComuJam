using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableItem : Interactable
{
    public Item item;
    public bool destruir = true;
    public bool pego = false;
    public override void Interact()
    {
        if(!pego&& !Inventory.HasItem(item))
        {
            Inventory.SetItem(item);
            Debug.Log("Coletou " + item.itemName);
            this.pego = true;
        }
        if (destruir)
        {
            Destroy(gameObject);
        }
    }
}
