using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Suzkiviado : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float tamanhoDaMinhaPica = 10000000;
    public bool subiu = false;
    public bool desceu =false;
    public long giros = 0;
    int largura = Screen.width/2;
    int altura = Screen.height/2;
    private int mouseX = 0, mouseY = 0;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Conexão com o componente de física
    }

    // Update is called once per frame
    void Update()
    {
        move();
        contarGiros();
        inputMousePosition();
        AtualizarObservacao();
    }
    void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f );

        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }
    void AtualizarObservacao()
    {
        this.transform.rotation = Quaternion.Euler(0f,0f, Mathf.Atan2((float)mouseY,(float)mouseX)*Mathf.Rad2Deg-90);       
    }
    void contarGiros()
    {
        float posicao = Mathf.Abs(this.transform.rotation.z);
        if ( posicao> 0.95f)
        {
            subiu = !subiu;
        }
        if(posicao < 0.05f)
        {
            desceu = !desceu;
        }
        if(subiu&& desceu)
        {
            giros++;
            subiu = !subiu;
            desceu = !desceu;
        }
        //Debug.Log($"{subiu} {desceu} {giros} {posicao} ");
    }
    void inputMousePosition()
    {
        var a = Input.mousePosition;
        this.mouseX = largura-(int)a.x; this.mouseY = altura - (int)a.y;
        
    }
}
