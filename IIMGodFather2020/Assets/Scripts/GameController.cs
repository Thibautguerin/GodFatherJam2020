using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerController player;
    public GameObject tree;

    [Header("Map")]
    public float radiusLimitMap = 10;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!tree)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(tree.transform.position, radiusLimitMap);
    }
}
