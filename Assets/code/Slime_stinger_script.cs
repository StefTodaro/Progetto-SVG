using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_stinger_script : MonoBehaviour
{
    private float timer=0;

    public LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
           collision.gameObject.GetComponent<Animator>().SetBool("hit", true);
            Destroy(gameObject);
        }

        // Verifica se la collisione è avvenuta con un oggetto del layer desiderato
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            // Distruggi l'oggetto corrente
            Destroy(gameObject);
        }
    }
}
