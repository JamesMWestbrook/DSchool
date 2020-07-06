using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public static DialogueCanvas dialogueCanvas;

    public GameObject SpeakerPanel;
    public GameObject DialoguePanel;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public bool InDialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogueCanvas = this;
        speakerText.text = "";
        dialogueText.text = "";

        SpeakerPanel.GetComponent<RectTransform>().LeanScale(new Vector3(0, 0, 0), 0f);
        DialoguePanel.GetComponent<RectTransform>().LeanScale(new Vector3(0, 0, 0), 0f);

        actions = GameManager.Instance.playerController.playerActions;
    }

    private PlayerActions actions;
    void Update()
    {
        if (InDialogue && actions.Interact.WasPressed)
        {
            NextDialogue();
        }
    }
    public DialogueSO currentDialogue;
    private int dialogueIndex;
    public void Dialogue(DialogueSO dialogue)
    {
        InDialogue = true;
        currentDialogue = dialogue;
        speakerText.text = "Speaker: " +  dialogue.Script[0].Speaker;
        dialogueText.text = dialogue.Script[0].dialogue;

        LeanTween.scale(DialoguePanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(SpeakerPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);

        UpdateText();
        GameManager.Instance.playerController.InControl = false;
    }
    public void NextDialogue()
    {
        if(dialogueIndex + 1 >= currentDialogue.Script.Count)
        {

            //shrink screen and return input
        }
        else //hasn't run out of dialogue yet
        {
            dialogueIndex++;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        speakerText.text = "Speaker: " + currentDialogue.Script[dialogueIndex].Speaker;
        dialogueText.text = currentDialogue.Script[dialogueIndex].dialogue;

        speakerText.maxVisibleCharacters = 0;
        dialogueText.maxVisibleCharacters = 0;
        LeanTween.value(speakerText.gameObject, (float x) => speakerText.maxVisibleCharacters = (int)x, 0, speakerText.text.Length, 0.3f);
        LeanTween.value(dialogueText.gameObject, (float x) => dialogueText.maxVisibleCharacters = (int)x, 0, dialogueText.text.Length, 0.3f);
    }

    public void EndDialogue()
    {

    }
}
