using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    // Referência ao diálogo associado a este botão
    public Dialogue dialogue;

    // Define o diálogo atual no UIManager ao clicar no botão
    public void SetDialogue()
    {
        UIManager.SetDialogue(dialogue);
    }
}

