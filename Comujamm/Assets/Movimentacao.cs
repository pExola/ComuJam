using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = this.transform.position;
        this.transform.position = new Vector3(this.transform.position.x+ Input.GetAxis("Horizontal"), this.transform.position.y+ Input.GetAxis("Vertical"), 0);
    }
}
