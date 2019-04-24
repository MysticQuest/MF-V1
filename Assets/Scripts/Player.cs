using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;


[Serializable]
public class PlayerStats
{
    public float mSpeed = 2f;
    public float mSpeedWhileAtt = 2.5f;
    public int maxHealth = 100;
}

public class Player : MonoBehaviour
{

    //caching
    public PlayerStats playerStats;

    Rigidbody2D rBody;
    Animator anim;

    public Vector2 mousePos;
    Vector2 movement;

    //movement  calculations
    float moveX;
    float moveY;
    bool IsWalking = false;
    bool hasmoveX = false;
    bool hasmoveY = false;

    [Header("Flip Delay")]
    public float lockFlipTime = 0.5f;
    bool locked = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hasmoveX = Mathf.Abs(rBody.velocity.x) > Mathf.Epsilon;
        hasmoveY = Mathf.Abs(rBody.velocity.y) > Mathf.Epsilon;
        IsWalking = hasmoveX || hasmoveY;

        Walk();
        GetMousePos();
        Flip();
    }

    private void Walk()
    {
        moveX = CrossPlatformInputManager.GetAxis("Horizontal");
        moveY = CrossPlatformInputManager.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        if (!locked) { rBody.velocity = movement * playerStats.mSpeed; } //normal movement
        else { rBody.velocity = movement * playerStats.mSpeedWhileAtt; } //movement while attacking

    }

    private void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void Flip()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && !locked)
        {
            Vector2 mouseDirection = new Vector2(Math.Sign(mousePos.x), 1);
            if (mouseDirection.x == 0) { return; }
            transform.localScale = mouseDirection;
            StartCoroutine(LockFlip(lockFlipTime));
        }

        if (IsWalking && !locked)
        {
            Vector2 moveDirection = new Vector2(Math.Sign(rBody.velocity.x), 1);
            if (moveDirection.x == 0) { return; }
            transform.localScale = moveDirection;
        }

    }

    IEnumerator LockFlip(float lockFlipTime)
    {
        locked = true;
        yield return new WaitForSeconds(lockFlipTime);
        locked = false;
    }
}
