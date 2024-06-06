using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    public Transform[] dropPoints=new Transform[4];
    public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop(int i)
    {
        Instantiate(block, dropPoints[i].position, block.transform.rotation);
    }
}
