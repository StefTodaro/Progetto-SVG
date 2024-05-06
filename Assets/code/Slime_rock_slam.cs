using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_rock_slam : MonoBehaviour
{

    private movement mov;
    private Rigidbody2D rb;
    private float rockSlamForce = 11f;
    // Start is called before the first frame update
    void Start()
    {
        mov = gameObject.GetComponent<movement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.isSlamming)
        {
            StartCoroutine(Slam());
        }
        
    }

    IEnumerator Slam()
    {
        while (!mov.isGrounded)
        {
            // Continua la discesa fino a quando il personaggio non tocca il terreno


            rb.velocity = Vector2.down * rockSlamForce;
            yield return null;
        }

        mov.isSlamming = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (mov.isSlamming)
        {
            if (collision.gameObject.CompareTag("Obstacles"))
            {
                
                Destroy(collision.gameObject);
                mov.isGrounded = false;
                mov.isSlamming = true;
            }

        }

    }
}
