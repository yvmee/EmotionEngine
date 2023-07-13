using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogueNodeVisitor
{
    void Visit(BasicDialogueNode node);
    void Visit(ChoiceDialogueNode node);
}
