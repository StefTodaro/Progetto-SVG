using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard_logic : MonoBehaviour
{
    public Transform player; // Riferimento al giocatore
    public float visibilityRange = 6f; // Range di visibilità del nemico
    public float attackRange = 3f;
    public float fadeSpeed = 2f; // Velocità di dissolvenza dell'opacità

    public Animator anim;
    public bool canAttack;
    public float attackTimer;

    private Renderer enemyRenderer; // Riferimento al renderer del nemico
    private Color initialColor; // Colore iniziale del nemico
    private float initialAlpha; // Opacità iniziale del nemico
    public bool isVisible;
    public  Rigidbody2D rb;
    public Mob_onGround_chase chase;
    public bool hasJumped;

    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        initialColor = enemyRenderer.material.color;
        initialAlpha = initialColor.a;
        initialColor.a = 0; // Imposta l'opacità iniziale del colore del materiale
        enemyRenderer.material.color = initialColor; // Applica il colore iniziale al materiale
        chase = GetComponent<Mob_onGround_chase>();
        

        canAttack = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        RaycastHit2D hitl = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("ground"));
        RaycastHit2D hitr = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("ground"));
        




        // Controlla la visibilità del nemico in base alla distanza e al trigger di distanza
        if (distanceToPlayer <= visibilityRange)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }

        // Controlla la visibilità del nemico in base alla distanza e al trigger di distanza
        if (isVisible)
        {
            // Rendi gradualmente il nemico visibile
            FadeIn();
        }
        else
        {
            // Rendi il nemico invisibile
            FadeOut();
        }

        if (distanceToPlayer <= attackRange && canAttack  )
        {
            anim.SetBool("attack", true);
            canAttack = false;
        }

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
        }

        if (!canAttack && attackTimer>=1.5f)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if ((hitr.collider!=null || hitl.collider!=null) && chase.onGround && chase.isChasing)
        {
            Jump();
        }
      
        if(chase.onGround && hasJumped)
        {
            hasJumped = false;
        }

    }

    public void FadeIn()
    {
        Color newColor = initialColor;
        newColor.a = Mathf.MoveTowards(enemyRenderer.material.color.a, initialAlpha, fadeSpeed * Time.deltaTime);
        enemyRenderer.material.color = newColor;
    }

    // Rendi il nemico invisibile
    public void FadeOut()
    {
        Color newColor = initialColor;
        newColor.a = Mathf.MoveTowards(enemyRenderer.material.color.a, 0, fadeSpeed * Time.deltaTime);
        enemyRenderer.material.color = newColor;
    }

    public void SetAttackFalse()
    {
        anim.SetBool("attack", false);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        hasJumped = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer("ground") && !hasJumped)
            {

                Jump();
            }
    }



}
