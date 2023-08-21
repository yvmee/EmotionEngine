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

    [SerializeField] private DiscreteEmotion emotionPulse;
    [SerializeField] private bool hardEmotion = false;
    
    [SerializeField] private string[] _affectedStates;

    public string ChoicePreview => choicePreview;
    public DialogueNode ChoiceNode => choiceNode;
    public DiscreteEmotion EmotionPulse => emotionPulse;
    public bool HardEmotion => hardEmotion;
    public string[] AffectedStates => _affectedStates;
}


[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Choice")]
public class ChoiceDialogueNode : DialogueNode
{
    [SerializeField]
    private DialogueChoice[] choices;
    public DialogueChoice[] Choices => choices;
}
