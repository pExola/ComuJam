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
            
            animator.SetBool("isPlaying", !animator.GetBool("isPlaying"));
            RatoScript.emAlerta = !RatoScript.emAlerta;
        }
    }
}
