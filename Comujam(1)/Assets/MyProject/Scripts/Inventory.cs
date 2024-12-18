using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items= new List<Item>();

    public static Inventory instance;

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
        {
            Debug.LogError("Tentativa de adicionar um item nulo ao inventário!");
            return;
        }
           
        instance.items.Add(item);
        UIManager.SetInventoryImage(item);

    }

    public static bool HasItem(Item item) 
    {
        if (instance == null)
        {
            Debug.LogError("Tentativa de verificar um item nulo no inventário!");
            return false; 
        }
        return instance.items.Contains(item);
    }

    public static void UseItem(Item item) 
    {
        if (instance == null)
            return;
        item.usarItem();
        if (item.uniqueUse)
        {
            instance.items.Remove(item);
        }
        UIManager.AtualizarInventario();
    }

    public static void UseItemInDialogue(Item item)
    {
        if (instance == null)
            return;
        item.usarItem();
        instance.items.Remove(item);
        UIManager.AtualizarInventario();
    }

    public static void RemoveItem(Item item)
    {
        if(instance == null)
        {
            return;
        }
        instance.items.Remove(item);
        UIManager.AtualizarInventario();

    }

    public static Item GetItem(int id) {
        if (instance.items.Count > id)
        {
            return instance.items[id];
        }
        else
        {
            return null;
        }

    }
    public static List<Item> GetItems()
    {
        return instance.items;
    }
}
