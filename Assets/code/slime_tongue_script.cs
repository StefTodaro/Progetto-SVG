using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_tongue_script : MonoBehaviour
{
    public GameObject player;
    public float dragSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            Vector3 directionToPlayer = player.transform.position - collision.transform.position;

            // Normalizza la direzione per avere una velocità costante
            directionToPlayer.Normalize();

            // Sposta l'oggetto nella direzione del giocatore con una velocità costante
            collision.gameObject.transform.Translate(directionToPlayer * dragSpeed);
        }
    }
}

    
