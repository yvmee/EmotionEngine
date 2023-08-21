using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private RectTransform choicesBoxTransform;
    [SerializeField] private UIChoiceController choiceControllerPrefab;

    [SerializeField] private GameObject[] choices;

    private bool _inChoice = false;
    private DialogueNode _nextNode = null;
    private SideQuest _sideQuest;

    private PlayerInputController _playerInputController;

    private void Awake()
    {
        _playerInputController = FindObjectOfType<PlayerInputController>();
        DialogueController.DialogueStart.AddListener(OnDialogueNodeStart);
        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
        FindObjectOfType<PlayerInputController>().uiTextController = this;
    }

    private void OnDialogueNodeStart(DialogueNode node, SideQuest sideQuest)
    {
        _playerInputController.LockForInteraction();
        
        gameObject.SetActive(true);
        _sideQuest = sideQuest;
        Visit(node);
    }


    private void OnDialogueNodeEnd()
    {
        _playerInputController.UnlockForInteraction();
        
        _nextNode = null;
        dialogueText.text = "";
        speakerText.text = "";

        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
        DialogueController.DialogueStop.Invoke();
    }


    public void NextNode()
    {
        if (_inChoice) return;
        //if (_nextNode == null) OnDialogueNodeEnd();
        Visit(_nextNode);
    } 

    public void Visit(DialogueNode node)
    {
        if (node == null)
        {
            OnDialogueNodeEnd();
            return;
        }
        
        dialogueText.fontStyle = FontStyles.Normal;
        dialogueText.text = node.DialogueLine.Text;
        speakerText.text = node.DialogueLine.Speaker.CharacterName;
        
        switch (node)
        {
            case BasicDialogueNode dialogueNode:
                Visit(dialogueNode);
                break;
            case ChoiceDialogueNode choiceNode:
                Visit(choiceNode);
                break;
            case NarratorDialogueNode narratorNode:
                Visit(narratorNode);
                break;
        }
    }

    private void Visit(BasicDialogueNode node)
    {
        _nextNode = node.NextNode;
        
    }

    private void Visit(ChoiceDialogueNode node)
    {
        _inChoice = true;
        choicesBoxTransform.gameObject.SetActive(true);

        int i = 0;
        foreach (DialogueChoice choice in node.Choices)
        {
            choices[i].SetActive(true);
            choices[i].GetComponent<UIChoiceController>().Choice = choice;
            choices[i].GetComponent<UIChoiceController>().SetUIController(this);
            ++i;
        }
    }

    private void Visit(NarratorDialogueNode node)
    {
        dialogueText.fontStyle = FontStyles.Italic;
        _nextNode = node.NextNode;
    }

    public void DeactivateChoices()
    {
        foreach (var c in choices)
        {
            c.SetActive(false);
        }
        choicesBoxTransform.gameObject.SetActive(false);
        _inChoice = false;
    }

    public void CheckQuests(string[] affectedQuests)
    {
        foreach (var quest in affectedQuests)
        {
            _sideQuest.TrySetQuest(quest);
        }
    }
}
