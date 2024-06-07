using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_mob : MonoBehaviour
{
    public GameObject cloud;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //funzione con cui modificare il comportamento di tutti i mob per ucciderli
    private void KillMob()
    {
        gameObject.SetActive(false);
        Instantiate(cloud, transform.position, cloud.transform.rotation);
    }
}
