using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RatoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool emAlerta = false;
    private GameObject rato;
    public List<Vector3> pontosDeViagem;
    public Vector3 toca;
    private NavMeshAgent agentRato;
    private int destAtual=0;
    private bool retornandoParaToca = false;
    private Vector3 posInicial;
    public Transform posEstante,posEscotilha;
    public Teleport porta;
    [SerializeField] private bool estadoNormal = true;
    void Start()
    {
        rato = gameObject;
        agentRato = GetComponent<NavMeshAgent>();
        var position = GetComponent<Transform>().position;
        posInicial = new Vector3(position.x, position.y, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoNormal)
        {
            if (emAlerta)
            {
                movimentar();
            }
            else
            {
                agentRato.SetDestination(posInicial);
            }

        }
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
    public void comecarComerQueijo()
    {
        StartCoroutine(comerQueijo());
    }
    IEnumerator comerQueijo()
    {
        bool walking =true;
        agentRato.SetDestination(posEstante.position);
        yield return new WaitForSeconds(0.1f);
        while (walking)
        {
            if (agentRato.remainingDistance > 2)
            {
                yield return null;
            }
            else
            {
                walking = false;
                agentRato.SetDestination(posEstante.position);
            }
        }
        yield return new WaitForSeconds(2.1f);
        agentRato.SetDestination(posEscotilha.position);
        walking = true;
        yield return new WaitForSeconds(0.1f);
        while (walking)
        {
            if (agentRato.remainingDistance > 2)
            {
                yield return null;
            }
            else
            {
                walking = false;
                agentRato.SetDestination(posEscotilha.position);
            }
        }
        porta.enabled = true;
        rato.SetActive(false);
    }




    public void desativarAlerta()
    {
        emAlerta = false;
    }

    public void ativarAlerta()
    {
        emAlerta = true;
    }
    public void desativarEstadoNormal()
    {
        estadoNormal = false;
    }

    public void ativarEstadoNormal()
    {
        estadoNormal = true;
    }

}

