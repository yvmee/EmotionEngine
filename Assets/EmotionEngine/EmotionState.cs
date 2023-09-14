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
        
        [SerializeField] private EmotionType type;
        [SerializeField] [Range(0.0f, 1.0f)] private float intensity;

        public EmotionType Type => type;
        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }
    }
    
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/EmotionState")]
    public class EmotionState : ScriptableObject
    {
        [field: SerializeField] private EmotionVariable Joy = new(EmotionType.Joy);
        [field: SerializeField] private EmotionVariable Sadness = new(EmotionType.Sadness);
        [field: SerializeField] private EmotionVariable Anger = new(EmotionType.Anger);
        [field: SerializeField] private EmotionVariable Fear = new(EmotionType.Fear);
        [field: SerializeField] private EmotionVariable Surprise = new(EmotionType.Surprise);
        [field: SerializeField] private EmotionVariable Pride = new(EmotionType.Pride);

        public void SetEmotionState(EmotionState toCopy)
        {
            foreach (var emotionVariable in GetEmotions())
            {
                emotionVariable.Intensity = toCopy.GetEmotion(emotionVariable.Type).Intensity;
            }
        }
        
        public List<EmotionVariable> GetEmotions()
        {
            return new List<EmotionVariable>() { Joy, Sadness, Anger, Fear, Surprise, Pride };
        }

        public EmotionVariable GetEmotion(EmotionType type)
        {
            return type switch
            {
                EmotionType.Joy => Joy,
                EmotionType.Sadness => Sadness,
                EmotionType.Anger => Anger,
                EmotionType.Fear => Fear,
                EmotionType.Surprise => Surprise,
                EmotionType.Pride => Pride,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, type + " is not a valid Emotion type!")
            };
        }
    }
}
