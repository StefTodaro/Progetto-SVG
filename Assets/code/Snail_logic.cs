using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_logic : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
   // public Mob_patrol patrol;
    public bool hide=false;
    public float distance=3f;
    public Transform player;
    public float originaleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       // patrol = GetComponent<Mob_patrol>();
        rb = GetComponent<Rigidbody2D>();
        
       // originaleSpeed = patrol.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToTarget = Vector2.Distance(transform.position, player.position);
        
        anim.SetBool("hide", hide);

        if (distanceToTarget<=distance && !hide)
        {
            //patrol.moveSpeed = 0;
            hide = true;
        }
        else if (distanceToTarget > distance && hide)
        {
            //patrol.moveSpeed = originaleSpeed;
            hide = false;
        }

        //se la lumaca è nascosta allora non si muove
        if (hide)
        {
            if (GetComponent<Mob_patrol>() != null)
            {
                GetComponent<Mob_patrol>().isPatrolling = false;
            }
        }
        else
        {

            if (GetComponent<Mob_patrol>() != null)
            {
                GetComponent<Mob_patrol>().isPatrolling = true;
            }
        }

    }
}
