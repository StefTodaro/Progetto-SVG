using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_lizard_logic : MonoBehaviour
{
    public GameObject tongue;
    public tonguePivotLogic swing;
    public float pivotDistance=3.5f;
    public float rotationSpeed=3f; // Forza di rotazione
    public bool attached;



 

    void Start()
    {
        
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


        tongue.transform.position = new Vector2(gameObject.transform.position.x+2, gameObject.transform.position.y);


        if (swing.pivot==null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Tongue_stretch());

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


    IEnumerator Tongue_stretch()
    {
        
        tongue.SetActive(true);

        // Attendi per la durata specificata
        yield return new WaitForSeconds(0.3f);

        // Disattiva l'oggetto dopo il tempo specificato
        tongue.SetActive(false);
    }

    

}
