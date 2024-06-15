using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_stinger_script : MonoBehaviour
{
    private float timer=0;

    public GameObject hitEffect;

    public LayerMask targetLayer;

    public AudioClip hitAudio;
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
        {   // controlla che il pungiglione non abbia colpito una lumaca (immune)
            if (!collision.GetComponent<Snail_logic>())
            {
                //controllo per impedire a certi nemici di inseguire il giocatore 
                //dopo essere stati colpito
                if (collision.GetComponent<Mob_chase>())
                {
                    collision.GetComponent<Mob_chase>().canChase = false;
                }

                collision.gameObject.GetComponent<Animator>().SetBool("hit", true);
                //attiva la funzione di contraccolpo del boss
                if (collision.GetComponent<Boss_logic>())
                {
                    collision.GetComponent<Boss_logic>().HitJump();
                }
                else
                {   //se colpisce un mob roccia gli aumenta il counter dei colpi
                    if (collision.GetComponent<Rock_logic>())
                    {
                        collision.GetComponent<Rock_logic>().hit.hit += 1;
                    }
                    collision.gameObject.GetComponent<Animator>().SetBool("hit", true);
                }
               
               
            }
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Verifica se la collisione è avvenuta con un oggetto del layer desiderato 
        //e controlla che non sia una piattaforma
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground") && !collision.gameObject.CompareTag("platform"))
        {
            // Distruggi l'oggetto corrente
            Instantiate(hitEffect, transform.position, transform.rotation);
            SoundEffectManager.Instance.PlaySoundEffect(hitAudio, transform, 0.55f);
            Destroy(gameObject);
        }
    }
}
