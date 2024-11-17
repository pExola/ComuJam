using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public static void SetItem(Item item) 
    {
        if (instance == null)
            return;

        instance.items.Add(item);
        UIManager.SetInventoryImage(item);

    }

    public static bool HasItem(Item item) 
    {
        if (instance == null)
            return false;

        return instance.items.Contains(item);
    }

    public static void UseItem(Item item) 
    {
        if (instance == null)
            return;

        instance.items.Remove(item);
        UIManager.RemoveInventoryImage(item);   
    }
}
