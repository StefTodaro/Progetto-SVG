using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_bird_double_jump : MonoBehaviour
{
    public movement mov;
    public bool canDoubleJump = false;
    public Rigidbody2D rb;
    public Animator anim;
    public float DoubleJumpForce = 5.5f;
    public bool jumped = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        mov = gameObject.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mov.isGrounded && !jumped)
        {
            canDoubleJump = true;
        }

        if(mov.isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
        }

        if( canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            
            canDoubleJump = false;
            anim.SetBool("doubleJump", true);
            jumped = true;
            
        }

        if(mov.isGrounded && !canDoubleJump)
        {
            anim.SetBool("doubleJump", false);
        }

        if (mov.isGrounded && jumped)
        {
            jumped = false;
        }
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, DoubleJumpForce);
        anim.SetBool("doubleJump", false);
    }

  
}
