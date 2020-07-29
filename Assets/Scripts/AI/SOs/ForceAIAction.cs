using Mono.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "ForceAction", menuName = "DD/Actions/ForceAction")]
public class ForceAIAction : DecayingDev.Action
{
    public override int ArgumentCount => 1;
    public DecayingDev.Action action;
    public override void Execute(string[] args, GameObject user)
    {
        int actorID = Int32.Parse(args[0]);

        Actor targetActor = GameManager.Instance.Actors[actorID].GetComponent<Actor>();
        targetActor.CurSecondaryAction = action;

        List<string> secondArgList = new List<string>();
        secondArgList = args.ToList();
        secondArgList.RemoveAt(0);
        targetActor.ExecuteSecondAction(secondArgList);
        Debug.Log(name);

        Actor actor = user.GetComponent<Actor>();
    }
}
