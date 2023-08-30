using System;
using UnityEngine;

namespace EmotionEngine
{
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/EmotionEvent")]
    public class EmotionEvent : ScriptableObject
    {
        public DiscreteEmotion emotion;
        public GoalAlignment[] goalAlignments;
    }

    [Serializable]
    public class GoalAlignment
    {
        public string tag;
        public bool alignment;
    }
}
