using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     if(mainCamera != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;

            Vector3 newPosition = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);

            transform.position = newPosition;
        }   
    }
}
