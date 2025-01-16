using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Teleport : Interactable
{
    public string text;
    public GameObject posicaoTeleport;
    public GameObject player;
    public NavMeshAgent agent;
    public string cena;
    public GameObject escotilha;
    public bool enabledTeleport = false;
    public async override void Interact()
    {
        if (isInteracting)
            return;
        if (enabledTeleport)
        {
            isInteracting = true;
            Debug.Log($"{text} teleportanto!");
            await Task.Delay(1000);
            if (escotilha != null)
            {
                var animatorEscotilha = escotilha.GetComponent<Animator>();
                if (animatorEscotilha != null)
                {
                    animatorEscotilha.SetBool("isOpening", true);

                }
                else
                {
                    animatorEscotilha.SetBool("isOpening", false);
                }
            }
            SceneManager.LoadScene(cena);
        }
    }
}
