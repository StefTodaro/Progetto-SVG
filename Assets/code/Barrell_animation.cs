using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrell_animation : MonoBehaviour
{ 
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
        if (collision.gameObject.CompareTag("Player")){
            if (!collision.GetComponent<movement>().isGrounded)
            {
                GetComponent<Animator>().SetBool("jumpOn", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Animator>().SetBool("jumpOn", false);
    }
}
