using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    private int levelIndex;
    private GameObject[] children;
  
    void Start()
    {

        FindLevels();
        LoadMap();
    }



    void Update()
    {
      
    }

    //funzione per trovare i livelli nella mappa
    public void FindLevels()
    {
        // Trova il GameObject con il tag "waypoint"
        GameObject parent = GameObject.FindGameObjectWithTag("wayPoint");

        // Inizializza l'array con i figli del GameObject padre
        children = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            // Popola l'array con i figli waypoint
            children[i] = parent.transform.GetChild(i).gameObject;
        }
    }

    //funzione per settare il livello attuale come completato
    public void SetCurrentLevelCompl()
    {
        FindLevels();
        //indice del livello corrente 
        int c = PlayerPrefs.GetInt("currentLevel");
        children[c].GetComponent<Level>().complete();
        children[c].GetComponent<Level>().SaveLevelCompleted(c);

    }


    //reimposta i livelli completati all'interno della mappa
    public void LoadMap()
    {
        Level currentIndex;
        for (int i = 0; i < children.Length; i++)
        {
                if (children[i].GetComponent<Level>().index() == PlayerPrefs.GetInt("level"+i))
                {
                    currentIndex = children[i].GetComponent<Level>();
                    currentIndex.complete();
                }
        }
    }





}

