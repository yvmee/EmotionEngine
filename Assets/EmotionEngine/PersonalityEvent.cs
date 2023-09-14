using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [CreateAssetMenu(menuName = "Scriptable Objects/EmotionEngine/PersonalityEvent")]
    public class PersonalityEvent : ScriptableObject
    {
        public EmotionState tendencies;
        public Goal[] goals;
    }
}
