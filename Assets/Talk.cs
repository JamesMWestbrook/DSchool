using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : Interactable
{
    public TalkClassSO TalkSO;
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

[CreateAssetMenu(fileName = "GroupSO", menuName = "DD/DialogueContainer")]
public class TalkClassSO : ScriptableObject
{
    public List<DialogueSO> dialogue;
    public bool Loop;

}