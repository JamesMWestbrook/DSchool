using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int LanguageIndex;
    public PlayerController playerController;

    public static GameManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
