using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{

    SpellManager spellManager;
    Explosion explosionScript;
    GameObject explosionPrefab;

    public int damage;

    void Start()
    {
        spellManager = FindObjectOfType<SpellManager>();
        explosionPrefab = spellManager.explosionPrefab;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
        if (explosionScript) { explosion.GetComponent<DamageDealer>().damage = damage; }

    }
}
