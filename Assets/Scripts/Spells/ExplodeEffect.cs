using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{

    SpellManager spellManager;
    DamageDealer damageDealer;
    GameObject explosionPrefab;
    bool IsQuitting;

    public int explDamage;

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
        if (!IsQuitting && explosionPrefab)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position + transform.up * 1.05f, transform.rotation) as GameObject;
            damageDealer = explosion.GetComponent<DamageDealer>();
            if (damageDealer) { damageDealer.damage = explDamage; }
        }
    }

    private void OnApplicationQuit()
    {
        IsQuitting = false;
    }
}
