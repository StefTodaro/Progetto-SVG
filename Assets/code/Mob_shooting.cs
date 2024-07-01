using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject player;
    private Animator anim;
    public float range=6.5f;
    public float maxAngle = 0;
    public float minAngle =- 120;

    public AudioClip shotAudio;

    //tempo necessario per il mob per sparare il primo colpo
    public float startShootTimer=0.65f;
    //tempo che intercorre tra uno sparo e l'altro 
    private float shootTimer=1.75f;
    private bool canShoot=true;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //altezza del giocatore rispetto al mob
        float playerRelativeY = player.transform.position.y - transform.position.y;

        //distanza reltiva dal mob
        float distance = Vector2.Distance(transform.position, player.transform.position);
       
        //se il giocatore è in range ed non si trova sopra il mob
        if (distance <range)
        {  
            if (playerRelativeY < 0 )
            {
                startShootTimer-=Time.deltaTime;
                
                if (canShoot  && startShootTimer<=0)
                {
                    RotateTowardsTarget();
                    anim.SetBool("shoot", true);
                    canShoot = false;                    
                }
            }

        }
        else
        {
            
            //si resetta il timer per iniziare a sparare
            if (startShootTimer <= 0)
            {
                startShootTimer = 0.65f;
            }
        }

        //tempo fra un colpo e l'altro
        if (!canShoot)
        {   
            shootTimer -= Time.deltaTime;

            if (shootTimer <=0f)
            {
                shootTimer = 1.75f;
                canShoot = true;

            }
        }
         
       
        
    }

    public void Shoot()
    {
        SoundEffectManager.Instance.PlaySoundEffect(shotAudio, transform, 0.4f);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        
    }

    public void Reload()
    {
       
        anim.SetBool("shoot", false);
        //si resetta la rotazione del mob dopo lo sparo
        transform.rotation = Quaternion.identity;


    }

    private void RotateTowardsTarget()
    {
        
        Vector2 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.Normalize();

        // Calcola l'angolo di rotazione in base alla direzione del giocatore
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        var offeset = 90f;


        // Verifica se l'angolo verso il giocatore è all'interno del range
        if (angleToPlayer > minAngle && angleToPlayer < maxAngle)
        {
            // L'angolo verso il giocatore è all'interno del range, quindi gira verso di lui
            transform.rotation = Quaternion.Euler(Vector3.forward * (angleToPlayer + offeset));
        }
    }
    }
