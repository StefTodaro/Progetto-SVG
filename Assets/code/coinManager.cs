using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class coinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    float bounceForce = 3.5f;

    private coinCounter cC;

    private void Start()
    {
        GameObject cCObject = GameObject.FindGameObjectWithTag("coinCounter");
        if (cCObject != null)
        {
            cC = cCObject.GetComponent<coinCounter>();

            if (cC == null)
            {
                Debug.LogError("No CoinCounter component found on the object with tag 'CoinCounter'!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'CoinCounter' found in the scene!");
        }
    }
    public void InstantiateCoin(Vector3 spawnPosition)
    {
        
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        coin.SetActive(true);
        Rigidbody2D coinRB = coin.GetComponent<Rigidbody2D>();

        coinRB.AddForce(Vector2.right * bounceForce, ForceMode2D.Impulse);
        coinRB.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    public void HandleCoinCollision(Collision2D collision)
    {
        // Se the coin collides with the ground
        if (collision.gameObject.CompareTag("Wall"))
        {
            Rigidbody2D coinRB = collision.gameObject.GetComponent<Rigidbody2D>();

            
            coinRB.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }

    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectCoin(collision.gameObject);
        }
    }

    private void CollectCoin(GameObject player)
    {
       
        cC.addCoin();
        Debug.Log("num coin: " + cC.getCoin());
        
        UpdateCoinText();
        
        
        gameObject.SetActive(false); // Disattiva la moneta
         
        
    }

    public int getNumberCoin()
    {
        return cC.getCoin();
    }
    public void UpdateCoinText()
    {
        GameObject coinCounter = GameObject.Find("CoinCounterG");
        if(coinCounter != null)
        {
            Text coinText = coinCounter.GetComponentInChildren<Text>();
            if(coinText != null)
            {
                coinText.text = cC.getCoin().ToString();
            }
        }
    }

    public void resetCoin(int nCoin)
    {
        cC.setCoin(nCoin);
        UpdateCoinText();
    }


   
}

