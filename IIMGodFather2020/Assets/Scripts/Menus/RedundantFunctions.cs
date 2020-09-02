using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedundantFunctions : MonoBehaviour
{
    [Header("Menu")]
    public string returnMainMenu;

    public void ReturnMainMenu()
    {
        Debug.Log("Retour au menu");
        SceneManager.LoadScene(returnMainMenu);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
