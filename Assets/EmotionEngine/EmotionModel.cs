using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    public class EmotionEngineException : System.Exception
    {
        public EmotionEngineException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class EmotionChangedEvent : UnityEvent <DiscreteEmotion> {}
    [Serializable]
    public class EmotionStimulus : UnityEvent <EmotionEvent, bool> {}
    [Serializable]
    public class SetPersonality : UnityEvent <PersonalityEvent> {}
    public class EmotionModel : MonoBehaviour
    {
        private DiscreteEmotion _emotionState;
        [SerializeField] private PersonalityModel personality;
        [SerializeField] private MoodModel moodModel;
        
        public static EmotionChangedEvent EmotionStateChanged = new(); 
        public static EmotionStimulus EmotionStimulusEvent = new();
        public static SetPersonality SetPersonalityEvent = new();
    
        void Awake()
        {
            _emotionState = ScriptableObject.CreateInstance<DiscreteEmotion>();
            EmotionStimulusEvent.AddListener(EmotionStimulus);
            SetPersonalityEvent.AddListener(ProcessPersonalityEvent);
        }

        private void FixedUpdate()
        {
            _emotionState.Decay();
            EmotionStateChanged.Invoke(_emotionState);
        }

        public void ProcessPersonalityEvent(PersonalityEvent personalityEvent)
        {
            personality.SetPersonality(personalityEvent);
        }

        public void EmotionStimulus(EmotionEvent emotionStimulus, bool hard)
        {
            Debug.Log("Emotion Event raised: " + emotionStimulus.emotion.name);
            Debug.Log(emotionStimulus.emotion.GetEmotions());
            
            if (hard) RaiseHardEmotionEvent(emotionStimulus);
            else RaiseSoftEmotionEvent(emotionStimulus);
        }

        public void RaiseHardEmotionEvent(EmotionEvent emotionEvent)
        {
            foreach (var e in _emotionState.GetEmotions())
            {
                e.Intensity = emotionEvent.emotion.GetEmotion(e.Type).Intensity;
            }
            EmotionStateChanged.Invoke(_emotionState);
        }
        
        public void RaiseSoftEmotionEvent(EmotionEvent emotionEvent)
        {
            DiscreteEmotion emotion = personality.ProcessEmotion(emotionEvent);
            emotion = moodModel.ProcessEmotion(emotion);
            SetNewState(emotion);
            EmotionStateChanged.Invoke(_emotionState);
        }

        private void SetNewState(DiscreteEmotion emotion)
        {
            _emotionState.GetEmotion(EmotionType.Joy).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Joy).Intensity + emotion.GetEmotion(EmotionType.Joy).Intensity), 0f), 1f);
            _emotionState.GetEmotion(EmotionType.Sadness).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Sadness).Intensity + emotion.GetEmotion(EmotionType.Sadness).Intensity), 0f), 1f);
            _emotionState.GetEmotion(EmotionType.Anger).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Anger).Intensity + emotion.GetEmotion(EmotionType.Anger).Intensity), 0f), 1f);
            _emotionState.GetEmotion(EmotionType.Fear).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Fear).Intensity + emotion.GetEmotion(EmotionType.Fear).Intensity), 0f), 1f);
            _emotionState.GetEmotion(EmotionType.Surprise).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Surprise).Intensity + emotion.GetEmotion(EmotionType.Surprise).Intensity), 0f), 1f);
            _emotionState.GetEmotion(EmotionType.Pride).Intensity =
                Math.Min(Math.Max((_emotionState.GetEmotion(EmotionType.Pride).Intensity + emotion.GetEmotion(EmotionType.Pride).Intensity), 0f), 1f);
        }

        public void DebugEvent(DiscreteEmotion e)
        {
            Debug.Log(e.GetEmotion(EmotionType.Sadness));
        }
    }
}
