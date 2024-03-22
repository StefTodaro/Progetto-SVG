using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public float bounce=6;
    public Rigidbody2D rb;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;
    
  
  

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

            if (!collision.GetComponent<Transformation_handler>().transformed)
            {
                slime_form.transform.position = slime.transform.position;
                slime.SetActive(false);
                slime_form.SetActive(true);
            }

            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(rb.velocity.x, bounce);

            

         


        }

    }

}
