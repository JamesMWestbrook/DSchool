using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CaptionsText : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI Text;
    private Transform Player;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Text = GetComponentInChildren<TextMeshProUGUI>();
        Player = GameManager.Instance.playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Target) return;
        float Distance = Vector3.Distance(Player.position, Target.position);
        Debug.Log(Distance);
        image.enabled = true;
        Text.enabled = true;
        if (Distance < 7f)
        {
            Text.fontSize = 36;
        }
        else if(Distance < 9f)
        {
            Text.fontSize = 30;
        }else if(Distance < 11)
        {
            Text.fontSize = 26;
        }
        else if(Distance < 13)
        {
            Text.fontSize = 22;
        }
        else if (Distance < 15)
        {
            Text.fontSize = 19;
        }
        else if (Distance < 17)
        {
            Text.fontSize = 17;
        }
        else
        {
            image.enabled = false;
            Text.enabled = false;
        }
    }
}
