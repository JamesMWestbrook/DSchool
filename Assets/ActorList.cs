using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorList : MonoBehaviour
{
    public List<GameObject> Actors;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Actors = Actors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
