using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    // Painel e texto de intera��o
    public GameObject interactionPanel;
    public TMP_Text interactionText;

    // Retrato e textos de respostas do di�logo
    public Image portrait;
    public TMP_Text[] answersText;

    // Controle de di�logo
    private bool inDialogue;
    private TextInteraction currentInteraction;
    private bool skipDialogue;

    // Cursores personalizados
    public Texture2D[] cursors;

    // Invent�rio UI
    public Image[] inventoryImages;
    public Image[] inventorySelectors;

    // Velocidade do texto gradual
    public float textSpeed = 0.05f;
    private Coroutine typingCoroutine;

    // Vari�vel de inst�ncia para armazenar o di�logo atual
    private Dialogue currentDialogue;

    TextInteraction textInteraction;

    // Verificar estado de di�logo
    public static bool InDialogue()
    {
        return instance != null && instance.inDialogue;
    }

    // Awake: Inicializar a inst�ncia e carregar UI
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        carregaInvUI();
    }

    // Carregar invent�rio na interface
    public void carregaInvUI()
    {
        if (Inventory.instance != null)
        {
            foreach (var item in Inventory.GetItems())
            {
                SetInventoryImage(item);
            }
        }
    }

    // Atualizar invent�rio
    public static void AtualizarInventario()
    {
        RemoveAllInventoryImage();
        foreach (var item in Inventory.GetItems())
        {
            SetInventoryImage(item);
        }
    }

    // Alterar cursor conforme o tipo de objeto
    public static void SetCursors(ObjectType objectType)
    {
        if (instance == null) return;
        Cursor.SetCursor(instance.cursors[(int)objectType], Vector2.zero, CursorMode.Auto);
    }

    // Selecionar item no invent�rio
    public static void selectItem(int id)
    {
        if (instance == null) return;

        for (int i = 0; i < instance.inventorySelectors.Length; i++)
        {
            instance.inventorySelectors[i].gameObject.SetActive(i == id);
        }
    }

    // Definir imagem do item no invent�rio
    public static void SetInventoryImage(Item item)
    {
        if (instance == null) return;

        foreach (var image in instance.inventoryImages)
        {
            if (!image.gameObject.activeInHierarchy)
            {
                image.sprite = item.itemImage;
                image.gameObject.SetActive(true);
                break;
            }
        }
    }

    // Configurar texto de intera��o
    public static void SetText(TextInteraction interactable)
    {
        if (instance == null)
            return;

        instance.portrait.sprite = interactable.portraitImage;

        if (interactable.conditionalItem != null)
        {
            if (Inventory.HasItem(interactable.conditionalItem))
            {
                instance.interactionText.text = interactable.conditionalText;
                if (interactable.useItem)
                {
                    if (interactable.conditionalItem.removeOnDialogue)
                    {
                        Inventory.UseItemInDialogue(interactable.conditionalItem);
                    }
                    else
                    {
                        Inventory.UseItem(interactable.conditionalItem);
                        interactable.onUseItem.Invoke();
                    }
                }
            }
            else
            {
                instance.interactionText.text = interactable.text;
            }
        }
        else
        {
            instance.interactionText.text = interactable.text;
        }
        instance.interactionPanel.SetActive(true);
        instance.textInteraction = interactable;

        if (interactable.conditionalItem != null)
        {
            Debug.Log($"Verificando item condicional: {interactable.conditionalItem.itemName}");

            if (Inventory.HasItem(interactable.conditionalItem))
            {
                Debug.Log("Item condicional encontrado no invent�rio.");
            }
            else
            {
                Debug.Log("Item condicional n�o encontrado no invent�rio.");
            }
        }
        else
        {
            Debug.Log("Nenhum item condicional configurado.");
        }
    }

    // Desativar painel de intera��o
    public static void DisableInteraction()
    {
        if (instance == null) return;

        instance.interactionPanel.SetActive(false);
        if (instance.currentInteraction != null)
        {
            instance.currentInteraction.isInteracting = false;
        }
    }

    // Remover imagem do invent�rio
    public static void RemoveInventoryImage(Item item)
    {
        if (instance == null) return;

        foreach (var image in instance.inventoryImages)
        {
            if (image.sprite == item.itemImage)
            {
                image.gameObject.SetActive(false);
                break;
            }
        }
    }

    // Remover todas as imagens do invent�rio
    public static void RemoveAllInventoryImage()
    {
        if (instance == null) return;

        for (int i = 0; i < instance.inventoryImages.Length; i++)
        {
            instance.inventoryImages[i].gameObject.SetActive(false);
        }
    }

    // Configurar di�logo
    public static void SetDialogue(Dialogue dialogue)
    {
        if (instance == null) return;

        if (dialogue.isEnd)
        {
            FinishDialogue();
            return;
        }

        instance.inDialogue = true;
        SetCursors(ObjectType.text);
        DisableInteraction();

        instance.currentDialogue = dialogue;
        instance.portrait.sprite = dialogue.portrait;

        instance.interactionText.text = dialogue.dialogueText;

        // Colocar o texto gradualmente (nome da fun��o ja fala, mas � bom saber lendo no pt-br n�)
        
        instance.StartCoroutine(ShowDialogueTextGradually(dialogue.dialogueText));

        // Recompensa do di�logo
        if (dialogue.recompensaDialogo != null)
        {
            if (!Inventory.HasItem(dialogue.recompensaDialogo))
            {
                Inventory.SetItem(dialogue.recompensaDialogo);
            }
        }

        // Remo��o de item condicional
        if (dialogue.removeConditionalItem)
        {
            Inventory.RemoveItem(dialogue.conditionalItem);
            AtualizarInventario();
        }

        // Configurar respostas
        foreach (var answerText in instance.answersText)
        {
            answerText.gameObject.SetActive(false);
        }

        instance.interactionPanel.SetActive(true);

        // Verificar se o dialogo � do tipo evento para, assim rodar o evento!!!!!

        if( dialogue is DialogueEvent dialogoComEvento)
        {
            Debug.Log("Come�ando o evento do dialogo que contem evento");
            //instance.StartCoroutine(instance.InvokeEventAfterDelay(dialogoComEvento.delayStartEvento, dialogoComEvento.EventToRunAfterPlay));
            dialogoComEvento.EventToRunAfterPlay.Invoke();
        }

        if (dialogue.conditionalItem != null)
        {
            Debug.Log($"Item condicional necess�rio: {dialogue.conditionalItem.itemName}");
        }
        else
        {
            Debug.Log("Este di�logo n�o exige item condicional.");
        }

    }
    private IEnumerator InvokeEventAfterDelay(float delay, UnityEvent evento)
    {
        yield return new WaitForSeconds(delay);
        evento.Invoke();
    }
    private static IEnumerator ShowDialogueTextGradually(string text)
    {

        instance.interactionText.text = "";  // Limpar o texto atual
        for (int i = 0; i < text.Length; i++)
        {
            instance.interactionText.text += text[i]; // Adiciona uma letra por vez
            yield return new WaitForSeconds(0.005f); // Tempo para atraso entre cada letra
        }
        // Ap�s o texto do di�logo ser totalmente exibido, come�ar a exibir as respostas
        yield return new WaitForSeconds(0.5f); // Aguardar um tempo extra, se necess�rio, ap�s o di�logo completo
        instance.StartCoroutine(ShowAnswersGradually(instance.currentDialogue));
    }

    private static IEnumerator ShowAnswersGradually(Dialogue dialogue)
    {
        if (instance.currentDialogue == null) yield break; // Se n�o houver di�logo, sair

        int index = 0;
        foreach (var answer in instance.currentDialogue.answers)
        {
            if (index < instance.answersText.Length &&
                (answer.conditionalItem == null || Inventory.HasItem(answer.conditionalItem)))
            {
                instance.answersText[index].text = answer.playerAnswer;
                instance.answersText[index].GetComponent<AnswerButton>().dialogue = answer;

                // Exibir a resposta com um pequeno atraso
                yield return new WaitForSeconds(0.5f); // Tempo entre a exibi��o das respostas

                instance.answersText[index].gameObject.SetActive(true);
                index++;
            }
        }

    }

    // Finalizar di�logo
    public static void FinishDialogue()
    {
        if (instance == null) return;

        foreach (var text in instance.answersText)
        {
            text.gameObject.SetActive(false);
        }

        instance.inDialogue = false;
        DisableInteraction();
    }
}