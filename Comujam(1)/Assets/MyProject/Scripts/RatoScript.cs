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
    public Vector3 pontoInicial;
    public Vector3 pontoDestino;

    private bool indoParaDestino = true;
    void Start()
    {
        agentRato = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (emAlerta)
        {
            movimentar();
        }
        else if (retornandoParaToca) 
        {
            retornarParaToca();
        }*/

        if (emAlerta)
        {
            indoParaDestino = !indoParaDestino;
        }


        if (indoParaDestino)
        {
            agentRato.SetDestination(pontoDestino);
        }
        else
        {
            agentRato.SetDestination(pontoInicial);
        }
    }
    public void movimentar()
    {
        //if (retornandoParaToca) return;

        Vector3 pos = pontosDeViagem[destAtual % pontosDeViagem.Count];
        
        agentRato.SetDestination(pos);
        if (agentRato.velocity.magnitude == 0)
        {
            destAtual += 1;
        }
    }

    /*public void retornarParaToca()
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

