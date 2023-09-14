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
        DialogueController.StartDialogue(dialogueObjects[_index]);
        inDialogue = true;
    }

    private void StopInteraction()
    {

        if (_index < (dialogueObjects.Length - 1))
        {
            ++_index;
        }
        
        inDialogue = false;
    }
}
