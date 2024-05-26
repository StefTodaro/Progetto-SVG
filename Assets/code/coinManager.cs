using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class coinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    float bounceForce = 3.5f;
    private int coinCount = 0;

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
   
    public int nCoin()
    {
        return coinCount;
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
       // Debug.Log("Coin: " + coinCount);
        coinCount+=1;
        UpdateCoinText();
        
        gameObject.SetActive(false); // Disattiva la moneta
         
        
    }

    public void UpdateCoinText()
    {
        GameObject coinCounter = GameObject.Find("CoinCounter");
        if(coinCounter != null)
        {
            Text coinText = coinCounter.GetComponentInChildren<Text>();
            if(coinText != null)
            {
                coinText.text = coinCount.ToString();
            }
        }
    }

    public void resetCoin(int nCoin)
    {
        coinCount = nCoin;
        UpdateCoinText();
    }
    }