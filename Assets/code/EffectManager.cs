using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEffect()
    {
        Destroy(gameObject);
    }

    public void DeactivateEffect()
    {
       gameObject.SetActive(false);
    }
}
