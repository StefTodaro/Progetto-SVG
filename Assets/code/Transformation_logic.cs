using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Transformation_logic : MonoBehaviour
{
    public GameObject[] transformations = new GameObject[3];
    public bool full=false;
    //contatore delle trasformazioni
    public int t=0;
    //variabile per tenere conto della trasformazione attuale
    public int c = 0;
    public GameObject baseSlime;

   
    // Start is called before the first frame update
    void Start()
    {
        baseSlime = GameObject.FindGameObjectWithTag("Player");
        transformations[0] = baseSlime;
        transformations[1] = baseSlime;
        transformations[2] = baseSlime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (t == 3)
        {
            full = true;
        }
        else
        {
            full = false;
        }
    }

  

  

}