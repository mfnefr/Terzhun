using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    public static DialogueManager Instance {get; private set;}
    public Transform player;

    [Header("Dialogue Settings")]
    private Story story;
    public bool dialogueIsPlaying {get; private set;}

    [Header("UI Reference")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("UI Choices")]
    public GameObject choicesPrefab;
    public GameObject choicesHolder;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!dialogueIsPlaying) return;

        // přeskakování dialogů
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
            DisplayChoices();
        }
        else if(story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = story.currentChoices;

        foreach(Transform child in choicesHolder.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Choice choice in currentChoices)
        {
            GameObject choiceObj = Instantiate(choicesPrefab, choicesHolder.transform);

            TextMeshProUGUI text = choiceObj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = choice.text;

            Button button = choiceObj.GetComponent<Button>();
            button.onClick.AddListener(() => MakeChoice(choice.index));
            
        }
    }

    private void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);

        ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        story = new Story(inkJson.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
