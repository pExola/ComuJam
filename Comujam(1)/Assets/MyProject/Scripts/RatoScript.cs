using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RatoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool emAlerta = false;
    public List<Vector3> pontosDeViagem;
    public Vector3 toca;
    private NavMeshAgent agentRato;
    private int destAtual=0;
    private bool retornandoParaToca = false;
    private Vector3 posInicial;
    void Start()
    {
        agentRato = GetComponent<NavMeshAgent>();
        var position = GetComponent<Transform>().position;
        posInicial = new Vector3(position.x, position.y, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (emAlerta)
        {
            movimentar();
        }
        else
        {
            agentRato.SetDestination(posInicial);
        }
        /*else if (retornandoParaToca) 
        {
            retornarParaToca();
        }*/
    }
    public void movimentar()
    {
        if (retornandoParaToca) return;

        Vector3 pos = pontosDeViagem[destAtual % pontosDeViagem.Count];
        
        agentRato.SetDestination(pos);
        if (agentRato.velocity.magnitude == 0)
        {
            destAtual += 1;
        }
    }

   /* public void retornarParaToca()
    {
        agentRato.SetDestination(toca);

        if (!agentRato.pathPending && agentRato.remainingDistance <= agentRato.stoppingDistance)
        {
            retornandoParaToca = false; 
        }
    }

    public void AtivarRetornoParaToca()
    {
        retornandoParaToca = true;
        emAlerta = false; 
    }*/
}

