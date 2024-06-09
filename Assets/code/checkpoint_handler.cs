using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class checkpoint_handler : MonoBehaviour
{
    //fornisce il punto di respawn allo slime di base
    public bool activated=false;
    public GameObject game_manager;
    public GameObject objectList;
    public Animator anim;
    public AudioClip activationAudio;
    
    
    
   

   // public GameObject baseSlime;
    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("game_manager");
        objectList= GameObject.FindGameObjectWithTag("inObjects");
        anim = gameObject.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
      // checkpoint_base = GameObject.Find("slime").GetComponent<checkpoint_handler>().checkpoint_base;
    }

  

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")  && !activated)
            {
                SoundEffectManager.Instance.PlaySoundEffect(activationAudio, transform, 0.5f);
                activated = true;
                game_manager.GetComponent<GameManager_logic>().SetCheckpoint(transform.position);
                anim.SetBool("active", activated);
            }
        }

    
    
    }
