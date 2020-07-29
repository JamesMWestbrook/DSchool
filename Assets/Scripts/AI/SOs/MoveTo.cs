using DecayingDev;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MoveTo", menuName = "DD/Actions/MoveTo")]
public class MoveTo : DecayingDev.Action
{
    public override int ArgumentCount => 1;

    public override void Execute(string[] args, GameObject user)
    {

        int argument = Int32.Parse(args[0]);

        Debug.Log(argument);
        user.GetComponent<Animator>().SetBool("Moving", true);
        user.GetComponent<NavMeshAgent>().destination = LevelInfo.levelInfo.WayPoints[argument].position;
    }
}
