using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Dialogue Channel")]
public class DialogueChannel : ScriptableObject
{
    public delegate void DialogueCallback(DialogueObject dialogue);
    public DialogueCallback OnDialogueRequested;
    public DialogueCallback OnDialogueStart;
    public DialogueCallback OnDialogueEnd;

    public delegate void DialogueNodeCallback(DialogueNode node);
    public DialogueNodeCallback OnDialogueNodeRequested;
    public DialogueNodeCallback OnDialogueNodeStart;
    public DialogueNodeCallback OnDialogueNodeEnd;

    public void RaiseRequestDialogue(DialogueObject dialogue)
    {
        OnDialogueRequested?.Invoke(dialogue);
    }

    public void RaiseDialogueStart(DialogueObject dialogue)
    {
        OnDialogueStart?.Invoke(dialogue);
    }

    public void RaiseDialogueEnd(DialogueObject dialogue)
    {
        OnDialogueEnd?.Invoke(dialogue);
    }

    public void RaiseRequestDialogueNode(DialogueNode node)
    {
        OnDialogueNodeRequested?.Invoke(node);
    }

    public void RaiseDialogueNodeStart(DialogueNode node)
    {
        OnDialogueNodeStart?.Invoke(node);
    }

    public void RaiseDialogueNodeEnd(DialogueNode node)
    {
        OnDialogueNodeEnd?.Invoke(node);
    }
}
