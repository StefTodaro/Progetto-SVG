using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_patrol : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocità di movimento del nemico
    public Transform[] patrolPoints; // Punti di pattuglia che il nemico deve seguire
    private int currentPatrolIndex = 0; // Indice del punto di pattuglia corrente
    // Flag per tenere traccia della direzione di movimento
    //da settare in base allo sprite iniziale
    private bool movingRight = false; 
    public bool isPatrolling=true;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        currentPatrolIndex = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            // Controlla se il nemico è arrivato al punto di pattuglia corrente
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
            {
                currentPatrolIndex+=1;
                
                if(currentPatrolIndex>= patrolPoints.Length)
                {
                    currentPatrolIndex = 0;
                }

                if (gameObject.transform.position.x<= patrolPoints[currentPatrolIndex].position.x)
                {
                    movingRight = true;
                }
                else
                {
                    movingRight = false;
                }

                // Inverti la scala sull'asse X per girare lo sprite nella direzione corretta
                anim.SetFloat("speed", moveSpeed);
                gameObject.GetComponent<SpriteRenderer>().flipX = movingRight;
            }

            // Muovi il nemico verso il punto di pattuglia corrente
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);
        }
    }
}
