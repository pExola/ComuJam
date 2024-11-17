using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Update()
    {
        
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
    public static bool HasItemOnList()
    {
        if (instance == null)
            return false;

        return instance.items.Count>0;
    }

    public static void UseItem(Item item) 
    {
        if (instance == null)
            return;

        instance.items.Remove(item);
        UIManager.RemoveInventoryImage(item);   
    }
    public static Item GetItem(int id)
    {
        if (Inventory.HasItemOnList())
        {
            return instance.items[id];

        }
        else
        {
            return null;
        }
    }
}
