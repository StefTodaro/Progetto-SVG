using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public CanvasGroup OptionPanel;
    public CanvasGroup CreditsPanel;
    public CanvasGroup GameControl;
    public void PlayGame()
    {
        SceneManager.LoadScene("Selezione livelli");
    }

    public void Option()
    {
        OptionPanel.alpha = 1;
        OptionPanel.blocksRaycasts = true;

    }
    public void Back()
    {
        OptionPanel.alpha = 0;
        OptionPanel.blocksRaycasts = false;

    }
    public void Credits()
    {
        CreditsPanel.alpha = 1;
        CreditsPanel.blocksRaycasts = true;
    }
    public void BackC()
    {
        CreditsPanel.alpha = 0;
        CreditsPanel.blocksRaycasts = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Control()
    {
        GameControl.alpha = 1;
        GameControl.blocksRaycasts = true;
    }

    public void BackControl()
    {
        GameControl.alpha = 0;
        GameControl.blocksRaycasts = false;
    }
}
