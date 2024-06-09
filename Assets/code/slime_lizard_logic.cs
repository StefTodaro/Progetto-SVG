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
    public Animator anim;

    public AudioClip tonguesound;

    void Start()
    {
        mov = GetComponent<movement>();
    }

    void OnEnable()
    {   
        
        mov = GetComponent<movement>();
        swing.isSwinging = false;
        attached = false;
        swing.pivot = null;
        anim = gameObject.GetComponent<Animator>();
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

        if (swing.pivot!=null)
        {

             if (Input.GetMouseButtonDown(0))
            {
                swing.isSwinging = true;
                attached = true;
                SoundEffectManager.Instance.PlaySoundEffect(tonguesound, transform, 0.4f);
                anim.SetBool("attached", true);

            }

        }

        if(Input.GetMouseButtonUp(0) && attached)
        {
            attached = false;
            anim.SetBool("attached", false);
        }

    }

}
