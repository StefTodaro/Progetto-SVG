using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public GameObject coinPrefab;
    public GameObject deathPanel;

    public GameObject landingEffect;
    public GameObject slimeDrops;

    public AudioClip jumpAudioClip;
    public AudioClip landAudioClip;
    public AudioClip slamAudioClip;
    public AudioClip deathAudioClip;
    public AudioClip hitAudioClip;
    



    public bool canBeHit=true;
    //timer per far lampeggiare il personaggio quando colpito
    float flashTimer;
    public float invulnerabilityTimer;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        coinPrefab = GameObject.FindGameObjectWithTag("coin");
        deathPanel = GameObject.Find("DeathPanel");
    }

    private void Update()
    {
        // Controlla se il personaggio � a terra
        anim.SetBool("onGround", isGrounded);

        //controllo se il personaggio ha la per l'oscillazione e se sta oscillando
        if (GetComponent<slime_lizard_logic>() && GetComponent<slime_lizard_logic>().swing.isSwinging)
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

        if (!isGrounded)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
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

        if ((isGrounded || isSwinging) && isSlamming)
        {
            isSlamming = false;
            canSlam = true;
        }

        if (!canBeHit)
        {
           FlashEffect();
        }
        else if(canBeHit && !GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if(jumpAudioClip!=null)
        SoundEffectManager.Instance.PlaySoundEffect(jumpAudioClip, transform, 0.5f);
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
        
        /*
        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
        */
    }

    //effetua l'effetto lampeggiante 
    public void FlashEffect()
    {
        flashTimer += Time.deltaTime;
        invulnerabilityTimer += Time.deltaTime;

        // Se il timer supera la durata del lampeggio
        if (flashTimer >= 0.3f)
        {
            // Alterna la visibilit� dello sprite renderer
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            flashTimer = 0f;
        }

        if (invulnerabilityTimer >= 2)
        {
            canBeHit = true;
            invulnerabilityTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") && canBeHit)
        {
            Hit();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") && canBeHit)
        {
            Hit();  
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("death"))
        {
            
            DieAndRespawn();

        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {   
            //controlli per l'istaziazione delle interazioni con il terreno
            if (!collision.gameObject.CompareTag("Object"))
            {
                Instantiate(landingEffect, transform.position, transform.rotation);

                if (!isSlamming)
                {
                    if (landAudioClip != null)
                        SoundEffectManager.Instance.PlaySoundEffect(landAudioClip, transform, 0.3f);
                }
                else
                {
                    if (slamAudioClip != null)
                        SoundEffectManager.Instance.PlaySoundEffect(slamAudioClip, transform, 1f);
                }
            }

        }

        if (collision.gameObject.CompareTag("Object"))
        {
            if (isSlamming)
            {
                
                coinManager coinManager = coinPrefab.GetComponent<coinManager>();
                if (coinManager != null && collision.GetComponent<Object_logic>().dropCoin)
                {
                    coinManager.InstantiateCoin(collision.transform.position);
                }
                collision.GetComponent<Object_logic>().Break();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //controlla se il personaggio � a terra
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }
    }

    public void DieAndRespawn()
    {
      
        Instantiate(slimeDrops, transform.position, transform.rotation);
        SoundEffectManager.Instance.PlaySoundEffect(deathAudioClip, transform, 0.5f);
        GameManager_logic.Instance.LockCamera();
        deathPanel.GetComponent<Death_panel_logic>().ActivePanel();
        gameObject.GetComponent<Transformation_handler>().LosePower();
        GameManager_logic.Instance.RespawnPlayer(gameObject);
    }  


    public void Hit()
    {

        if (gameObject.GetComponent<Transformation_handler>().transformed)
        {
            SoundEffectManager.Instance.PlaySoundEffect(hitAudioClip, transform, 0.4f);
            gameObject.GetComponent<Transformation_handler>().LosePower();
            //attiva l'invulnerabilit� cos� che sia attiva per tutte le trasformazioni
            gameObject.GetComponent<Transformation_handler>().ActivateInvulnerability();
            
        }
        else
        {
            DieAndRespawn();
        }
    }
   


}
