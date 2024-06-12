using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource audioMusic;
    // Start is called before the first frame update
    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (audioMusic == null)
            {
            audioMusic = GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //funzione richiamabile da tutti i gameObject per effettuare suoni
    public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(audioMusic, spawnTransform.position, transform.rotation); ;

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
