using UnityEngine;
using System.Collections;

public class PlayerMovementOld : MonoBehaviour
{

    Rigidbody2D rBody;
    public Animator anim;
    //public Animation anim2;
    public bool isAttacking;
    public float speed;
    public float speedWhileAttacking;
    public Vector2 movement_vector;

    //public GameObject sword;
    //public SpriteRenderer layer;

    public GameObject player;

    public Vector2 mousepos;

    public bool IsAttacking = false;

    //public float dirx;
    //public float diry;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking = anim.GetBool("IsAttacking");


        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("InputX", movement_vector.x);
            anim.SetFloat("InputY", movement_vector.y);
        }
        else
        {
            anim.SetBool("IsWalking", false);
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

        //mouse look when attacking

        if (isAttacking == true)
        {
            anim.SetFloat("Input_x", mousepos.x);
            anim.SetFloat("Input_y", mousepos.y);

        }


        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    }

    void FixedUpdate()
    {
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        movement_vector = new Vector2(movex, movey).normalized;

        if (isAttacking == false)
        {
            rBody.AddForce(movement_vector * speed);
        }
        else
        {
            rBody.AddForce(movement_vector * speedWhileAttacking);
        }
    }
}
