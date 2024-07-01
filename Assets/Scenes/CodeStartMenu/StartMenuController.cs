using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject CreditsPanel;
    public GameObject ControlsPanel;
    [SerializeField] private AudioClip buttonClick;
    
    public void PlayGame()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);
        SceneManager.LoadScene("Selezione livelli");
        
    }

    public void Option()
    {
        OptionPanel.SetActive(true);
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);

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
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);
    }
    public void Credits()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);
        CreditsPanel.SetActive(true);
    }
   
    public void QuitGame()
    {
        Application.Quit();
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);
    }

    public void Control()
    {
        ControlsPanel.SetActive(true);
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.90f);
    }

   

    

    
}
