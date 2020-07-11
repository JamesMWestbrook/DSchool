using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [NonSerialized] public string InteractText = "Interact With Me";
    public virtual void InteractFunction()
    {

    }
    public virtual void Bitch()
    {
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();
        InteractionManager interactions = other.GetComponent<InteractionManager>();
        if (interactions)
        {
            interactions.Interactables.Add(this);
            interactions.UpdateInteractables();
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        InteractionManager interactions = other.GetComponent<InteractionManager>();
        Actor actor = other.GetComponent<Actor>();
        if (interactions)
        {
            interactions.Interactables.Remove(this);
            interactions.UpdateInteractables();
        }
    }
}
