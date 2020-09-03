using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{ 

    [Header("Canvas")]
    public GameObject pausePanel;
    public PlayerController player;  

    public void ResumeGame()
    {
        player.SetInPause(false);
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    
}
