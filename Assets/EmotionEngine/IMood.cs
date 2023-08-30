namespace EmotionEngine
{
    public interface IMood
    {
        public DiscreteEmotion ProcessEmotion(DiscreteEmotion emotionEvent);
        public DiscreteEmotion GetCurrentMood();
    }
}
