using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/EmotionStimulus")]
    public class EmotionStimulus : ScriptableObject
    {
        public EmotionState emotionState;
        public GoalAlignment[] goalAlignments;
        public bool storyEvent = false;
    }

    [Serializable]
    public class GoalAlignment
    {
        public string tag;
        public bool aligns; // true -> aligns with goal, false -> contradicts goal
    }
}
