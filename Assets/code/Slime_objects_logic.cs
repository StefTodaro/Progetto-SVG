using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Slime_objects_logic : MonoBehaviour
{
    public float detectionRadius = 0.7f;
    //oggetti inglobati dal giocatore
    public GameObject inObject;
    
    public float incorporateTimer;
    public float incorporateTime = 0.8f;
    public float ejectTimer;
    public float ejectTime = 0.8f;
    public bool canIncorporate = true;
    public bool canEject = true;
    public int indexCoin = 0; //Contatore delle box incorporate

    public AudioClip interactSound;

    private TextMeshProUGUI boxText;
    
  
    // Start is called before the first frame update
    void Start()
    {
        ejectTimer = ejectTime;
        incorporateTimer = incorporateTime;
        inObject = GameObject.FindGameObjectWithTag("inObjects");
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        float minDistance = Mathf.Infinity;
        Collider2D nearestCollider = null;

        // Trova l'oggetto più vicino
        foreach (Collider2D collider in colliders)
        {
            // Calcola la distanza tra il giocatore e l'oggetto
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            // Se l'oggetto è più vicino dell'oggetto attualmente più vicino, aggiornalo
            if (distance < minDistance && collider.CompareTag("Object") && 
                inObject.GetComponent<Incorporated_objects_list>().list.Count<=4)
            {
                minDistance = distance;
                nearestCollider = collider;
            }
        }

        // Interagisci solo con l'oggetto più vicino se è stato trovato e il giocatore preme il tasto
        if (nearestCollider != null && Input.GetButtonDown("Fire2") && canIncorporate)
        {
            SoundEffectManager.Instance.PlaySoundEffect(interactSound, transform, 1f);
            inObject.GetComponent<Incorporated_objects_list>().list.Add(nearestCollider.gameObject);
            nearestCollider.gameObject.SetActive(false);
            
            Debug.Log("**Box: " + indexCoin);
            UpdateInObjectUI(inObject.GetComponent<Incorporated_objects_list>().list.Count());
            
        }
        if (canIncorporate == false)
        {
            incorporateTimer -= Time.deltaTime;
        }
        if (incorporateTimer <= 0)
        {
            canIncorporate = true;
            incorporateTimer = incorporateTime;
        }

        if (canEject == false)
        {
            ejectTimer -= Time.deltaTime;
        }
        if (ejectTimer <= 0)
        {
            canEject = true;
            ejectTimer = ejectTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && canEject)
        {
            //Se non si hanno casse non fare niente
            if (inObject.GetComponent<Incorporated_objects_list>().list.Count() > 0){
                canIncorporate = false;
                canEject = false;
                SoundEffectManager.Instance.PlaySoundEffect(interactSound, transform, 1f);
                GetComponent<movement>().anim.SetBool("drop", true);
                inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count - 1].SetActive(true);
                
                // Posiziona l'oggetto sotto i piedi del giocatore
                Vector3 playerFeetPosition = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y, 0f);
                inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count - 1].transform.position = playerFeetPosition;
                removeBox();
            }

        }
    }

    public void removeBox()
    {
        
        inObject.GetComponent<Incorporated_objects_list>().list.Remove(inObject.GetComponent<Incorporated_objects_list>().list[inObject.GetComponent<Incorporated_objects_list>().list.Count - 1]);
        UpdateInObjectUI(inObject.GetComponent<Incorporated_objects_list>().list.Count());
    }
    
    public void UpdateInObjectUI(int num)
    {
        boxText = GameObject.FindGameObjectWithTag("boxCounter").GetComponent<TextMeshProUGUI>();

        boxText.SetText(num.ToString());
    }


    public void EndDrop()
    {
        GetComponent<movement>().anim.SetBool("drop", false);
    }

    void OnDrawGizmosSelected()
    {
        // Disegna la zona di rilevamento per debug
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
