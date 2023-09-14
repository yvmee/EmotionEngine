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
    private EmotionStimulus _emotion;
    private bool _hardEmotion;
    private PersonalityEvent _personalityEvent;

    public DialogueChoice Choice
    {
        set
        {
            choice.text = value.ChoicePreview;
            _choiceNextNode = value.ChoiceNode;
            _emotion = value.EmotionStimulus;
            _hardEmotion = value.HardEmotion;
            _personalityEvent = value.PersonalityEvent;
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
        if (_emotion != null) 
            SendEmotionEvent();
        if (_personalityEvent != null)
            EmotionEngine.EmotionEngine.ChangePersonalityEvent.Invoke(_personalityEvent);
        uiText.Visit(_choiceNextNode);
    }
    
    private void SendEmotionEvent()
    {
        var e = Instantiate(_emotion);
        e.emotionState = Instantiate(_emotion.emotionState);
        EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(e);
    }
}
