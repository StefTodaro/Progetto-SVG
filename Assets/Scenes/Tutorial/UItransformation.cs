using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UItransformation : MonoBehaviour
{
    
  
   
    public bool repeat1, repeat2, repeat3;
    
    public GameObject plateBird;
    public GameObject plateRino;
    public GameObject plateBee;
    public GameObject rino;
    public GameObject bird;
    public GameObject bee;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeAfterDelay());
    }

    IEnumerator InitializeAfterDelay()
    {

        yield return new WaitForSeconds(0.1f);
        plateBird.SetActive(false); plateRino.SetActive(false); plateBee.SetActive(false);

        repeat1 = true;
        repeat2 = true;
        repeat3 = true;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if(rino.activeInHierarchy && repeat1)
        {
            activeTransfRinoPanel();
            repeat1 = false;
            
        }
        if(bird.activeInHierarchy && repeat2)
        {
            activeTransfBirdPanel();
            repeat2 = false;
        }
        if(bee.activeInHierarchy && repeat3)
        {
            activeTransfBeePanel();
                repeat3 = false;
        }
        
        closePanel();
    }

   
    public void closePanel()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            if (plateBird.activeInHierarchy)
            {
                plateBird.SetActive(false);
            } else if(plateRino.activeInHierarchy) 
            {
                plateRino.SetActive(false);
            } else if (plateBee.activeInHierarchy)
            {
                plateBee.SetActive(false);
            }
        }
    }
    public void activeTransfBeePanel()
    {
        Time.timeScale = 0f;
        plateBee.SetActive(true);
    }

    public void activeTransfBirdPanel()
    {
        Time.timeScale = 0f;
        plateBird.SetActive(true);
    }

    public void activeTransfRinoPanel()
    {
        Time.timeScale = 0f;
        plateRino.SetActive(true);
    }

    
    
}
