using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Transformation_logic : MonoBehaviour
{
    public GameObject[] transformations = new GameObject[3];
    public GameObject[] transformationsUI;
    //lista con gli sprite delle trasformazioni per la UI
    public List<Sprite> transformationSprite;
    public GameObject selector;

    public bool full=false;
    //contatore delle trasformazioni
    public int t=0;
    //variabile per tenere conto della trasformazione attuale
    public int c = 0;
    public GameObject baseSlime;


   
    // Start is called before the first frame update
    void Start()
    {   
        /*//ricerca del base slime anche se questo è inattivo nella gerarchia 
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if ( obj.name == "slime")
            {
                baseSlime=obj;
            }
        }*/
        baseSlime = GameObject.Find("SlimeBase");

        transformations[0] = baseSlime;
        transformations[1] = baseSlime;
        transformations[2] = baseSlime;

        transformationsUI = GameObject.FindGameObjectsWithTag("FormUI");
        //si definisce il cursore dei selezione della trasformazione e la posizione iniziale
        selector = GameObject.FindGameObjectWithTag("Selector");
        selector.transform.position = transformationsUI[c].transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (t == 3)
        {
            full = true;
        }
        else
        {
            full = false;
        }
        if (selector.transform.position!= transformationsUI[c].transform.position)
        {
            selector.transform.position = Vector2.MoveTowards(selector.transform.position, transformationsUI[c].transform.position, 5);
        }
    }

    public void UpdateUI(int n)
    {
        foreach (Sprite sprite in transformationSprite)
        {
            if (transformations[n].name == sprite.name)
            {
                transformationsUI[n].GetComponent<Image>().sprite = sprite;

                // Muove il giocatore nella direzione dell'oggetto
                break;
            }
        }
    }


  

}
