using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    public bool AI = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor(Door door)
    {
        if (AI)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        animator.SetBool("Open", true);
    }
}
