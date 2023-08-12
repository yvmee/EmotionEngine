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
    public class EmotionChangedEvent : UnityEvent <IEmotion> {}
    public class EmotionModel : MonoBehaviour
    {
        private IEmotion _emotionState;
        [SerializeField] private ScriptableObject emotionState;
        [SerializeField] private PersonalityModel personality;
        [SerializeField] private Mood mood;

        public static EmotionChangedEvent EmotionStateChanged = new(); 

        void Awake()
        {

            if (emotionState != null && emotionState is IEmotion iEmotion)
            {
                _emotionState = iEmotion;
            }
            else
            {
                throw new EmotionEngineException("Initial emotion state must implement IEmotion!");
            }
            EmotionStateChanged.AddListener(DebugEvent);
        }

        private void FixedUpdate()
        {
            _emotionState.Decay();
            EmotionStateChanged.Invoke(_emotionState);
        }

        public void RaiseHardEmotionEvent(IEmotion emotionEvent)
        {
            _emotionState = emotionEvent;
            EmotionStateChanged.Invoke(_emotionState);
        }
        
        public void RaiseSoftEmotionEvent(IEmotion emotionEvent)
        {
            Debug.Log("Emotion Event raised!");
            
            IEmotion emo = personality.ProcessEmotion(emotionEvent);
            _emotionState = mood.ProcessEmotion(emo);
            EmotionStateChanged.Invoke(_emotionState);
        }

        public IEmotion GetCurrentState()
        {
            return _emotionState;
        }

        public void DebugEvent(IEmotion e)
        {
            Debug.Log("Unity Event");
            DiscreteEmotion disEmo = (DiscreteEmotion)e;
            Debug.Log(disEmo.GetEmotion(EmotionType.Sadness));
        }
    }
}
