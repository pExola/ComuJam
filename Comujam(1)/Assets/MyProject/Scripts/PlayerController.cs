using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject arrow;
    private NavMeshAgent agent;
    public GameObject camera;
    public int velScrollCamera = 2, maxDistCamera=50,minDistCamera=20;
    public int posCameraAtras = 10;
    private int posCameraAtrasReal;
    private GameObject currentArrow;
    private Animator animacao;
    private bool estaParado = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animacao = GetComponent<Animator>();
        posCameraAtrasReal = posCameraAtras;
    }

    // Update is called once per frame
    void Update()
    {
        identificarClick();
        andarNormal();
        andarClickando();
        atualizarPosicaoCamera();
    }
    void identificarClick()
    {


    }
    void andarNormal()
    {
        this.transform.position = new Vector3(this.transform.position.x+Input.GetAxis("Horizontal"), this.transform.position.y, this.transform.position.z+ Input.GetAxis("Vertical"));
    }
    void andarClickando()
    {
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);

                SpawnArrow(hit.point);
                PlayAnimation("Walking");

                estaParado = false;
            }
        }
        if (agent.velocity.x == 0 && agent.velocity.y == 0 && agent.velocity.z == 0 && !estaParado)
        {
            PlayAnimation("Drunk Idle");
        }

        Debug.Log($"{agent.velocity.x} {agent.velocity.y} {agent.velocity.z}");
    }

    void atualizarPosicaoCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll > 0f)
        {
            if (this.camera.transform.position.y + velScrollCamera <= maxDistCamera)
            {
                this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y + velScrollCamera, this.transform.position.z);
                posCameraAtrasReal += velScrollCamera;
            }
        }
        else if (scroll < 0f)
        {
            if (this.camera.transform.position.y - velScrollCamera >= minDistCamera)
            {
                this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y - velScrollCamera, this.transform.position.z);
                posCameraAtrasReal -= velScrollCamera;
            }
        }
        else
        {
            this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y, this.transform.position.z - posCameraAtrasReal);
        }
        camera.transform.LookAt(this.transform.position);
    }


    void SpawnArrow(Vector3 position)
    {
        // Ajusta a posição para que a seta fique um pouco acima do chão
        if(currentArrow != null)
        {
            Destroy(currentArrow);
        }
        Vector3 arrowPosition = position + Vector3.up * 0.1f;

        // Instancia o prefab da seta
        currentArrow= Instantiate(arrow, arrowPosition, Quaternion.identity);
    }
    void PlayAnimation(string animationName)
    {
        // Reinicia e força a animação desejada
        animacao.Play(animationName);
    }
}
