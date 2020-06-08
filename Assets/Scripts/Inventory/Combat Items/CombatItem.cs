using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatItem", menuName = "Combat Item", order = 1)]
public class CombatItem : ScriptableObject
{
    public Mesh Model;
    public Sprite Icon;
    public string Name;
    public string Description;

    public bool Usable;
    public bool Mixable;
    public bool Stackable = true;

}
