using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMusic : MonoBehaviour
{
    triggerMusic triggerM;
    // Start is called before the first frame update
    void Start()
    {
        triggerM = GameObject.FindWithTag("clip").GetComponent<triggerMusic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && triggerM.getStatusMusic())
        {
            triggerM.StopMusic();
            triggerM.setStautsMusic(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
