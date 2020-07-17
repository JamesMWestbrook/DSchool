using DecayingDev;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public static DialogueCanvas dialogueCanvas;
    private Talk currentTalk;

    public GameObject SpeakerPanel;
    public GameObject DialoguePanel;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public bool InDialogue;
    // Start is called before the first frame update

    public GameObject CaptionsPanel;
    public TextMeshProUGUI captionsText;

    void Start()
    {
        dialogueCanvas = this;
        speakerText.text = "";
        dialogueText.text = "";

        SpeakerPanel.GetComponent<RectTransform>().LeanScale(new Vector3(0, 0, 0), 0f);
        DialoguePanel.GetComponent<RectTransform>().LeanScale(new Vector3(0, 0, 0), 0f);

        actions = GameManager.Instance.playerController.playerActions;
    }

    private bool InputAllowed;
    private PlayerActions actions;
    void Update()
    {
        if (InDialogue && actions.Interact.WasPressed && InputAllowed)
        {
            NextDialogue();
        }
    }
    public DialogueObject currentDialogue;
    private int dialogueIndex;
    public void Dialogue(DialogueObject dialogue, Talk _talk)
    {
        currentTalk = _talk;
        InDialogue = true;
        currentDialogue = dialogue;
        speakerText.text = "Speaker: " +  dialogue.Script[0].Speaker;
        dialogueText.text = dialogue.Script[0].dialogue;

        LeanTween.scale(DialoguePanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(SpeakerPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
        StartCoroutine(DelayInput(false));

        UpdateText();
        GameManager.Instance.playerController.InControl = false;
    }
    public void NextDialogue()
    {
        if(dialogueIndex + 1 >= currentDialogue.Script.Count)
        {
            EndDialogue();
            InputAllowed = false;
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
        InputAllowed = false;
        StartCoroutine(DelayInput(false));

    }

    public void EndDialogue()
    {
        dialogueIndex = 0;
        int index = currentTalk.dialogueIndex + 1;
        if(index >= currentTalk.TalkSO.dialogue.Count)
        {
            if (currentTalk.TalkSO.Loop)
            {
                currentTalk.dialogueIndex = 0;
            }
        }
        else
        {
            currentTalk.dialogueIndex++;
        }
        StartCoroutine(DelayInput(true));
        LeanTween.scale(DialoguePanel.gameObject, new Vector3(0, 0, 0), 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(SpeakerPanel.gameObject, new Vector3(0, 0, 0), 0.3f).setEase(LeanTweenType.easeOutQuad);

        currentTalk.GetComponentInParent<Actor>().ResumeAction();
    }

    public IEnumerator DelayInput(bool FinishedDialogue)
    {

        yield return new WaitForSeconds(0.3f);

        if (FinishedDialogue)
        {
            GameManager.Instance.playerController.InControl = true;
        }
        else
        {
            InputAllowed = true;
        }

    }

    private ConversationAction currentAction;
    private int conversationIndex;
    private int currentSpeaker;
    public void StartCaptions(ConversationAction action, int dialogueIndex)
    {
        int actorID = action.Conversations[dialogueIndex].SpeakerGOIndex;
        currentSpeaker = actorID;
        conversationIndex = dialogueIndex;
        currentAction = action;
        GameManager.Instance.Actors[actorID].GetComponent<Actor>().CaptionIndicator.SetActive(true);

        CaptionsPanel.SetActive(true);
        AIDialogue dialogue = action.Conversations[dialogueIndex];
        captionsText.text = dialogue.SpeakerText + ": " + dialogue.Dialogue;
        captionsText.maxVisibleCharacters = 0;

        LeanTween.value(captionsText.gameObject, (float x) => captionsText.maxVisibleCharacters = (int)x, 0, captionsText.text.Length, 1);
        StartCoroutine(DelayedFunction(RunNextCaption, dialogue.Delay));
        StartCoroutine(DelayedFunction(HideIndicator, dialogue.Delay - 0.1f));
        GetComponentInChildren<CaptionsText>().Target = GameManager.Instance.Actors[actorID].transform;
    }
    private IEnumerator DelayedFunction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    private void RunNextCaption()
    {
        StartCaptions(currentAction, conversationIndex + 1);
    }
    private void HideIndicator()
    {
        GameManager.Instance.Actors[currentSpeaker].GetComponent<Actor>().CaptionIndicator.SetActive(false);
    }
    //Start Caption
    
    //IEnumerator for running next along with showing showable text
    

}
