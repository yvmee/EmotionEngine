using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

namespace EmotionEngine
{
    public interface IPersonality
    {
        public IEmotion ProcessEmotion(IEmotion emotionEvent);
    }
}

