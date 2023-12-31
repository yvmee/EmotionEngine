using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.Serialization;

public class SwordAttack : MonoBehaviour
{

    public Collider2D swordCollider;
    
    public int damage = 3;

    [FormerlySerializedAs("emotionEvent")] public EmotionStimulus emotionStimulus;

    private Vector2 _rightAttackOffset;
    private void Start()
    {
        _rightAttackOffset = transform.localPosition;
    }


    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = _rightAttackOffset;
        SendEmotionEvent();
    }
    
    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(_rightAttackOffset.x * -1, _rightAttackOffset.y);
        SendEmotionEvent();
    }

    private void SendEmotionEvent()
    {
        var e = Instantiate(emotionStimulus);
        e.emotionState = Instantiate(emotionStimulus.emotionState);
        EmotionEngine.EmotionEngine.EmotionStimulusEvent.Invoke(e);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
