using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public float speedMovement;

    [Header("Attack")]
    public float attackRate;
    public float attackDommage;

    public virtual void Init()
    {

    }

    public abstract void Attack();

    public abstract void Movement();

    public abstract void Die();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Stop Move");
    }
}
