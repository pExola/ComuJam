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
    // Item necessário para o texto alternativo
    public Item conditionalItem;

    // Indica se um item será usado durante a interação
    public bool useItem;
    // Evento executado caso o item seja usado
    public UnityEvent onUseItem;

    // Método chamado ao interagir com o objeto
    public override void Interact()
    {
        if (isInteracting) // Evita interações múltiplas simultâneas
            return;

        isInteracting = true; // Marca o objeto como em interação
        UIManager.SetText(this); // Configura o texto na UI
    }
}

