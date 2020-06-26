using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

namespace DecayingDev
{
    public abstract class Action : ScriptableObject
    {
        public abstract int ArgumentCount { get; }
        public abstract void Execute(string[] args, GameObject user);
        public virtual string GetArgumentName(int argIndex) { return $"arg {argIndex}"; }


    }
}