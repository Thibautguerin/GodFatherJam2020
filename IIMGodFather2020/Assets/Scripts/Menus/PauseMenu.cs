using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{ 

    [Header("Canvas")]
    public GameObject pausePanel;

    

    public void ResumeGame()
    {
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    
}
