using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneControls : MonoBehaviour
{
    private PlayableDirector dir;
   [SerializeField] private PlayerController player;
    private PlayerActions actions;

    public float TimeToSkipTo;
    // Start is called before the first frame update



    private bool Paused;
    void Start()
    {
        dir = GetComponent<PlayableDirector>();
        actions = player.playerActions;
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.Pause.WasPressed)
        {
            if (Paused)
            {
                Paused = false;
                Time.timeScale = 1;
            }
            else
            {
                
                Time.timeScale = 0;
                Paused = true;
            }     
        }
    }
}
