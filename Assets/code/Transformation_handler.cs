using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation_handler : MonoBehaviour
{
    public bool transformed = false;
    public GameObject baseSlime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && transformed == true)
        {
            LosePower();
        }

    }

    public void LosePower()
    {
        baseSlime.transform.position = gameObject.transform.position;

        gameObject.SetActive(false);

        baseSlime.SetActive(true);
    }


}
