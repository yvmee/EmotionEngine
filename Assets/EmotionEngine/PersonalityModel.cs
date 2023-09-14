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
    public class PersonalityModel : MonoBehaviour
    {
        [SerializeField] private float joyTendency;
        [SerializeField] private float sadnessTendency;
        [SerializeField] private float angerTendency;
        [SerializeField] private float fearTendency;
        [SerializeField] private float surpriseTendency;
        [SerializeField] private float prideTendency;

        [SerializeField] private List<Goal> goals;
        private Dictionary<string, EmotionVariable> _goals = new();

        [SerializeField] private float personalityImpactOnEmotion = 0.5f;

        private void Awake()
        {
            foreach (var goal in goals)
            {
                _goals.Add(goal.Tag, goal.EmotionVariable);
            }
        }

        public EmotionState ProcessEmotion(EmotionStimulus emotionStimulus)
        {
            EmotionState emotionState = ScriptableObject.CreateInstance<EmotionState>();
            emotionState.SetEmotionState(emotionStimulus.emotionState);

            // process goal alignment
            foreach (var goalAlignment in emotionStimulus.goalAlignments)
            {
                if (!_goals.ContainsKey(goalAlignment.tag)) break;
                
                var effect = _goals[goalAlignment.tag];
                // goal alignment leads to increased joy and slightly decreased negative emotions
                if (goalAlignment.aligns)
                {
                    emotionState.GetEmotion(effect.Type).Intensity += effect.Intensity;
                    emotionState.GetEmotion(EmotionType.Sadness).Intensity -= effect.Intensity/5;
                    emotionState.GetEmotion(EmotionType.Anger).Intensity -= effect.Intensity/5;
                    emotionState.GetEmotion(EmotionType.Fear).Intensity -= effect.Intensity/5;
                }
                // goal contradiction leads to increased negative emotions
                else
                {
                    emotionState.GetEmotion(EmotionType.Sadness).Intensity += effect.Intensity/5 * 2;
                    emotionState.GetEmotion(EmotionType.Anger).Intensity += effect.Intensity/5 * 2;
                    emotionState.GetEmotion(EmotionType.Fear).Intensity += effect.Intensity/5;
                }
            }
            
            // process emotion variables
            
            foreach (var emotionVariable in emotionState.GetEmotions())
            {
                float tendency = emotionVariable.Type switch
                {
                    EmotionType.Joy => joyTendency,
                    EmotionType.Sadness => sadnessTendency,
                    EmotionType.Anger => angerTendency,
                    EmotionType.Fear => fearTendency,
                    EmotionType.Surprise => surpriseTendency,
                    EmotionType.Pride => prideTendency,
                    _ => throw new ArgumentOutOfRangeException()
                };
                emotionVariable.Intensity += emotionVariable.Intensity * (tendency - 0.5f) * personalityImpactOnEmotion;
            }
            return emotionState;
        }

        public void SetPersonality(PersonalityEvent personalityEvent)
        {
            EmotionState emotionState = personalityEvent.tendencies;
            if (emotionState != null)
            {
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Joy).Intensity >= 0)
                    joyTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Joy).Intensity;
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Sadness).Intensity >= 0)
                    sadnessTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Sadness).Intensity;
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Anger).Intensity >= 0)
                    angerTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Anger).Intensity;
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Fear).Intensity >= 0)
                    fearTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Fear).Intensity;
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Surprise).Intensity >= 0)
                    surpriseTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Surprise).Intensity;
                if (personalityEvent.tendencies.GetEmotion(EmotionType.Pride).Intensity >= 0)
                    prideTendency = personalityEvent.tendencies.GetEmotion(EmotionType.Pride).Intensity;
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
