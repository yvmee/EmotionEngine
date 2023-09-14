using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class TreeEmotionAsset : EmotionAsset
{

    [SerializeField] private Sprite joy;
    [SerializeField] private Sprite sadness;
    [SerializeField] private Sprite anger;
    [SerializeField] private Sprite fear;
    [SerializeField] private Sprite surprise;
    [SerializeField] private Sprite pride;
    [SerializeField] private Sprite neutral;
    
    protected override void ChangeAsset(EmotionState emotionState)
    {
        var discreteEmotion = (EmotionState) emotionState;
        var strongest = new EmotionVariable(EmotionType.Joy);
        foreach (var e in discreteEmotion.GetEmotions())
        {
            if (e.Intensity >= strongest.Intensity) strongest = e;
        }

        if (strongest.Intensity <= threshold)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = neutral;
            return;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = strongest.Type switch
        {
            EmotionType.Joy => joy,
            EmotionType.Sadness => sadness,
            EmotionType.Anger => anger,
            EmotionType.Fear => fear,
            EmotionType.Surprise => surprise,
            EmotionType.Pride => pride,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
