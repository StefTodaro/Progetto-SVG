using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class platform_logic : MonoBehaviour
{
    public bool on=false;
    public GameObject player;
    public Collider2D col;
    //oggetto che serve per far tornare le trasformazioni nella loro collezione iniziale
    public Transform originalParent;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //se il giocatore va in basso mentre si trova sulla piattaforma il collider si annulla per un breve lasso di tempo
        if(on && Input.GetKeyDown(KeyCode.S))
        {   
            StartCoroutine(DisablePlatformCollider());
        }

    }

    private IEnumerator DisablePlatformCollider()
    {
        // Disabilita il collider della piattaforma

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);

        // Attendere per la durata specificata
        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            on = true;
            player=collision.gameObject;
            originalParent= collision.transform.parent;
            collision.transform.SetParent(transform);
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            on = false;
            collision.transform.SetParent(originalParent);
        }
    }

}
