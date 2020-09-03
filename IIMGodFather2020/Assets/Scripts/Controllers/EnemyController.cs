using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<EnemyBehaviour> enemyList = new List<EnemyBehaviour>();
    public EnemyBehaviour[] enemyPrefabs;

    public GameObject spawnParentLaderjackPlacement;
    private GameObject[] spawnLaderjackPlacement;

    public GameObject spawnParentSpiritsPlacement;
    private GameObject[] spawnSpiritsPlacement;

    public GameObject spawnParentFirePlacement;
    private GameObject[] _spawnFirePlacement;

    public float minTimeSpawn = 10;
    public float maxTimeSpawn = 70;
    private float _timeSpawn = 0;
    private float _selectedTimeSpawn = 0;

    private void Start()
    {
        GameController.instance.enemyController = this;
        _selectedTimeSpawn = Random.Range(minTimeSpawn, maxTimeSpawn);

        spawnLaderjackPlacement = new GameObject[spawnParentLaderjackPlacement.transform.childCount];
        for (int i = 0; i < spawnParentLaderjackPlacement.transform.childCount; i++)
        {
            spawnLaderjackPlacement[i] = spawnParentLaderjackPlacement.transform.GetChild(i).gameObject;
        }
        spawnSpiritsPlacement = new GameObject[spawnParentSpiritsPlacement.transform.childCount];
        for (int i = 0; i < spawnParentSpiritsPlacement.transform.childCount; i++)
        {
            spawnSpiritsPlacement[i] = spawnParentSpiritsPlacement.transform.GetChild(i).gameObject;
        }
        _spawnFirePlacement = new GameObject[spawnParentFirePlacement.transform.childCount];
        for (int i = 0; i < spawnParentFirePlacement.transform.childCount; i++)
        {
            _spawnFirePlacement[i] = spawnParentFirePlacement.transform.GetChild(i).gameObject;
        }

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
                SpawnEnemy(0, spawnSpiritsPlacement);
                break;
            case 1:
                SpawnEnemy(1, spawnLaderjackPlacement);
                break;
            case 2:
                SpawnEnemy(2, _spawnFirePlacement);
                break;
        }
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
    private IEnumerator StopPlacement(GameObject objectPlacement)
    {
        objectPlacement.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        objectPlacement.SetActive(true);
    }
    #endregion
}
