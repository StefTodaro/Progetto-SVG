using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public Transform[] waypoints; // I punti del percorso
    private Transform player; 
    private int currentWaypointIndex = 0; 
    public float moveSpeed = 2f; 
    private bool isMoving = false; 
    private float originalZ; // La coordinata Z originale del personaggio
    private bool facingRight = true;
    private Animator animator;
    private StatusLevel SL;
    
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalZ = player.position.z;
        animator= player.GetComponent<Animator>();

        player.position = new Vector3(waypoints[0].position.x, waypoints[0].position.y, originalZ);
        SL = FindObjectOfType<StatusLevel>();
        List<StatusLevel.Level> levels = SL.levels;
        
        
    }

    void Update()
    {
        //Forse conviene fare funzione?
        //Gestione del movimento del personaggio all'interno della mappa di selezione livelli
        if (!isMoving)
        {
            // Gestisci input per movimento tra i waypoints
            if ((Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S)) && currentWaypointIndex == 0)
            {
                MoveToNextWaypoint(1);
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D)) && currentWaypointIndex == 1)
            {
               //Bloccare??? DA 1 A 2 (wayPoint intermedio per raggiungere il 3)
                    MoveToNextWaypoint(1);
                    if (!facingRight) rotatePlayer();
                
                
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentWaypointIndex == 2)
            {
                MoveToPreviousWaypoint(1);
                if (facingRight) rotatePlayer();
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentWaypointIndex == 1)
            {
                MoveToPreviousWaypoint(1);
            }
            else if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentWaypointIndex == 2)
            {
                if (SL.isLevelEnabled(3))
                {
                    MoveToNextWaypoint(1);
                }
            }
            else if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentWaypointIndex == 3)
            {
                MoveToPreviousWaypoint(1);
            }
            else if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentWaypointIndex == 3){
                if (SL.isLevelEnabled(4))
                {
                    MoveToNextWaypoint(1);
                }
            }
            else if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentWaypointIndex == 4){
                MoveToPreviousWaypoint(1);
                
            }
            else if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && currentWaypointIndex == 2)
            {
                if (SL.isLevelEnabled(5))
                {
                    MoveToNextWaypoint(3);
                    if (!facingRight) rotatePlayer();
                }
            }
            else if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentWaypointIndex == 5)
            {
                MoveToPreviousWaypoint(3);
                if (facingRight) rotatePlayer();
            }
            
        }
        else
        {
            MoveTowardsWaypoint();
        }
    }

    //Funzione per ruotare il personaggio quando torna indietro
    void rotatePlayer()
    {
        
        facingRight = !facingRight;
        Vector3 theScale = player.localScale;
        theScale.x *= -1;
        player.localScale = theScale;
    }

    //Input: num: intero 
    //Funzione per andare avanti di num passi verso i WayPoint
    void MoveToNextWaypoint(int num)
    {
        currentWaypointIndex += num;
        isMoving = true;
        animator.SetBool("isMoving", true);
    }


    //Input: num: intero 
    //Funzione per andare indietro di num passi verso i WayPoint
    void MoveToPreviousWaypoint(int num)
    {
        currentWaypointIndex -= num;
        isMoving = true;
        animator.SetBool("isMoving", true);
    }
   

    //Funzione per compiere il movimento effettivo movimento
    void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = new Vector3(targetWaypoint.position.x, targetWaypoint.position.y, originalZ);
        player.position = Vector2.MoveTowards(player.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(player.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }
}
