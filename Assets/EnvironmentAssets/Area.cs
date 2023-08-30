using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class Area : MonoBehaviour
{
    public EmotionEvent emotionEvent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            EmotionModel.EmotionStimulusEvent.Invoke(emotionEvent, false);
    }
    
    private void SendEmotionEvent()
    {
        var e = Instantiate(emotionEvent);
        e.emotion = Instantiate(emotionEvent.emotion);
        EmotionModel.EmotionStimulusEvent.Invoke(e, false);
    }
}
