using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject interactionPanel;
    public TMP_Text interactionText;
    public Image portrait;

    bool inDialogue;
    TextInteraction textInteraction;

    public Texture2D[] cursors;

    public Image[] inventoryImages;

    public bool InDialogue { get => inDialogue; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
    }
    public static void SetCursors(ObjectType objectType) 
    {
        if (instance == null)
            return;
        Cursor.SetCursor(instance.cursors[(int)objectType], Vector2.zero, CursorMode.Auto);
    }

    public static void SetInventoryImage(Item item) 
    {
        if (instance == null)
            return;

        for (int i = 0; i < instance.inventoryImages.Length; i++) 
        {
            if (!instance.inventoryImages[i].gameObject.activeInHierarchy) 
            {
                instance.inventoryImages[i].sprite = item.itemImage;
                instance.inventoryImages[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public static void SetText(TextInteraction interactable) 
    {
        if(instance == null) 
            return;

        instance.portrait.sprite = interactable.portraitImage;

        if (interactable.conditionalItem != null)
        {
            Debug.Log("Tem item condicional");
            if (Inventory.HasItem(interactable.conditionalItem))
            {
                Debug.Log("Tem item");
                instance.interactionText.text = interactable.conditionalText;
                if (interactable.useItem) 
                {
                    Inventory.UseItem(interactable.conditionalItem);
                    interactable.onUseItem.Invoke();
                }
            }
            else
            {
                Debug.Log("Jogador n tem nada");
                instance.interactionText.text = interactable.text;
            }
        }
        else
        {
            Debug.Log("Não tem item condicional");
            instance.interactionText.text = interactable.text;
        }
        instance.interactionPanel.SetActive(true);
        instance.textInteraction = interactable;
    }
    public static void DisableInteraction() 
    {
        if (instance == null)
            return;

        instance.interactionPanel.SetActive(false);
        if (instance.textInteraction != null)
            instance.textInteraction.isInteracting = false;
    }

    public static void RemoveInventoryImage (Item item) 
    {
        if (instance == null)
            return;

        for (int i = 0; i < instance.inventoryImages.Length; i++) 
        {
            if (instance.inventoryImages[i].sprite == item.itemImage) 
            {
                instance.inventoryImages[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public static void SetDialogue(Dialogue dialogue) 
    {
        if(instance == null)
            return;

        instance.inDialogue = true;
        SetCursors(ObjectType.none);
        DisableInteraction();
        instance.portrait.sprite = dialogue.portrait;  
    }
}
