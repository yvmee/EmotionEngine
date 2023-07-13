using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueInstigator : MonoBehaviour
{
    [SerializeField]
    private DialogueChannel dialogueChannel;
    [SerializeField]
    private FlowChannel flowChannel;
    [SerializeField]
    private FlowState dialogueState;

    private DialogueSequencer _dialogueSequencer;
    private FlowState _cachedFlowState;

    private void Awake()
    {
        _dialogueSequencer = new DialogueSequencer();

        _dialogueSequencer.OnDialogueStart += OnDialogueStart;
        _dialogueSequencer.OnDialogueEnd += OnDialogueEnd;
        _dialogueSequencer.OnDialogueNodeStart += dialogueChannel.RaiseDialogueNodeStart;
        _dialogueSequencer.OnDialogueNodeEnd += dialogueChannel.RaiseDialogueNodeEnd;

        dialogueChannel.OnDialogueRequested += _dialogueSequencer.StartDialogue;
        dialogueChannel.OnDialogueNodeRequested += _dialogueSequencer.StartDialogueNode;
    }

    private void OnDestroy()
    {
        dialogueChannel.OnDialogueNodeRequested -= _dialogueSequencer.StartDialogueNode;
        dialogueChannel.OnDialogueRequested -= _dialogueSequencer.StartDialogue;

        _dialogueSequencer.OnDialogueNodeEnd -= dialogueChannel.RaiseDialogueNodeEnd;
        _dialogueSequencer.OnDialogueNodeStart -= dialogueChannel.RaiseDialogueNodeStart;
        _dialogueSequencer.OnDialogueEnd -= OnDialogueEnd;
        _dialogueSequencer.OnDialogueStart -= OnDialogueStart;

        _dialogueSequencer = null;
    }

    private void OnDialogueStart(DialogueObject dialogue)
    {
        dialogueChannel.RaiseDialogueStart(dialogue);

        _cachedFlowState = FlowStateMachine.Instance.CurrentState;
        flowChannel.RaiseFlowStateRequest(dialogueState);
    }

    private void OnDialogueEnd(DialogueObject dialogue)
    {
        flowChannel.RaiseFlowStateRequest(_cachedFlowState);
        _cachedFlowState = null;

        dialogueChannel.RaiseDialogueEnd(dialogue);
    }
}
