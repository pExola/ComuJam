using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    // Refer�ncia ao di�logo associado a este bot�o
    public Dialogue dialogue;

    // Define o di�logo atual no UIManager ao clicar no bot�o
    public void SetDialogue()
    {
        UIManager.SetDialogue(dialogue);
    }
}

