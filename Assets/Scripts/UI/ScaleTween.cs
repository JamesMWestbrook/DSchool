using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        OnClose();
    }
    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f);
    }

    // Update is called once per frame
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
