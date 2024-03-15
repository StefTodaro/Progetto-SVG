using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocità di movimento del nemico
    public Transform[] patrolPoints; // Punti di pattuglia che il nemico deve seguire
    private int currentPatrolIndex = 0; // Indice del punto di pattuglia corrente
    private bool movingForward = true; // Flag per tenere traccia della direzione di movimento


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Controlla se il nemico è arrivato al punto di pattuglia corrente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            // Se il nemico è arrivato all'ultimo punto di pattuglia, inverti la direzione
            if (currentPatrolIndex == patrolPoints.Length - 1)
            {
                movingForward = false;
            }
            // Se il nemico è arrivato al primo punto di pattuglia, torna a muoversi in avanti
            else if (currentPatrolIndex == 0)
            {
                movingForward = true;
            }

            // Incrementa o decrementa l'indice del punto di pattuglia in base alla direzione di movimento
            currentPatrolIndex += movingForward ? 1 : -1;
        }

        // Muovi il nemico verso il punto di pattuglia corrente
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);

    }
}
