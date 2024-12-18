using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Camada do chão usada para navegação
    public LayerMask groundLayer;

    // Prefab de seta para indicar o destino
    public GameObject arrow;

    // Componente de navegação para movimentação do player
    public NavMeshAgent agent;

    // Velocidade de zoom e limites da câmera
    public int velScrollCamera = 2, maxDistCamera = 50, minDistCamera = 20;

    // Referência à seta atual instanciada
    private GameObject currentArrow;

    // Indica se o cursor está sobre o chão
    public bool cursorOnGround;

    // Componente de animação do player
    private Animator animacao;

    // Capacidade máxima do inventário
    public int capacidadeInventario = 30;

    // Índice do item selecionado no inventário
    public int itemSelecionado = 0;

    // Referência para interações do player
    PlayerInteractions playerInteractions;

    // Posição do player no mundo
    public static Transform PlayerPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animacao = GetComponent<Animator>();
        playerInteractions = GetComponent<PlayerInteractions>();
        PlayerPos = GetComponent<Transform>();
    }

    void Update()
    {
        if (agent.velocity.magnitude == 0)
        {
            animacao.SetBool("isWalking", false); // Define animação de parado
        }
        else
        {
            animacao.SetBool("isWalking", true); // Define animação de andando
        }

        if (UIManager.InDialogue()) // Pausa movimentação durante o diálogo
            return;

        andarClickando();
        usarItem();
        selecionarItem();
        UIManager.selectItem(itemSelecionado); // Atualiza item selecionado na UI
    }

    // Movimenta o player clicando no chão
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
                    SpawnArrow(hit.point); // Instancia a seta no destino
                    playerInteractions.CancelInteraction(); // Cancela interação atual
                }
            }
            else
            {
                cursorOnGround = false;
            }
        }
        else
        {
            cursorOnGround = false;
        }

        if (agent.velocity.magnitude == 0)
        {
            animacao.SetBool("isWalking", false);
        }
        else
        {
            animacao.SetBool("isWalking", true);
        }

    }

    public bool CursorOnGround()
    {
        return cursorOnGround;
    }


    // Instancia uma seta no ponto clicado
    void SpawnArrow(Vector3 position)
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }
        Vector3 arrowPosition = position + Vector3.up * 0.1f;
        currentArrow = Instantiate(arrow, arrowPosition, Quaternion.identity);
    }

    // Usa o item selecionado no inventário
    void usarItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Item item = Inventory.GetItem(itemSelecionado);
            if (item != null)
            {
                Inventory.UseItem(item);
            }
        }
    }

    // Seleciona item no inventário com teclas 1 a 4
    void selecionarItem()
    {
        for (KeyCode key = KeyCode.Alpha1; key <= KeyCode.Alpha4; key++)
        {
            if (Input.GetKeyDown(key))
            {
                itemSelecionado = key - KeyCode.Alpha0 - 1; // Calcula índice
            }
        }
    }
}
