using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{

    [Header("Menu")]
    public string nextLevel;


    public void NextLevel()
    {
        Debug.Log("Niveau suivant !");
        SceneManager.LoadScene(nextLevel);
    }

    
}
