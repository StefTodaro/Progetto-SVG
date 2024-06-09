using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_logic : MonoBehaviour
{
    public bool onGround;
    public bool dropCoin=true;
    public GameObject cloud;
    public AudioClip boxBreakAudioClip;
    public GameObject boxBreakEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Break()
    {
        if (boxBreakAudioClip != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(boxBreakAudioClip, transform, 0.2f);
        }
        Instantiate(boxBreakEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GetComponent<Animator>())
        {
                GetComponent<Animator>().SetBool("jumpOn", true);
            
          
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            onGround = true;
            Instantiate(cloud,transform.position,cloud.transform.rotation);
        }

        //Controllo se la cassa è sopra ad un'altra, cosa che probabilmente succede quando queste sono a contatto 
        //con il terreno
        if (collision.gameObject.CompareTag("Object"))
        {
            onGround=true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            onGround = false;
        }

       
        if (collision.gameObject.CompareTag("Object"))
        {
            onGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("jumpOn", false);

        }
       
    }
}
