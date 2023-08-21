using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DiscreteEmotion emotion;
    public bool hardEmotion;
    private bool _dead;
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
        _dead = true;
        if (emotion != null) EmotionModel.EmotionPulseSend.Invoke(emotion, hardEmotion);
        Destroy(gameObject);
    }

    private float health = 1;

}
