using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [Serializable]
    public class Goal
    {
        [SerializeField]
        private string tag;
        [SerializeField]
        private Emotion emotionImpact;

        public string Tag => tag;
        public Emotion Emotion => emotionImpact;
    }
    public class PersonalityModel : MonoBehaviour, IPersonality
    {
        // Proneness to Emotion
        [SerializeField] private float joyProneness;
        [SerializeField] private float sadnessProneness;
        [SerializeField] private float angerProneness;
        [SerializeField] private float fearProneness;
        [SerializeField] private float surpriseProneness;
        [SerializeField] private float prideProneness;

        [SerializeField] private Goal[] goals;

        [SerializeField] private float magicnumber = 0.5f;
        
        public IEmotion ProcessEmotion(IEmotion emotionEvent)
        {
            Debug.Log("Personality Calculation");
            var emotion = (DiscreteEmotion)emotionEvent;
            foreach (var e in emotion.GetEmotions())
            {
                float proneness = e.Type switch
                {
                    EmotionType.Joy => joyProneness,
                    EmotionType.Sadness => sadnessProneness,
                    EmotionType.Anger => angerProneness,
                    EmotionType.Fear => fearProneness,
                    EmotionType.Surprise => surpriseProneness,
                    EmotionType.Pride => prideProneness,
                    _ => throw new ArgumentOutOfRangeException()
                };
                e.Intensity += e.Intensity * proneness * magicnumber;
            }
            
            
            return emotionEvent;
        }
    }
}
