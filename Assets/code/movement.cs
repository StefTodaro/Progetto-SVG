using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

public class movement : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float jumpForce = 6.5f;
    public float slumForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public float moveInput;

    public bool isSwinging;

    public bool isSlamming;
    public bool canSlam=true;
    public float slamTimer;
    public float slamTimecharge = 0.75f;

    public Rigidbody2D rb;
    public  bool isGrounded;
    public bool facingRight;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    private void Update()
    {
        // Controlla se il personaggio è a terra
        anim.SetBool("onGround", isGrounded);

        //controllo se il personaggio ha la per l'oscillazione e se sta oscillando
        if(GetComponent<slime_lizard_logic>() && GetComponent<slime_lizard_logic>().swing.isSwinging)
        {
            isSwinging = true;
        }
        else
        {
            isSwinging = false;
        }

        // Movimento orizzontale
        if (!isSlamming && !isSwinging) {
            Movement();
        }
        anim.SetFloat("speed", Mathf.Abs(moveInput));

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            anim.SetBool("jump", true);
        }

        if(!isGrounded )
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump",false);
        }

        if (moveInput < 0 && facingRight)
        {
            facingRight = false;
            Flip();
            
        }
        else if (moveInput > 0 && !facingRight)
        {
            facingRight = true;
            Flip();
        }


        SlamCharge();

        if (!isGrounded && Input.GetKeyDown(KeyCode.S) && canSlam)
        {
            Slam();
        }

        if ((isGrounded || isSwinging )&& isSlamming)
        {
            isSlamming = false;
            canSlam = true;
        }

    }

    public void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    private void Slam()
    {
        canSlam = false;
        isSlamming = true;
         rb.velocity += Vector2.down * slumForce;
    }

    public void SlamCharge()
    {
        if (!canSlam)
        {
            slamTimer += Time.deltaTime;
            if (slamTimer >= slamTimecharge)
            {
                canSlam = true;
                slamTimer = 0;
            }
        }
    }
    public void Flip()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Specchia lo sprite sull'asse x
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            Hit();
        }
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Mob"))
        {
            Hit();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("death"))
        {
            
            DieAndRespawn();

            if (gameObject.GetComponent<Transformation_handler>().transformed)
            {
                gameObject.GetComponent<Transformation_handler>().LosePower();

            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Mob"))
        {
            Hit();
        }


        if (collision.gameObject.CompareTag("Object"))
        {
            if (isSlamming)
            {
                Destroy(collision.gameObject);
            }
        }



    }

   
    private void OnTriggerExit2D(Collider2D collision)
    {  
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            
            isGrounded = false;
        }


    }


    public void DieAndRespawn()
    {
        // Imposta la posizione del giocatore sul punto di respawn
        if (gameObject.GetComponent<Transformation_handler>().transformed)
        {
            gameObject.GetComponent<Transformation_handler>().LosePower();
        }

        transform.position = GetComponent<checkpoint_handler>().checkpoint_base.position;
      //  SceneManager.LoadScene("SampleScene");
        gameObject.transform.position = transform.position;
    }  


    public void Hit()
    {

        if (gameObject.GetComponent<Transformation_handler>().transformed)
        {
            gameObject.GetComponent<Transformation_handler>().LosePower();
        }
        else
        {
            DieAndRespawn();
        }
    }
   


}
