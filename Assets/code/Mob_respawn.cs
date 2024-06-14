using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Mob_respawn : MonoBehaviour
{
    public GameObject mob_manager;
    public float respawnTimer = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        mob_manager=GameObject.Find("MobManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        respawnTimer = 0;
    }
   



}
