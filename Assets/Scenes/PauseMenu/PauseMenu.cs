using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Confirm;
    public GameObject Controls;
    public GameObject Options;
    public bool isPaused = false;

    
  
   
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
               
            }
            else
            {
                Pause();
                ReloadPauseMenu();
            }
        }   
    }

    //Funzione per ripristinare il menu di pausa una volta chiuso
    public void ReloadPauseMenu()
    {
        Confirm.SetActive(false);
        Options.SetActive(false);
        Controls.SetActive(false);
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //riprendi il gioco
        isPaused = false;
        
    }

    public void Pause()
    {
        
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //ferma il gioco
        isPaused = true;
    }

    public void LoadLevelSelection()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Selezione livelli");
    }

}
