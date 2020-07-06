using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "DD/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<Dialogue> Script;
}
