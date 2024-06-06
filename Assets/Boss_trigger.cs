using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_trigger : MonoBehaviour
{ public bool activated=false;
  public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindObjectOfType<Boss_logic>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTrigger()
    {
        activated = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            boss.GetComponent<Boss_logic>().StartFight();
            gameObject.SetActive(false);
            activated = true;
        }
    }
}
