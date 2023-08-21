using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Narrator")]
public class NarratorDialogueNode : DialogueNode
{
    [SerializeField]
    private DialogueNode _nextNode;
    public DialogueNode NextNode => _nextNode;

}
