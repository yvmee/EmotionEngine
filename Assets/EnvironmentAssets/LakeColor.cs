using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LakeColor : EmotionAsset
{
    public Color neutralColor = Color.white;
    public Color joyColor;
    public Color sadnessColor;
    public Color angerColor;
    public Color fearColor;
    public Color surpriseColor;
    public Color prideColor;

    protected override void ChangeAsset(EmotionState emotionState)
    {
        var strongest = new EmotionVariable(EmotionType.Joy);
        
        foreach (var e in emotionState.GetEmotions())
        {
            if (e.Intensity >= strongest.Intensity) strongest = e;
        }
        
        if (strongest.Intensity <= threshold)
        {
            gameObject.GetComponent<Tilemap>().color = neutralColor;
            return;
        }
        
        gameObject.GetComponent<Tilemap>().color = strongest.Type switch
        {
            EmotionType.Joy => joyColor,
            EmotionType.Sadness => sadnessColor,
            EmotionType.Anger => angerColor,
            EmotionType.Fear => fearColor,
            EmotionType.Surprise => surpriseColor,
            EmotionType.Pride => prideColor,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
