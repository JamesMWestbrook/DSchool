using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    public Mesh itemMesh;
    public Material itemMaterial;
    


    // Start is called before the first frame update
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.sharedMesh = itemMesh;
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.sharedMaterial = itemMaterial;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
