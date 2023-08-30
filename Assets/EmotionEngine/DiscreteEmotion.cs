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
    public class EmotionVariable
    {
        public EmotionVariable(EmotionType type)
        {
            this.type = type;
        }
        
        [SerializeField]
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
    public class DiscreteEmotion : ScriptableObject
    {
        [field: SerializeField] private EmotionVariable Joy = new EmotionVariable(EmotionType.Joy);
        [field: SerializeField] private EmotionVariable Sadness = new EmotionVariable(EmotionType.Sadness);
        [field: SerializeField] private EmotionVariable Anger = new EmotionVariable(EmotionType.Anger);
        [field: SerializeField] private EmotionVariable Fear = new EmotionVariable(EmotionType.Fear);
        [field: SerializeField] private EmotionVariable Surprise = new EmotionVariable(EmotionType.Surprise);
        [field: SerializeField] private EmotionVariable Pride = new EmotionVariable(EmotionType.Pride);

        [SerializeField] private float decayingFactor = 0.00001f;
        
        public void Decay()
        {
            foreach (var e in GetEmotions())
            {
                e.Intensity -= e.Intensity * decayingFactor;
            }
        }
        
        
        
        

        public List<EmotionVariable> GetEmotions()
        {
            return new List<EmotionVariable>() { Joy, Sadness, Anger, Fear, Surprise, Pride };
        }

        public EmotionVariable GetEmotion(EmotionType type)
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

        
    }
}
