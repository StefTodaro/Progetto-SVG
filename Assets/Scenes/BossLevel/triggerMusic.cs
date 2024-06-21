using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerMusic : MonoBehaviour
{
    private bool StartedMusic = false;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>();
        StartedMusic = false;
    }

    public void setStautsMusic(bool status)
    {
        StartedMusic = status;
    }
    public bool getStatusMusic()
    {
        return StartedMusic;
    }

    
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        
        if (collision.CompareTag("Player") && !StartedMusic)
        {
           
            audioSource.volume = PlayerPrefs.GetFloat("volumeM");
            audioSource.Play();
            StartedMusic = true;
            
            
        }
        

        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
