using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class PlayerInteractions : MonoBehaviour
{
    PlayerController controller;
    IEnumerator interactRoutine;
    Interactable currentInteractable;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent <PlayerController>();
        animator = GetComponent <Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable;
            hit.collider.TryGetComponent<Interactable>(out interactable);
            if (interactable != null)
            {
                UIManager.SetCursors(interactable.objectType);
           
                if (Input.GetButtonDown("Fire1")) 
                {
                    interactRoutine = Interact(interactable);
                    StartCoroutine(interactRoutine);
                    Debug.Log("Obj");
                    
                    //animator.SetTrigger("Acao");

                }
            }
            else if (controller.CursorOnGround())
            {
                UIManager.SetCursors(ObjectType.ground);
                Debug.Log("chao");
            }
            else 
            {
                UIManager.SetCursors(ObjectType.none);
                Debug.Log("fora");
            }
        }
        else
        {
            UIManager.SetCursors(ObjectType.none);
            Debug.Log("forao");
        }
    }

    IEnumerator Interact(Interactable interactable) 
    {
        bool walking = true;
        controller.agent.SetDestination(interactable.transform.position);
        yield return new WaitForSeconds(0.1f);
        while (walking) 
        {
            if (controller.agent.remainingDistance > 2)
            {
                yield return null;
            }
            else 
            {
                walking = false;
                controller.agent.SetDestination(transform.position);
                animator.SetBool("onAction", true);
                voltar();
            }
        }
        interactable.Interact();
        currentInteractable = interactable;
    }

    public void CancelInteraction() 
    {
        if (interactRoutine != null) 
            StopCoroutine(interactRoutine);

        if (currentInteractable != null) 
        {
            currentInteractable.isInteracting = false;
            currentInteractable = null;
        }
    }
    public async Task voltar()
    {
        await Task.Delay(1000);
        animator.SetBool("onAction", false);
    }
}
