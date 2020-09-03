using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackEnemy : EnemyBehaviour
{
    private bool _sens = false;
    private bool _isAttack = false;
    public Animator animator;

    private void FixedUpdate()
    {
        if (!_isAttack)
            transform.position += new Vector3(_sens ? -speedMovement : speedMovement, 0, 0) * Time.deltaTime;
    }

    public override void Init()
    {
        base.Init();
        _isAttack = false;
        if (GameController.instance.tree.transform.position.x < transform.position.x)
        {
            _sens = true;
        }
        else
        {
            _sens = false;
        }
    }

    public override void Die(PlayerController collide, bool trigger)
    {
        if (collide.GetGravity() > 0)
        {
            base.Die(collide, trigger);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            _isAttack = true;
            Attack();
        }
    }

    public override void Attack()
    {
        base.Attack();
        animator.SetBool("Attack", true);

    }
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y > transform.position.y)
            {
                Die(GameController.instance.player, false);
            }
        }
    }
}
