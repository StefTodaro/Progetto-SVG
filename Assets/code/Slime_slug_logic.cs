using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_slug_logic : MonoBehaviour
{
    public Quaternion rotation;
    public movement mov;
    public bool isOnWall=false;
    public float vertical_movement;
    private Rigidbody2D rb;
    public bool facingUp ;
    public float sideJumpForce = 6f;
    public float wallJumpTime = 0.3f;
    public float wallJumpTimer;
    public bool hasWallJumped = false;


    public float canWallJumpTimer;
    public float canWallJumpTime = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        rotation = gameObject.transform.rotation;
        mov = GetComponent<movement>();
        rb = GetComponent<Rigidbody2D>();
        facingUp = true;
        wallJumpTimer = wallJumpTime;
        canWallJumpTimer = canWallJumpTime;
    }

    // Update is called once per frame
    void Update()
    {
         
        RaycastHit hit;

       
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f, LayerMask.NameToLayer("ground"))
            && !mov.isGrounded )
        {
            isOnWall = true;
        }
       /* else if (!Physics.Raycast(transform.position, -transform.up, out hit, 1f, LayerMask.NameToLayer("ground"))
            &&  isOnWall)
        {
            isOnWall= false;
            rb.gravityScale = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            //per riportare il personaggio nella rotazione corretta anche se è girato verso il basso 
            if (!facingUp)
            {
                FlipVertical();
                facingUp = true;
            }
            canWallJumpTimer -= Time.deltaTime;
        }*/

        if (isOnWall)
        {
            AttachToWall();
        }

            // Visualizza il raycast nella scena (solo per debugging)
            Debug.DrawLine(transform.position, transform.position + -transform.up * 1, Color.red);
     

        if ((isOnWall|| canWallJumpTimer>=0) && Input.GetKeyDown(KeyCode.Space))
        {
            isOnWall = false;
            mov.anim.SetBool("jump", true);
            hasWallJumped = true;
            
            if (mov.facingRight)
            {
                mov.facingRight = false;
            }
            else
            {
                mov.facingRight = true;
            }
            mov.Flip();
        }

        if (hasWallJumped)
        {
            WallJump();
        }

        if (wallJumpTimer <= 0)
        {
            wallJumpTimer = wallJumpTime;
            hasWallJumped = false;
        }

        if((mov.isGrounded || isOnWall) && wallJumpTimer >= 0)
        {
            wallJumpTimer = wallJumpTime;
        }
    }

    private void FlipVertical()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }


    private void WallJump()
    {
        wallJumpTimer -= Time.deltaTime;
        mov.moveInput = 0;
        isOnWall = false;


        if (!mov.facingRight)
        {
            rb.velocity = new Vector2(-sideJumpForce, mov.jumpForce);
        }
        else
        {
            rb.velocity = new Vector2(sideJumpForce, mov.jumpForce);
        }
    }

    public void RoteateSlime()
    {
        if (mov.isSlamming)
        {
            mov.isSlamming = false;
        }

        if (mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (!mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

    }

    public void AttachToWall()
    {

        
        rb.gravityScale = 0;
        vertical_movement = Input.GetAxis("Vertical");
        mov.anim.SetFloat("speed", Mathf.Abs(vertical_movement));
        rb.velocity = new Vector2(rb.velocity.x, vertical_movement * mov.moveSpeed);


        if (vertical_movement < 0 && facingUp)
        {
            FlipVertical();
            facingUp = false;
        }
        else if (vertical_movement > 0 && !facingUp)
        {
            FlipVertical();
            facingUp = true;

        }

        if (canWallJumpTimer <= 0)
        {
            canWallJumpTimer = canWallJumpTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !mov.isGrounded  && !isOnWall)
        {
            RoteateSlime();
        }  
    }

}
