using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_logic : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public Mob_patrol mob;
    public bool hide=false;
    public float distance=3f;
    public Transform player;
    public float originaleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mob = GetComponent<Mob_patrol>();
        rb = GetComponent<Rigidbody2D>();
        
        originaleSpeed = mob.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToTarget = Vector2.Distance(transform.position, player.position);
        
        anim.SetBool("hide", hide);
        if (distanceToTarget<=distance && !hide)
        {
            mob.moveSpeed = 0;
            hide = true;
        }
        else if (distanceToTarget > distance && hide)
        {
            mob.moveSpeed = originaleSpeed;
            hide = false;
        }

    }
}
