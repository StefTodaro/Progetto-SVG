using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coinCounter : MonoBehaviour
{
    public int numCoin= 0; //Numero monete in generale
    public int coinsAtLevel = 0; //Numero monete all'inizio del livello
   


    //Nello start segnare il numero di monete iniziali
    private void Start()
    {
       LoadCoins();
    }

    public void UpdateCoinText()
    {
        TextMeshProUGUI coinText = GameObject.FindGameObjectWithTag("coinNum").GetComponent<TextMeshProUGUI>();
        coinText.SetText(getCoin().ToString());
    }



    //Aggiorna il contatore delle monete prese SOLO in un livello
    public void addCoin()
    {
        numCoin += 1;
        coinsAtLevel += 1;
        UpdateCoinText();
    }

   public void setCoin(int num)
    {
        numCoin = num;
        UpdateCoinText();
    }

    //Richiamare quando si finisce un livello
    //If level_completed then SaveCoinsAtLevel poco prima di switchare scena 
    public void SaveCoinsAtLevel()
    {
        coinsAtLevel = 0;
        UpdateCoinText();
        
    }
    
    //Richiamare quando si ricomincia il livello
    public void ResetCoinsToLevel()
    {
        numCoin = PlayerPrefs.GetInt("coins");
        coinsAtLevel = 0;
        UpdateCoinText();
    }
    
       
    public int getCoinLevel()
    {
        return coinsAtLevel;
    }
    public int getCoin()
    {
        return numCoin;
    }


    //funzioni per salvare e caricare il numero di monete

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", numCoin);
    }

    public void LoadCoins()
    {
        numCoin = PlayerPrefs.GetInt("coins");
        UpdateCoinText();
    }
}
