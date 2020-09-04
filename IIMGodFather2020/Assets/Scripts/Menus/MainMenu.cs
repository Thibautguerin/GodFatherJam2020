﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Options")]
    public AudioMixer audioMixer;

    [Header("Menu")]
    public string nameLevel;

    public Texture2D cursorTexture = null;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        TransitionController.instance?.FadeOut();
        Time.timeScale = 1;
    }

    public void SetMenuVolume(float volume)
    {
        audioMixer.SetFloat("MainMenuVolume", volume);
    }
    
    public void PlayGame()
    {
        Debug.Log("Play !");
        Time.timeScale = 1;
        TransitionController.instance?.FadeIn(()=> SceneManager.LoadScene(nameLevel));
    }

    public void QuitGame()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }
}
