using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class MusicManagement : MonoBehaviour
{
    // Start is called before the first frame update

    SoundMixerManager soundMixerManager;
    public AudioMixer audioM;
    //public AudioClip music;
    
   
    public Slider volumeSlider;
    private float MusicVolume = 0.65f;
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("soundMixerManager").GetComponent<SoundMixerManager>();
        audioM = soundMixerManager.audioMixer;
       
        
        

        
        volumeSlider.value = MusicVolume;

       

        //audioSource.volume = MusicVolume;
        audioM.SetFloat("Music", Mathf.Log10(MusicVolume) * 20f);
        


    }

    // Update is called once per frame
    void Update()
    {
        //audioM.SetFloat("Music", Mathf.Log10(MusicVolume) * 20f);
        //audioSource.volume = MusicVolume;
      //  PlayerPrefs.SetFloat("volume", MusicVolume);
    }

    //Funzione che viene attivata dallo Slider
    public void VolumeUpdater(float volume)
    {
        MusicVolume = volume;
        volumeSlider.value = volume;
        audioM.SetFloat("Music", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("volumeM", MusicVolume);
    }
    public void OnEnable()
    {
        if (PlayerPrefs.GetFloat("volumeM")!= 0)
        {
            MusicVolume= PlayerPrefs.GetFloat("volumeM");
        }
    }


    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volumeM");
        
        volumeSlider.value = 1;
    }
}
