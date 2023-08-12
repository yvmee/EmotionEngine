using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    public class Mood : MonoBehaviour, IMood
    {
        [SerializeField] private DiscreteEmotion currentMood;

        [SerializeField] private float impactOnEmotion = 0.1f;
        [SerializeField] private float impactOnMood = 0.5f;

        private void Awake()
        {
            if (currentMood == null) currentMood = ScriptableObject.CreateInstance<DiscreteEmotion>();
        }

        public IEmotion ProcessEmotion(IEmotion emotionEvent)
        {
            Debug.Log("Calculating Mood!");

            var discreteEmotion = (DiscreteEmotion)emotionEvent;
            foreach (var e in discreteEmotion.GetEmotions())
            {

                e.Intensity += (currentMood.GetEmotion(e.Type).Intensity - e.Intensity) * impactOnEmotion;
                currentMood.GetEmotion(e.Type).Intensity += (e.Intensity - currentMood.GetEmotion(e.Type).Intensity) * impactOnMood;
            }
            
            return emotionEvent;
        }

        public IEmotion GetCurrentMood()
        {
            return currentMood;
        }
    }
}
