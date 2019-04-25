using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class SpellManager : MonoBehaviour
{
    [Header("Spell Stats")]
    public int damage;
    public int manaCost;
    public int explDamage;
    public float projSpeed = 10f;
    public float bulletSpread = 0f;
    public float cdAfterCast = 1f;
    public float lifeTime = 2f;
    public float castTime = 1f;
    public Vector3 spawnPoint;
    public bool IsProjectile = true;
    public GameObject castPrefab;
    public GameObject spellPrefab;
    public GameObject explosionPrefab;

    [Header("Spell Sounds")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float explosionSFXVol = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.75f;

    [Header("Misc Spell Options")]
    public float angleOffset;
    public Vector3 castSpawnOffset;
    public Vector3 spellSpawnOffset;

    //IGNORE
    bool coolDown = false;
    Player player;
    GameObject cast;
    GameObject spell;
    DamageDealer damageDealer;
    Rigidbody2D spellBody;
    ExplodeEffect explodeEffect;
    float rotZ;
    Vector2 bulletSpreadV;
    float bulletSpreadRR;
    //IGNORE

    public void Start()
    {
        player = GetComponentInParent<Player>();
    }


    public void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && coolDown == false)
        {
            coolDown = true;
            StartCoroutine(StartCasting());
        }
    }

    IEnumerator StartCasting()
    {
        cast = Instantiate(castPrefab, transform.position + castSpawnOffset, transform.rotation) as GameObject;
        cast.transform.parent = transform;
        Destroy(cast, castTime + 0.1f);
        yield return new WaitForSeconds(castTime);
        StartCoroutine(ShootSomething());
    }

    IEnumerator ShootSomething()
    {
        CursorAngle();
        spawnPoint = this.transform.position; //TO BE REVISITED
        bulletSpreadRR = Random.Range(-bulletSpread, bulletSpread);
        spell = Instantiate(spellPrefab, spawnPoint + spellSpawnOffset, transform.rotation) as GameObject;
        ConfigureSpell();
        Destroy(spell, lifeTime);
        yield return new WaitForSeconds(cdAfterCast);
        coolDown = false;
    }

    private void ConfigureSpell()
    {
        //if (spellBody) { spellBody.velocity = player.mousePos.normalized * projSpeed; } // ALTERNATIVE
        spellBody = spell.GetComponent<Rigidbody2D>();
        if (spellBody) { SpellMovementType(); }
        damageDealer = spell.GetComponent<DamageDealer>();
        if (damageDealer) { damageDealer.damage = damage; }
        explodeEffect = spell.GetComponent<ExplodeEffect>();
        if (explodeEffect) { explodeEffect.explDamage = explDamage; }

        //AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);
    }

    private void SpellMovementType()
    {
        if (IsProjectile)
        {
            spellBody.velocity = transform.up * projSpeed;
        }
        else
        {
            return;
        }
    }

    private void CursorAngle()
    {
        rotZ = Mathf.Atan2(player.mousePos.y + bulletSpreadRR, player.mousePos.x + bulletSpreadRR) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + angleOffset);
        //transform.rotation = Quaternion.AngleAxis(rotZ + angleOffset, Vector3.forward); //ALTERNATIVE
    }

}

