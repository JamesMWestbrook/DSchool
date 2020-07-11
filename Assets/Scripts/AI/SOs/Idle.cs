using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "DD/Actions/Idle")]
public class Idle : DecayingDev.Action
{
    public override int ArgumentCount => 2;

    public override void Execute(string[] args, GameObject user)
    {
        int argument = Int32.Parse(args[0]);

        user.GetComponent<Actor>().StartIdle(argument * 1f);
          
        //logic for setting rotation and an animation but don't have the means right now
    }
}
