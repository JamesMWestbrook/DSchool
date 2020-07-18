using InControl.UnityDeviceProfiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "TurnToward", menuName = "DD/Actions/TurnToward")]
public class TurnTowardAction : DecayingDev.Action
{
    public override int ArgumentCount => 2;
    float speed = 1f;
    public override void Execute(string[] args, GameObject user)
    {
        int index = Int32.Parse(args[0]);
        if (args.Length == 2)
        {
            speed = float.Parse(args[1]);

        }
        else { speed = 1f; }
        Transform target;
        if(index == -1)
        {
            target = GameManager.Instance.playerController.transform;
        }
        else
        {
            target = GameManager.Instance.Actors[index].transform;
        }
        RotateActor rotateActor = user.GetComponent<RotateActor>();
         rotateActor.target= target;
        rotateActor.speed = speed;
        Actor actor = user.GetComponent<Actor>();
        actor.ExecuteAction(actor.ActionIndex + 1);
    }
}
