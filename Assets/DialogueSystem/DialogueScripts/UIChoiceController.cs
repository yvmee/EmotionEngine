using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI choice;
    [SerializeField]
    private UITextController uiText;
    
    private DialogueNode _choiceNextNode;
    private DiscreteEmotion _emotion;
    private bool _hardEmotion;
    private string[] _affectedStates;

    public DialogueChoice Choice
    {
        set
        {
            choice.text = value.ChoicePreview;
            _choiceNextNode = value.ChoiceNode;
            _emotion = value.EmotionPulse;
            _hardEmotion = value.HardEmotion;
            _affectedStates = value.AffectedStates;
        }
    }

    public void SetUIController(UITextController uiController)
    {
        uiText = uiController;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        uiText.DeactivateChoices();
        if (_emotion != null) EmotionModel.EmotionPulseSend.Invoke(_emotion, _hardEmotion);
        uiText.CheckQuests(_affectedStates);
        uiText.Visit(_choiceNextNode);
    }
}
