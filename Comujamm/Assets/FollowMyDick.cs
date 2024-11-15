using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMyDick : MonoBehaviour
{
    GameObject pimbada;
    Vector3 tramit;
    float x, y, z;
    void Start()
    {
        pimbada = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        seguir();
    }
    void seguir()
    {
        x = pimbada.transform.position.x;
        y = pimbada.transform.position.y;
        z = this.transform.position.z;
        tramit = new Vector3(x, y, z);
        this.transform.position = tramit;
    }
}
