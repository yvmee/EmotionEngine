using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Basic")]
public class BasicDialogueNode : DialogueNode
{
    [SerializeField]
    private DialogueNode _nextNode;
    public DialogueNode NextNode => _nextNode;
}
