using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

   [SerializeField] private AudioSource soundEffectObject;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //funzione richiamabile da tutti i gameObject per effettuare suoni
    public void PlaySoundEffect(AudioClip audioClip,Transform spawnTransform,float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, transform.rotation); ;

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject,clipLength );
    }
}
