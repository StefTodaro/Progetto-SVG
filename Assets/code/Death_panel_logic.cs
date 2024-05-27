using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_panel_logic : MonoBehaviour
{
    public GameObject mainCamera;
    public Animator anim;
  
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("camera");
        anim=gameObject.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void ActivePanel()
    {   
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = true;
        anim.enabled=true;
    }

    public void ResetCameraFocus()
    {
        mainCamera.GetComponent<Camera_Follow>().cameraLocked = false;
       
    }

    public void EndPanelTrnsition()
    {
        anim.enabled = false;
    }
}
