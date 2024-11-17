using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteraction : Interactable
{
    public string text;
    public Sprite portraitImage;

    public string conditionalText;
    public Item conditionalItem;

    public override void Interact()
    {
        if (isInteracting)
            return; 

        isInteracting = true;
        UIManager.SetText(this);
    }
}
