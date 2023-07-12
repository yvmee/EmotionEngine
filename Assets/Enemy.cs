using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        Destroy(gameObject);
    }

    private float health = 1;

}
