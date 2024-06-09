
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_patrol : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocità di movimento del nemico
    public Transform[] patrolPoints; // Punti di pattuglia che il nemico deve seguire
    private int currentPatrolIndex = 0; // Indice del punto di pattuglia corrente
    // Flag per tenere traccia della direzione di movimento
    //da settare in base allo sprite iniziale
    public bool movingRight = false;
    public bool isPatrolling = true;
    public Animator anim;

    //si estrapolano le rotazioni iniziali
    private float rotationx;
    private float rotationy;
    private float rotationz;


    // Start is called before the first frame update
    void Start()
    {
        rotationx = transform.rotation.eulerAngles.x;
        rotationy = transform.rotation.eulerAngles.y;
        rotationz = transform.rotation.eulerAngles.z;
        anim = gameObject.GetComponent<Animator>();
        currentPatrolIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {

        ChangePatrolPoint();

        if (isPatrolling)
        {

            SetDirection();

            anim.SetFloat("speed", moveSpeed);
            // Muovi il nemico verso il punto di pattuglia corrente
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);
        }



    }

    private void ChangePatrolPoint()
    {
        // Controlla se il nemico è arrivato al punto di pattuglia corrente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
        {
            currentPatrolIndex += 1;

            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }

        }
    }


    private void SetDirection()
    {//controlla se il mob si è avvicinato  al punto di arrivo per poi cambiarne la direzione
        if (gameObject.transform.position.x <= patrolPoints[currentPatrolIndex].position.x)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }
        RoteateMob();
    }

    //rotea il mob in base alla direzione
    private void RoteateMob()
    {
        //per far girare  il mob nella direzione di movimento
        //si controlla prima se sia il mob Snail che ha un comportamento separato
        if (!GetComponent<Snail_logic>())
        {
            if (movingRight)
            {
                transform.rotation = Quaternion.Euler(rotationx, 180, rotationz);
            }
            else
            {
                transform.rotation = Quaternion.Euler(rotationx, 0, rotationz);
            }
        }
        else
        {
            if (movingRight)
            {
                transform.rotation = Quaternion.Euler(0, rotationy, rotationz);
            }
            else
            {
                transform.rotation = Quaternion.Euler(180, rotationy, rotationz);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //per fare in modo che i nemici non collidano tra loro nel movimento
        if (collision.gameObject.CompareTag("Mob") && collision.gameObject.CompareTag("coin"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>()); ;
        }
    }
}

