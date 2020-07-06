using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : Interactable
{
    public List<DialogueSO> dialogue;
    private int dialogueIndex;
    public Talk()
    {
        InteractText = "Talk";
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InteractFunction()
    {
        DialogueCanvas.dialogueCanvas.Dialogue(dialogue[dialogueIndex]);
    }



}
