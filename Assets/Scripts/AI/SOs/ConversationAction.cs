using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConverseAction", menuName = "DD/Actions/Conversation")]

public class ConversationAction : DecayingDev.Action
{
    public override int ArgumentCount => 1;
    public List<int> InvolvedActors;
    public List<AIDialogue> Conversations;

    public override void Execute(string[] args, GameObject user)
    {
        for (int i = 0; i < InvolvedActors.Count; i++)
        {
            GameObject actor = GameManager.Instance.Actors[InvolvedActors[i]];
            if (actor == user) continue;
           actor.GetComponent<Actor>().PauseAction();
            
        }
        user.GetComponent<Actor>().state = Actor.ScheduleState.RunningAction;
        DialogueCanvas.dialogueCanvas.StartCaptions(this, 0);
    }
}


[System.Serializable]
public class AIDialogue
{
    public int SpeakerGOIndex;
    public string SpeakerText;
    public string Dialogue;
    public float Delay = 3f;
}