using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent <PlayerController>();
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
                Debug.Log("Objeto interativo do tipo " + interactable.objectType);
            }
            else if (controller.CursorOnGround())
            {
                Debug.Log("Cursor no chao");
            }
            else 
            {
                Debug.Log("Sem cursor");
            }
        }
        else
        {
            Debug.Log("Sem cursor");
        }
    }
}
