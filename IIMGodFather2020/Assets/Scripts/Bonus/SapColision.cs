using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapColision : MonoBehaviour
{
    public GameObject Sap;
    public float lifeTime = 10f;
    private bool isHit = false;

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isHit && other.tag == "Floor")
        {
            Vector3 pos = new Vector3(transform.position.x, other.GetComponent<Collider2D>().bounds.center.y + other.GetComponent<Collider2D>().bounds.size.y / 2 + Sap.GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
            Instantiate(Sap, pos,Sap.transform.rotation);
            isHit = true;
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
