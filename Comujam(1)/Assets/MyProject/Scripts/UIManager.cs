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

    TextInteraction textInteraction;

    public Texture2D[] cursors;
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

    public static void SetText(TextInteraction interactable) 
    {
        if(instance == null) 
            return;

        instance.portrait.sprite = interactable.portraitImage;

        if (interactable.conditionalItem != null)
        {
            Debug.Log("Tem item");
            instance.interactionText.text = interactable.conditionalText;
        }
        else 
        {
            Debug.Log("Jogador n tem nada");
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
}
