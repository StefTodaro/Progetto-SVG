using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_slug_logic : MonoBehaviour
{
    public Quaternion rotation;
    public movement mov;
    public bool isOnWall=false;
    public float verticalMovement;
    private Rigidbody2D rb;
    public bool facingUp ;
    public float sideJumpForce = 6f;
    public bool hasWallJumped = false;
    public float canWallJumpTimer;
    public float canWallJumpTime = 0.15f;

    public bool isRotated = false;


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
        RaycastHit2D hit;
        Vector2 direction = mov.facingRight ? Vector2.right : Vector2.left;

        if (!mov.isGrounded && !isOnWall)
        {
            hit = Physics2D.Raycast(transform.position, direction, 0.7f, LayerMask.GetMask("ground"));
            Debug.DrawRay(transform.position, direction * 0.7f, Color.red);

            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                isOnWall = true;
                RoteateSlime();
                mov.SetCanTurn(false);
            }
        }

        //controllo per la rotazione del personaggio nel verso di marcia
            if (isOnWall && isRotated && GameManager_logic.Instance.GetCanMove())
        {

            if (verticalMovement < 0 && facingUp)
            {
                facingUp = false;
                mov.Flip();
            }
            else if (verticalMovement > 0 && !facingUp)
            {
                facingUp = true;
                mov.Flip();
            }
        }
    }


    private void FixedUpdate()
    {
        if (isOnWall && isRotated && GameManager_logic.Instance.GetCanMove())
        {
            AttachToWall();
            VerticalMovement();
        }
    }

    public bool GetIsOnWall()
    {
        return isOnWall;
    }


    //funzione di movimento del personaggio lungo il muro
    private void VerticalMovement()
    {
        if (!GameManager_logic.Instance.GetInactive())
        {
            verticalMovement = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x,verticalMovement * mov.moveSpeed);
            mov.anim.SetFloat("speed", Mathf.Abs(verticalMovement));
        }
    }


    //funzione per roteare lo slime perpendicolarmente la muro
    public void RoteateSlime()
    {   
        if (mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 90);
        }
        else if (!mov.facingRight)
        {
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y,-90);
        }

        isRotated = true;
        //lo slime inizialmente sarà sempre orientato verso l'alto
        facingUp = true;
    }



    //funzione richiamata nel'istante in cui il personaggio si attacca ad un muro
    public void AttachToWall()
    {
        if (mov.isSlamming)
        {
            mov.isSlamming = false;
        }

        rb.gravityScale = 0;

        if (canWallJumpTimer <= 0)
        {
            canWallJumpTimer = canWallJumpTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            if ((!facingUp))
            {
                facingUp = true;
            }
        }
    }


    //indica che il personaggio è a contatto con un muro
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ( collision.tag=="Wall")
        {
            isOnWall = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //funzione per far scendere il personaggio dal muro
        if (isOnWall && collision.tag == "Wall")
        {
            isOnWall = false;
            mov.SetCanTurn(true);
            rb.gravityScale = 1;
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
            if (isRotated)
            {
                isRotated = false;
            }
            if (canWallJumpTime > 0)
            {
                canWallJumpTimer -= Time.deltaTime;
            }

            //per riportare il personaggio nella rotazione corretta anche se è girato verso il basso 
            if (!facingUp)
            {
                mov.Flip();
                facingUp = true;
            }
        }
    }

}
