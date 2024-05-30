using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public CanvasGroup OptionPanel;
    public CanvasGroup CanvasPanel;
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
        CanvasPanel.alpha = 1;
        CanvasPanel.blocksRaycasts = true;
    }
    public void BackC()
    {
        CanvasPanel.alpha = 0;
        CanvasPanel.blocksRaycasts = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
