using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class movement : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float jumpForce = 6.5f;
    public float slumForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;

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
            rb.velocity += Vector2.down * slumForce;
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

       

    }

    public void DieAndRespawn()
    {
        // Imposta la posizione del giocatore sul punto di respawn
        transform.position = GetComponent<checkpoint_handler>().checkpoint.position;
      //  SceneManager.LoadScene("SampleScene");
        gameObject.transform.position = transform.position = GetComponent<checkpoint_handler>().checkpoint.position;

        // Esegui altre azioni di morte, ad esempio perdere punti vita o visualizzare un'animazione di morte
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
