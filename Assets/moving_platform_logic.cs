using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform_logic : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float moveSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Muovi il nemico verso il punto di pattuglia corrente
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);
        ChangePatrolPoint();

    }

    private void ChangePatrolPoint()
    {
        // Controlla se il nemico è arrivato al punto di pattuglia corrente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
        {
            currentPatrolIndex += 1;

            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }

        }
    }
    }
