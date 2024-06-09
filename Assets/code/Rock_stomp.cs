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
    public Mob_patrol patrol;
    public Transformation_logic transformations;
    public Transformation_handler handler;
    public int hit = 0;

    public Vector2 initialColliderSize;
    public Vector2 initialColliderOffset;

    public AudioClip hitAudio;





    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        patrol = parent.GetComponent<Mob_patrol>();
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
        initialColliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        initialColliderOffset = gameObject.GetComponent<BoxCollider2D>().offset;
    }

   
    // Update is called once per frame
    void Update()
    {   
       
        //si adatta all'utlima forma del mob
        if (hit >= 2)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.41f,0.21f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.012f, 0.34f);

        }
        else {
            gameObject.GetComponent<BoxCollider2D>().size = initialColliderSize;
            gameObject.GetComponent<BoxCollider2D>().offset = initialColliderOffset;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player"))
        {
            SoundEffectManager.Instance.PlaySoundEffect(hitAudio, transform, 0.6f);
            hit++;
             if (!transformations.full && hit==3 && slime_form != null) 
             {
                AddTransformation();
                handler.ChangeForm();
            }


            if (collision.GetComponent<movement>().isSlamming)
            {
                collision.GetComponent<movement>().isSlamming = false;
            }
            parent.GetComponent<Animator>().SetBool("hit", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
            
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
