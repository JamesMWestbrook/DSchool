using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
   [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            TestHit test = hit.transform.gameObject.GetComponent<TestHit>();
            if (test)
            {
                test.HitByRay(parent);
            }
        }
    }
}
