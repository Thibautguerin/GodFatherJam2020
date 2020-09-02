using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementEnemy : EnemyBehaviour
{
    private bool _sens = false;
    private bool _isAttack = false;

    private void FixedUpdate()
    {
        if(!_isAttack)
            transform.position += new Vector3(_sens?-speedMovement: speedMovement, 0,0) * Time.deltaTime;                                                                                                                                                         
    }

    public override void Init()
    {
        base.Init();
        if (GameController.instance.tree.transform.position.x < transform.position.x)
        {
            _sens = true;
        }
        else
        {
            _sens = false;
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackAction());
    }

    //TODO
    private IEnumerator AttackAction()
    {
        yield return new WaitForSeconds(attackRate);
        //Dmg to tree
        StartCoroutine(AttackAction());
    }

    public override void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tree")
        {
            _isAttack = true;
            Attack();
        }
    }
}
