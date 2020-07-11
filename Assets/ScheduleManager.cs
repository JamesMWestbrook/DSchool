using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager : MonoBehaviour
{

    public List<ScheduleGroup> Days;

    private void Start() //Anything that needs to ovveride the schedule will replace it at Awake
                         //This is Start that way it can be overridden before it is run
    {
        StartCoroutine(DelayedFunc());
    }

    public IEnumerator DelayedFunc()
    {
        yield return new WaitForSeconds(0.5f);
        List<GameObject> actors = GameManager.Instance.Actors;
        for (int i = 0; i < Days[0].AllActors.Count; i++)
        {
            Actor actor = actors[i].GetComponent<Actor>();
            actor.schedule = Days[0].AllActors[i].schedule;
            actor.StartSchedule();
        }
    }
}

