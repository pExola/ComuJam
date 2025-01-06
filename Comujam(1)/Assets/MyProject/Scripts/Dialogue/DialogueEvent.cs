using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewDialogueEvent", menuName = "DialogueEvent")]
public class DialogueEvent : Dialogue
{
    public UnityEvent EventToRunAfterPlay;
    public float delayStartEvento;
}
