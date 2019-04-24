using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;


[Serializable]
public class PlayerStatsOld
{
    public float mSpeed = 2f;
    public float mSpeedWhileAtt = 2.5f;
    public float mSpeedAnimAtt = 0.25f;
    public int maxHealth = 100;
}

public class PlayerOld : MonoBehaviour
{
    public PlayerStatsOld playerStatsOld;

    Rigidbody2D rBody;
    Animator anim;

    Vector2 mousePos;
    Vector2 movement;
    float moveX;
    float moveY;
    bool IsWalking = false;
    bool IsAttacking = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool hasmoveX = Mathf.Abs(rBody.velocity.x) > Mathf.Epsilon;
        bool hasmoveY = Mathf.Abs(rBody.velocity.y) > Mathf.Epsilon;
        IsWalking = hasmoveX || hasmoveY;

        Walk();
        Attack();

        Animations();
    }

    private void Walk()
    {
        moveX = CrossPlatformInputManager.GetAxis("Horizontal");
        moveY = CrossPlatformInputManager.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY).normalized;
        rBody.velocity = movement * playerStatsOld.mSpeed;
    }

    private void Attack()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void Animations()
    {
        anim.SetBool("IsWalking", IsWalking);
        if (movement != Vector2.zero)
        {
            anim.SetFloat("InputX", moveX);
            anim.SetFloat("InputY", moveY);
            anim.SetFloat("SpeedMult", 1);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("IsAttacking", true);
            IsAttacking = true;
        }
        else
        {
            anim.SetBool("IsAttacking", false);
            IsAttacking = false;
        }

        if (IsAttacking == true)
        {
            anim.SetFloat("InputX", mousePos.x);
            anim.SetFloat("InputY", mousePos.y);
            rBody.velocity = rBody.velocity / playerStatsOld.mSpeedWhileAtt;
            anim.SetFloat("SpeedMult", playerStatsOld.mSpeedAnimAtt);
        }
    }
}
