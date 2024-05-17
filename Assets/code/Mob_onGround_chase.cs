using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Mob_onGround_chase : MonoBehaviour
{

    
    public float moveSpeed = 3f; // Velocità di movimento del nemico
    public float chaseRange = 6f; // Distanza di rilevamento del giocatore
    // Start is called before the first frame update

    public Transform player; // Riferimento al giocatore
    public Vector3 initialPosition; // Indice del waypoint corrente
    public bool isChasing=false;
    public float distanceToInitialPosition ;
    public bool onGround = true;
    public Rigidbody2D rb;


    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToInitialPosition = Vector2.Distance(transform.position, initialPosition);


        // Se il giocatore è entro la distanza di rilevamento del nemico, inseguilo
        if (distanceToPlayer <= chaseRange && !isChasing)
        {
            isChasing = true;
        }
        else if(distanceToPlayer >= chaseRange && isChasing)
        {
            // Se il giocatore non è rilevato, continua a muoversi lungo i waypoint
            isChasing = false;
        }
        if (isChasing )
        {
            ChasePlayer();

        }
        if(!isChasing && distanceToInitialPosition>0.4f)
        {
            MoveToWaypoint();
        }
    }

    void ChasePlayer()
    {
        Vector2 distance = (player.position - transform.position);
        if (Mathf.Abs (distance.x) > 1f)
        {
            // Muovi il nemico nella direzione del giocatore
            if (player.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else if (player.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
       


    }

    // Movimento lungo i waypoint
    void MoveToWaypoint()
    {

        // Controlla se il nemico è arrivato al waypoint corrente

        // Muovi il nemico verso il waypoint corrente
        if (initialPosition.x > transform.position.x) { 
        Vector2 direction = (initialPosition - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 direction = ( transform.position- initialPosition).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            onGround = false;
        }
    }
}
