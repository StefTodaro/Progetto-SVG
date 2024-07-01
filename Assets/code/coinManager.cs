using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class coinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    float bounceForce = 3.5f;
    
    private coinCounter cC;

    public GameObject takenEffect;
    public AudioClip pickUpAudio;
  
    
    private void Start()
    {
         cC = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        
    }
    public void InstantiateCoin(Vector3 spawnPosition)
    {
        
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        coin.SetActive(true);
        Rigidbody2D coinRB = coin.GetComponent<Rigidbody2D>();

        coinRB.AddForce(Vector2.right * bounceForce, ForceMode2D.Impulse);
        coinRB.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectCoin(collision.gameObject);
            if (pickUpAudio != null)
            SoundEffectManager.Instance.PlaySoundEffect(pickUpAudio, transform, 0.35f);
            Instantiate(takenEffect,transform.position, transform.rotation);
            
        }
    }





    private void CollectCoin(GameObject player)
    {
       
        cC.addCoin();
        
        gameObject.SetActive(false); // Disattiva la moneta
         
    }

    

    
    public int getNumberCoin()
    {
        return cC.getCoin();
    }


    public void resetCoin(int nCoin)
    {   
        cC.setCoin(nCoin + PlayerPrefs.GetInt("coins"));
        
    }


   
}

