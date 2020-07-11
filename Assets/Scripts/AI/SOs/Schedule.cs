using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Schedule", menuName = "DD/Schedule")]
public class Schedule : ScriptableObject
{
    public List<PeriodSlot> Periods;
}
