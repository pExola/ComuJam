using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    public Dialogue dialogue;
    public override void Interact()
    {
        UIManager.SetDialogue(dialogue);
        //CameraClass.setPos(GetComponent<GameObject>().GetComponent<Transform>().transform.position);
    }
}
