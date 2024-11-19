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
    public TMP_Text[] answersText;
    bool inDialogue;
    TextInteraction textInteraction;

    public Texture2D[] cursors;

    public Image[] inventoryImages;

    public static bool InDialogue() 
    {
        if (instance == null)
            return false;

        return instance.inDialogue;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        carregaInvUI();
    }
    public void carregaInvUI() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        foreach (var item in Inventory.GetItems())
        {
            SetInventoryImage(item);
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
    public static void RemoveAllInventoryImage()
    {
        if (instance == null)
            return;

        for (int i = 0; i < instance.inventoryImages.Length; i++)
        {
            instance.inventoryImages[i].gameObject.SetActive(false);    
        }
    }

    public static void SetDialogue(Dialogue dialogue)
    {
        if (instance == null)
            return;

        if (dialogue.isEnd)
        {
            FinishDialogue();
            return;
        }

        instance.inDialogue = true;
        SetCursors(ObjectType.text);
        DisableInteraction();
        instance.portrait.sprite = dialogue.portrait;
        instance.interactionText.text = dialogue.dialogueText;
        if (dialogue.recompensaDialogo != null)
        {
            if (!Inventory.HasItem(dialogue.recompensaDialogo))
            {
                Inventory.SetItem(dialogue.recompensaDialogo); 
            }
        }
        if (dialogue.removeConditionalItem)
        {
            Inventory.RemoveItem(dialogue.conditionalItem);
            RemoveAllInventoryImage();
            instance.carregaInvUI();
        }
        var CaixasUI = instance.answersText;
        var RespostasDialogos = dialogue.answers;
        foreach(var caixa in CaixasUI)
        {
            caixa.gameObject.SetActive(false);
        }
        for (int indiceUI = 0,indiceRespostaDialogo=0; indiceRespostaDialogo < RespostasDialogos.Length; indiceRespostaDialogo++)
        {
            if (indiceUI < RespostasDialogos.Length)
            {
                if (Inventory.HasItem(RespostasDialogos[indiceRespostaDialogo].conditionalItem) || RespostasDialogos[indiceRespostaDialogo].conditionalItem == null)
                {
                    CaixasUI[indiceUI].text = RespostasDialogos[indiceRespostaDialogo].playerAnswer;
                    CaixasUI[indiceUI].GetComponent<AnswerButton>().dialogue = RespostasDialogos[indiceRespostaDialogo];
                    CaixasUI[indiceUI].gameObject.SetActive(true);
                    indiceUI += 1;
                }
            }
        }
        instance.interactionPanel.SetActive(true);
    }

    public static void FinishDialogue() 
    {
        if (instance == null)
            return;

        for (int i = 0; i < instance.answersText.Length; i++) 
        {
            instance.answersText[i].gameObject.SetActive(false);
        }

        instance.inDialogue = false;
        DisableInteraction();
    }
}
