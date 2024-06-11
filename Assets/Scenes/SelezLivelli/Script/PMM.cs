using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using static UnityEditor.Progress;

public class PMM : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPaused = false;
    public GameObject MapMenu;
    public GameObject Confirm;
    public GameObject Controls;
    public GameObject Options;


    public void ResumePause()
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
                ReloadMenu();
            }

        }
    }

    public void Update()
    {
        ResumePause();
    }

    public void Resume()
    {
        MapMenu.SetActive(false);
        isPaused = false;
    }
    public void Pause()
    {
        MapMenu.SetActive(true);
        isPaused = true;
    }

    public void ReloadMenu()
    {
        Confirm.SetActive(false);
        Options.SetActive(false);
        Controls.SetActive(false);
    }
}
