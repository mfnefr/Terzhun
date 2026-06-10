using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    public static DialogueManager Instance {get; private set;}
    public Transform player;
    private NPC currentNPC;

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

    public void EnterDialogueMode(TextAsset inkJson, NPC npc)
    {
        this.currentNPC = npc;
        story = new Story(inkJson.text);

        if (!string.IsNullOrEmpty(currentNPC.GetDialogueState()))
        {
            story.state.LoadJson(currentNPC.GetDialogueState());
            story.ChoosePathString("rozcesti");
        }
        
        story.variablesState["haveCat"] = CheckForCatInInventory();

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        if (currentNPC != null && story != null)
        {
            string currentState = story.state.ToJson();
            currentNPC.SetDialogueState(currentState);
        }

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private bool CheckForCatInInventory()
    {
        List<UISlotHandler> inventoryContents = InventoryManager.Instance.GetInventoryContents();

        foreach (UISlotHandler cSlot in inventoryContents)
        {
            if (cSlot.item != null && cSlot.item.itemName == "Cat")
            {
                InventoryManager.Instance.ClearItemSlot(cSlot);
                return true;
            }
        }
        return false;
    }
}
