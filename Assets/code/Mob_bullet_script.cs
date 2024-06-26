using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_bullet_script : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public float timer=0 ;
    public LayerMask targetLayer;

    public GameObject hitEffect;

    public AudioClip hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");


        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;


        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot-90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Verifica se la collisione è avvenuta con un oggetto del layer desiderato
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.layer == LayerMask.NameToLayer("ground") && collision.gameObject.tag!="platform"))
        {
            // Distruggi l'oggetto corrente
            SoundEffectManager.Instance.PlaySoundEffect(hitAudio, transform, 0.6f);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
