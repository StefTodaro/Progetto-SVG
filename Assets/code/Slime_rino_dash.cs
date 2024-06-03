using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime_rino_dash : MonoBehaviour
{
    public float dashSpeed = 15f; // Velocità del dash
    public float dashDuration = 0.3f; // Durata del dash
    public float dashCooldown = 1f; // Tempo di recupero del dash
    public bool canDash = true;
    public bool isDashing = false;
    public float dashTimer = 0;

    private movement mov;
    private Rigidbody2D rb;
    public GameObject dashEffect;

    public  GameObject coinPrefab;
    public GameObject boxBreakEffect;

    // Start is called before the first frame update
    void Start()
    {
        mov = gameObject.GetComponent<movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isDashing = false;
    }
    // Update is called once per frame
    void Update()
    {
        

        if (canDash && Input.GetMouseButtonDown(0) && mov.isGrounded)
        {
            if (mov.facingRight)
            {
                Instantiate(dashEffect, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 180,0));
            }
            
            StartCoroutine(Dash());
           
        }
        if (!canDash)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown)
            {
                canDash = true;
                dashTimer = 0;
            }
        }

        
    }

    IEnumerator Dash()
    {
        // Imposta lo stato del dash
        canDash = false;
        isDashing = true;
        


        var initialDirection= mov.moveInput;

        //indica la durata del dash
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            mov.anim.SetBool("isDashing", true);
            mov.moveInput = initialDirection;
            // Continua il movimento fino a quando il giocatore non raggiunge la posizione di destinazione o il dash non viene interrotto
            if (mov.facingRight)
            {
                rb.velocity = new Vector2(dashSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, 0);
            }
            dashTimer += Time.deltaTime; // Aggiorna il timer
            yield return null;
        }
        isDashing = false;
        rb.velocity = Vector2.zero;
        mov.anim.SetBool("isDashing", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (isDashing)
        {
            if (collision.gameObject.CompareTag("Obstacles"))
            {
                collision.gameObject.SetActive(false); // Distruggi la tile individuale
                collision.gameObject.GetComponent<BlockBreakEffect>().CallBreakEffect();
            }

        }

        if (collision.gameObject.CompareTag("Object"))
        {
            if (isDashing)
            { 
                coinManager coinManager = coinPrefab.GetComponent<coinManager>();
                if (coinManager != null && collision.gameObject.GetComponent<Object_logic>().dropCoin)
                {
                    coinManager.InstantiateCoin(collision.transform.position);
                }
                Instantiate(boxBreakEffect, collision.transform.position, collision.transform.rotation);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
