using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Vie")]
    public int maxTreeHealth = 100;
    public int currentHealth;

    public GameObject[] healthPoints;


    private void Start()
    {
        currentHealth = maxTreeHealth;

        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= 25 * (i + 1))
            {
                healthPoints[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TakeDamage(25);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RestoreLife(25);
        }
    }


    public void RestoreLife(int lifeUp)
    {
        currentHealth += lifeUp;
        if (currentHealth >= maxTreeHealth)
        {
            currentHealth = maxTreeHealth;
        }

        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= 25 * (i + 1))
            {
                healthPoints[i].SetActive(true);

            }
            else
            {
                healthPoints[i].SetActive(false);
            }

        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (currentHealth >= 25 * (i+1))
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
    }

    public void Die()
    {
        GameController.instance.Defeat();
        Destroy(gameObject);
    }
}
