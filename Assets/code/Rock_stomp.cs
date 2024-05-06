using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rock_stomp : MonoBehaviour
{
    public float bounce = 6;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;
    public Rock_patrol patrol;
    public Transformation_logic transformations;
    public Transformation_handler handler;





    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        patrol = parent.GetComponent<Rock_patrol>();
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

            
             if (!transformations.full && patrol.hit==2)
             {
                    AddTransformation();
                    handler.ChangeForm();
            }
           
            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
            if (patrol.hit == 1)
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.42f, gameObject.GetComponent<BoxCollider2D>().size.y);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, 0.31f);
                //off y 0.31  size x 0.42

            }

            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                if (!collision.GetComponent<Slime_bird_double_jump>().canDoubleJump)
                {
                    collision.GetComponent<Slime_bird_double_jump>().canDoubleJump = true;
                    collision.GetComponent<Slime_bird_double_jump>().jumped = false;
                }
            }
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
