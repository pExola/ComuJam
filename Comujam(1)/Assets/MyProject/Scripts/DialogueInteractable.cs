using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    // Refer�ncia ao di�logo associado ao objeto
    public Dialogue dialogue;

    // M�todo chamado ao interagir com o objeto
    public override void Interact()
    {
        UIManager.SetDialogue(dialogue); // Configura o di�logo no UIManager
    }
}

