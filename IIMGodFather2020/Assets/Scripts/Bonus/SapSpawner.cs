using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapSpawner : MonoBehaviour
{
    public GameObject sapParticle;
    public SpriteRenderer spawnRenderer;
    public float spawnDelay = 5;

    private float nextSpawnTime;

    private void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }

    private void Spawn()
    {
        Vector3 center = spawnRenderer.bounds.center;
        Vector3 size = spawnRenderer.bounds.size;
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), transform.position.z);
        nextSpawnTime = Time.time + spawnDelay;
        Instantiate(sapParticle, pos, sapParticle.transform.rotation);
    }

    private bool ShouldSpawn()
    {
        return Time.time > nextSpawnTime;
    }
}
