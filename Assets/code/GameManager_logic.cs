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
    public GameObject objectList;
    public GameObject mainCamera;
    
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

    [SerializeField] int indexLevel;

    //Variabile per assicurarsi che il registro di "livello" sia stato settato 
    public static bool hasSetted = false;
    private void Start()
    {
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
        objectList = GameObject.FindGameObjectWithTag("inObjects");

        cManager = GameObject.FindGameObjectWithTag("coin").GetComponent<coinManager>();
        cCounter = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>(); ;
        inc_obj = GameObject.FindGameObjectWithTag("inObjects");

    }


    //si inizializzano le variabili utili ad inizio livello non permanenti
    public void StartLevel()
    { //si prende il riferimento al livello attuale
      
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = GameObject.FindGameObjectWithTag("Start");
        mainCamera = GameObject.FindGameObjectWithTag("camera");
        objectsToReset = FindObjectsOfType<ResettableObjects>().Select(o => o.gameObject).ToList();
        objectList = GameObject.FindGameObjectWithTag("inObjects");
        inc_obj = GameObject.FindGameObjectWithTag("inObjects");
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
        transformations.ResetTransformation();   //Rinizializza la lista delle trasformazioni
        player = GameObject.FindGameObjectWithTag("Player");

        cCounter.ResetCoinsToLevel();
        coinsTaken.Clear();// si ripulisce la lista delle monete prese 
        inc_obj.GetComponent<Incorporated_objects_list>().ClearInObject(); //Svuota la lista degli oggetti assorbiti
        var Checkpoint = GameObject.Find("Checkpoint");
        //si reimposta come checkpoint attuale la posizione iniziale 
        checkpointPosition = startPosition.transform.position;
        RespawnPlayer(player);
        ResetCheckpoints();
        restart = false;
        
    }
   
    
    public void ResetCheckpoints()
    {
        foreach(GameObject checkpoint in activatedCheckpoints)
        {
            checkpoint.GetComponent<checkpoint_handler>().DisableCheckPoint();
        }
        activatedCheckpoints.Clear();  
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
            /*Se l'oggetto � inglobato dal giocatore oppure
            � una moneta presa dal giocatore 
            prima di raggiungere il checjpoint non respawna*/
            if (!objectList.GetComponent<Incorporated_objects_list>().list.Contains(obj) &&
                !coinsTaken.Contains(obj))
            {
                obj.GetComponent<ResettableObjects>().ResetState();
            } 
        }

        if (!restart)
        {
            //Reset the coin in the level 
            cManager.resetCoin(checkpointCoins);
            objectList.GetComponent<Incorporated_objects_list>().ClearInObject();
        }
    }

    
    public void UpdateCoinText()
    {
        TextMeshProUGUI coinText = GameObject.FindGameObjectWithTag("coinNum").GetComponent<TextMeshProUGUI>();
        GameObject coinCounter = GameObject.FindGameObjectWithTag("coinNum");
        
        //TextMeshPro coinText = coinCounter.GetComponentInChildren<TextMeshPro>();

        coinText.SetText(cCounter.getCoin().ToString());
       
    }

    //funzioni per gestire l'obbiettivo della main caamera
    public void LockCamera()
    {
        /*if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("camera");
        }*/
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = true;
    } 
    
    public void UnlockCamera()
    {
       /* if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("camera");
        }*/
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = false;
    }

    
   
    public void EndLevel()
    {
        cCounter.SaveCoinsAtLevel();
        coinsTaken.Clear();
        inc_obj.GetComponent<Incorporated_objects_list>().ClearInObject();
        ResetCheckpoints();

        var exitTime = 1f;
        endLevelTimer += Time.deltaTime;
        //se c'� la musica di fine livello allora prima di caricare la nuova scena 
        //aspetter� che finisca
        if (endLevelMusic != null )
        {
            exitTime = endLevelMusic.length;
            SoundEffectManager.Instance.PlaySoundEffect(endLevelMusic, transform, 0.75f);
        }
        if (endLevelTimer > exitTime )
        {
            endLevelTimer = 0;
            SceneManager.LoadScene("Selezione Livelli");
        }

        //PlayerPrefs.SetInt("livello", indexLevel);

        //si resettano le trasformazioni dello slime a fine livello
        transformations.ResetTransformation();

        //si svuota la lista degli elementi da ricaricare
        objectsToReset.Clear();
        if (!hasSetted)
        {
            hasSetted = true;
        }
        PlayerPrefs.Save();
      
    }

}
