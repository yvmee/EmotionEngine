using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public static UnityEvent<DialogueNode, SideQuest> DialogueStart = new();
    public static UnityEvent DialogueStop = new();
    public static UnityEvent NextNode;

    public static void StartDialogue(DialogueObject obj, SideQuest sideQuest)
    {
        Debug.Log("Start Dialogue");
        DialogueStart.Invoke(obj.FirstNode, sideQuest);
    }
}
