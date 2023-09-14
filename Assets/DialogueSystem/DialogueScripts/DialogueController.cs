using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public static UnityEvent<DialogueNode> DialogueStart = new();
    public static UnityEvent DialogueStop = new();
    public static UnityEvent NextNode;

    public static void StartDialogue(DialogueObject obj)
    {
        DialogueStart.Invoke(obj.FirstNode);
    }
}
