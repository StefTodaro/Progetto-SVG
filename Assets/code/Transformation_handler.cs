using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation_handler : MonoBehaviour
{
    public bool transformed = false;
    public GameObject baseSlime;
    public float transformJump=5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = baseSlime.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && transformed == true)
        {
            LosePower();
            rb.velocity = new Vector2(rb.velocity.x, transformJump);
        }

    }

    public void LosePower()
    {
        baseSlime.transform.position = gameObject.transform.position;

        gameObject.SetActive(false);

        baseSlime.SetActive(true);
    }


}
