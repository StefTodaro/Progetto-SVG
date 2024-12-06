using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class GameManager_logic : MonoBehaviour
{
    public static GameManager_logic Instance;
    public Vector2 checkpointPosition;
    public List<GameObject> objectsToReset;
    public Incorporated_objects_list objectList;
    public GameObject mainCamera;

    //indica se il giocatore può muoversi o meno
    private bool canMove = true;
    
    //monete del giocatore nel momento di attvazione del checkpoint
    public int checkpointCoins;

    public List<GameObject> activatedCheckpoints;
    //Lista che tiene conto delle monete prese. Utile per la gestione del respawn
    public List<GameObject> coinsTaken = new List<GameObject>();


    private coinManager cManager;
    private coinCounter cCounter;
    private Transformation_logic transformations;
    public GameObject startPosition;
    private bool restart = false;
    GameObject player;

    GameObject inc_obj;

    public float endLevelTimer;
    public AudioClip endLevelMusic;



    //booleano che indica se il gioco è inattivo
    [SerializeField] private bool inactive=false;

    //Variabile per assicurarsi che il registro di "livello" sia stato settato 
    public static bool hasSetted = false;

    AudioSource audioSource;

    private void Start()
    {

        //cancella i dati delle ultime partite
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        mainCamera = GameObject.FindGameObjectWithTag("camera");
        startPosition = GameObject.FindGameObjectWithTag("Start");
        objectsToReset = FindObjectsOfType<ResettableObjects>().Select(o => o.gameObject).ToList();
        objectList = GameObject.FindGameObjectWithTag("inObjects").GetComponent<Incorporated_objects_list>();

        cManager = GameObject.FindGameObjectWithTag("coin").GetComponent<coinManager>();
        cCounter = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>(); ;
        inc_obj = GameObject.FindGameObjectWithTag("inObjects");
    }

    private void Update()
    {
        
    }

    //si inizializzano le variabili utili ad inizio livello non permanenti
    public void StartLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!canMove)
        {
            canMove = true;
        }

        startPosition = GameObject.FindGameObjectWithTag("Start");
        checkpointPosition = startPosition.transform.position;
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>(); ;
        //si cancella la lista delle trasformazioni in scena
        transformations.transformationsInScene.Clear();

        cManager = GameObject.FindGameObjectWithTag("coin").GetComponent<coinManager>();
        cCounter = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        mainCamera = GameObject.FindGameObjectWithTag("camera");
        objectsToReset = FindObjectsOfType<ResettableObjects>().Select(o => o.gameObject).ToList();
        objectList = GameObject.FindGameObjectWithTag("inObjects").GetComponent<Incorporated_objects_list>();
        inc_obj = GameObject.FindGameObjectWithTag("inObjects");
    }

    //set e get sell inactive
    public void SetInactive(bool inac)
    {
        inactive=inac;
    }

    public bool GetInactive()
    {
        return inactive;
    }


    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool can)
    {
        canMove = can;
    }


    //Metodo che restituisce il valore della variabile booleana restart
    public bool hasRestart()
    {
        return restart;
    }

    //Va chiamato nella funzione restart level del menu di pausa
    public void RestartLevel()
    {
        restart = true;
        inactive = false;
        transformations.ResetTransformation();   //Rinizializza la lista delle trasformazioni
        player = GameObject.FindGameObjectWithTag("Player");
        cCounter.ResetCoinsToLevel();
        coinsTaken.Clear();// si ripulisce la lista delle monete prese 
        inc_obj.GetComponent<Incorporated_objects_list>().ClearInObject(); //Svuota la lista degli oggetti assorbiti

        ResetCheckpoints();
        //si reimposta come checkpoint attuale la posizione iniziale 
        RespawnPlayer(player);       
        restart = false;
        
    }
   
    
    public void ResetCheckpoints()
    {
        foreach(GameObject checkpoint in activatedCheckpoints)
        {
            checkpoint.GetComponent<checkpoint_handler>().DisableCheckPoint();
        }
        activatedCheckpoints.Clear();
        checkpointPosition = startPosition.transform.position;
        
    }
    public void SetCheckpoint(GameObject newCheckpointPosition)
    {
        activatedCheckpoints.Add(newCheckpointPosition);
        checkpointPosition = newCheckpointPosition.transform.position;
        checkpointCoins = cCounter.getCoinLevel();
       


        /* si fa un controllo sulle monete che il giocatore
         * ha preso fino al momento di attivazione del checkpoint 
         * per non farle respawnare*/
        foreach (GameObject obj in objectsToReset)
        {
            if(obj.CompareTag("coin") && !obj.activeSelf)
            {
                coinsTaken.Add(obj);
            }

        }

    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        
        foreach (GameObject obj in objectsToReset)
        {
            /*Se l'oggetto è inglobato dal giocatore oppure
            è una moneta presa dal giocatore 
            prima di raggiungere il checkpoint non respawna*/
            if (!objectList.list.Contains(obj) && !coinsTaken.Contains(obj))
            {
                obj.GetComponent<ResettableObjects>().ResetState();
            } 
        }
        if (!restart)
        {
            //Resetta le monete al numero di monete al momento 
            //dell'attivazione del checkpoint
            cManager.resetCoin(checkpointCoins);
        }
        if (restart)
        {
            objectList.ClearInObject();
        }
    }


    //funzioni per gestire l'obbiettivo della main caamera
    public void LockCamera()
    {
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = true;
    } 
    
    public void UnlockCamera()
    {
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = false;
    }

   
    public void EndLevel()
    {
        audioSource = GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>();

        //se c'è la musica di fine livello allora prima di caricare la nuova scena 
        //aspetterà che finisca e suona una sola volta 
        audioSource.Stop();
        if (endLevelMusic != null && !inactive)
        {
            SoundEffectManager.Instance.PlaySoundEffect(endLevelMusic, transform, 1f);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<movement>().Stop();
        SetCanMove(false);
        endLevelTimer += Time.deltaTime;
      

        if (endLevelTimer > endLevelMusic.length)
        {
            endLevelTimer = 0;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Selezione Livelli");
           
            
            cCounter.SaveCoinsAtLevel();
            coinsTaken.Clear();
            inc_obj.GetComponent<Incorporated_objects_list>().ClearInObject();
            //si resettano le trasformazioni dello slime una volta caricata la mappa 
            transformations.ResetTransformation();

            //si resettano i checkpoint del livello
            ResetCheckpoints();

            //si svuota la lista degli elementi da ricaricare
            objectsToReset.Clear();

            //a fine livello si salvano le trasformazioni e le monete
            cCounter.SaveCoins();
            transformations.SaveTransformations();
        }
    }


    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        //fa ripartire la musica una volta tornanti nella selezione livelli
        audioSource = GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>();
        audioSource.Play();
       
        //si aggiornano i dati sui livelli completati
        levelManager lm = GameObject.FindGameObjectWithTag("levelManager").GetComponent<levelManager>();
        lm.SetCurrentLevelCompl();

        // Rimuove il callback per evitare duplicati
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    //funzione per quando il giocatore esce dal livello
    //tramite il pulsante "quit"

    public void QuitLevel()
    {
        
        transformations.ResetTransformation();

        //si resettano i checkpoint del livello
        ResetCheckpoints();

        objectsToReset.Clear();
        inc_obj.GetComponent<Incorporated_objects_list>().ClearInObject();

        coinsTaken.Clear();
        //si resetta il gioco come attivo
        inactive = false;

        SceneManager.LoadScene("Selezione livelli");
    }

    

}
