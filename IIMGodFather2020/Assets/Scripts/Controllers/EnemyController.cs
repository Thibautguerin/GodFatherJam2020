using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<EnemyBehaviour> enemyList = new List<EnemyBehaviour>();
    public EnemyBehaviour[] enemyPrefabs;

    public GameObject spawnParentLaderjackPlacement;
    private GameObject[] _spawnLaderjackPlacement;

    public GameObject spawnParentSpiritsPlacement;
    private GameObject[] _spawnSpiritsPlacement;

    public GameObject spawnParentFirePlacement;
    private GameObject[] _spawnFirePlacement;

    [Header("Fire Spawn")]
    public float minFireTimeSpawn = 10;
    public float maxFireTimeSpawn = 70;

    [Header("Spirit Spawn")]
    public float minSpiritTimeSpawn = 10;
    public float maxSpiritTimeSpawn = 70;

    [Header("Lumberjack Spawn")]
    public float minLumberjackTimeSpawn = 10;
    public float maxLumberjackTimeSpawn = 70;

    private float _timeSpawn = 0;
    private float _selectedTimeSpawn = 0;

    private void Start()
    {
        GameController.instance.enemyController = this;
        _spawnLaderjackPlacement = new GameObject[spawnParentLaderjackPlacement.transform.childCount];
        for (int i = 0; i < spawnParentLaderjackPlacement.transform.childCount; i++)
        {
            _spawnLaderjackPlacement[i] = spawnParentLaderjackPlacement.transform.GetChild(i).gameObject;
        }
        _spawnSpiritsPlacement = new GameObject[spawnParentSpiritsPlacement.transform.childCount];
        for (int i = 0; i < spawnParentSpiritsPlacement.transform.childCount; i++)
        {
            _spawnSpiritsPlacement[i] = spawnParentSpiritsPlacement.transform.GetChild(i).gameObject;
        }
        _spawnFirePlacement = new GameObject[spawnParentFirePlacement.transform.childCount];
        for (int i = 0; i < spawnParentFirePlacement.transform.childCount; i++)
        {
            _spawnFirePlacement[i] = spawnParentFirePlacement.transform.GetChild(i).gameObject;
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    #region SpawnEnemy
    public void SpawnRandomEnemy()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                SpawnEnemy(0, _spawnSpiritsPlacement);
                break;
            case 1:
                SpawnEnemy(1, _spawnLaderjackPlacement);
                break;
            case 2:
                SpawnEnemy(2, _spawnFirePlacement);
                break;
        }
    }
    private IEnumerator SpawnFire()
    {
        float timeToWait = Random.Range(minFireTimeSpawn, maxFireTimeSpawn);
        yield return new WaitForSeconds(timeToWait);
        SpawnEnemy(2, _spawnFirePlacement);
        StartCoroutine(SpawnFire());
    }
    private IEnumerator SpawnSpirit()
    {
        float timeToWait = Random.Range(minSpiritTimeSpawn, maxSpiritTimeSpawn);
        yield return new WaitForSeconds(timeToWait);
        SpawnEnemy(0, _spawnSpiritsPlacement);
        StartCoroutine(SpawnSpirit());

    }
    private IEnumerator SpawnLanderjack()
    {
        float timeToWait = Random.Range(minLumberjackTimeSpawn, maxLumberjackTimeSpawn);
        yield return new WaitForSeconds(timeToWait);
        SpawnEnemy(1, _spawnLaderjackPlacement);
        StartCoroutine(SpawnLanderjack());

    }
    public void SpawnEnemy(int index, GameObject[] placements)
    {
        EnemyBehaviour enemy = Instantiate(enemyPrefabs[index]);
        enemy.transform.position = GetRandomPlacement(placements).transform.position;
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
        enemy.Init();
    }

    public GameObject GetRandomPlacement(GameObject[] placement)
    {
        return placement[Random.Range(0, placement.Length)];
    }
    #endregion
}
