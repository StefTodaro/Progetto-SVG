using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float slumForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public Transform checkpoint;
    


    private Rigidbody2D rb;
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
        RaycastHit2D hit= Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayer);
        isGrounded = hit;
       
        anim.SetBool("onGround", isGrounded);

        // Movimento orizzontale
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(moveInput));

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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


        if (!isGrounded && Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity += Vector2.down * slumForce ;
        }

    }

    private void Flip()
    {
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            // Il giocatore è morto, respawnare al checkpoint
            DieAndRespawn();
        }
    }

    public void DieAndRespawn()
    {
        // Imposta la posizione del giocatore sul punto di respawn
        transform.position = checkpoint.position;

        // Esegui altre azioni di morte, ad esempio perdere punti vita o visualizzare un'animazione di morte
    }



}
