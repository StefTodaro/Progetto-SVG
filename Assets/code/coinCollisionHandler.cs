using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionHandler : MonoBehaviour
{
    private coinManager coinManager;

    private void Start()
    {
        
        coinManager = FindObjectOfType<coinManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        coinManager.HandleCoinCollision(collision);
    }
}
