using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerController controller;
    IEnumerator interactRoutine;
    Interactable currentInteractable;
    public GameObject caixaDeSom;
    private Animator caixaDeSomAnimator;
    public int estadoCaixaDeSom = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        if (caixaDeSom != null)
        {
            caixaDeSomAnimator = caixaDeSom.GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.InDialogue())
            return;
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
                    UIManager.DisableInteraction();
                    interactRoutine = Interact(interactable);
                    StartCoroutine(interactRoutine);
                    //if (interactable.gameObject.name == "caixa_de_som")
                    //{
                    //    if (estadoCaixaDeSom == 0)
                    //    {
                    //        estadoCaixaDeSom = 1;
                    //    }
                    //}
                }
            }
            else if (controller.CursorOnGround())
            {
                UIManager.SetCursors(ObjectType.ground);
            }
            else
            {
                UIManager.SetCursors(ObjectType.none);
            }
        }
        else
        {
            UIManager.SetCursors(ObjectType.none);
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
            }
        }
        interactable.Interact();
        currentInteractable = interactable;
    }

    public void CaixaDeSomStateMachine()
    {
        if (estadoCaixaDeSom == 0)
        {
            caixaDeSomAnimator.SetBool("isPlaying", false);
        }
        else if (estadoCaixaDeSom == 1)
        {
            caixaDeSomAnimator.SetBool("isPlaying", true);
            Task.Delay(7000);
            estadoCaixaDeSom = 0;
        }
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
}
