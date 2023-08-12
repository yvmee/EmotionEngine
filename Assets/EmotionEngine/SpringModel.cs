using UnityEngine;

namespace EmotionEngine
{
    
    public class SpringNode
    {
        public float[] Pos = new []{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
        public float Mass = 1.0f;
    }
    public class Spring
    {
        public float L;
        public float K;
        public SpringNode node0;
        public SpringNode node1;
    }
    public class SpringModel : MonoBehaviour, IPersonality, IMood, IEmotion
    {
        // Fixed Springnode:Story ~ Spring 0 ~ Springnode:Personality ~ Spring 1 ~ Springnode:Emotion

        IEmotion IPersonality.ProcessEmotion(IEmotion emotionEvent)
        {
            throw new System.NotImplementedException();
        }

        IEmotion IMood.ProcessEmotion(IEmotion emotionEvent)
        {
            throw new System.NotImplementedException();
        }
        
        public IEmotion GetCurrentMood()
        {
            throw new System.NotImplementedException();
        }

        public void Decay()
        {
            throw new System.NotImplementedException();
        }
    }
}
