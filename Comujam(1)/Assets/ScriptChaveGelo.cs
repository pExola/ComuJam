using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptChaveGelo : MonoBehaviour
{
    public Item chave;
    public GameObject objCubo;
    public Teleport tpCena;


    public void entregarAChaveProCabunco()
    {
        Inventory.SetItem(chave);
        objCubo.SetActive(false);
        tpCena.enabledTeleport = true;
    }
}
