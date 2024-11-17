using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask groundLayer;
    public GameObject arrow;
    public NavMeshAgent agent;
    public GameObject camera;
    public int velScrollCamera = 2, maxDistCamera=50,minDistCamera=20;
    public int posCameraAtras = 10;
    private int posCameraAtrasReal;
    private GameObject currentArrow;
    public bool cursorOnGround;
    private Animator animacao;
    private bool estaParado = true;
    public List<GameObject> inventario;
    public int capacidadeInventario = 30;

    PlayerInteractions playerInteractions;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animacao = GetComponent<Animator>();
        playerInteractions = GetComponent<PlayerInteractions>();
        posCameraAtrasReal = posCameraAtras;
        inventario = new List<GameObject>(capacidadeInventario);
    }

    // Update is called once per frame
    void Update()
    {
        andarClickando();
        atualizarPosicaoCamera();
    }
    void andarClickando()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                cursorOnGround = true;
                if (Input.GetMouseButtonDown(0))
                {
                    agent.SetDestination(hit.point);
                    SpawnArrow(hit.point);
                    playerInteractions.CancelInteraction();
                    UIManager.DisableInteraction();
                }
            }
            else
            {
                cursorOnGround = false;
            }
        }
        else 
        {
            cursorOnGround= false;  
        }
        
                
        if (agent.velocity.magnitude == 0 )
        {
            animacao.SetBool("isWalking", false);
        }
        else
        {
            animacao.SetBool("isWalking", true);
        }
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


    public bool CursorOnGround()
    {
        return cursorOnGround;
    }

}
