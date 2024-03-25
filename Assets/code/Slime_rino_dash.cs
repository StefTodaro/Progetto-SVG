using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_rino_dash : MonoBehaviour
{
    public float dashSpeed = 15f; // Velocità del dash
    public float dashDuration = 0.3f; // Durata del dash
    public float dashCooldown = 1f; // Tempo di recupero del dash
    private bool canDash = true;
    public bool isDashing = false;


    private movement mov;
    private Rigidbody2D rb;
    private float rockSlamForce = 11f;

    // Start is called before the first frame update
    void Start()
    {
        mov = gameObject.GetComponent<movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDash && Input.GetButtonDown("Fire1") && mov.isGrounded)
        {
            StartCoroutine(Dash());
        }


    }

    IEnumerator Dash()
    {
        // Imposta lo stato del dash
        canDash = false;
        isDashing = true;
        var originalGravity = rb.gravityScale;

        var initialDirection= mov.moveInput;

        rb.gravityScale = 0;
        float dashTimer = 0f;

        isDashing = true;


        while (dashTimer < dashDuration)
        {
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

        // Se il dash non è stato interrotto, reimposta lo stato

        rb.velocity = Vector2.zero;
        isDashing = false;
        rb.gravityScale = originalGravity;

        // Riabilita il dash dopo la durata del dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (isDashing)
        {
            if (collision.gameObject.CompareTag("Obstacles"))
            {
                Destroy(collision.gameObject); // Distruggi la tile individuale
            }

        }
    }
}
