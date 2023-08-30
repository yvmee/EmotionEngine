using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSwitch : MonoBehaviour
{
    public Vector3 position;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger entered: " + col.gameObject.name);
        if (col.gameObject.CompareTag("Player"))
            Teleport(col.gameObject);
    }

    public void Teleport(GameObject colGameObject)
    {
        colGameObject.transform.position = position;
    }
}
