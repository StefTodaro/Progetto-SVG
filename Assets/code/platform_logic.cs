using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class platform_logic : MonoBehaviour
{
    public bool on=false;
    public Collider2D col;
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
        col.enabled = false;

        // Attendere per la durata specificata
        yield return new WaitForSeconds(0.5f);

        // Riabilita il collider della piattaforma
        col.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            on = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            on = false;
        }
    }

}