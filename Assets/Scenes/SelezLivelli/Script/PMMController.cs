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

    
    


  
   

    public void option()
    {
        Options.SetActive(true);
        
    }

    public void controls()
    {
        Controls.SetActive(true);
        
    }
    
    public void quit()
    {
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
    }

    public void yesChoice()
    {
       
        MapMenu.SetActive(false);
        Confirm.SetActive(false);
        Application.Quit();
    }

    public void noChoice()
    {

        Confirm.SetActive(false);
    }

   
}
