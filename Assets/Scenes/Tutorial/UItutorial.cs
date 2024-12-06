using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItutorial : MonoBehaviour
{
    public GameObject plate;
    // private bool isPaused = false;
    [SerializeField] private bool isShown = false;
    [SerializeField] private bool repeat = true;
    

    // Start is called before the first frame update
    void Start()
    {
        plate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isShown  && Input.GetMouseButtonDown(0))
        {
            HideTutorialMessage();
        }
    }

    
    public void HideTutorialMessage()
    {
        Time.timeScale = 1f;
        plate.SetActive(false);
        GameManager_logic.Instance.SetInactive(false);
        repeat = false;
        isShown = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isShown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isShown && repeat)
            {
                Time.timeScale = 0f;
                plate.SetActive(true);
                GameManager_logic.Instance.SetInactive(true);
                isShown = true;
            }

        }


    }
}
