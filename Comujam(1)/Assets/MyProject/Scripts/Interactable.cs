using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    ground, door, text, dialogue, colectable, none, cursorBlue
}

public abstract class Interactable : MonoBehaviour
{
    public bool isInteracting;
    public ObjectType objectType;

    public abstract void Interact();
}
