using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
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
    public float backForce = 7.5f;
    public float jumpForce = 4;
    //gestisce le fasi della boss fight
    //phase[0]:prima fase
    //phase[1]:seconda fase
    //phase[2]:fuga
    //phase[3]:attesa
    //phase[4]:ultima fase
    public bool[] phase=new bool[5];
    //intero per tenere traccia delle fasi 
    public int p;
    //timer per che tiene conto del tempo in aria trascorso dal boss
    public float inAirTimer = 0;
    public float distanceToPlayer;
    //effetti
    public GameObject slashEffect;
    public GameObject landingEffect;
    public GameObject landingAttack;

    //variabile che indica se il boss è in fase di combattimento
    public bool isFighting = false;
    float waitTime = 0;

    float restartFight = 0;

    public Transform escapePoint;
    public Transform returnPoint;

    public BoxCollider2D hitBox;

    public GameObject blockDropper;

    public AudioClip landingSound;
    public AudioClip swordSound;
    
    //timer per far iniziare la vittoria 
    //dopo aver sconfitto il boss
    public float winTimer = 0;
    
  





    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        blockDropper = GameObject.Find("DropPoints");
     
        //si inizializza la prima fase 
        phase[0] = true;

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (isFighting)
        {
            Turn();
            Attack();
            Wallcheck();
        }

        anim.SetBool("onGround", isGrounded);
        
       //controllo per impedire che il boss rimanga incastrato
        if (!isGrounded)
        {
            inAirTimer += Time.deltaTime;
            if (inAirTimer >= 3f)
            {
                inAirTimer = 0f;
                isGrounded = true;
            }
        }
        else
        {
            inAirTimer = 0;
        }


        //si attiva la fuga del boss
        if (phase[2] && isGrounded)
        {
            isFighting = false;
            RunAway();
        }

        if (phase[5])
        {
            Death();
        }

    }

    //funzione per far girare il boss nella direzione del giocatore
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

    public void Attack()
    {

        if (distanceToPlayer <= 4f && canAttack)
        {

            anim.SetBool("attack", true);
        }
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 3.5)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
    }

    //funzione per controllare che il boss non sia bloccato contro un muro
    public bool Wallcheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, 2f, LayerMask.NameToLayer("ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetCurrentPhase()
    {
        return p;
    }

    public void Death()
    {
        isFighting = false;
        anim.speed = 1;
        if (isGrounded)
        {
            anim.SetBool("death", true);
            //si rende il corpo del boss inoffensivo
            gameObject.tag = "Untagged";
        }

        winTimer += Time.deltaTime;
        //si aspetta qualche secondo prima di terminare il livello
        if (winTimer >= 4f )
        {
            GameManager_logic.Instance.EndLevel();
        }
    }


    public void StartFight()
    {
        
        isFighting = true;
        StartCoroutine(RandomJump());
    }


    public void PassToNextPhase()
    {
        phase[p] = false;
        p += 1;
        phase[p] = true;
    }

    //funione per far crollare i blocchi
    public void MakeBlockFall()
    {

        //il numero di blocchi varia in base alla fase
        blockDropper.GetComponent<BossDrop>().Drop(1);
        blockDropper.GetComponent<BossDrop>().Drop(2);

        if (phase[4])
        {
            blockDropper.GetComponent<BossDrop>().Drop(3);
            blockDropper.GetComponent<BossDrop>().Drop(4);
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



    //funzione richiamata quando il boss viene colpito
    public void HitJump()
    {

        rb.AddForce(Vector2.up * backForce, ForceMode2D.Impulse);
       
            if (!Wallcheck())
            {
                rb.AddForce(transform.right * backForce*2 , ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(-transform.right * backForce * 2, ForceMode2D.Impulse);
            }
    }



    public void JumpToPlayer()
    { 
        Vector3 direction = (player.transform.position - transform.position).normalized;
        var distanceToPoint = Mathf.Abs(transform.position.x - player.transform.position.x);

        jumpForce=Random.Range(5.6f, 6.3f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(direction * distanceToPoint , ForceMode2D.Impulse);
    }


    public void RunAway()
    {
        
        waitTime += Time.deltaTime;
        if (waitTime >= 0.8f)
        {
            //si modifica il tag in modo che il colpisca il giocatore 
            gameObject.tag = ("Untagged");
            // mobs.GetComponent<BossMobsSpawn>().SpawnMobs();
            Vector3 direction = (escapePoint.position - transform.position).normalized;
            var distanceToPoint = Mathf.Abs(transform.position.x - escapePoint.position.x);


            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            rb.AddForce(direction * distanceToPoint, ForceMode2D.Impulse);
            hitBox.isTrigger = true;
            isFighting = false;
            waitTime = 0;
        }

    }

    //funzione per far tornare il boss in combattimento
    public void ReturnToBattle()
    {
        // mobs.GetComponent<BossMobsSpawn>().SpawnMobs();
        gameObject.transform.position = returnPoint.position;
        StartFight();
        
    }


    //funzione per resettare tutte le componenti principali del boss
    public void ResetBossFight()
    {
        rb.velocity = Vector2.zero;
        phase[p] = false;
        p = 0;
        phase[p] = true;

        isFighting = false;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            if (isFighting)
            {
                isGrounded = true;
                SoundEffectManager.Instance.PlaySoundEffect(landingSound, transform, 0.5f);
                Instantiate(landingEffect, new Vector2(transform.position.x + 0.5f, transform.position.y - 1f), landingEffect.transform.rotation);
            }

            //nell'ultima fase di combattimento attiva gli attacchi d'atterraggio
            if (phase[4] && isFighting && !collision.CompareTag("platform"))
            {   
                Instantiate(landingAttack, new Vector2(transform.position.x + 0.5f, transform.position.y - 0.8f), landingAttack.transform.rotation);
            }

            //quando attera c'è la possibilità  di un decimo che faccia cadere dei blocchi
            var dropchance = Random.Range(1, 10);
            if (dropchance == 1 && isFighting)
            {

                MakeBlockFall();
            }
        }
        //raggiunta la posizione di fuga torna tangibile 
        if (collision.gameObject.CompareTag("Trigger") && hitBox.isTrigger ==true)
        {
            hitBox.isTrigger = false;

            //raggiunta la posizione di fuga si attiva la fase di attesa 
            if (phase[2])
            {
                // si reimposta il tag 
                gameObject.tag = ("Mob");
                GameObject.Find("MobsSpawn").GetComponent<BossMobsSpawn>().SpawnMobs();
                PassToNextPhase();
            }

        }
        //distruzione degli oggetti che colpisce
        if (collision.CompareTag("Object"))
        {
            collision.GetComponent<Object_logic>().Break();
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
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


    public  IEnumerator RandomJump()
    {   
        //indica le probabilità che il boss esegua un'azione 
        while (isFighting)
        {   
            //indicano quale azione deve essere eseguita. Maggiore è la fase più
            //probabili saranno le azioni aggressive
            var action = Random.Range(0f, 1f);
            float probability=0 ;
            //tempo fra l'esecuzione di un'azione e l'altra
            float waitTime=0;

            //si modificano i parametri in base alla fase, tra cui la velocità delle animazioni
            if (phase[0])
            {
                probability = 0.65f;
                 waitTime = Random.Range(1.3f, 4);
                anim.speed = 1;
            }
            else if(phase[1]){
                probability = 0.50f;
                waitTime = Random.Range(1, 4);
                anim.speed = 1.4f;
            }else if (phase[4])
            {
                probability = 0.35f;
                waitTime = Random.Range(1, 3.5f);
                anim.speed = 1.7f;
            }

            yield return new WaitForSeconds(waitTime);


            if (isGrounded && !anim.GetBool("attack") && !anim.GetBool("hit")  && isFighting ) 
            {
                //si controlla prima qual è la distanza ideale per effettuare una determinata azione
                if (distanceToPlayer < 3.5f )
                {
                    if (!Wallcheck())
                    {
                        BackJump();
                    }
                    else
                    {
                        JumpToPlayer();
                    }
                }
                else if (distanceToPlayer >= 3.5f && distanceToPlayer < 6f)
                {
                    if (action < probability)
                    {
                        BackJump();
                    }
                    else
                    {
                        JumpToPlayer();
                    }
                } else if (distanceToPlayer >= 6f)
                {
                    JumpToPlayer();
                }

            }
        }
    }

   
    //funzione per instanziare il fendente d'aria 
    public void InstatiateSlash()
    {
        SoundEffectManager.Instance.PlaySoundEffect(swordSound, transform, 0.6f);
        if (facingRight) {
            Instantiate(slashEffect, new Vector2(transform.position.x + 2.5f, transform.position.y), transform.rotation);
        }
        else {
            Instantiate(slashEffect, new Vector2(transform.position.x - 2.5f, transform.position.y), transform.rotation);
        }
    }

    //controoli per l'animazioni d'attacco
    public void EndAttack()
    {
        anim.SetBool("attack", false);
        canAttack = false;
    }

    public void EndHit()
    {
        anim.SetBool("hit", false);

    }
}
