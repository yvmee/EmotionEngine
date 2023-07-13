using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDialogueChoiceController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI choice; 
    [SerializeField]
    private DialogueChannel dialogueChannel;

    private DialogueNode _choiceNextNode;

    public DialogueChoice Choice
    {
        set
        {
            choice.text = value.ChoicePreview;
            _choiceNextNode = value.ChoiceNode;
        }
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        dialogueChannel.RaiseRequestDialogueNode(_choiceNextNode);
    }
}
