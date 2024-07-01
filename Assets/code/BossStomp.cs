using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossStomp : MonoBehaviour
{
    public float bounce = 6;
    public Boss_logic boss;
    //serve per aumentare la precisione per il rilevamento dei colpi
    public float timeBetweenHits;
    public bool canBeHit = true;
    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.GetComponentInParent<Boss_logic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeHit)
        {
            timeBetweenHits += Time.deltaTime;
            if(timeBetweenHits >= 0.3f)
            {
                timeBetweenHits = 0;
                canBeHit = true;
            }
        }
    }

    public void Hit()
    {
        if (boss.isFighting)
        {
            boss.HitJump();
            if (boss.GetCurrentPhase() != 2)
            {
                boss.PassToNextPhase();
            }
            GetComponentInParent<Animator>().SetBool("hit", true);
        }
    }

  

    void OnTriggerEnter2D(Collider2D collision)
    {
        //handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player") )
        {   
            if (collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming = false;
                collision.GetComponent<movement>().canSlam = true;
            }

            //controllo per far effettuare un ulteriore salto allo slime_bird
            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                if (!collision.GetComponent<Slime_bird_double_jump>().canDoubleJump)
                {
                    collision.GetComponent<Slime_bird_double_jump>().canDoubleJump = true;
                    collision.GetComponent<Slime_bird_double_jump>().jumped = false;
                }
            }

            if (canBeHit)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
                canBeHit = false;
                Hit();
            }
        }
    }
}
