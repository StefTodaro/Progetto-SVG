using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Secret_wall_logic : MonoBehaviour
{
   public  bool isVisible=false;
    public Color initialColor; 
    public Tilemap wallRenderer;
    public float initialAlpha;
    public float fadeSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<Tilemap>();
        initialColor = wallRenderer.color;
        initialAlpha = initialColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible && wallRenderer.color.a!=0 )
        {
            FadeOut();
        }
        if (!isVisible && wallRenderer.color.a != 1)
        {
            FadeIn();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isVisible = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isVisible = false;
        }
    }

    public void FadeOut()
    {
        Color newColor = initialColor;
        newColor.a = Mathf.MoveTowards(wallRenderer.color.a, 0, fadeSpeed * Time.deltaTime);
        wallRenderer.color = newColor;
    }

    public void FadeIn()
    {
        Color newColor = initialColor;
        newColor.a = Mathf.MoveTowards(wallRenderer.color.a, initialAlpha, fadeSpeed * Time.deltaTime);
        wallRenderer.color = newColor;
    }
}
