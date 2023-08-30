using UnityEngine;

namespace EmotionEngine
{
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/PersonalityEvent")]
    public class PersonalityEvent : ScriptableObject
    {
        public DiscreteEmotion proneness;
        public Goal[] goals;
    }
}
