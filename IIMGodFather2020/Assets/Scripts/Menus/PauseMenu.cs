using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu")]
    public string menuLevel;
    public string nameLevel;

    [Header("Canvas")]
    public Canvas canvas;

    public void Quit()
    {
        Debug.Log("Retour au menu principal");
        SceneManager.LoadScene(menuLevel);
    }

    public void ResumeGame()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(nameLevel);
        Time.timeScale = 1;
    }
}
