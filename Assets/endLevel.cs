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
    
    void Start()
    {

        //inc_obj = GameObject.FindGameObjectWithTag("inObjects").GetComponent<Incorporated_objects_list>();
        //CoinCounterScript = GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();
        gManager = GameObject.FindGameObjectWithTag("game_manager").GetComponent<GameManager_logic>();
        //transf_l = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            gManager.EndLevel();
            Completed = true;
            
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
