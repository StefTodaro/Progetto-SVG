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

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {   //altezza del giocatore rispetto al mob
        float playerRelativeY = player.transform.position.y - transform.position.y;

        //distanza reltiva dal mob
        float distance = Vector2.Distance(transform.position, player.transform.position);
       

        if (distance <range)
        {

            if (playerRelativeY < 0)
            {
                timer += Time.deltaTime;

                if (timer > 2.5f)
                {
                    timer = 0;
                    RotateTowardsTarget();
                    anim.SetBool("shoot", true);
                }
            }

        }
        else
        {
             transform.rotation = Quaternion.identity;
        }
         
       
        
    }

    public void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        
    }

    public void reload()
    {
        // Instantiate(bullet, bulletPos.position, Quaternion.identity);
        anim.SetBool("shoot", false);
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
