using UnityEngine;

namespace EmotionEngine
{
    public class EmotionModel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EmotionState.SetState(new float[]{0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f});
        }
    
        public void RaiseHardEmotionEvent(EmotionEvent emotionEvent)
        {
            EmotionState.SetState(emotionEvent.GetImpulse());
        }
        
        public void RaiseSoftEmotionEvent(EmotionEvent emotionEvent)
        {
            //
        }
    }
}
