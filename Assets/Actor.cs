using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (agent.remainingDistance < 1)
            {
                animator.SetBool("Moving", false);
            }

        }
    }



    public void OpenDoor(Door door)
    {
//            agent.enabled = false;
            agent.isStopped = true;
            animator.SetBool("Moving", false);
        string side = "";
        switch (door.side)
        {
            case DoorInteract.Side.Front:
                side = "OpenFront";
                break;
            case DoorInteract.Side.Back:
                side = "OpenBack";
                break;
        }
        StartCoroutine(DoorOpening(door, side));
    }

    public IEnumerator DoorOpening(Door door, string side)
    {
        door.Open = true;
        door.GetComponent<Animator>().SetTrigger(side);
        yield return new WaitForSeconds(2);
        agent.isStopped = false;
        animator.SetBool("Moving", true);
    }

}
