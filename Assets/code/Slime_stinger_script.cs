using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_stinger_script : MonoBehaviour
{
    private float timer=0;

    public GameObject hitEffect;

    public LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            if (!collision.GetComponent<Snail_logic>())
            {
                collision.gameObject.GetComponent<Animator>().SetBool("hit", true);
                if (collision.GetComponent<Boss_logic>())
                {
                    collision.GetComponent<Boss_logic>().HitJump();
                }
                else
                {
                    collision.gameObject.GetComponent<Animator>().SetBool("hit", true);
                }
               
            }
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Verifica se la collisione è avvenuta con un oggetto del layer desiderato
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground") && !collision.gameObject.CompareTag("platform"))
        {
            // Distruggi l'oggetto corrente
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
