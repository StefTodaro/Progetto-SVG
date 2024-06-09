using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCounter : MonoBehaviour
{
    public int numCoin= 0; //Numero monete in generale
    public int coinsAtLevel = 0; //Numero monete all'inizio del livello


    //Nello start segnare il numero di monete iniziali
    private void Start()
    {
        //SaveCoinsAtLevel();  FORSE NON CE N'E' BISOGNO...dovrebbe salvarsi in automatico
    }

    //Aggiorna il contatore delle monete prese SOLO in un livello
    public void addCoin()
    {
        numCoin += 1;
        coinsAtLevel += 1;
        Debug.Log("Metodo addCoin: NumCoin: " + numCoin);
        Debug.Log("Metodo addCoin: coinsAtLevel " + coinsAtLevel);
        GameManager_logic.Instance.UpdateCoinText();
    }

   public void setCoin(int num)
    {
        numCoin = num;
        GameManager_logic.Instance.UpdateCoinText();
    }

    //Richiamare quando si finisce un livello
    //If level_completed then SaveCoinsAtLevel poco prima di switchare scena 
    public void SaveCoinsAtLevel()
    {
        coinsAtLevel = 0;
        GameManager_logic.Instance.UpdateCoinText();
        
    }
    
    //Richiamare quando si ricomincia il livello
    public void ResetCoinsToLevel()
    {
        numCoin -= coinsAtLevel;
        coinsAtLevel = 0;
        Debug.Log("Metodo Reset numCoin: " + numCoin);
        Debug.Log("Metodo Reset CoinsAtLevel: " + coinsAtLevel);
        GameManager_logic.Instance.UpdateCoinText();
    }
    
       
    public int getCoinLevel()
    {
        return coinsAtLevel;
    }
    public int getCoin()
    {
        return numCoin;
    }
}
