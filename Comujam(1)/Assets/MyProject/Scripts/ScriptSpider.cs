using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScriptSpider : MonoBehaviour
{
    public Transform PositionToWalkFirst; // Defina o destino no Inspector
    public NavMeshAgent SpiderNavAgent; // Certifique-se de que o agente está configurado
    public Animator SpiderAnimatorController;
    public GameObject ratoArrombadoQueVaiMorrerNessaPorra;
    private bool indoRato = false,ratoMorto = false;
    float distance;
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
        distance = Vector3.Distance(PositionToWalkFirst.position, transform.position);
        Debug.Log($"Distancia = {distance.ToString()} Magnetude= {SpiderNavAgent.velocity.magnitude.ToString()}");
        if (SpiderNavAgent.velocity.magnitude < 0.4)
        {
            if (distance <= 3 && !ratoMorto)
            {
                SpiderNavAgent.ResetPath();
                Debug.Log("Ativando o comer");
                SpiderAnimatorController.SetBool("isEating", true); // Define animação de comer
                StartCoroutine(ExecuteAfterDelay(3f));

            }
            else
            {
                SpiderAnimatorController.SetBool("isEating", false); // Define animação de comer
            }
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
    public IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Ação que será executada após o delay
        ratoMorto = true;
        this.ratoArrombadoQueVaiMorrerNessaPorra.SetActive(false);
    }
}
