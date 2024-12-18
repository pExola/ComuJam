using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    // Referência ao diálogo associado ao objeto
    public Dialogue dialogue;

    // Método chamado ao interagir com o objeto
    public override void Interact()
    {
        UIManager.SetDialogue(dialogue); // Configura o diálogo no UIManager
    }
}

