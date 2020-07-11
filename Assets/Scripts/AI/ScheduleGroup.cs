using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[CreateAssetMenu(fileName = "ScheduleGroup", menuName = "DD/ScheduleGroup")]
public class ScheduleGroup : ScriptableObject
{
    public List<Something> AllActors;
}


[System.Serializable]
public class Something
{
    public int actor;
    public Schedule schedule;
}