using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


namespace EmotionEngine
{
    public enum EmotionType
    {
        Joy,
        Sadness,
        Anger,
        Fear,
        Surprise,
        Pride
    }
    
    [Serializable]
    public class Emotion
    {
        public Emotion(EmotionType type)
        {
            this.type = type;
        }
        
        //[SerializeField]
        private EmotionType type;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float intensity;

        public EmotionType Type => type;
        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }
    }
    
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/DiscreteEmotion")]
    public class DiscreteEmotion : ScriptableObject, IEmotion
    {
        [field: SerializeField] private Emotion Joy = new Emotion(EmotionType.Joy);
        [field: SerializeField] private Emotion Sadness = new Emotion(EmotionType.Sadness);
        [field: SerializeField] private Emotion Anger = new Emotion(EmotionType.Anger);
        [field: SerializeField] private Emotion Fear = new Emotion(EmotionType.Fear);
        [field: SerializeField] private Emotion Surprise = new Emotion(EmotionType.Surprise);
        [field: SerializeField] private Emotion Pride = new Emotion(EmotionType.Pride);

        [SerializeField] private float decayingFactor = 0.00001f;

        public List<Emotion> GetEmotions()
        {
            return new List<Emotion>() { Joy, Sadness, Anger, Fear, Surprise, Pride };
        }

        public Emotion GetEmotion(EmotionType type)
        {
            switch (type)
            {
                case EmotionType.Joy:
                    return Joy;
                case EmotionType.Sadness:
                    return Sadness;
                case EmotionType.Anger:
                    return Anger;
                case EmotionType.Fear:
                    return Fear;
                case EmotionType.Surprise:
                    return Surprise;
                case EmotionType.Pride:
                    return Pride;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, type + " is not a valid Emotion type!");
            }
        }

        public void Decay()
        {
            foreach (var e in GetEmotions())
            {
                e.Intensity -= e.Intensity * decayingFactor;
            }
        }
    }
}
