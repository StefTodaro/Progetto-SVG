using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMM_distrib : MonoBehaviour
{
    public GameObject[] spritePrefab=new GameObject[6]; // Prefab dello sprite da posizionare
    public int gridLength = 11;
    public int gridWidth = 22;

    public Vector2 cellSize=new Vector2(96f,96f); // Dimensione di ciascun quadrato della griglia

    void Start()
    {
        PlaceSprites();
    }

    void PlaceSprites()
    {   
        //ultima immagine istanziata
        GameObject lastSprite=null;

        //posizione del primo quadrato in alto a sinistra
        float startX = -912f;
        float startY = 490f ;

        //si utilizzano dimensioni della schermata misurata in quadrati
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 20; x++)
            {   

                int r = Random.Range(0, 6);
                //se l'immagine che sta per essere istanziata è uguale alla precedente
                //allora si ricalcola
                if (lastSprite == spritePrefab[r])
                {
                    r = Random.Range(0, 6);
                }
                GameObject cell = Instantiate(spritePrefab[r]);
                 lastSprite = cell;
                // si imposta lo sfondo come genitore 
                cell.transform.SetParent(transform, false);

                RectTransform rectTransform = cell.GetComponent<RectTransform>();

                Vector2 position = new Vector2( startX+ (x * cellSize.x),startY - (y * cellSize.y));
                rectTransform.anchoredPosition = position;
                rectTransform.sizeDelta = cellSize;
            }
        }
    }
}
