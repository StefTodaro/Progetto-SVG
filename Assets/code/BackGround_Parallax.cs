using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Parallax : MonoBehaviour
{
    Vector3 startPosition;
    public Camera cam;
    public float modifier;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 camOffset = cam.transform.position * modifier;
        transform.position = startPosition + new Vector3(camOffset.x, camOffset.y, 0);
    }
}
