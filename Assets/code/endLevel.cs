using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel : MonoBehaviour
{
    // Start is called before the first frame update

    private bool Completed;
    private coinCounter CoinCounterScript;
    private Transformation_logic transf_l;
    private Incorporated_objects_list inc_obj;
    private GameManager_logic gManager;
    [SerializeField] private Animator anim;
    
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ! Completed)
        {
            anim.SetBool("end", true);
            Completed = true;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Completed)
        {
            GameManager_logic.Instance.EndLevel();
        }
    }
}
