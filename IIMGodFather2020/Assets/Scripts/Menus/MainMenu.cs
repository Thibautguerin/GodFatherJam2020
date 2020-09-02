using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [Header("Options")]
    public AudioMixer audioMixer;

    [Header("Menu")]
    public string nameLevel;
    
    
    
    public void SetMenuVolume(float volume)
    {
        audioMixer.SetFloat("MainMenuVolume", volume);
    }
    
    public void PlayGame()
    {
        Debug.Log("Play !");
        SceneManager.LoadScene(nameLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }
}
