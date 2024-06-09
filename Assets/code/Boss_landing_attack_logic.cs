using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_landing_attack_logic : MonoBehaviour
{
    public bool left;
    public float timeToLive = 1.5f;
    public float timerOfLife;
    public float speed=4.2f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOfLife < timeToLive)
        {
            if (left)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * -speed * Time.deltaTime);
            }
            timerOfLife += Time.deltaTime;
        }else if (timerOfLife >= timeToLive)
            {
                Destroy(gameObject);
            }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {   
           collision.GetComponent<Object_logic>().Break();
            Destroy(gameObject);
        }
    }
}
