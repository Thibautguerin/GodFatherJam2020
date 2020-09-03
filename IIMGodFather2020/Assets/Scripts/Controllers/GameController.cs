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

    [Header("Canvas Conditions Victoire/Défaite")]
    public GameObject defeatPanel;
    public GameObject victoryPanel;

    [Header("Timer")]
    public float timer = 20;
    public bool isPlaying = false;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("Map")]
    public float radiusLimitMap = 10;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        isPlaying = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausePanel.gameObject.activeInHierarchy == false)
            {
                player.SetInPause(true);
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }else
            {
                player.SetInPause(false);
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (timer > 0 && isPlaying)
        {
            timer -= Time.deltaTime;
            UIController.instance.Timer(Mathf.RoundToInt(timer));
        }

        if (timer <= 0)
        {
            victoryPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Attack()
    {
        isPlaying = true;
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        defeatPanel.gameObject.SetActive(true);
    }
}
