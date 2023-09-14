using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    public class MoodModel : MonoBehaviour
    {
        [SerializeField] private EmotionState currentMood;

        [SerializeField] private float moodImpactOnEmotion = 0.7f;
        [SerializeField] private float emotionImpactOnMood = 0.8f;

        private void Awake()
        {
            if (currentMood == null) currentMood = ScriptableObject.CreateInstance<EmotionState>();
        }

        public EmotionState ProcessEmotion(EmotionState emotionStateEvent)
        {
            foreach (var emotionVariable in emotionStateEvent.GetEmotions())
            {

                emotionVariable.Intensity += 
                    (currentMood.GetEmotion(emotionVariable.Type).Intensity - emotionVariable.Intensity) 
                    * moodImpactOnEmotion;
                currentMood.GetEmotion(emotionVariable.Type).Intensity += 
                    (emotionVariable.Intensity - currentMood.GetEmotion(emotionVariable.Type).Intensity) 
                    * emotionImpactOnMood;
            }
            
            return emotionStateEvent;
        }

        public EmotionState GetCurrentMood()
        {
            return currentMood;
        }
    }
}
