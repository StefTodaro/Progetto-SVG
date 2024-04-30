using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class checkpoint_handler : MonoBehaviour
{
    //fornisce il punto di respawn allo slime di base
    public Transform checkpoint_base;
    
    
   

   // public GameObject baseSlime;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        checkpoint_base = GameObject.Find("slime").GetComponent<checkpoint_handler>().checkpoint_base;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("checkpoint"))
        {
            checkpoint_base = collision.transform;
        }
    }
    }
