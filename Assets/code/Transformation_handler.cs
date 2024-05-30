using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Transformation_handler : MonoBehaviour
{   
    public bool transformed = false;
    public float transformJump=5f;
    public Rigidbody2D rb_base;
    public GameObject currentTransformation;
    public Transformation_logic transformations;

    // Start is called before the first frame update
    void Start()
    {
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();

        rb_base = transformations.baseSlime.GetComponent<Rigidbody2D>();
        currentTransformation = transformations.transformations[transformations.c];
    }

    //assegnazione delle variabili quando l'oggetto viene attivato per il passaggio da una forma all'altra 
    void OnEnable()
    {
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
        
        rb_base = transformations.baseSlime.GetComponent<Rigidbody2D>();
        currentTransformation = transformations.transformations[transformations.c];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && transformed == true)
        {
            LosePower();
            rb_base.velocity = new Vector2(rb_base.velocity.x, transformJump);
        }
       
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            ChangeForm();
        }
    }

    public void LosePower()
    {
        transformations.t-=1;
        //la forma attuale viene disattivata e vengono raccolti tutti i dati utili
        var isGrounded = currentTransformation.GetComponent<movement>().isGrounded;
        var actualPosition = currentTransformation.transform.position;

        currentTransformation.SetActive(false);
        
        //si sostituisce la forma attuale con quella di base
        transformations.transformations[transformations.c] = transformations.baseSlime;
        currentTransformation = transformations.transformations[transformations.c];

        transformations.UpdateUI(transformations.c);

        //vengono assegnate le iniformazioni raccolte prima alla nuova forma(slime base )
        currentTransformation.SetActive(true);
        currentTransformation.GetComponent<movement>().isSwinging = false;
        currentTransformation.transform.position = actualPosition;
        currentTransformation.GetComponent<movement>().isGrounded = isGrounded;   
}

   public void ActivateInvulnerability()
    {
        currentTransformation.GetComponent<movement>().canBeHit = false;
        currentTransformation.GetComponent<movement>().rb.velocity = Vector2.zero;
    }

    public void ChangeForm()
    {
       
        var isGrounded = currentTransformation.GetComponent<movement>().isGrounded;
        var velocity = currentTransformation.GetComponent<movement>().rb.velocity;
        var isSlamming = currentTransformation.GetComponent<movement>().isSlamming;
        var canSlam = currentTransformation.GetComponent<movement>().canSlam;
        var slamTimer = currentTransformation.GetComponent<movement>().slamTimer;
        var canBeHit = currentTransformation.GetComponent<movement>().canBeHit;
        var invulnerabilityTimer = currentTransformation.GetComponent<movement>().invulnerabilityTimer;
        var actualPosition = currentTransformation.transform.position;
        
        currentTransformation.SetActive(false);

        //in base al tasto premuto si scorre il vettore delle trasformazioni
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (transformations.c <= 2)
            {
                transformations.c += 1;
            }
            if(transformations.c==3)
            {
                transformations.c = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (transformations.c >= 0)
            {
                transformations.c -= 1;
            }
            if(transformations.c==-1)
            {
                transformations.c = 2;
            }
        }
        currentTransformation = transformations.transformations[transformations.c];

        currentTransformation.SetActive(true);
        transformations.UpdateUI(transformations.c);
        currentTransformation.GetComponent<movement>().canBeHit = canBeHit;
        currentTransformation.GetComponent<movement>().invulnerabilityTimer = invulnerabilityTimer;
        currentTransformation.transform.position = actualPosition;
        currentTransformation.GetComponent<movement>().isGrounded = isGrounded;
        currentTransformation.GetComponent<movement>().isSlamming = isSlamming;
        currentTransformation.GetComponent<movement>().rb.velocity = velocity;
        currentTransformation.GetComponent<movement>().canSlam = canSlam;
        currentTransformation.GetComponent<movement>().slamTimer = slamTimer;
        currentTransformation.GetComponent<movement>().isGrounded = isGrounded;
        
    }
}
