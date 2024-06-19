using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItutorial : MonoBehaviour
{
    public GameObject plate;
    private bool isPaused = false;
    private bool isShown = false;
    private bool repeat = true;
    

    // Start is called before the first frame update
    void Start()
    {
        plate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            HideTutorialMessage();
        }
    }

    
    public void HideTutorialMessage()
    {
        Time.timeScale = 1f;
        isPaused = false;
        plate.SetActive(false);
        repeat = false;
       // isShown = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isShown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isShown && !isPaused && repeat)
            {
                Debug.Log("triggersss");
                Time.timeScale = 0f;
                plate.SetActive(true);
                isPaused = true;
                isShown = true;
            }

        }


    }
}
