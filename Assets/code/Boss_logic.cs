using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_logic : MonoBehaviour
{
    public GameObject player;
    public bool facingRight;
    public bool isGrounded;
    public Rigidbody2D rb;
    public Animator anim;

    public bool canAttack = true;
    public float attackTimer;

    public float backForce=5;
    public float jumpForce=4;
    public float minJumpForce = 2;
    public float maxJumpForce = 6;
    public float minJumpHorizontalForce = 500;
    public float maxJumpHorizontalForce = 1500;
    public float minDistance = 2.5f;
    public float maxDistance = 100f;

    public float distanceToPlayer;




    // Start is called before the first frame update
    void Start()
    {
       anim=GetComponent<Animator>();
       rb=GetComponent<Rigidbody2D>();
       StartCoroutine(RandomBackJump());
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);

        Turn();

        anim.SetBool("onGround", isGrounded);
        if (distanceToPlayer < 3f && canAttack)
        {
            
            anim.SetBool("attack", true);
        }

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 2)
            {
                canAttack = true;
                attackTimer = 0;    
            }
        }


    }

    public void Turn()
    {
        if (transform.position.x > player.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void BackJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (facingRight)
        {
            rb.AddForce(Vector2.left * backForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.right * backForce, ForceMode2D.Impulse);
        }
    }

    public void HitJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (facingRight)
        {
            rb.AddForce(Vector2.left * backForce *2, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.right * backForce*2, ForceMode2D.Impulse);
        }
    }

    public void JumpToPlayer()
    {
        if (!facingRight)
        {
            rb.AddForce(new Vector2(-distanceToPlayer, 5.3f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(distanceToPlayer, 5.3f), ForceMode2D.Impulse);

        }
    }

    public void EndAttack()
    {
        anim.SetBool("attack", false);
        canAttack = false;
    }

    public void EndHit()
    {
        anim.SetBool("hit", false);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator RandomBackJump()
    {
        while (true)
        {
            float waitTime = Random.Range(1, 4);
            yield return new WaitForSeconds(waitTime);

            if (isGrounded && !anim.GetBool("attack") && !anim.GetBool("hit")) // Salta solo se è a terra
            {   if(distanceToPlayer<3.5f )
                {
                    BackJump();
                }
                else if(distanceToPlayer>=3.5f && distanceToPlayer <6f)
                {
                    var action = Random.Range(0f, 1f);

                    if(action < 0.40f)
                    {
                        BackJump();
                    }
                    else
                    {
                        JumpToPlayer();
                    }
                }else if (distanceToPlayer >= 6f)
                {
                    JumpToPlayer();
                }

            }
        }
    }
}
