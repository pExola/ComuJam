using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public Dialogue dialogue;

    public void SetDialogue() 
    {
        UIManager.SetDialogue(dialogue);
    }
}
