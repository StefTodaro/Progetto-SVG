using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class checkpoint_handler : MonoBehaviour
{

    public Transform checkpoint;
   

   // public GameObject baseSlime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("checkpoint"))
        {
            checkpoint = collision.transform;
        }
    }
    }
