using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMMController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MapMenu;
    public GameObject Confirm;
    public GameObject Controls;
    public GameObject Options;

    [SerializeField] private AudioClip buttonClick;

  
    public void option()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        Options.SetActive(true);
        
    }

    public void controls()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        Controls.SetActive(true);
        
    }
    
    public void quit()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        Confirm.SetActive(true);
       
    }
    public void back()
    {
        
        if (Controls.activeInHierarchy)
        {
            Controls.SetActive(false);
        } else if (Options.activeInHierarchy)
        {
            Options.SetActive(false);
        }
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
    }

    public void yesChoice()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        MapMenu.SetActive(false);
        Confirm.SetActive(false);
        Application.Quit();
    }

    public void noChoice()
    {
        SoundEffectManager.Instance.PlaySoundEffect(buttonClick, transform, 0.25f);
        Confirm.SetActive(false);
    }

   
}
