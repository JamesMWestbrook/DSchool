using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractionManager : MonoBehaviour
{
    public TextMeshProUGUI InteractText;

    public List<Interactable> Interactables;
    public int InteractIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void UpdateInteractables()
{
    if (Interactables.Count > 0)
    {
        InteractText.text = Interactables[InteractIndex].InteractText;
    }
    else
    {
        InteractText.text = "";
    }

}
}
