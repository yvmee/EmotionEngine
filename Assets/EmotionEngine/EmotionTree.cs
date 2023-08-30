using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using EmotionEngine;

public class EmotionTree : MonoBehaviour
{
    [SerializeField] private float treshold = 0.2f;
    
    [SerializeField] private Sprite joy;
    [SerializeField] private Sprite sadness;
    [SerializeField] private Sprite anger;
    [SerializeField] private Sprite fear;
    [SerializeField] private Sprite surprise;
    [SerializeField] private Sprite pride;

    private void Awake()
    {
        EmotionModel.EmotionStateChanged.AddListener(ChangeAsset);
    }

    private void ChangeAsset(DiscreteEmotion emotion)
    {
        var strongest = new EmotionVariable(EmotionType.Joy);
        foreach (var e in emotion.GetEmotions())
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
