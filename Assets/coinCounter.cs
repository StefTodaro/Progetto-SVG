using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCounter : MonoBehaviour
{
    public int numCoin= 0;

    public void addCoin()
    {
        numCoin += 1;
    }

   public void setCoin(int num)
    {
        numCoin = num;
    }

    public int getCoin()
    {
        return numCoin;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
