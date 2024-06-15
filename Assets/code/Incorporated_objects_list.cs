using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Incorporated_objects_list : MonoBehaviour
{
    Slime_objects_logic sl_obj;
    public List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update

    public void ClearInObject()
    {
        list.Clear();
        sl_obj.UpdateInObjectUI(list.Count());
        
        
    }

    private void Start()
    {
        sl_obj = GameObject.FindGameObjectWithTag("Player").GetComponent<Slime_objects_logic>();
    }
    
}
