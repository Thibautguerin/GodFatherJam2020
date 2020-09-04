using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public int speed;
    public int stopPosition;
    [Header("Menu")]
    public string returnMainMenu;

    private bool sceneChange = false;

    private void Start()
    {
        TransitionController.instance.FadeOut();
    }
    private void Update()
    {
        if (transform.position.y < stopPosition)
            transform.Translate(0, speed * Time.deltaTime, 0);
        else if (!sceneChange)
        {
            sceneChange = true;
            Debug.Log("Retour au menu");
            TransitionController.instance.FadeIn(()=> SceneManager.LoadScene(returnMainMenu));
        }
    }
}
