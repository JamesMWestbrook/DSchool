using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    Transform main;
    Quaternion q;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main.transform;
        q = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(main.transform);
        transform.Rotate(0, -180, 0);

        q = transform.rotation;
        //q.y = main.rotation.y;
        
        
        transform.rotation = q;
    }
}
