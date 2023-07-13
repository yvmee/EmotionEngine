using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class UIDialogueTextBoxController : MonoBehaviour, DialogueNodeVisitor
{ 
    [SerializeField]
    private TextMeshProUGUI speakerText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private RectTransform choicesBoxTransform;
    [SerializeField]
    private UIDialogueChoiceController choiceControllerPrefab;

    [SerializeField]
    private DialogueChannel dialogueChannel;

    private bool _listenToInput = false;
    private DialogueNode _nextNode = null;

    private void Awake()
    {
        dialogueChannel.OnDialogueNodeStart += OnDialogueNodeStart;
        dialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;

        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        dialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
        dialogueChannel.OnDialogueNodeStart -= OnDialogueNodeStart;
    }

    private void Update()
    {
        if (_listenToInput && Input.GetButtonDown("Submit"))
        {
            dialogueChannel.RaiseRequestDialogueNode(_nextNode);
        }
    }

    private void OnDialogueNodeStart(DialogueNode node)
    {
        gameObject.SetActive(true);

        dialogueText.text = node.DialogueLine.Text;
        speakerText.text = node.DialogueLine.Speaker.CharacterName;

        node.Accept(this);
    }

    private void OnDialogueNodeEnd(DialogueNode node)
    {
        _nextNode = null;
        _listenToInput = false;
        dialogueText.text = "";
        speakerText.text = "";

        foreach (Transform child in choicesBoxTransform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
    }

    public void Visit(BasicDialogueNode node)
    {
        _listenToInput = true;
        _nextNode = node.NextNode;
    }

    public void Visit(ChoiceDialogueNode node)
    {
        choicesBoxTransform.gameObject.SetActive(true);

        foreach (DialogueChoice choice in node.Choices)
        {
            UIDialogueChoiceController newChoice = Instantiate(choiceControllerPrefab, choicesBoxTransform);
            newChoice.Choice = choice;
        }
    }
}
