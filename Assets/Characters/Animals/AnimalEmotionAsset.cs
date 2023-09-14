using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmotionEngine;
using UnityEngine;

public class AnimalEmotionAsset : EmotionAsset
{

    private Animal _animal;
    
    private void Start()
    {
        _animal = GetComponent<Animal>();
    }

    protected override void ChangeAsset(EmotionState emotionState)
    {
        if (_animal == null) return;
        
        var strongest = new EmotionVariable(EmotionType.Joy);
        foreach (var e in emotionState.GetEmotions().Where(e => e.Intensity >= strongest.Intensity))
        {
            strongest = e;
        }

        if (strongest.Intensity <= threshold) return;

        switch (strongest.Type)
        {
            case EmotionType.Joy:
                _animal.SetAnimalState(Animal.AnimalState.Normal);
                break;
            case EmotionType.Sadness:
                _animal.SetAnimalState(Animal.AnimalState.Running);
                break;
            case EmotionType.Anger:
                _animal.SetAnimalState(Animal.AnimalState.Normal);
                break;
            case EmotionType.Fear:
                _animal.SetAnimalState(Animal.AnimalState.Running);
                break;
            case EmotionType.Surprise:
                _animal.SetAnimalState(Animal.AnimalState.Normal);
                break;
            case EmotionType.Pride:
                _animal.SetAnimalState(Animal.AnimalState.Normal);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
}
