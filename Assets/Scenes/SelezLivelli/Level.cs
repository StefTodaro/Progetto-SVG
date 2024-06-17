using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;



public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool unlocked;
    [SerializeField] private bool completed;
    [SerializeField] private int levelIndex; //Indice del livello
    [SerializeField] private string levelName;
    [SerializeField] private Transform waypointUp;
    [SerializeField] private Transform waypointDown;
    [SerializeField] private Transform waypointRight;
    [SerializeField] private Transform waypointLeft;
    bool isOnLevel;

    Animator anim;
    
    public List<GameObject> waypoints;

    agentMovement agent;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        
        
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetBool("unlocked", unlocked);
        anim.SetBool("completed", completed);


        StartLevel();
        Movement();
    }

    public void unlock()
    {
        unlocked = true;
    }

    public void complete()
    {
        completed = true;
        foreach(var wayp in waypoints)
        {
            wayp.GetComponent<Level>().unlock();
        }
    }

    public void StartLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            if (levelName != null && isOnLevel)
            {
                SceneManager.LoadScene(levelName);

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            isOnLevel = true;
            agent = collision.GetComponent<agentMovement>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            isOnLevel = false;
            agent = null;
        }
        
    }

    void Movement()
    {
        if (isOnLevel)
        {
            if (waypointUp != null && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                if (waypointUp.GetComponent<Level>().unlocked) { Debug.Log("UP"); agent.Movement(waypointUp); }

            }
            if (waypointDown != null && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
            {
                if (waypointDown.GetComponent<Level>().unlocked) { Debug.Log("DOWN"); agent.Movement(waypointDown); }
            }
            if (waypointRight != null && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
            {
                if (waypointRight.GetComponent<Level>().unlocked) { Debug.Log("RIGHT"); agent.Movement(waypointRight); }
            }
            if (waypointLeft != null && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
            {
                if (waypointLeft.GetComponent<Level>().unlocked) { Debug.Log("LEFT"); agent.Movement(waypointLeft); }
            }
        }
    }
    
    
    //TODO 

}
