using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Shooter : MonoBehaviour
{



    [Header("Projectile Stats")]
    public float projSpeed = 10f;
    public float cdAfterCast = 1f;
    public float lifeTime = 2f;
    public float castTime = 1f;
    public GameObject spellPrefab;
    public GameObject castPrefab;

    [Header("Sound")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float explosionSFXVol = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.75f;

    bool coolDown = false;
    Player player;
    //Quaternion prefabAngle;

    [Header("Angle Offset")]
    public float offset;

    public GameObject cast;

    public void Start()
    {
        player = GetComponentInParent<Player>();
    }


    public void Update()
    {
        CursorAngle();

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

    IEnumerator ShootSomething()
    {
        GameObject spell = Instantiate(spellPrefab, transform.position + new Vector3(0, 0.1f, 0), transform.rotation) as GameObject;
        spell.GetComponent<Rigidbody2D>().velocity = player.mousePos.normalized * projSpeed;
        //AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);
        Destroy(spell, lifeTime);
        yield return new WaitForSeconds(cdAfterCast);
        coolDown = false;
    }

    IEnumerator StartCasting()
    {
        cast = Instantiate(castPrefab, transform.position + new Vector3(0, 0, 0), transform.rotation) as GameObject;
        cast.transform.parent = transform;
        Destroy(cast, castTime + 0.1f);
        yield return new WaitForSeconds(castTime);
        StartCoroutine(ShootSomething());
    }

    private void CursorAngle()
    {
        float rotZ = Mathf.Atan2(player.mousePos.y, player.mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);
        transform.RotateAround(player.transform.position, Vector3.up * 10f, 20 * Time.deltaTime);
    }

}

