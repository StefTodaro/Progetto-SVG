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
    [SerializeField] private string levelName;//per richiamare la scena associata
    //i waypoint accessibili dal livello viaggiando nelle direzioni associate
    [SerializeField] private Transform waypointUp;
    [SerializeField] private Transform waypointDown;
    [SerializeField] private Transform waypointRight;
    [SerializeField] private Transform waypointLeft;
    bool isOnLevel;

    Animator anim;
    //livelli che vengono sbloccati una volta completato il livello corrente
    public List<GameObject> waypoints;

    //riferimento lo slime all'interno della mappa 
    agentMovement agent;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        
        
    }


    public int index() { return levelIndex; }

    //Funzione per debug 
    public string status()
    {
        return "level: " + levelIndex + " status: " + unlocked;
    }

    public bool isCompleted()
    {
        return completed;
    }

    public bool isUnlocked() { return unlocked; }
    // Update is called once per frame
    void Update()
    {   
        //setta le animazioni dei waypoint per un feeedback visivo
        if (!anim.GetBool("unlocked"))
        
            anim.SetBool("unlocked", unlocked);
        
        if (!anim.GetBool("completed"))
            anim.SetBool("completed", completed);


        Movement();
        StartLevel();
        
    }



    //rende il livello raggiungibile
    public void unlock()
    {
        unlocked = true;
    }

    //imposta il livello completato e rende accessibili i livelli collegati
    public void complete()
    {
        completed = true;
        foreach(var wayp in waypoints)
        {
            wayp.GetComponent<Level>().unlock();
        }
    }

    //inizia il livello associato al waypoint
    public void StartLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            if (levelName != null && isOnLevel)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                PlayerPrefs.SetInt("livello", levelIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene(levelName);
            }
        }
    }

    //una volta caricato il livello si inizializzano tutti i valori utili
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        // Disiscriversi dall'evento sceneLoaded per evitare problemi di memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameManager_logic.Instance.StartLevel();
    }

    //definisce se il giocatore è sopra il waypoint o meno
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

    //indica in quali posizioni il giocatore si possa muovere in base ai waypoint collegati 
    //nelle quattro direzioni
    void Movement()
    {
        if (isOnLevel)
        {
            if (waypointUp != null && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                if (waypointUp.GetComponent<Level>().unlocked) { agent.Movement(waypointUp); }

            }
            if (waypointDown != null && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
            {
                if (waypointDown.GetComponent<Level>().unlocked) { agent.Movement(waypointDown); }
            }
            if (waypointRight != null && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
            {
                if (waypointRight.GetComponent<Level>().unlocked) {  agent.Movement(waypointRight); }
            }
            if (waypointLeft != null && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
            {
                if (waypointLeft.GetComponent<Level>().unlocked) { agent.Movement(waypointLeft); }
            }
        }
    }
  
}
