using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : Interactable
{
    public enum Side
    {
        Front,
        Back
    }
    public Side side;
    public Door door;
    public override void InteractFunction()
    {
        base.InteractFunction();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Actor actor = other.gameObject.GetComponent<Actor>();
        if (actor && !door.Open)
        {
            if (actor.AI)
            {
                door.side = side;
                actor.OpenDoor(door);
            }

        }
        
    }
}
