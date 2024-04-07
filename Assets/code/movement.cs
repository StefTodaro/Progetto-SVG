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

    public bool onPlatform;
    Collider2D platformCollider;

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

        // Movimento orizzontale
        if (!isSlamming)
        {
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

        if (isGrounded && isSlamming)
        {
            isSlamming = false;
            canSlam = true;
        }

        if (onPlatform && Input.GetKeyDown(KeyCode.S))
        {

            // Avvia la coroutine per disabilitare temporaneamente il collider della piattaforma
            StartCoroutine(DisablePlatformCollider(platformCollider));
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
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private IEnumerator DisablePlatformCollider(Collider2D platformCollider)
    {
        // Disabilita il collider della piattaforma
        platformCollider.enabled = false;

        // Attendere per la durata specificata
        yield return new WaitForSeconds(0.5f);

        // Riabilita il collider della piattaforma
        platformCollider.enabled = true;
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

        if (collision.gameObject.CompareTag("platform") )
        {
            onPlatform = true;
            platformCollider = collision.gameObject.GetComponent<Collider2D>();
        }



    }

   
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("platform"))
        {
            onPlatform = false;
        }

    }


    public void DieAndRespawn()
    {
        // Imposta la posizione del giocatore sul punto di respawn
        transform.position = GetComponent<checkpoint_handler>().checkpoint.position;
      //  SceneManager.LoadScene("SampleScene");
        gameObject.transform.position = transform.position;

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
