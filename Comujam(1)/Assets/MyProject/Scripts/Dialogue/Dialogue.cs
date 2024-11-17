using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public Dialogue[] answers;
    public string dialogueText;
    public string playerAnswer;
    public Sprite portrait;
    public bool isEnd;
    public Item conditionalItem;
}
