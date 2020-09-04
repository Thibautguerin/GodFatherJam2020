﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEnemy : EnemyBehaviour
{
    private bool _sens = false;
    private bool _isAttack = false;

    private float _sizeScale;

    private void FixedUpdate()
    {
        if(!_isAttack)
            transform.position += new Vector3(_sens?-speedMovement: speedMovement, 0,0) * Time.deltaTime;                                                                                                                                                         
    }

    public override void Init()
    {
        base.Init();
        _sizeScale = transform.localScale.x;
        if (GameController.instance.tree.transform.position.x < transform.position.x)
        {
            _sens = true;
            transform.localScale = new Vector3(_sizeScale, _sizeScale, _sizeScale);
        }
        else
        {
            _sens = false;
            transform.localScale = new Vector3(-_sizeScale, _sizeScale, _sizeScale);
        }
    }

    public override void Die(PlayerController collide, bool trigger)
    {
        if (collide.GetGravity() > 0)
        {
            base.Die(collide, trigger);
            StopAllCoroutines();
            SoundEffectsController.instance.MakeBadSpiritDeathSound();
            Destroy(gameObject);
        }
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
        if (collision.gameObject.tag == "Attack")
        {
            if (GameController.instance.player.GetBiggerAttack())
            {
                Die(GameController.instance.player, false);
            }
        }
    }

    public override void Attack()
    {
        base.Attack();
        SoundEffectsController.instance.MakeBadSpiritAttackSound();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameController.instance.player.GetBiggerAttack())
            {
                Die(GameController.instance.player, false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameController.instance.player.GetBiggerAttack())
            {
                Die(GameController.instance.player, false);
            }
        }
    }
}
