using UnityEngine;

namespace EmotionEngine
{
    public class EmotionEvent : MonoBehaviour
    {
        [SerializeField] private float[] emotionImpulse;

        public float[] GetImpulse()
        {
            return emotionImpulse;
        }
    }
}
