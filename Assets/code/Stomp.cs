using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public float bounce=6;
    public Rigidbody2D rb;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_bee;
    
  
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(rb.velocity.x, bounce);

            

            if (!collision.GetComponent<movement>().getTransformed())
            {
                
                //Se lo slime si trova sul nemico, cambia aspetto e acquisisce un'abilità
                slime_bee.transform.position = slime.transform.position;
                slime_bee.GetComponent<movement>().SetTransformed(true);

                slime.SetActive(false);

                slime_bee.SetActive(true);
            }


        }

    }

}
