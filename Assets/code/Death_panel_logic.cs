using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_panel_logic : MonoBehaviour
{
    public Animator anim;
  
    // Start is called before the first frame update
    void Start()
    {
        anim=gameObject.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void ActivePanel()
    {   
        anim.enabled=true;
    }

    public void ResetCameraFocus()
    {
       GameManager_logic.Instance.UnlockCamera();
       
    }

    public void EndPanelTransition()
    {
        anim.enabled = false;
    }
}
