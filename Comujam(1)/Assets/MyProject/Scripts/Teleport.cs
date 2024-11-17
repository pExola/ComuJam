using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async override void Interact()
    {
        if (isInteracting)
            return;

        isInteracting = true;
        Debug.Log($"{text} teleportanto!");
        await Task.Delay(1000);
        SceneManager.LoadScene(cena);
    }
}
