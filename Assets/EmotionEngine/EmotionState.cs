namespace EmotionEngine
{
    public static class EmotionState
    {
        // Representation of Emotion State: Discrete Emotions from 0 to 10
        /* Joy, Sadness, Anger, Fear, Surprise, Pride */
    
        private static float[] _emotionState = new float[6];

        public static void SetState(float[] emotion)
        {
            if (emotion.Length == _emotionState.Length)
                _emotionState = emotion;
        }

        public static float[] GetEmotionState()
        {
            return _emotionState;
        }
    }
}
