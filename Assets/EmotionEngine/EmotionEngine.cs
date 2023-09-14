using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [Serializable] public class EmotionChangedEvent : UnityEvent <EmotionState> {}
    [Serializable] public class EmotionStimulusEvent : UnityEvent <EmotionStimulus> {}
    [Serializable] public class ChangePersonality : UnityEvent <PersonalityEvent> {}
    public class EmotionEngine : MonoBehaviour
    {
        [SerializeField] private EmotionState emotionStateState;
        [SerializeField] private PersonalityModel personality;
        [SerializeField] private MoodModel moodModel;
        
        [SerializeField] private float decayingFactor = 0.00001f;
        
        public static EmotionChangedEvent EmotionStateChanged = new(); 
        public static EmotionStimulusEvent EmotionStimulusEvent = new();
        public static ChangePersonality ChangePersonalityEvent = new();
    
        void Awake()
        {
            if (emotionStateState == null)
                emotionStateState = ScriptableObject.CreateInstance<EmotionState>();
            EmotionStimulusEvent.AddListener(EmotionStimulus);
            ChangePersonalityEvent.AddListener(ProcessPersonalityEvent);
        }

        private void FixedUpdate()
        {
            Decay();
        }

        public void ProcessPersonalityEvent(PersonalityEvent personalityEvent)
        {
            personality.SetPersonality(personalityEvent);
        }

        public void EmotionStimulus(EmotionStimulus emotionStimulus)
        {
            Debug.Log("Emotion Event raised: " + emotionStimulus.emotionState.name);

            if (emotionStimulus.storyEvent) StoryEmotionEvent(emotionStimulus);
            else EmotionEvent(emotionStimulus);
        }

        public void StoryEmotionEvent(EmotionStimulus emotionStimulus)
        {
            foreach (var e in emotionStateState.GetEmotions())
            {
                e.Intensity = emotionStimulus.emotionState.GetEmotion(e.Type).Intensity;
            }
            EmotionStateChanged.Invoke(emotionStateState);
        }
        
        public void EmotionEvent(EmotionStimulus emotionStimulus)
        {
            EmotionState emotionState = personality.ProcessEmotion(emotionStimulus);
            emotionState = moodModel.ProcessEmotion(emotionState);
            SetNewState(emotionState);
            EmotionStateChanged.Invoke(emotionStateState);
        }

        private void SetNewState(EmotionState emotionState)
        {
            /*
            emotionStateState.GetEmotion(EmotionType.Joy).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Joy).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Sadness).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Sadness).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Anger).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Anger).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Fear).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Fear).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Surprise).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Surprise).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Pride).Intensity =
                Math.Min(Math.Max((emotionState.GetEmotion(EmotionType.Pride).Intensity), 0f), 1f); */
            
            emotionStateState.GetEmotion(EmotionType.Joy).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Joy).Intensity + emotionState.GetEmotion(EmotionType.Joy).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Sadness).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Sadness).Intensity + emotionState.GetEmotion(EmotionType.Sadness).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Anger).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Anger).Intensity + emotionState.GetEmotion(EmotionType.Anger).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Fear).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Fear).Intensity + emotionState.GetEmotion(EmotionType.Fear).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Surprise).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Surprise).Intensity + emotionState.GetEmotion(EmotionType.Surprise).Intensity), 0f), 1f);
            emotionStateState.GetEmotion(EmotionType.Pride).Intensity =
                Math.Min(Math.Max((emotionStateState.GetEmotion(EmotionType.Pride).Intensity + emotionState.GetEmotion(EmotionType.Pride).Intensity), 0f), 1f); 
        }
        
        private void Decay()
        {
            foreach (var emotionVariable in emotionStateState.GetEmotions())
            {
                emotionVariable.Intensity -= emotionVariable.Intensity * decayingFactor;
            }
            EmotionStateChanged.Invoke(emotionStateState);
        }
    }
}
