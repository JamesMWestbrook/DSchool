﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SkipTest : MonoBehaviour
{
    public PlayableDirector dir;
    public float TimeToSkipTo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dir.time = TimeToSkipTo;
        }   
    }
}
