using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip music;
    void Start()
    {
        MusicManager.Instance.PlaySoundEffect(music, transform, 0.65f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
