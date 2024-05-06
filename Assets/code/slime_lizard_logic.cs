using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_lizard_logic : MonoBehaviour
{
    public GameObject tongue;
    public tonguePivotLogic swing;
    public float pivotDistance=2.5f;
    public bool attached;
    public movement mov;

    void Start()
    {
        mov = GetComponent<movement>();
        
    }


    void Update()
    {
        //ricerca dei pivot circostanti
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pivotDistance);

        float minDistance = Mathf.Infinity;

        Collider2D nearestCollider = null;

        foreach (Collider2D collider in colliders)
        {
            if (!attached)
            {
                // Calcola la distanza tra il giocatore e l'oggetto
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                // Se l'oggetto è più vicino dell'oggetto attualmente più vicino, aggiornalo
                if (distance < minDistance && collider.CompareTag("pivot"))
                {
                    minDistance = distance;
                    nearestCollider = collider;
                }
                if (distance >= pivotDistance)
                {

                    swing.pivot = null;
                }

                if (nearestCollider != null)
                {
                    swing.pivot = nearestCollider.gameObject.transform;
                }
            }
        }

        //controlla in che direzione il giocatore si deve girare per osservare il pivot
        if (mov.facingRight)
        {
            tongue.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);
        }
        else
        {
            tongue.transform.position = new Vector2(gameObject.transform.position.x - 2, gameObject.transform.position.y);
        }

        if (swing.pivot==null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //inserire script nel caso la lingua sia un elemento separato dallo slime 

            }
        }//condizione per avviare il dondolio quando c'è un oggetto pivot vicino al personaggio
        else{

             if (Input.GetButtonDown("Fire1"))
            {
                swing.isSwinging = true;
                attached = true;

            }
           
        }

        if(Input.GetButtonUp("Fire1") && attached)
        {
            attached = false;
        }

    }

}
