using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : EnemyBehaviour
{
    public float multiplyRate = 1;
    public int limitSpawn = 2;

    public float spawnRadiusMin = 0.2f;
    public float spawnRadiusMax = 1;

    public override void Init()
    {
        base.Init();
        Attack();
        StartCoroutine(Duplicate());
    }

    public override void Die(PlayerController collide, bool trigger)
    {
        if (collide.GetGravity() == 0 && trigger)
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

    public IEnumerator Duplicate()
    {
        yield return new WaitForSeconds(multiplyRate);
        if (limitSpawn <= 0)
            yield break;
        FireEnemy newEnemy = Instantiate(this);
        bool X = Random.Range(0, 2) == 0 ? true : false;
        bool Y = Random.Range(0, 2) == 0 ? true : false;

        newEnemy.transform.position = new Vector2(
            transform.position.x + Random.Range(spawnRadiusMin, spawnRadiusMax) * (X ? 1 : -1),
            transform.position.y + Random.Range(spawnRadiusMin, spawnRadiusMax) * (Y ? 1 : -1)
        );
        limitSpawn -= 1;
        newEnemy.limitSpawn = limitSpawn;
        StartCoroutine(Duplicate());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            Die(GameController.instance.player, true);
        }
    }
}
