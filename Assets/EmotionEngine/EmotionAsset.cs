using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmotionEngine
{
    public abstract class EmotionAsset : MonoBehaviour
    {
        protected void Awake()
        {
            EmotionModel.EmotionStateChanged.AddListener(ChangeAsset);
        }

        protected abstract void ChangeAsset(DiscreteEmotion emotion);

    }
}

