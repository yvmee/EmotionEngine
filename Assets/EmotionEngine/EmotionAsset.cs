using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmotionEngine
{
    public abstract class EmotionAsset : MonoBehaviour
    {
        public float threshold;
        protected void Awake()
        {
            EmotionEngine.EmotionStateChanged.AddListener(ChangeAsset);
        }

        protected abstract void ChangeAsset(EmotionState emotionState);

    }
}

