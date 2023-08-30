using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

namespace EmotionEngine
{
    public interface IPersonality
    {
        public DiscreteEmotion ProcessEmotion(EmotionEvent emotionEvent);
        public void SetPersonality(PersonalityEvent personalityEvent);
    }
}

