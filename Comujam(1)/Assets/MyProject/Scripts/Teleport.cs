using System.Collections;
using System.Collections.Generic;
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
    public override void Interact()
    {
        if (isInteracting)
            return;

        isInteracting = true;
        Debug.Log($"{text} teleportanto!");
        SceneManager.LoadScene(cena);
    }
}
