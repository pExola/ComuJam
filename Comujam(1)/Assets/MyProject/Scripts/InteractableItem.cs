using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InteractableItem : Interactable
{
    public GameObject objeto;
    public Animator animator;
    public override void Interact()
    {
        Debug.Log("Interagindo");
        if(animator != null)
        {
            bool isPlaying = !animator.GetBool("isPlaying");
            animator.SetBool("isPlaying", isPlaying);
            RatoScript.emAlerta = isPlaying;

            if (isPlaying)
            {

                StartCoroutine(DesativarSomAposTempo(5f));
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
}
