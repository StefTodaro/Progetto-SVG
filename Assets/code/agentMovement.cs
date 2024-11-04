using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentMovement : MonoBehaviour
{
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float posx = transform.position.x;
        float posy = transform.position.y;
        //quando viene caricata la scena imposto la posizione dello slime
        //nell'ultima posizione salvata
        if(PlayerPrefs.GetFloat("slimePosx")!=0)
        posx = PlayerPrefs.GetFloat("slimePosx");

        if (PlayerPrefs.GetFloat("slimePosy") != 0)
        posy = PlayerPrefs.GetFloat("slimePosy");

        transform.position=new Vector2(posx, posy);
    }



    public void Movement(Transform waypoint)
    {
        agent.SetDestination(waypoint.position);
        if(waypoint.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
