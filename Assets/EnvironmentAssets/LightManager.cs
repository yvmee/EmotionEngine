using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightManager : EmotionAsset
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
            foreach (var light2D in gameObject.GetComponentsInChildren<Light2D>())
                light2D.color = neutralColor;
            return;
        }

        foreach (var light2D in gameObject.GetComponentsInChildren<Light2D>())
        {
            light2D.color = strongest.Type switch
            {
                EmotionType.Joy => joyColor,
                EmotionType.Sadness => sadnessColor,
                EmotionType.Anger => angerColor,
                EmotionType.Fear => fearColor,
                EmotionType.Surprise => Random.ColorHSV(),
                EmotionType.Pride => prideColor,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
