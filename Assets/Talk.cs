using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class Talk : Interactable
{
    public DialogueGroup TalkSO;
    public int dialogueIndex;
    public Talk()
    {
        InteractText = "Talk";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InteractFunction()
    {
        DialogueCanvas.dialogueCanvas.Dialogue(TalkSO.dialogue[dialogueIndex], this);
    }
}
[System.Serializable]
public class Dialogue
{
    public string Speaker;
    [TextArea] public string dialogue;
    public TimelineClip timeline;
}