using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = 0.65f;

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
