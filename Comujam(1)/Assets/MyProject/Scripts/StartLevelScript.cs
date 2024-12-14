using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartLevelScript : MonoBehaviour
{
    public List<UnityEvent> Eventos;
    public List<float> Delay;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Eventos.Count; ++i)
        {
            StartCoroutine(InvokeEventAfterDelay(Delay[i], Eventos[i]));
        }

        
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    private IEnumerator InvokeEventAfterDelay(float delay, UnityEvent evento)
    {
        yield return new WaitForSeconds(delay);
        evento.Invoke();
    }
}
