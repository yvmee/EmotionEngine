using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EmotionEngine
{
    [Serializable]
    public class Spring
    {
        public double s; // stiffness
        public double dr; // restlength
        public SpringNode node0;
        public SpringNode node1;
        public bool freeze;
    }
}
