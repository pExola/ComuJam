using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScriptSpider : MonoBehaviour
{
    public Transform PositionToWalkFirst; // Defina o destino no Inspector
    public NavMeshAgent SpiderNavAgent; // Certifique-se de que o agente est� configurado
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
        //Debug.Log($"Distancia = {distance.ToString()} Magnetude= {SpiderNavAgent.velocity.magnitude.ToString()}");
        if (SpiderNavAgent.velocity.magnitude < 0.4)
        {
            if (distance <= 3 && !ratoMorto)
            {
                SpiderNavAgent.ResetPath();
                Debug.Log("Ativando o comer");
                //StartCoroutine(Execute());
                StartCoroutine(ExecuteAfterDelay(3f));
                SpiderAnimatorController.SetBool("isEating", true); // Define anima��o de comer
                Debug.Log("Ativando o som");

                FindObjectOfType<AudioManager>().Play("RatoMorrendo");

            }
            else
            {
                SpiderAnimatorController.SetBool("isEating", false); // Define anima��o de comer
            }
            SpiderAnimatorController.SetBool("isWalking", false); // Define anima��o de parado

        }
        else
        {
            SpiderAnimatorController.SetBool("isWalking", true); // Define anima��o de andando
        }
    }

    public void SpiderWalkToPoint()
    {
        if (PositionToWalkFirst != null && SpiderNavAgent != null)
        {
            //C�digo que vai fazer a a��o da aranha andar e comer o rato 
            //� necess�rio todas as referencias estarem corretas
            Debug.Log("Andando para o ponto desejado");

            SpiderNavAgent.SetDestination(PositionToWalkFirst.position);
            
        }
        else
        {
            Debug.LogWarning("Faltando refer�ncias: verifique o PositionToWalkFirst ou o SpiderNavAgent.");
        }
    }
    public IEnumerator ExecuteAfterDelay(float delay)
    {
        //FindObjectOfType<AudioManager>().Play("RatoMorrendo");

        yield return new WaitForSeconds(delay);

        // A��o que ser� executada ap�s o delay
        Debug.Log("Rato morreu");

        ratoMorto = true;
        this.ratoArrombadoQueVaiMorrerNessaPorra.SetActive(false);
    }
    public IEnumerator Execute()
    {
        //FindObjectOfType<AudioManager>().Play("RatoMorrendo");
        yield return new WaitForSeconds(0.3f);
    }
}
