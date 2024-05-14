using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_bird_double_jump : MonoBehaviour
{
    public movement mov;
    public bool canDoubleJump = false;
    public Rigidbody2D rb;
    public Animator anim;
    public float DoubleJumpForce = 5.5f;
    public bool jumped = false;
    //booleano che verifica se il personaggio sia su una cassa
    public bool onBox=false;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        mov = gameObject.GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mov.isGrounded && !jumped)
        {
            canDoubleJump = true;
        }

        if(mov.isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
        }

        if( canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            
            canDoubleJump = false;
            anim.SetBool("doubleJump", true);
            jumped = true;
            
        }

        if(mov.isGrounded && !canDoubleJump)
        {
            anim.SetBool("doubleJump", false);
        }

        if (mov.isGrounded && jumped && !onBox)
        {
            jumped = false;
        }
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, DoubleJumpForce);
        anim.SetBool("doubleJump", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //controllo che il giocatore sia su una cassa in aria , in modo da limitare il numero di salti effettuabili
        if (collision.CompareTag("Object") && !collision.GetComponent<Object_logic>().onGround)
        {
            onBox = true;
        }
        //se la cassa è a terra allora il doppio salto è abilitato
        if (collision.CompareTag("Object") && collision.GetComponent<Object_logic>().onGround)
        {
            onBox = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            onBox = false;
        }
    }




}
