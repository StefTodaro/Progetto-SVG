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
    private GameObject pauseMenuScript;
    [SerializeField] private AudioClip buttonClick;



    public void RestartLevel()
    {
        //Richiamare funzione per il restart livello
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        pauseMenuScript.GetComponent<PauseMenu>().Resume();
        GameManager_logic.Instance.RestartLevel();


    }
    

    public void ExitLevel()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        confirm.SetActive(true);
    }

    public void Controls()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        controls.SetActive(true);
    }

    public void Options()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
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
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
    }

    public void yesChoice()
    {
        //programmare l'uscita dal livello 
        PauseMenu.SetActive(false);
        pauseMenuScript.GetComponent<PauseMenu>().LoadLevelSelection();
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);


    }

    public void noChoice()
    {
        confirm.SetActive(false);
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
    }

  
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuScript = GameObject.Find("PauseMenu");
        
    }

    
}
