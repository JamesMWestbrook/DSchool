using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    public int ActorID;
    private Animator animator;
    private NavMeshAgent agent;
    private DecayingDev.Action CurAction;
    public ScheduleState state;

    private int CurDay = 0;
    private int ActionIndex = 0;
    public Schedule schedule;


    public enum ScheduleState
    {
        RunningAction,
        WaitingForNextAction,
        PausedAction,
        IgnoringSchedule
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();


    }
    public void StartSchedule()
    {
        ExecuteAction(0, true);

    }
    // Update is called once per frame
    void Update()
    {
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
                case ScheduleState.IgnoringSchedule:

                    break;
            }
        }
    }



    void ExecuteAction(int index, bool started = false)
    {
        state = ScheduleState.WaitingForNextAction;
        ActionIndex = index;
        PeriodSlot period;
        DecayingDev.Action _AIAction;
        if (index < schedule.Periods.Count)
        {
            period = schedule.Periods[index];
            _AIAction = period.action;
        }
        else
        {
            ActionIndex = 0;
            period = schedule.Periods[0];
            _AIAction = period.action;
        }

        if (started)
        {
            StartCoroutine(DelayedAIAction(_AIAction, period.args.ToArray(), 0));

        }
        else
        {
            StartCoroutine(DelayedAIAction(_AIAction, period.args.ToArray(), PreviousIndex(schedule.Periods)));
        }
        Debug.Log(ActionIndex);

        //action.Execute(period.args.ToArray(), gameObject);
        CurAction = _AIAction;
    }
public  void PauseAction()
    {
        if(CurAction is MoveTo)
        {
            animator.SetBool("Moving", false);
            agent.isStopped = true;
        }
    }

    public void ResumeAction()
    {
        if (CurAction is MoveTo)
        {
            animator.SetBool("Moving", true);
            agent.isStopped = false;
            state = ScheduleState.RunningAction;
        }
        if(CurAction is Idle)
        {
           
        }
    }
    void AwaitNextAction()
    {
        if (CurAction is MoveTo)
        {
            if (agent.remainingDistance < 1)
            {
                ExecuteAction(ActionIndex + 1);
                animator.SetBool("Moving", false);
            }
        }
        if(CurAction is Idle)
        {
            if (!Idling)
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
    public bool IsBusy;
    public bool Idling;
    public void StartIdle(float time)
    {
        Idling = true;
        StartCoroutine(Idle(time));
        state = ScheduleState.RunningAction;
    }
    public IEnumerator Idle(float time)
    {
        if (IsBusy) yield return null;
        yield return new WaitForSeconds(time);
        Idling = false;

    }
    public IEnumerator DelayedAIAction(DecayingDev.Action _AIAction, string[] args, float delayTime)
    {


        yield return new WaitForSeconds(delayTime);

        _AIAction.Execute(args, gameObject);
        state = ScheduleState.RunningAction;
    }

    public void OpenDoor(Door door)
    {
//            agent.enabled = false;
            agent.isStopped = true;
            animator.SetBool("Moving", false);
        string side = "";
        switch (door.side)
        {
            case DoorInteract.Side.Front:
                side = "OpenFront";
                break;
            case DoorInteract.Side.Back:
                side = "OpenBack";
                break;
        }
        StartCoroutine(DoorOpening(door, side));
    }

    public IEnumerator DoorOpening(Door door, string side)
    {
        door.Open = true;
        door.GetComponent<Animator>().SetTrigger(side);
        yield return new WaitForSeconds(2);
        agent.isStopped = false;
        animator.SetBool("Moving", true);
    }

}
