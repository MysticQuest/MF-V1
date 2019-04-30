using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 200;
    public float invTime = 0.5f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        StartCoroutine(GetHit(damageDealer));
    }

    IEnumerator GetHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamaged();
        damageDealer.Hit();
        HitEffects();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        yield return new WaitForSeconds(invTime);
        ResetHitEffects();
    }

    private void HitEffects()
    {
        float spriteAlpha = spriteRenderer.color.a;

    }

    private void ResetHitEffects()
    {

    }

    private void Die()
    {
        //AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVol);
        Destroy(gameObject);
    }
}
