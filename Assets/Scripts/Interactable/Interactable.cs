using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable :MonoBehaviour
{
    [HideInInspector] public string InteractText = "Interact With Me";
    public virtual void InteractFunction()
    {

    }
    public virtual void Bitch()
    {
        Debug.Log("bitch");
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor)
        {
            if (!actor.AI)
            {
                actor.Interactables.Add(this);
                actor.UpdateInteractables();
            }
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor)
        {
            if (!actor.AI)
            {
                actor.Interactables.Remove(this);
                actor.UpdateInteractables();
            }
        }
    }
}
