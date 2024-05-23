using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableObjects : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private bool initialPatrolState;
   

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        if(gameObject.GetComponent<Mob_patrol>())
        {
            initialPatrolState = gameObject.GetComponent<Mob_patrol>().isPatrolling;
        }

    }


    public void ResetState()
    {   
        
            gameObject.SetActive(true);
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            transform.localScale = initialScale;
        
        //reset di stati per diverse caratteristiche dei mob
        if (gameObject.GetComponent<Mob_patrol>())
        {
            gameObject.GetComponent<Mob_patrol>().isPatrolling = initialPatrolState;
        }

        if (gameObject.GetComponent<Rino_Logic>())
        {
            gameObject.GetComponent<Rino_Logic>().canCharge=true;
        }

        if (gameObject.GetComponent<Rock_logic>())
        {
            gameObject.GetComponent<Animator>().SetBool("respawn", true);
            gameObject.GetComponent<Rock_logic>().hit.hit = 0 ;
        }
       

    }
}
