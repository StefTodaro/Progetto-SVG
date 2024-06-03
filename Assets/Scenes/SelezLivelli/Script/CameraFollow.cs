using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform player; // Il Transform del personaggio da seguire
    public Vector3 offset; // L'offset della telecamera rispetto al personaggio

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position + offset;
            newPosition.z = transform.position.z; // Mantiene la coordinata Z originale della telecamera
            transform.position = newPosition;
        }
    }
}
