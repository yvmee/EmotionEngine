using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class StoryBeat : MonoBehaviour
{
    [SerializeField] private EmotionStimulus emotionStimulus;
    // Start is called before the first frame update
    void Start()
    {
        EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(emotionStimulus);
    }
}
