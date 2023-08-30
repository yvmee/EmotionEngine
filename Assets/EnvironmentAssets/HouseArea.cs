using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponentInChildren<Rain>().EnterHouse();
            Debug.Log("House trigger entered");
        }
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<Rain>().ExitHouse();
            Debug.Log("House trigger exited");
        }
            
    }
}
