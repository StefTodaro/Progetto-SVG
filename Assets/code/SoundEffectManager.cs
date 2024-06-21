using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    private float MusicFx = 0.65f;
    public Slider volumeSlider;
    SoundMixerManager soundMixerManager;
    public AudioMixer audioM;
    [SerializeField] private AudioSource soundEffectObject;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        soundMixerManager = GameObject.FindGameObjectWithTag("soundMixerManager").GetComponent<SoundMixerManager>();
        audioM = soundMixerManager.audioMixer;

        

        volumeSlider.value = MusicFx;
        audioM.SetFloat("SoundFX", Mathf.Log10(MusicFx) * 20f);
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

        audioSource.volume = MusicFx;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject,clipLength );
    }


    public void VolumeUpdater(float volume)
    {
        MusicFx = volume;
        volumeSlider.value = volume;
        audioM.SetFloat("SoundFX", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("volumeFX", MusicFx);
    }
    public void OnEnable()
    {
        if (PlayerPrefs.GetFloat("volumeFX") != 0)
        {
            MusicFx = PlayerPrefs.GetFloat("volumeFX");
        }
    }
}
