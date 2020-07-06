using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class Dialogue 
{
    public string Speaker;
    [TextArea] public string dialogue;
    public TimelineClip timeline;
}

[System.Serializable]
public class DialogueSlot
{
    
}