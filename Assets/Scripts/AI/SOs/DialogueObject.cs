using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "DD/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public List<Dialogue> Script;
}
