using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmotionEngine;
using UnityEngine;

public class AnimalEmotionAsset : EmotionAsset
{
    [SerializeField] private float treshold = 0.2f;
    private Animal _animal;

    private void Start()
    {
        _animal = GetComponent<Animal>();
    }

    protected override void ChangeAsset(DiscreteEmotion emotion)
    {
        var strongest = new EmotionVariable(EmotionType.Joy);
        foreach (var e in emotion.GetEmotions().Where(e => e.Intensity >= strongest.Intensity))
        {
            strongest = e;
        }

        if (strongest.Intensity <= treshold) return;

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
