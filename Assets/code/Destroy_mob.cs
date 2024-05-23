using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_mob : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(false);
        
    }

    //funzione con cui modificare il comportamento di tutti i mob per ucciderli
    private void KillMob()
    {
        gameObject.SetActive(false);
    }
}
