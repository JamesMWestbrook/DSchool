using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
public class QCCommands : MonoBehaviour
{
    [Command]
    public static void PauseActor(int i)
    {
        GameManager.Instance.Actors[i].GetComponent<Actor>().PauseAction();
    }
}
