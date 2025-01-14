using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScriptSpider : MonoBehaviour
{
    public Transform PositionToWalkFirst; // Defina o destino no Inspector
    public NavMeshAgent SpiderNavAgent; // Certifique-se de que o agente está configurado
    public Animator SpiderAnimatorController;
    private bool indoRato = false;
    private void Start()
    {
        if (SpiderNavAgent == null)
        {
            SpiderNavAgent = GetComponent<NavMeshAgent>();
            SpiderAnimatorController = GetComponent<Animator>();
        }
    }

    public void Update()
    {
        float distance = Vector3.Distance(PositionToWalkFirst.position, transform.position);
        if (SpiderNavAgent.velocity.magnitude == 0)
        {
            SpiderAnimatorController.SetBool("isWalking", false); // Define animação de parado
            
        }
        else
        {
            SpiderAnimatorController.SetBool("isWalking", true); // Define animação de andando
        }
    }

    public void SpiderWalkToPoint()
    {
        if (PositionToWalkFirst != null && SpiderNavAgent != null)
        {
            //Código que vai fazer a ação da aranha andar e comer o rato 
            //É necessário todas as referencias estarem corretas
            Debug.Log("Andando para o ponto desejado");

            SpiderNavAgent.SetDestination(PositionToWalkFirst.position);
            
        }
        else
        {
            Debug.LogWarning("Faltando referências: verifique o PositionToWalkFirst ou o SpiderNavAgent.");
        }
    }
}
