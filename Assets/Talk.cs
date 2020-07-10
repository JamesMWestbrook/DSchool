using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : Interactable
{
    public List<DialogueSO> dialogue;
    public int dialogueIndex;
    public bool Loop;
    public Talk()
    {
        InteractText = "Talk";
    }

    // Start is called before the first frame update
    void Start()
    {
        Bitch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InteractFunction()
    {
        DialogueCanvas.dialogueCanvas.Dialogue(dialogue[dialogueIndex], this);
    }



}
