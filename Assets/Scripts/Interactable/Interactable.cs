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
        Debug.Log(InteractText);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();
        InteractionManager interactions = other.GetComponent<InteractionManager>();
        if (!actor && interactions)
        {
            interactions.Interactables.Add(this);
            interactions.UpdateInteractables();
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (!actor)
        {
            InteractionManager interactions = other.GetComponent<InteractionManager>();
            interactions.Interactables.Remove(this);
            interactions.UpdateInteractables();
        }
    }
}
