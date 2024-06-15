using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableObjects : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private bool initialActive;
    private bool initialPatrolState;
   

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        initialActive = gameObject.activeSelf;
        if(gameObject.GetComponent<Mob_patrol>())
        {
            initialPatrolState = gameObject.GetComponent<Mob_patrol>().isPatrolling;
        }

    }


    public void ResetState()
    {   
        
            gameObject.SetActive(initialActive);
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            transform.localScale = initialScale;
        
        //reset di stati per diverse caratteristiche di vari gameObject
        if (gameObject.GetComponent<Mob_patrol>())
        {
            gameObject.GetComponent<Mob_patrol>().isPatrolling = initialPatrolState;
        }
        //si resetta la possibilità di caricare del rinoceronte
        if (gameObject.GetComponent<Rino_Logic>())
        {
            gameObject.GetComponent<Rino_Logic>().canCharge=true;
        }

        //si riporta la roccia nella prima fase 
        if (gameObject.GetComponent<Rock_logic>())
        {
            gameObject.GetComponent<Animator>().SetBool("respawn", true);
            gameObject.GetComponent<Rock_logic>().hit.hit = 0 ;
        }

         //si reimposta un mob per l'inseguimento
        if (gameObject.GetComponent<Mob_chase>())
        {
            gameObject.GetComponent<Mob_chase>().canChase = true;
        }


        //controlla che l'oggetto sia il gestore di mob della bossfight
        if (gameObject.GetComponent<BossMobsSpawn>())
        {
            gameObject.GetComponent<BossMobsSpawn>().ResetMobInScene();
        }

        //si resetta il trigger che dà inizio alla bossfight 
        if (gameObject.GetComponent<Boss_trigger>())
        {
            gameObject.GetComponent<Boss_trigger>().ResetTrigger();
        }

        //controlla che l'oggetto sia il boss e ne resetta le caratteristiche principali
        if (gameObject.GetComponent<Boss_logic>())
        {
            gameObject.GetComponent<Boss_logic>().ResetBossFight();
        }
    }
}
