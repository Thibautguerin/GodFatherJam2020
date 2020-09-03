using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Vie")]
    public int maxTreeHealth = 100;
    public int currentHealth;

    public GameObject[] healthPoints;

    public int limitDangerMusic = 15;

    private void Start()
    {
        currentHealth = maxTreeHealth;
        GameController.instance.tree = this;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= maxTreeHealth / healthPoints.Length * (i + 1))
            {
                healthPoints[i].SetActive(true);
            }
        }
    }

    public void RestoreLife(int lifeUp)
    {
        currentHealth += lifeUp;
        SoundEffectsController.instance.MakeSapTreeHealSound();
        if (currentHealth >= maxTreeHealth)
        {
            currentHealth = maxTreeHealth;
        }

        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= maxTreeHealth / healthPoints.Length * (i + 1))
            {
                healthPoints[i].SetActive(true);
            }
            else
            {
                healthPoints[i].SetActive(false);
            }

        }
        if (currentHealth > limitDangerMusic)
        {
            SoundManager.instance.PlayNormalMusic();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= maxTreeHealth / healthPoints.Length * (i+1))
            {
                healthPoints[i].SetActive(true);

            }else
            {
                healthPoints[i].SetActive(false);
            }    
        }

        if (currentHealth <= 0)
        {  
            Die();
        }
        else
        {
            if (currentHealth < limitDangerMusic)
            {
                SoundManager.instance.PlayDangerMusic();
            }
        }
    }

    public void Die()
    {
        GameController.instance.Defeat();
        Destroy(gameObject);
    }
}
