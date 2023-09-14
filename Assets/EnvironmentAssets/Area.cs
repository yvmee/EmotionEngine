using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Serialization;

public class Area : MonoBehaviour
{
    [FormerlySerializedAs("emotionEvent")] public EmotionStimulus emotionStimulus;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(emotionStimulus);
    }
    
    private void SendEmotionEvent()
    {
        var e = Instantiate(emotionStimulus);
        e.emotionState = Instantiate(emotionStimulus.emotionState);
        EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(e);
    }
}
