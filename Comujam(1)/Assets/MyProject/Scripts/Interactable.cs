using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    ground, door, text, dialogue, colectable, none
}

public class Interactable : MonoBehaviour
{
    public ObjectType objectType; 
}
