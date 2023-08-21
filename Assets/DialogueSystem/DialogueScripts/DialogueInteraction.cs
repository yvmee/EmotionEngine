using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject[] dialogueObjects;
    private int _index = 0;
    public bool inDialogue;

    private void Start()
    {
        DialogueController.DialogueStop.AddListener(StopInteraction);
    }

    public void Interact(GameObject player)
    {
        SideQuest sideQuest = gameObject.GetComponent<SideQuest>();
        DialogueController.StartDialogue(dialogueObjects[_index], sideQuest);
        inDialogue = true;
    }

    private void StopInteraction()
    {
        if (dialogueObjects.Length > 0) _index = (_index + 1) % dialogueObjects.Length;
        inDialogue = false;
    }
}
