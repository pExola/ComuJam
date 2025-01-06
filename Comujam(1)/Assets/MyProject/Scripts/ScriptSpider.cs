using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptSpider : MonoBehaviour
{
    public Transform PositionToWalkFirst;
    //private Transform StartPositiohn;
    public NavMeshAgent SpiderNavAgent;

    public void Start()
    {
        //PositionToWalkFirst = transform;
        this.SpiderNavAgent = this.GetComponent<NavMeshAgent>();
    }

    public void SpiderWalkToPoint()
    {
        Debug.Log("Andando para o ponto desejado");
        SpiderNavAgent.SetDestination(PositionToWalkFirst.position);

    }

}
