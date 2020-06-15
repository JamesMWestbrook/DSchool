using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
   public GameObject bitch;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.color(bitch, new Color(200, 0, 0, 255), 0.0f);
       // skin.material.SetColor(0, new Color(255, 100, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
