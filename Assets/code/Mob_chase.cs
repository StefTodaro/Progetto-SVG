using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinding;
using UnityEngine;

public class Mob_chase : MonoBehaviour
{
    public Transform player; // Il transform del personaggio da inseguire
    //public float chaseDistance = 7.5f; // La distanza a cui il nemico inizia ad inseguire il personaggio
   public float moveSpeed = 3f; // La velocità di movimento del nemico
    private AIPath path;
    public float activationDistance = 5f;
    private Vector3 initialPosition;



    void Start()
    {
        path = GetComponent<AIPath>();
        initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToTarget = Vector2.Distance(transform.position, player.position);

        if (distanceToTarget <= activationDistance)
        {
            // Calcola il percorso
            path.maxSpeed = moveSpeed;
            path.destination = player.position;
        }else if(distanceToTarget > activationDistance && gameObject.transform.position!= initialPosition)
        {
            path.destination = initialPosition;
        }else if(distanceToTarget > activationDistance && gameObject.transform.position == initialPosition)
        {
            path.destination = Vector3.zero;
        }

        if (path.desiredVelocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;  // Orienta lo sprite verso destra
        }
        else if (path.desiredVelocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Orienta lo sprite verso sinistra
        }
    }

}


