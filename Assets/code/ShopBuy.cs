using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopBuy : MonoBehaviour
{
    public float bounce = 6;
    public GameObject parent;
    public GameObject slime_form;
    public Transformation_logic transformations;
    public Transformation_handler handler;
    public coinCounter coinCounter;
    public int price=5;

    public GameObject aquisitionEffect;

    public AudioClip cashInSound;
    public AudioClip cantBuySound;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        transformations = GameObject.FindGameObjectWithTag("t_handler").GetComponent<Transformation_logic>();
        coinCounter= GameObject.FindGameObjectWithTag("coinCounter").GetComponent<coinCounter>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        handler = collision.gameObject.GetComponent<Transformation_handler>();
        //il secondo controllo permette di effettuare l'acquisto solo se il giocatore salta sopra l'oggetto
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.transform.position.y > (transform.position.y + 0.5) &&
            !transformations.full)
        {
            if (coinCounter.getCoin() >= price)
            {
                SoundEffectManager.Instance.PlaySoundEffect(cashInSound, transform, 0.7f);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounce);


                if (slime_form != null)
                {
                    AddTransformation();
                    handler.ChangeForm();
                }


                if (collision.gameObject.GetComponent<movement>().isSlamming)
                {
                    collision.gameObject.GetComponent<movement>().isSlamming = false;
                    collision.gameObject.GetComponent<movement>().canSlam = true;
                }

                coinCounter.setCoin(coinCounter.getCoin() - price);
                GameManager_logic.Instance.UpdateCoinText();
                Instantiate(aquisitionEffect, transform.position, transform.rotation);
                Destroy(parent);
            }
            else
            {
                SoundEffectManager.Instance.PlaySoundEffect(cantBuySound, transform, 0.7f);

            }
        }
        
    }

    public void AddTransformation()
    {
        //controlla che la trasformazione non sia già contenuta nella lista delle trasformazioni 
        if (!transformations.transformations.Contains(slime_form))
        {
            //cerca la prima posizione in cui è possibile inserire la trasformazione appena ottenuta
            for (int i = 0; i < 3; i++)
            {
                if (transformations.transformations[i] == transformations.baseSlime)
                {
                    //trasforma il giocatore nella forma appena ottenuta
                    transformations.c = i;
                    transformations.transformations[transformations.c] = slime_form;
                    break;
                }
            }
            transformations.t += 1;
        }
    }
}
