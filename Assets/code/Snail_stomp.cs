using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class Snail_stomp : MonoBehaviour
{

    public float bounce = 6;
    public GameObject slime;
    public GameObject slime_form;

 
    public Animator anim;

    public Transformation_logic transformations;
    public Transformation_handler handler;

    public AudioClip hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player"))
        {

            SoundEffectManager.Instance.PlaySoundEffect(hitAudio, transform, 0.6f);
            if (collision.GetComponent<movement>().isSlamming)
            {
               
                collision.GetComponent<movement>().isSlamming = false;
               

                if (!transformations.full && slime_form != null)
                {
                    AddTransformation();
                    transformations.ChangeForm(transformations.GetCurrentTransformation());
                }

                collision.GetComponent<movement>().canSlam = true;

                anim.SetBool("hit", true);

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
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);


    }
    public void AddTransformation()
    {
        //controlla che la trasformazione non sia già contenuta nella lista delle trasformazioni 
        if (!transformations.ContainsTransformation(slime_form))
        {
            //cerca la prima posizione in cui è possibile inserire la trasformazione appena ottenuta
            for (int i = 0; i < 3; i++)
            {
                if (transformations.transformations[i].transformation == transformations.baseSlime)
                {
                    //trasforma il giocatore nella forma appena ottenuta
                    transformations.c = i;
                    transformations.SetCurrentTransformation(slime_form);
                    break;
                }
            }
            transformations.t += 1;
        }
    }
}
