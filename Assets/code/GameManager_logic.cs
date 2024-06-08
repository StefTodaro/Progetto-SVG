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

    
    private coinManager cManager;
  
    private coinCounter cCounter;
    private GameObject transformationHandler;
    public GameObject startCheckPoint;
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
        var Player = GameObject.FindGameObjectWithTag("Player");
        cCounter.ResetCoinsToLevel();
        Player.GetComponent<Slime_objects_logic>().ClearInObject(); //Svuota la lista degli oggetti assorbiti
        transformationHandler.GetComponent<Transformation_logic>().ClearTransformation();   //Rinizializza la lista delle trasformazioni NON FUNZIONA
        var Checkpoint = GameObject.Find("Checkpoint");
        if (Checkpoint.GetComponent<checkpoint_handler>().isActive(startCheckPoint)) //Se il checkpoint è stato attivato
        {
            checkpointPosition = startCheckPoint.transform.position;//Pone come checkpoint attuale il checkpoint iniziale
        } else
        {
            Player.transform.position = startPosition.transform.position;
        }
        RespawnPlayer(Player);
        restart = false;
        
    }
   

    public void SetCheckpoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
        checkpointCoins = cCounter.getCoinLevel();  
    }


    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        foreach (GameObject obj in objectsToReset)
        {


            //Se l'oggetto è inglobato dal giocatore allora non respawna
            if (obj.CompareTag("Object") &&
                !objectList.GetComponent<Incorporated_objects_list>().list.Contains(obj))
            {
                obj.GetComponent<ResettableObjects>().ResetState();

            }
            else if (!obj.CompareTag("Object"))
            {


                //controlla che l'oggetto sia il gestore di mob della bossfight
                if (obj.GetComponent<BossMobsSpawn>())
                {
                    obj.GetComponent<BossMobsSpawn>().ResetMobInScene();
                }


                if (obj.GetComponent<Boss_trigger>())
                {
                    obj.GetComponent<Boss_trigger>().ResetTrigger();
                }

                //controlla che l'oggetto sia il boss e ne resetta le caratteristiche principali
                if (obj.GetComponent<Boss_logic>())
                {
                    obj.GetComponent<Boss_logic>().ResetBossFight();
                }
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
