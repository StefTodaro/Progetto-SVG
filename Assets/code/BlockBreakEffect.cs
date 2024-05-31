using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakEffect : MonoBehaviour
{

    public GameObject breakEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CallBreakEffect()
    {

        Instantiate(breakEffect, transform.position, transform.rotation);
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);
                Instantiate(breakEffect, childTransform.position, childTransform.rotation);
            }
        }
    }
}
