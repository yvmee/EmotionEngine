using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DialogueChoice
{
    [SerializeField]  private string choicePreview;
    [SerializeField] private DialogueNode choiceNode;

    [SerializeField] private EmotionEvent emotionEvent;
    [SerializeField] private bool hardEmotion = false;
    [SerializeField] private PersonalityEvent personalityEvent;

    public string ChoicePreview => choicePreview;
    public DialogueNode ChoiceNode => choiceNode;
    public EmotionEvent EmotionEvent => emotionEvent;
    public bool HardEmotion => hardEmotion;
    public PersonalityEvent PersonalityEvent => personalityEvent;

}


[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Choice")]
public class ChoiceDialogueNode : DialogueNode
{
    [SerializeField]
    private DialogueChoice[] choices;
    public DialogueChoice[] Choices => choices;
}
