using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel : MonoBehaviour
{
    // Start is called before the first frame update

    private bool Completed;
    private coinCounter CoinCounterScript;
    void Start()
    {
      
       
        CoinCounterScript = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinCounterScript.SaveCoinsAtLevel();
            Debug.Log("Livello completato con: " + CoinCounterScript.getCoin()  + " monete. il valore di CoinsAtLevel è " + CoinCounterScript.coinsAtLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
