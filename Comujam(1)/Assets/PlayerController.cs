using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    bool andando = false;
    private NavMeshAgent agent;
    public GameObject camera;
    public int velScrollCamera = 5;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            }
        }
    }

    void atualizarPosicaoCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y, this.transform.position.z-5);
        if (scroll > 0f)
        {
            if(this.camera.transform.position.y + velScrollCamera<= 50)
            {
                this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y+velScrollCamera, this.transform.position.z);
            }
        }
        else if (scroll < 0f)
        {
            if (this.camera.transform.position.y - velScrollCamera>=  20)
            {
                this.camera.transform.position = new Vector3(this.transform.position.x, this.camera.transform.position.y - velScrollCamera, this.transform.position.z);
            }
        }
        camera.transform.LookAt(this.transform.position);
    }
}
