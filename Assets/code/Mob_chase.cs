using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_chase : MonoBehaviour
{
    public float speed = 3.5f;
    public float lineOfSight;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceToPlayer< lineOfSight)
        {

        }
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3( lineOfSight, lineOfSight));
    }

}
