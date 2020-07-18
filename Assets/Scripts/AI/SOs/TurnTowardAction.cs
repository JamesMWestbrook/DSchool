using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TurnToward", menuName = "DD/Actions/TurnToward")]
public class TurnTowardAction : DecayingDev.Action
{
    public override int ArgumentCount => 1;

    public override void Execute(string[] args, GameObject user)
    {
        int arg = Int32.Parse(args[0]);
        Transform target;
        if(arg == -1)
        {
            target = GameManager.Instance.playerController.transform;
        }
        else
        {
            target = GameManager.Instance.Actors[arg].transform;
        }

        user.GetComponent<RotateActor>().target = target;
        Actor actor = user.GetComponent<Actor>();
        actor.ExecuteAction(actor.ActionIndex + 1);
    }

}
