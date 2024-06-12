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
    //variabile della trasformazione attuale
    public int c = 0;
    public GameObject baseSlime;


   
    // Start is called before the first frame update
    void Start()
    {   
        //ricerca del base slime anche se questo è inattivo nella gerarchia 
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

        //controllo per far muovere il cursore sulla trasformazione attuale 
        if (selector.transform.position!= transformationsUI[c].transform.position)
        {
            selector.transform.position = Vector2.MoveTowards(selector.transform.position, transformationsUI[c].transform.position, 1080);
        }
    }
    public void ResetTransformation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Transformation_handler>().transformed){
            player.GetComponent<Transformation_handler>().LosePower();
        }
        for (int i = 0;  i<3;i++)
        {
            transformations[i] = baseSlime;
            UpdateUI(i);
        }

        //si riporta il giocatore alla prima trasformazione
        c = 0;
        
    }


    public void UpdateUI(int n)
    {
        foreach (Sprite sprite in transformationSprite)
        {
            if (transformations[n].name == sprite.name)
            {
                transformationsUI[n].GetComponent<Image>().sprite = sprite;

                break;
            }
        }
    }


  

}
