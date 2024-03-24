using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_stomp : MonoBehaviour
{
    public float bounce = 6;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;
    public Rock_patrol patrol;
   




    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        patrol = parent.GetComponent<Rock_patrol>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   

            
             if (slime_form != null && patrol.hit==2)
             {
                 if (!collision.GetComponent<Transformation_handler>().transformed)
                 {
                     slime_form.transform.position = slime.transform.position;
                     slime.SetActive(false);
                     slime_form.SetActive(true);
                 }
             }
           
            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
            if (patrol.hit == 1)
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.42f, gameObject.GetComponent<BoxCollider2D>().size.y);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, 0.31f);
                //off y 0.31  size x 0.42

            }
        }


    }

 

  
}
