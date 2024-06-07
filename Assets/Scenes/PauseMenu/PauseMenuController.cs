using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    public GameObject confirm;
    public GameObject PauseMenu;
    public GameObject controls;
    public GameObject options;
    private PauseMenu pauseMenuScript;

    
    
    public void RestartLevel()
    {
        //Richiamare funzione per il restart livello
    }
    

    public void ExitLevel()
    {
        confirm.SetActive(true);
    }

    public void Controls()
    {
        controls.SetActive(true);
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void back()
    {
        if (controls.activeInHierarchy)
        {
            controls.SetActive(false);
        } else if (options.activeInHierarchy)
        {
            options.SetActive(false);
        }

    }

    public void yesChoice()
    {
        //programmare l'uscita dal livello 
        PauseMenu.SetActive(false);
        pauseMenuScript.LoadLevelSelection();
        
       

    }

    public void noChoice()
    {
        confirm.SetActive(false);
    }

  
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuScript = FindObjectOfType<PauseMenu>();
    }

    
}
