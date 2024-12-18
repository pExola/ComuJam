using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    // Lista de respostas poss�veis que o jogador pode escolher
    public Dialogue[] answers;
    // Texto do di�logo atual
    public string dialogueText;
    // Texto da resposta do jogador para este di�logo
    public string playerAnswer;
    // Sprite exibido junto ao di�logo (retrato do personagem)
    public Sprite portrait;
    // Indica se este � o �ltimo di�logo
    public bool isEnd;
    // Item necess�rio para desbloquear este di�logo
    public Item conditionalItem;
    // Recompensa recebida ao concluir o di�logo
    public Item recompensaDialogo;
    // Remove o item condicional do invent�rio ap�s o di�logo
    public bool removeConditionalItem = false;
}

