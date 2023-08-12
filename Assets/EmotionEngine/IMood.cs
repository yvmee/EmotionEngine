namespace EmotionEngine
{
    public interface IMood
    {
        public IEmotion ProcessEmotion(IEmotion emotionEvent);
        public IEmotion GetCurrentMood();
    }
}
