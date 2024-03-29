using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_patrol : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocit� di movimento del nemico
    public Transform[] patrolPoints; // Punti di pattuglia che il nemico deve seguire
    private int currentPatrolIndex = 0; // Indice del punto di pattuglia corrente
    private bool movingForward = true; // Flag per tenere traccia della direzione di movimento
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        // Controlla se il nemico � arrivato al punto di pattuglia corrente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            // Se il nemico � arrivato all'ultimo punto di pattuglia, inverti la direzione
            if (currentPatrolIndex == patrolPoints.Length - 1)
            {
                movingForward = false;
            }
            // Se il nemico � arrivato al primo punto di pattuglia, torna a muoversi in avanti
            else if (currentPatrolIndex == 0)
            {
                movingForward = true;
            }
            currentPatrolIndex += movingForward ? 1 : -1;

            // Inverti la scala sull'asse X per girare lo sprite nella direzione corretta
            anim.SetFloat("speed", moveSpeed);
            gameObject.GetComponent<SpriteRenderer>().flipX = movingForward;
        }

        // Muovi il nemico verso il punto di pattuglia corrente
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);

    }
}
