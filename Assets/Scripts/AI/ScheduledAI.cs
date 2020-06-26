using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using DecayingDev;
using System;
using System.Runtime.CompilerServices;
using System.Data;

public class ScheduledAI : MonoBehaviour
{
    public List<Day> Days;
    private int CurDay = 0;
    private int ActionIndex = 0;
    private DecayingDev.Action CurAction;
    private NavMeshAgent nav;
    public ScheduleState state;
    private NavMeshAgent agent;
    public enum ScheduleState
    {
        RunningAction,
        WaitingForNextAction,
        PausedAction
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ExecuteAction(0, true);

    }
    public void Update()
    {
        switch (state)
        {
            case ScheduleState.RunningAction:
                AwaitNextAction();

                break;
            case ScheduleState.WaitingForNextAction:

                break;
            case ScheduleState.PausedAction:
                break;
        }
    }
    void ExecuteAction(int index, bool started = false)
    {
        state = ScheduleState.WaitingForNextAction;
        ActionIndex = index;
        PeriodSlot period = Days[CurDay].periodSlots[index];
        DecayingDev.Action _AIAction = period.action;

        if (started)
        {
            StartCoroutine(DelayedAIAction(_AIAction, period.args.ToArray(), 0));

        }
        else 
        { 
            StartCoroutine(DelayedAIAction(_AIAction, period.args.ToArray(), PreviousIndex(Days[CurDay].periodSlots))); 
        }
        //action.Execute(period.args.ToArray(), gameObject);
        CurAction = _AIAction;
    }
    void AwaitNextAction()
    {
        if (CurAction is MoveTo)
        {
            if (agent.remainingDistance < 1)
            {
                ExecuteAction(ActionIndex + 1);
            }
        }
    }
    float PreviousIndex(List<PeriodSlot> period)
    {
        if (ActionIndex - 1 < 0)
        {
            return period[period.Count - 1].DelayToNextAction;
        }
        else
        {
            return period[ActionIndex - 1].DelayToNextAction;
        }
    }

    public IEnumerator DelayedAIAction(DecayingDev.Action _AIAction, string[] args, float delayTime)
    {


        yield return new WaitForSeconds(delayTime);

        _AIAction.Execute(args, gameObject);
        state = ScheduleState.RunningAction;
    }

    //delayed action
    //void for executing either the next action, or the first one

}
