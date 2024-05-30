using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_mob : MonoBehaviour
{
    public Mob_manager mm;

    private void Start()
    {
        mm = GameObject.Find("MobManager").GetComponent<Mob_manager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //funzione con cui modificare il comportamento di tutti i mob per ucciderli
    private void KillMob()
    {
        gameObject.SetActive(false);
        mm.MakeCloud(transform);
    }
}
