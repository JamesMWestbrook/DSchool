using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueGroup", menuName = "DD/DialogueGroup")]
public class DialogueGroup : ScriptableObject
{
    public List<DialogueObject> dialogue;
    public bool Loop;

}
