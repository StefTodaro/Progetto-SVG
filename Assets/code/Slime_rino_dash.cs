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

    // Start is called before the first frame update
    void Start()
    {
        mov = gameObject.GetComponent<movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
       
        if (canDash && Input.GetMouseButtonDown(0) && mov.isGrounded)
        {
            StartCoroutine(Dash());
        }
        if (!canDash)
        {
            isDashing = false;

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
        rb.velocity = Vector2.zero;
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
