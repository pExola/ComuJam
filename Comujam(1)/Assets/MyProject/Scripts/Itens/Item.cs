using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string description;
    public bool uniqueUse;
    public bool removeOnDialogue;

    public void usarItem()
    {
        Debug.Log($"{itemName} usado!");
    }

   
}
