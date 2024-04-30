using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public float bounce=6;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;

    
  
  

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            if (slime_form != null)
            {
                if (!collision.GetComponent<Transformation_handler>().transformed)
                {
                    slime_form.transform.position = slime.transform.position;
                    slime.SetActive(false);
                    slime_form.SetActive(true);
                }

               
            }

            if (collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming = false;
                collision.GetComponent<movement>().canSlam = true;
            }

            //controllo per far effettuare un ulteriore salto allo slime_bird
            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                Debug.Log("UDIO");
                if (!collision.GetComponent<Slime_bird_double_jump>().canDoubleJump)
                {
                    collision.GetComponent<Slime_bird_double_jump>().canDoubleJump = true;
                    collision.GetComponent<Slime_bird_double_jump>().jumped = false;
                }
            }
            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
                

        }

    }

}
