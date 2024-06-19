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
    public bool isRotated;
    public float sideJumpForce = 6f;
  
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
    
        canWallJumpTimer = canWallJumpTime;
    }

    // Update is called once per frame
    void Update()
    {


        if (!mov.isGrounded && !isOnWall)
        {
            int direction;
            if (mov.facingRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right*direction, 0.555f, LayerMask.GetMask("ground"));
            if (hit.collider != null && !hit.collider.CompareTag("platform") 
               && !hit.collider.CompareTag("Object"))
            {
                    RoteateSlime();
            }else
            {
                transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);

            }
        }
        

        if (isOnWall)
        {
            AttachToWall();
        }

        if (!isOnWall )
        {
            rb.gravityScale = 1;
           
            if (canWallJumpTime > 0)
            {
                canWallJumpTimer -= Time.deltaTime;
            }
        }


        if (((isOnWall && isRotated )|| canWallJumpTimer >= 0) && Input.GetKeyDown(KeyCode.Space))
        {
            isOnWall = false;
            mov.Jump();
            WallJump();
            //controllo che serve a far girare il giocatore nella direzione di salto
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

     

    }

    private void FlipVertical()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }


    private void WallJump()
    {
        mov.moveInput = 0;
        isOnWall = false;
        isRotated = false;
        //si riporta lo sprite nelle condizioni originali
        if (!facingUp)
        {
            FlipVertical();
        }
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);


        if (!mov.facingRight)
        {
            rb.velocity = new Vector2(sideJumpForce, mov.jumpForce * 1.3f);
        }
        else
        {
            rb.velocity = new Vector2(-sideJumpForce, mov.jumpForce * 1.3f  );
        }


    }

    public void RoteateSlime()
    {
       
        isRotated = true;
        if (mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 90);
        }
        else if (!mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y,-90);
        }

        //lo slime inizialmente sarà sempre orientato verso l'alto
        facingUp = true;
    }


    public void AttachToWall()
    {
        if (mov.isSlamming)
        {
            mov.isSlamming = false;
        }

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

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!mov.isGrounded && isRotated && !isOnWall)
        {
            //controlla se il personaggio è roteato e tocca un muro
            if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
            {
                isOnWall = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //controlla se il personaggio è roteato e tocca un muro
        if (isOnWall && collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isOnWall = false;
            isRotated = false;
            //per riportare il personaggio nella rotazione corretta anche se è girato verso il basso 
            if (!facingUp)
            {
                FlipVertical();
                facingUp = true;
            }
        }
    }

}
