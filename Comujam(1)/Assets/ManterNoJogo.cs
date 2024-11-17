using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManterNoJogo : MonoBehaviour
{
    // Start is called before the first frame update
    public ManterNoJogo instance;
    void Start()
    {
        
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Define a instância e preserva o objeto
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
