using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavTest : MonoBehaviour
{
    NavMeshAgent Agent;

    [SerializeField] private Transform Destination;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        GetComponent<Animator>().SetBool("Moving", true);
        Agent.SetDestination(Destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
