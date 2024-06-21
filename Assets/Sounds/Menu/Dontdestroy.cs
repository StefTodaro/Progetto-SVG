using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroy : MonoBehaviour
{
    // Start is called before the first frame update
    
        public static Dontdestroy Instance;
    void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

    }  
}


