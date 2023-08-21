using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueException : System.Exception
{
    public DialogueException(string message) : base(message)
    {
    }
}

public class DialogueSequencer
{
    public delegate void DialogueCallback(DialogueObject dialogue);

    public delegate void DialogueNodeCallback(DialogueNode node);

    public DialogueCallback OnDialogueStart;
    public DialogueCallback OnDialogueEnd;
    public DialogueNodeCallback OnDialogueNodeStart;
    public DialogueNodeCallback OnDialogueNodeEnd;

    private DialogueObject _currentDialogue;
    private DialogueNode _currentNode;

    public void StartDialogue(DialogueObject dialogue)
    {
        if (_currentDialogue == null)
        {
            _currentDialogue = dialogue;
            OnDialogueStart?.Invoke(_currentDialogue);
            StartDialogueNode(dialogue.FirstNode);
        }
        else
        {
            throw new DialogueException("Can't start a dialogue when another is already running.");
        }
    }
    
    public void EndDialogue(DialogueObject dialogue)
    {
        if (_currentDialogue == dialogue)
        {
            StopDialogueNode(_currentNode);
            OnDialogueEnd?.Invoke(_currentDialogue);
            _currentDialogue = null;
        }
        else
        {
            throw new DialogueException("Trying to stop a dialogue that ins't running.");
        }
    }
    

    public void StartDialogueNode(DialogueNode node)
    {

            StopDialogueNode(_currentNode);

            _currentNode = node;

            if (_currentNode != null)
            {
                OnDialogueNodeStart?.Invoke(_currentNode);
            }
            else
            {
                EndDialogue(_currentDialogue);
            }

    }
    
    private void StopDialogueNode(DialogueNode node)
    {
        if (_currentNode == node)
        {
            OnDialogueNodeEnd?.Invoke(_currentNode);
            _currentNode = null;
        }
        else
        {
            throw new DialogueException("Trying to stop a dialogue node that ins't running.");
        }
    }
    
    
}
