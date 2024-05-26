using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager_logic : MonoBehaviour
{
    public static GameManager_logic Instance;
    private Vector2 checkpointPosition;
    public List<GameObject> objectsToReset;
    public GameObject objectList;
    
    public GameObject coinPrefab;
    //monete del giocatore nel momento di attvazione del checkpoint
    public int checkpointCoins;


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

        objectsToReset = FindObjectsOfType<ResettableObjects>().Select(o => o.gameObject).ToList();
        objectList = GameObject.FindGameObjectWithTag("inObjects");
        

    }

    private void Update()
    {
        
    }

    public void SetCheckpoint(Vector2 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
    }


    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        foreach (GameObject obj in objectsToReset)
        {   //Se l'oggetto è inglobato dal giocatore allora non respawna
            if (obj.CompareTag("Object") && 
                !objectList.GetComponent<Incorporated_objects_list>().list.Contains(obj))
            {
                obj.GetComponent<ResettableObjects>().ResetState();

            }
            else if(!obj.CompareTag("Object"))
            {
                obj.GetComponent<ResettableObjects>().ResetState();
            }
            //Reset the coin in the level 
            coinManager coin_manager = coinPrefab.GetComponent<coinManager>();
            coin_manager.resetCoin(checkpointCoins);
        }
    }

    
}
