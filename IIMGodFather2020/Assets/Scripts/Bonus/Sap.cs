using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : MonoBehaviour
{
    public float lifeTime = 8f;

    private void Awake()
    {
        Color c = GetComponent<SpriteRenderer>().material.color;
        c.a = 0f;
        GetComponent<SpriteRenderer>().material.color = c;
    }

    void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(LifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // gérer la collision avec le perso
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            Color c = GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            GetComponent<SpriteRenderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0f; f -= 0.05f)
        {
            Color c = GetComponent<SpriteRenderer>().material.color;
            c.a = f;
            GetComponent<SpriteRenderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(FadeOut());
    }
}
