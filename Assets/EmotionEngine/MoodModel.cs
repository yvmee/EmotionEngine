using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    public class MoodModel : MonoBehaviour, IMood
    {
        [SerializeField] private DiscreteEmotion currentMood;

        [SerializeField] private float impactOnEmotion = 0.1f;
        [SerializeField] private float impactOnMood = 0.5f;

        private void Awake()
        {
            if (currentMood == null) currentMood = ScriptableObject.CreateInstance<DiscreteEmotion>();
        }

        public DiscreteEmotion ProcessEmotion(DiscreteEmotion emotionEvent)
        {
            foreach (var e in emotionEvent.GetEmotions())
            {

                e.Intensity += (currentMood.GetEmotion(e.Type).Intensity - e.Intensity) * impactOnEmotion;
                currentMood.GetEmotion(e.Type).Intensity += (e.Intensity - currentMood.GetEmotion(e.Type).Intensity) * impactOnMood;
            }
            
            return emotionEvent;
        }

        public DiscreteEmotion GetCurrentMood()
        {
            return currentMood;
        }
    }
}
