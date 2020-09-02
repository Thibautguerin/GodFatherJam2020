using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<EnemyBehaviour> enemyList = new List<EnemyBehaviour>();
    public EnemyBehaviour[] enemyPrefabs;

    public GameObject[] spawnLaderjackPlacement;
    public GameObject[] spawnSpiritsPlacement;
    public GameObject[] spawnFirePlacement;

    public float minTimeSpawn = 10;
    public float maxTimeSpawn = 70;
    private float _timeSpawn = 0;
    private float _selectedTimeSpawn = 0;

    private void Start()
    {
        GameController.instance.enemyController = this;
        _selectedTimeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
    }

    private void Update()
    {
        _timeSpawn += Time.deltaTime;
        if(_timeSpawn > _selectedTimeSpawn)
        {
            SpawnRandomEnemy();
            _timeSpawn = 0;
            _selectedTimeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);
        }
    }

    #region SpawnEnemy
    public void SpawnRandomEnemy()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                SpawnEnemy(0);
                break;
            case 1:
                SpawnEnemy(1);
                break;
            case 2:
                SpawnEnemy(2);
                break;
        }
    }
    public void SpawnEnemy(int index)
    {
        EnemyBehaviour enemy = Instantiate(enemyPrefabs[index]);
        enemy.transform.position = GetRandomPlacement(spawnSpiritsPlacement).transform.position;
        enemy.Init();
    }

    public GameObject GetRandomPlacement(GameObject[] placement)
    {
        for (int i = 0; i < placement.Length; i++)
        {
            if (placement[i].activeSelf)
            {
                StartCoroutine(StopPlacement(placement[i]));
                return placement[i];
            }
        }
        return null;
    }
    private IEnumerator StopPlacement(GameObject objectPlacement)
    {
        objectPlacement.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        objectPlacement.SetActive(true);
    }
    #endregion
}
