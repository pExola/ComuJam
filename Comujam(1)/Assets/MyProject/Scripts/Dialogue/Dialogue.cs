using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    // Lista de respostas possíveis que o jogador pode escolher
    public Dialogue[] answers;
    // Texto do diálogo atual
    public string dialogueText;
    // Texto da resposta do jogador para este diálogo
    public string playerAnswer;
    // Sprite exibido junto ao diálogo (retrato do personagem)
    public Sprite portrait;
    // Indica se este é o último diálogo
    public bool isEnd;
    // Item necessário para desbloquear este diálogo
    public Item conditionalItem;
    // Recompensa recebida ao concluir o diálogo
    public Item recompensaDialogo;
    // Remove o item condicional do inventário após o diálogo
    public bool removeConditionalItem = false;
}

