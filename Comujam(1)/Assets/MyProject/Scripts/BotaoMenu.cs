using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoMenu : MonoBehaviour
{
    public void Jogar() 
    {
        SceneManager.LoadScene(1);
    }
}
