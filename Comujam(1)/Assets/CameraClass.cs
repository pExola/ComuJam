using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClass : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform posInicial;
    private static CameraClass instanciaCamera;
    private Camera mainCamera;
    void Start()
    {
        instanciaCamera = this;
        mainCamera = GetComponent<Camera>();
        instanciaCamera.mainCamera = mainCamera;
        instanciaCamera.posInicial = GetComponent<Transform>();
    }

    public static void setPosInicial()
    {
        instanciaCamera.mainCamera.transform.position = instanciaCamera.posInicial.position;
        instanciaCamera.mainCamera.transform.rotation = instanciaCamera.posInicial.rotation;
    }
    public static void setPos(Vector3 pos)
    {
        instanciaCamera.mainCamera.transform.position=new Vector3(pos.x,instanciaCamera.posInicial.position.y/4,pos.y);
    }
}
