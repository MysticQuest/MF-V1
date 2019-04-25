using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 200;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        GetHit(damageDealer);
    }

    private void GetHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamaged();
        damageDealer.Hit();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        //AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVol);
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
