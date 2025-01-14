using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class InteractableItem : Interactable
{
    public GameObject objeto;
    public Animator animator;
    public Item conditionalItem;
    public bool acionarRato;
    public Teleport doorToEnable;
    public bool uniqueInteract;
    [SerializeField] private bool interacted;
    public UnityEvent iniciarEvento;
    public override void Interact()
    {
        Debug.Log("Interagindo");
        
        if(animator != null)
        {
            
            if (conditionalItem)
            {
                if (Inventory.HasItem(conditionalItem) && !interacted)
                {
                    bool isPlaying = !animator.GetBool("isPlaying");
                    animator.SetBool("isPlaying", isPlaying);
                    StartCoroutine(InvokeEventAfterDelay(3f));
                    interacted = true;
                    FindObjectOfType<AudioManager>().Play("PrateleiraCongelando");

                }
                if (doorToEnable)
                {
                    doorToEnable.enabledTeleport = true;
                }
            }
            else
            {
                bool isPlaying = !animator.GetBool("isPlaying");
                animator.SetBool("isPlaying", isPlaying);
                if (acionarRato)
                {
                    RatoScript.emAlerta = isPlaying;

                    if (isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("RadinhoTocando");
                        StartCoroutine(DesativarSomAposTempo(5f));

                    }

                }

            }
        }
    }

    private IEnumerator DesativarSomAposTempo(float duracao)
    {
        // Espera pela duração especificada
        yield return new WaitForSeconds(duracao);

        // Desativa o som e o estado de alerta
        if (animator != null)
        {
            animator.SetBool("isPlaying", false);
        }
        RatoScript.emAlerta = false;

        // Chama o método para o rato retornar à toca
        //RatoScript rato = FindObjectOfType<RatoScript>();
        //if (rato != null)
        //{
        //    rato.AtivarRetornoParaToca();
        //}
    }
    private IEnumerator InvokeEventAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        iniciarEvento.Invoke();
    }
}
