using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        snail = GetComponentInParent<Snail_logic>();
        anim = GetComponentInParent<Animator>();
        parent = gameObject.transform.parent.gameObject;
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
        if (collision.CompareTag("Player"))
        {

            if (slime_form != null && hasSlammed)
            {
                if (!collision.GetComponent<Transformation_handler>().transformed)
                {
                    slime_form.transform.position = slime.transform.position;
                    slime.SetActive(false);
                    slime_form.SetActive(true);
                }

                parent.GetComponent<Animator>().SetBool("hit", true);
                hasSlammed = false;

            }

            if (collision.GetComponent<Slime_bird_double_jump>())
            {
                Debug.Log("UDIO");
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
}
