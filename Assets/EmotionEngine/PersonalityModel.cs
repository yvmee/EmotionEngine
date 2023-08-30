using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [Serializable]
    public class Goal
    {
        [SerializeField] private string tag;
        [SerializeField] private EmotionVariable emotionVariableImpact;

        public string Tag => tag;
        public EmotionVariable EmotionVariable => emotionVariableImpact;
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

        [SerializeField] private List<Goal> goals;
        private Dictionary<string, EmotionVariable> _goals = new Dictionary<string, EmotionVariable>();

        [SerializeField] private float magicnumber = 0.5f;

        private void Awake()
        {
            foreach (var goal in goals)
            {
                _goals.Add(goal.Tag, goal.EmotionVariable);
            }
        }

        public DiscreteEmotion ProcessEmotion(EmotionEvent emotionEvent)
        {
            var emotion = emotionEvent.emotion;
            
            // process Goals
            foreach (var goalAlignment in emotionEvent.goalAlignments)
            {
                if (!_goals.ContainsKey(goalAlignment.tag)) break;
                
                var effect = _goals[goalAlignment.tag];
                if (goalAlignment.alignment)
                {
                    emotion.GetEmotion(effect.Type).Intensity += effect.Intensity;
                    emotion.GetEmotion(EmotionType.Sadness).Intensity -= effect.Intensity/5 * 2;
                    emotion.GetEmotion(EmotionType.Anger).Intensity -= effect.Intensity/5 * 2;
                    emotion.GetEmotion(EmotionType.Fear).Intensity -= effect.Intensity/5;
                }
                
                else
                {
                    emotion.GetEmotion(EmotionType.Sadness).Intensity += effect.Intensity/5 * 2;
                    emotion.GetEmotion(EmotionType.Anger).Intensity += effect.Intensity/5 * 2;
                    emotion.GetEmotion(EmotionType.Fear).Intensity += effect.Intensity/5;
                }
            }
            
            // process emotion variables
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
            
            
            return emotionEvent.emotion;
        }

        public void SetPersonality(PersonalityEvent personalityEvent)
        {
            DiscreteEmotion emotion = personalityEvent.proneness;
            if (emotion != null)
            {
                if (personalityEvent.proneness.GetEmotion(EmotionType.Joy).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Joy).Intensity) /
                                   2;
                if (personalityEvent.proneness.GetEmotion(EmotionType.Sadness).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Sadness).Intensity) /
                                   2;
                if (personalityEvent.proneness.GetEmotion(EmotionType.Anger).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Anger).Intensity) /
                                   2;
                if (personalityEvent.proneness.GetEmotion(EmotionType.Fear).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Fear).Intensity) /
                                   2;
                if (personalityEvent.proneness.GetEmotion(EmotionType.Surprise).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Surprise).Intensity) /
                                   2;
                if (personalityEvent.proneness.GetEmotion(EmotionType.Pride).Intensity > 0)
                    joyProneness = (joyProneness + personalityEvent.proneness.GetEmotion(EmotionType.Pride).Intensity) /
                                   2;
            }

            if (personalityEvent.goals != null)
            {
                foreach (var goal in personalityEvent.goals)
                {
                    _goals.Add(goal.Tag, goal.EmotionVariable);
                    Debug.Log("Goal added: " + goal.Tag);
                }
            }
            
        }
    }
}
