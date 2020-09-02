using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Controllers")]
    public PlayerController player;
    public TreeController tree;
    public EnemyController enemyController;

    // Canvas de Défaite
    public Canvas defeatCanvas;
    public Canvas victoryCanvas;

    // Timer
    public float timer = 100;
    public bool isPlaying = false;


    
    [Header("Map")]
    public float radiusLimitMap = 10;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Update()
    {
        if (timer > 0 && isPlaying)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            victoryCanvas.gameObject.SetActive(true);
        }
    }

    public void Attack()
    {
        isPlaying = true;
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        defeatCanvas.gameObject.SetActive(true);
    }
}
