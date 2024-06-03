using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatusLevel : MonoBehaviour
{
    public static StatusLevel Instance { get; private set; }

    [System.Serializable]
    public class Level
    {
        public int numberLevel;
        public bool status;
        public GameObject levelDot;
        public Level(int numberLevel,bool status,GameObject levelDot)
        {
            this.numberLevel= numberLevel; 
            this.status= status;   
            this.levelDot = levelDot;
        }
    }
    public List<Level> levels = new List<Level>(); //Lista di livelli (1-4)
    public Sprite greenDot; 
    public Sprite redDot;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (levels.Count == 0)
        {
            initLevels(); //Da mettere in un ipotetico START del gioco!
        }
            //TEST
        //setStatusLevel(3, true); 
        //setStatusLevel(4, true);

    }

    //Inizializza i livelli come bloccati, eccetto il primo.
    public void initLevels()
    {
        levels.Add(new Level(1, true, GameObject.Find("wayPoint1")));
        levels.Add(new Level(3, false, GameObject.Find("wayPoint3")));
        levels.Add(new Level(4, false, GameObject.Find("wayPoint4")));
        levels.Add(new Level(5, false, GameObject.Find("wayPoint5")));

        UpdateLevelStatus();

    }
    public void Update()
    {
        
    }

    //Settare livelli: 
    //Input: level: intero, status: booleano
    // Prende il numero di un livello, e lo setta true(sbloccato) o false(bloccato)
    public void setStatusLevel(int level, bool status)
    {
        for (int i= 0; i < levels.Count; i++)
        {
            if (levels[i].numberLevel == level)
            {
                levels[i].status = status;
                EnableLevel(levels[i]);
                //Debug.Log($"Level {level} status updated to: {status}");
                
                break;
            }
        }
    }

    //Funzione per Aggiornare lo stato del livello
    private void UpdateLevelStatus()
    {
        for(int i= 0; i < levels.Count; i++)
        {
            EnableLevel(levels[i]);
        }
    }

    //Input: level: Level 
    //Attiva il livello passato in input
    private void EnableLevel(Level level)
    {
      if(level.levelDot != null)
        {
            SpriteRenderer spriteRenderer = level.levelDot.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = level.status ? greenDot : redDot;
            } else
            {
                Debug.LogError($"SpriteRenderer not found on {level.levelDot.name}");

            }
        }
      else
        {
            Debug.LogError($"levelDot is null for level {level.numberLevel}");

        }
    }

    //Input: level: int 
    //Output: booleano
    //Funzione che restituisce lo stato di un livello
    public bool isLevelEnabled(int level)
    {
        for(int i= 0; i < levels.Count; i++)
        {
            if (levels[i].numberLevel == level)
            {
                return levels[i].status;
            }
        }
        return false;

    }


}