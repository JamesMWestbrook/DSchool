using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable :MonoBehaviour
{

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
                actor.Interactables.Add(gameObject);
                actor.UpdateInteractables();
            }
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        Actor actor = GetComponent<Actor>();
        if (actor)
        {
            if (!actor.AI)
            {
                actor.Interactables.Remove(gameObject);
                actor.UpdateInteractables();
            }
        }
    }
}
