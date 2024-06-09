using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject CreditsPanel;
    public GameObject ControlsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Selezione livelli");
    }

    public void Option()
    {
        OptionPanel.SetActive(true);

    }
    public void Back()
    {
        if (OptionPanel.activeInHierarchy)
        {
            OptionPanel.SetActive(false);
        } else if (CreditsPanel.activeInHierarchy) 
        {
            CreditsPanel.SetActive(false);
        } else
        {
            ControlsPanel.SetActive(false);
        }
    }
    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Control()
    {
        ControlsPanel.SetActive(true);
    }

   

    

    
}
