using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public async void quebrarCadeado() 
    {
        Debug.Log("funcionando");
        objCubo.SetActive(false);
        await Task.Delay(1000);
        SceneManager.LoadScene("GAMEOVER");
        
    }
}
