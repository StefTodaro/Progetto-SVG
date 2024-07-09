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
    [SerializeField] private CinemachineFramingTransposer transposer;
    //variabile per controllare se il giocatore sta guardando a destra 
    [SerializeField] private bool right;


    // Start is called before the first frame update
    void Start()
    {
       

        // Ottieni il componente Cinemachine Virtual Camera attaccato a questo GameObject
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        transposer=cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraLocked)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            right=player.GetComponent<movement>().facingRight;
            cinemachineCamera.Follow = player.transform;
        }
        else
        {
            cinemachineCamera.Follow = null;
        }

        if (right)
        {
            transposer.m_TrackedObjectOffset.x = 3f;
        }
        else
        {
            transposer.m_TrackedObjectOffset.x = -3f;
        }


       
    }

}
