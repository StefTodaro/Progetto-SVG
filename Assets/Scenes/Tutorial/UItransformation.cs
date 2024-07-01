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
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeAfterDelay());
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (rino.name== player.name && repeat1)
        {
            activeTransfRinoPanel();
            repeat1 = false;

        }
        if (bird.name == player.name && repeat2)
        {
            activeTransfBirdPanel();
            repeat2 = false;
        }
        if (bee.name==player.name && repeat3)
        {
            activeTransfBeePanel();
            repeat3 = false;
        }

        closePanel();
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
    
   
    public void closePanel()
    {
        if (Input.GetMouseButtonDown(1))
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
