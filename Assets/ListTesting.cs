using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTesting : MonoBehaviour
{
    public List<float> OriginalList;

    public void Start()
    {
        List<float> TempList = OriginalList;
        TempList.Add(3.65f);
        TempList.Insert(0,72.9f);
    }
}
