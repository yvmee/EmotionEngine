using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [Serializable]
    public class SpringNode
    {
        public Vector6 Position = Vector6.Zero;
        public Vector6 Velocity = Vector6.Zero;
        public double mass = 1.0d;

    }
}
