using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Novo Item", menuName ="Item/Criar Novo Item")]
public class Items : ScriptableObject
{
    public int id;
    public string nomeItem;
    public int value;
    public Sprite icon;
}
