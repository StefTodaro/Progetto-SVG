using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class Snail_stomp : MonoBehaviour
{

    public float bounce = 6;
    public GameObject parent;
    public GameObject slime;
    public GameObject slime_form;

    public movement mov;
    public GameObject player;
    public Snail_logic snail;
    public Animator anim;
    public bool hasSlammed;

    public Transformation_logic transformations;
    public Transformation_handler handler;

    public AudioClip hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        snail = GetComponentInParent<Snail_logic>();
        anim = GetComponentInParent<Animator>();
        parent = gameObject.transform.parent.gameObject;

        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
    }

    // Update is called once per frame
    void Update()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        mov = player.GetComponent<movement>();

        if (mov.isSlamming == true)
        {
            hasSlammed = true;
        }
        if (mov.isGrounded && hasSlammed)
        {
            hasSlammed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handler = collision.GetComponent<Transformation_handler>();
        if (collision.CompareTag("Player"))
        {
            SoundEffectManager.Instance.PlaySoundEffect(hitAudio, transform, 0.6f);
            if (hasSlammed)
            {
                if (!transformations.full && slime_form != null)
                {
                    AddTransformation();
                    handler.ChangeForm();
                }

                parent.GetComponent<Animator>().SetBool("hit", true);
                hasSlammed = false;

            }

            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                if (!collision.GetComponent<Slime_bird_double_jump>().canDoubleJump)
                {
                    collision.GetComponent<Slime_bird_double_jump>().canDoubleJump = true;
                    collision.GetComponent<Slime_bird_double_jump>().jumped = false;
                }
            }

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);
            collision.gameObject.GetComponent<movement>().isSlamming = false;

           

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
