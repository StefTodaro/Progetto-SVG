using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private GameObject player;

    // Riferimento al componente Cinemachine Virtual Camera
    private CinemachineVirtualCamera cinemachineCamera;

    // Start is called before the first frame update
    void Start()
    {
       

        // Ottieni il componente Cinemachine Virtual Camera attaccato a questo GameObject
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cinemachineCamera.Follow = player.transform;
    }
}
