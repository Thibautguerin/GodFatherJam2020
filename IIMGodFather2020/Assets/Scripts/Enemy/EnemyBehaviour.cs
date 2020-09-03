using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public float speedMovement;

    [Header("Attack")]
    public float attackRate;
    public int attackDommage;

    private void Start()
    {
        GameController.instance.enemyController.enemyList.Add(this);
        //Init();
    }

    public virtual void Init()
    {

    }

    public virtual void Attack()
    {
        StartCoroutine(AttackAction());
    }

    private IEnumerator AttackAction()
    {
        yield return new WaitForSeconds(attackRate);
        GameController.instance.tree?.TakeDamage(attackDommage);
        StartCoroutine(AttackAction());
    }

    public abstract void Movement();

    public virtual void Die(PlayerController collide)
    {
        GameController.instance.enemyController.enemyList.Remove(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Stop Move");
    }
}
