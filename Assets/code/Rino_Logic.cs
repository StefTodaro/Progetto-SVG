using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Rino_Logic : MonoBehaviour
{
    public GameObject player;
    public Mob_patrol patrol;

    public bool isCharging = false;
    public float chargeDistance = 5;
    public bool canCharge = true;
    public float patrolSpeed;
    public float chargeSpeed = 6.5f;
    public bool stunned = false;

    public float bounceStength = 3;


    public Mob_onGround_chase chase;
    public AudioClip crashAudio;

    // Start is called before the first frame update
    void Start()
    {
        patrol = GetComponent<Mob_patrol>();
        patrolSpeed = patrol.moveSpeed;
    }

  
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //controllo se il giocatore si trova nella posizione giusta per effettuare la carica 
        if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= 1.5f)
        {
            if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) <= 10f)
            {
                //controlla che il giocatore sia nella stessa direzione in cui si sta muovendo il rinoceronte
                if (player.transform.position.x >= gameObject.transform.position.x && patrol.movingRight == true ||
                player.transform.position.x <= gameObject.transform.position.x && patrol.movingRight == false)
                {
                    if (canCharge)
                    {
                        canCharge = false;
                        patrol.isPatrolling = false;
                        patrol.anim.SetBool("charge", true);
                    }
                }
            }
               
        }
        
        if (isCharging)
        {
            transform.Translate(Vector2.left * chargeSpeed * Time.deltaTime);
        }


    }

    private void Charge()
    {
        patrol.anim.SetBool("charge", false);
        isCharging = true;
       
    }

    private void Unstun()
    {
        patrol.anim.SetBool("stunned", false);
        patrol.isPatrolling = true;
        canCharge = true;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCharging && !collision.CompareTag("Mob"))
        {
            SoundEffectManager.Instance.PlaySoundEffect(crashAudio, transform, 0.45f);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * bounceStength, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bounceStength, ForceMode2D.Impulse);
            isCharging = false;
            stunned = true;
            patrol.anim.SetBool("stunned", true);
        }
    }
}
