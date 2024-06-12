using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_logic : MonoBehaviour
{
    public static GameManager_logic Instance;
    private Vector2 checkpointPosition;
    public List<GameObject> objectsToReset;
    public GameObject objectList;
    public GameObject mainCamera;
    
    //monete del giocatore nel momento di attvazione del checkpoint
    public int checkpointCoins;
    //Lista che tiene conto delle monete prese. Utile per la gestione del respawn
    public List<GameObject> coinsTaken = new List<GameObject>();


    private coinManager cManager;
    private coinCounter cCounter;
    private GameObject transformationHandler;
    public GameObject startPosition;
    private bool restart = false;

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

        objectsToReset = FindObjectsOfType<ResettableObjects>().Select(o => o.gameObject).ToList();
        objectList = GameObject.FindGameObjectWithTag("inObjects");

        cManager = GameObject.FindGameObjectWithTag("coin").GetComponent<coinManager>();
        cCounter = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        transformationHandler = GameObject.FindGameObjectWithTag("t_handler");

    }

    //Va chiamato nella funzione restart level del menu di pausa
    public void RestartLevel()
    {
        restart = true;
        transformationHandler.GetComponent<Transformation_logic>().ResetTransformation();   //Rinizializza la lista delle trasformazioni
        var Player = GameObject.FindGameObjectWithTag("Player");
        cCounter.ResetCoinsToLevel();
        coinsTaken.Clear();// si ripulisce la lista delle monete prese 
        Player.GetComponent<Slime_objects_logic>().ClearInObject(); //Svuota la lista degli oggetti assorbiti
        var Checkpoint = GameObject.Find("Checkpoint");
        //si reimposta come checkpoint attuale la posizione iniziale 
        checkpointPosition = startPosition.transform.position;
        
        RespawnPlayer(Player);
        restart = false;
        
    }
   

    public void SetCheckpoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
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
        }
    }

    
    public void UpdateCoinText()
    {
        GameObject coinCounter = GameObject.Find("CoinCounterG");
        
        Text coinText = coinCounter.GetComponentInChildren<Text>();
        
        coinText.text = cCounter.getCoin().ToString();
        
        
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




}
