using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_logic : MonoBehaviour
{

    public Rock_stomp hit;
    public Mob_patrol patrol;
    public float miniSpeed = 5.4f;
    public float originalSpeed;
    public Vector2 initialColliderSize;
    public Vector2 initialColliderOffset;
    // Start is called before the first frame update
    void Start()
    {
      patrol = gameObject.GetComponent<Mob_patrol>();
      hit= gameObject.GetComponentInChildren<Rock_stomp>();
      originalSpeed= patrol.moveSpeed;
      initialColliderSize = gameObject.GetComponent<BoxCollider2D>().size;
      initialColliderOffset = gameObject.GetComponent<BoxCollider2D>().offset;

    }

   

    // Update is called once per frame
    void Update()
    {

       
        if (hit.hit >= 1 && !patrol.isPatrolling)
        {
            patrol.isPatrolling = true;
        }

        if (hit.hit >= 2)
        {
            patrol.moveSpeed = miniSpeed;
            //dimensioni per l'utlima forma del mob rock
           gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.57f, 0.32f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.015f, -0.015f);

        }
        else
        {
            patrol.moveSpeed = originalSpeed;
            gameObject.GetComponent<BoxCollider2D>().size = initialColliderSize;
            gameObject.GetComponent<BoxCollider2D>().offset = initialColliderOffset;
        }

        /*
                if (hit == 1)
                {
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.42f, gameObject.GetComponent<BoxCollider2D>().size.y);
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, 0.31f);

                }*/

    }

    private void SetNewHit()
    {

       patrol.anim.SetBool("hit", false);
    }
    //funzione utile per far tornare il mob nella prima forma dopo il respawn
    private void SetRespawnFalse()
    {
        if (patrol.anim.GetBool("respawn")){
            patrol.anim.SetBool("respawn", false);
        }
    }

    }
