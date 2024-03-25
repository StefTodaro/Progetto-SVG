using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class checkpoint_handler : MonoBehaviour
{

    public Transform checkpoint;
    public Transformation_handler tr;
   

   // public GameObject baseSlime;
    // Start is called before the first frame update
    void Start()
    {
        tr = gameObject.GetComponent<Transformation_handler>();
        
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
            tr.baseSlime.GetComponent<checkpoint_handler>().checkpoint = collision.transform;
        }
    }
    }
