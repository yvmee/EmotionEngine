using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool Paused;

    private void Start()
    {
        FindObjectOfType<PlayerInputController>().pauseMenu = gameObject;
        Unpause();
    }

    public void Resume()
    {
        Unpause();
    }

    public void ToMenu()
    {
        Unpause();
        SceneLoader.LoadScene("StartScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Unpause()
    {
        gameObject.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
    }
}
