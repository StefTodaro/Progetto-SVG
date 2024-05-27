using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private GameObject player;
    public bool cameraLocked;
    // Riferimento al componente Cinemachine Virtual Camera
    private CinemachineVirtualCamera cinemachineCamera;
    public Vector2 checkpointPos;

    // Start is called before the first frame update
    void Start()
    {
       

        // Ottieni il componente Cinemachine Virtual Camera attaccato a questo GameObject
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        checkpointPos = new Vector2(3.05715561f, -0.285339117f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraLocked)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            cinemachineCamera.Follow = player.transform;
        }
        else
        {
            cinemachineCamera.Follow = null;
        }
    }

}
