using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextInteraction : Interactable
{
    // Texto exibido ao interagir com o objeto
    public string text;
    // Imagem exibida no retrato
    public Sprite portraitImage;

    // Texto alternativo caso o jogador tenha um item condicional
    public string conditionalText;
    // Item necess�rio para o texto alternativo
    public Item conditionalItem;

    // Indica se um item ser� usado durante a intera��o
    public bool useItem;
    // Evento executado caso o item seja usado
    public UnityEvent onUseItem;

    // M�todo chamado ao interagir com o objeto
    public override void Interact()
    {
        if (isInteracting) // Evita intera��es m�ltiplas simult�neas
            return;

        isInteracting = true; // Marca o objeto como em intera��o
        UIManager.SetText(this); // Configura o texto na UI
    }
}

