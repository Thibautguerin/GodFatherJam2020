using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [Header("Vie")]
    public int maxTreeHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxTreeHealth;
        GameController.instance.tree = this;
    }


    public void RestoreLife(int lifeUp)
    {
        currentHealth += lifeUp;
        if (currentHealth >= maxTreeHealth)
        {
            currentHealth = maxTreeHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

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
