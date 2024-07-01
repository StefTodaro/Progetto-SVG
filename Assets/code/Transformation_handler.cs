using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using static Transformation_logic;

public class Transformation_handler : MonoBehaviour
{   
    public bool transformed = false;
    public float transformJump=5f;
    public GameObject dropFormEffect;
    public Transformation_logic transformations;
    public AudioClip releaseTransformSound;

    private void Start()
    {
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
        //si cambia nella prima trasformazione nella lista delel trasformazioni
        transformations.ChangeForm(transformations.GetCurrentTransformation());

    }
    //assegnazione delle variabili quando l'oggetto viene attivato per il passaggio da una forma all'altra 
    void OnEnable()
    {
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && transformed == true)
        {
            if (dropFormEffect != null)
            {
                Instantiate(dropFormEffect, transform.position, dropFormEffect.transform.rotation);
            }
            SoundEffectManager.Instance.PlaySoundEffect(releaseTransformSound, transform, 0.6f);
            transformations.LosePower();
        }
       
        //attivazione animazione per il cambio della trasformazione 
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            GetComponent<movement>().anim.SetBool("changing", true);
        }
    }

    public void EndTransformation()
    {
        GetComponent<movement>().anim.SetBool("changing", false);
       transformations.ChangeForm(transformations.GetCurrentTransformation());
    }
}
