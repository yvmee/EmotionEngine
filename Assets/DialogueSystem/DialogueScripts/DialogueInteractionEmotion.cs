using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DialogueInteractionEmotion : EmotionAsset, IInteractable
{
    [SerializeField] private DialogueObject[] dialogueObjects;
    [SerializeField] private int _emotionIndex = 0;
    public bool inDialogue;

    [SerializeField] private int joyDialogue;
    [SerializeField] private int sadnessDialogue;
    [SerializeField] private int angerDialogue;
    [SerializeField] private int fearDialogue;
    [SerializeField] private int surpriseDialogue;
    [SerializeField] private int prideDialogue;


    private void Start()
    {
        DialogueController.DialogueStop.AddListener(StopInteraction);
    }

    public void Interact(GameObject player)
    {
        Debug.Log("Interacting: " + dialogueObjects[_emotionIndex].name);
        DialogueController.StartDialogue(dialogueObjects[_emotionIndex]);
        inDialogue = true;
    }

    private void StopInteraction()
    {
        inDialogue = false;
    }

    protected override void ChangeAsset(EmotionState emotionState)
    {
        var strongest = new EmotionVariable(EmotionType.Joy);
        
        foreach (var e in emotionState.GetEmotions())
        {
            if (e.Intensity >= strongest.Intensity) strongest = e;
        }
        
        if (strongest.Intensity <= threshold)
        {
            _emotionIndex = 0;
            return;
        }
        
        _emotionIndex = strongest.Type switch
        {
            EmotionType.Joy => joyDialogue,
            EmotionType.Sadness => sadnessDialogue,
            EmotionType.Anger => angerDialogue,
            EmotionType.Fear => fearDialogue,
            EmotionType.Surprise => surpriseDialogue,
            EmotionType.Pride => prideDialogue,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
