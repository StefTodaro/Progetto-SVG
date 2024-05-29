using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossStomp : MonoBehaviour
{
    public float bounce = 6;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        parent.GetComponent<Boss_logic>().HitJump();
        parent.GetComponent<Animator>().SetBool("hit", true);
    }

  

    void OnTriggerEnter2D(Collider2D collision)
    {
        //handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player"))
        {
        /*
            if (!transformations.full && slime_form != null)
            {
                AddTransformation();
                handler.ChangeForm();
            }*/

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


            Hit();
            if (collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming = false;
            }

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
        }
    }
}
