using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class TreeEmotionAsset : EmotionAsset
{
    [SerializeField] private float treshold = 0.2f;
    
    [SerializeField] private Sprite joy;
    [SerializeField] private Sprite sadness;
    [SerializeField] private Sprite anger;
    [SerializeField] private Sprite fear;
    [SerializeField] private Sprite surprise;
    [SerializeField] private Sprite pride;
    
    protected override void ChangeAsset(IEmotion emotion)
    {
        var discreteEmotion = (DiscreteEmotion) emotion;
        var strongest = new Emotion(EmotionType.Joy);
        foreach (var e in discreteEmotion.GetEmotions())
        {
            if (e.Intensity >= strongest.Intensity) strongest = e;
        }

        if (strongest.Intensity <= treshold) return;

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
