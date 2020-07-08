using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : Interactable
{
    public DoorInteract()
    {
        InteractText = "Open";
    }
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
    public void Start()
    {
        Bitch();
    }

    override public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Actor actor = other.gameObject.GetComponent<Actor>();
        if (actor && !door.Open)
        {
                door.side = side;
                actor.OpenDoor(door);
        }
    }
}
