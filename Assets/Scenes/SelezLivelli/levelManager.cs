using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    private int levelIndex;
    private GameObject[] children;
    
    void Start()
    {
        // Trova il GameObject con il tag "waypoint"

        GameObject parent = GameObject.FindGameObjectWithTag("wayPoint");
        
        
       // Inizializza l'array con i figli del GameObject padre
       children = new GameObject[parent.transform.childCount];

       // Popola l'array con i figli waypoint
       for (int i = 0; i < parent.transform.childCount; i++)
       {
           children[i] = parent.transform.GetChild(i).gameObject;
               
       }

      




    }

  
    
    void Update()
    {
        
        //Prende la variabile dal registro
        levelIndex = PlayerPrefs.GetInt("livello");
        
        Level currentIndex;
        for(int i=0; i < children.Length && !children[i].GetComponent<Level>().isCompleted(); i++)
        {
            if (GameManager_logic.hasSetted)
            {
                if (children[i].GetComponent<Level>().index() == levelIndex )
                {
                    currentIndex = children[i].GetComponent<Level>();
                    currentIndex.complete();
                }
            }
        }
    }

   
    

    //Da rimuovere nella build finale - Serve a cancellare la chiave livello dal registro
    //al momento dell'uscita dall'applicazione.
    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("livello");
        PlayerPrefs.Save();
    }
}

