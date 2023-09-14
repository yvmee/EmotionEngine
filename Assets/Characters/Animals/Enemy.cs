using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EmotionStimulus emotion;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get => health;
    }

    private void Defeated()
    {
        if (emotion != null) EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(emotion);
        Destroy(gameObject);
    }

    private float health = 1;

}
