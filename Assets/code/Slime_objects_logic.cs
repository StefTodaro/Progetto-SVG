using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_objects_logic : MonoBehaviour
{
    public float detectionRadius = 0.7f;
    //oggetti inglobati dal giocatore
    public GameObject inObject;
    public float incorporateTimer;
    public float incorporateTime = 0.8f;
    public bool canIncorporate = true;
    




    // Start is called before the first frame update
    void Start()
    {
       
        incorporateTimer = incorporateTime;
        inObject = GameObject.FindGameObjectWithTag("inObjects");
    }

    
    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        float minDistance = Mathf.Infinity;
        Collider2D nearestCollider = null;

        // Trova l'oggetto più vicino
        foreach (Collider2D collider in colliders)
        {
            // Calcola la distanza tra il giocatore e l'oggetto
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            // Se l'oggetto è più vicino dell'oggetto attualmente più vicino, aggiornalo
            if (distance < minDistance && collider.CompareTag("Object") && 
                inObject.GetComponent<Incorporated_objects_list>().list.Count<=6)
            {
                minDistance = distance;
                nearestCollider = collider;
            }
        }

        // Interagisci solo con l'oggetto più vicino se è stato trovato e il giocatore preme il tasto
        if (nearestCollider != null && Input.GetButtonDown("Fire2") && canIncorporate)
        { 
            inObject.GetComponent<Incorporated_objects_list>().list.Add(nearestCollider.gameObject);
            nearestCollider.gameObject.SetActive(false);
        }
        if (canIncorporate == false)
        {
            incorporateTimer -= Time.deltaTime;
        }
        if (incorporateTimer <= 0)
        {
            canIncorporate = true;
            incorporateTimer = incorporateTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            canIncorporate = false;
            inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count - 1].SetActive(true);

            // Posiziona l'oggetto sotto i piedi del giocatore
            Vector3 playerFeetPosition = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y, 0f);
            inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count -1].transform.position = playerFeetPosition;
            inObject.GetComponent<Incorporated_objects_list>().list.Remove(inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count - 1]);

           
        }
    }

    void OnDrawGizmosSelected()
    {
        // Disegna la zona di rilevamento per debug
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
