using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo levelInfo;
    public List<Transform> WayPoints;
    // Start is called before the first frame update
    void Awake()
    {
        if(levelInfo != null)
        {
            Destroy(gameObject);
            return;
        }

        levelInfo = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoTo(int t)
    {

    }
}
