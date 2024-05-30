using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public float bounce=6;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;
    public Transformation_logic transformations;
    public Transformation_handler handler;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player"))
        {
          
            if (!transformations.full && slime_form!=null)
            {
                    AddTransformation();
                    handler.ChangeForm();
            }

            if (collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming = false;
                collision.GetComponent<movement>().canSlam = true;
            }

            //controllo per far effettuare un ulteriore salto allo slime_bird
            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                if (!collision.GetComponent<Slime_bird_double_jump>().canDoubleJump)
                {
                    collision.GetComponent<Slime_bird_double_jump>().canDoubleJump = true;
                    collision.GetComponent<Slime_bird_double_jump>().jumped = false;
                }
            }

            parent.GetComponent<Animator>().SetBool("hit", true);
            if(collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming=false;
            }

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);            
        }
    }

    public void AddTransformation()
    {
       //controlla che la trasformazione non sia già contenuta nella lista delle trasformazioni 
            if (!transformations.transformations.Contains(slime_form))
            {
            //cerca la prima posizione in cui è possibile inserire la trasformazione appena ottenuta
            for (int i = 0; i < 3; i++)
            {
                if (transformations.transformations[i] == transformations.baseSlime)
                {
                    //trasforma il giocatore nella forma appena ottenuta
                    transformations.c = i;
                    transformations.transformations[transformations.c] = slime_form;
                    break;
                }
            }
            transformations.t += 1;
            }
        }
}

