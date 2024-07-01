using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;



public class Transformations
{
    public GameObject transformation;
    //controlla se la trasformazione è stata acquistata dallo shop
    public bool fromShop;
}

public class Transformation_logic : MonoBehaviour
{
    //controlla quali trasformazioni sono presenti nella scena
    public List<GameObject> transformationsInScene=new List<GameObject>();
    //lista delle trasformazioni acquisite
    public Transformations[] transformations = new Transformations[3];
    //lista contenente tutte le trasformazioni
    public GameObject slimeForms;
    //lista delle trasformazioni presenti nella scena
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


    public float transformJump = 5f;
   





    // Start is called before the first frame update
    void Start()
    {

        for (int i=0; i < 3; i++)
        {

            transformations[i] = new Transformations();
            if (transformations[i].transformation == null)
            {
                //la forma vera e propria è un figlio dell'oggetto
                transformations[i].transformation = baseSlime;
            }

        }

        transformationsUI = GameObject.FindGameObjectsWithTag("FormUI");
        //si definisce il cursore dei selezione della trasformazione e la posizione iniziale
        selector = GameObject.FindGameObjectWithTag("Selector");
        selector.transform.position = transformationsUI[c].transform.position;
        //si caricano le trasformazioni comprate dallo shop (se ce ne sono)
        LoadTransformations();

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
        //funzione per il cambio della trasformazione
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (c <= 2)
                {
                    c += 1;
                }
                if (c == 3)
                {
                    c = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (c >= 0)
                {
                    c -= 1;
                }
                if (c == -1)
                {
                    c = 2;
                }
            }
        }

        
        //controllo per far muovere il cursore sulla trasformazione attuale 
        if (selector.transform.position!= transformationsUI[c].transform.position)
        {
            selector.transform.position = Vector2.MoveTowards(selector.transform.position, transformationsUI[c].transform.position, 1080);
        }
    }

    //funzione per definire il baseSlime all'inizio dei livelli
    public void SetBaseSlime(GameObject baseForm)
    {
        baseSlime = baseForm;
    }

    public GameObject GetBaseSlime()
    {
        return baseSlime;
    }

    public GameObject GetCurrentTransformation()
    {
        return transformations[c].transformation;
    }

    public void SetCurrentTransformation(GameObject newForm)
    {
      
        transformations[c].transformation=newForm;
    }


    public bool ContainsTransformation(GameObject formToCheck)
    {
        foreach(Transformations t in transformations)
        { 
            if(t.transformation == formToCheck)
            return true;
        }
        return false;
    }


    public bool IsFromShop(int i)
    {
        if (transformations[i].fromShop)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFromShop(bool fromShop)
    {
        transformations[c].fromShop = fromShop; 
    }

    public void UpdateUI(int n)
    {
        foreach (Sprite sprite in transformationSprite)
        {
            if (transformations[n].transformation.name == sprite.name)
            {
                transformationsUI[n].GetComponent<Image>().sprite = sprite;

                break;
            }
        }
    }

    public void ActivateInvulnerability()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
        movement mov=player.GetComponent<movement>();
        mov.canBeHit = false;
        mov.rb.velocity = new Vector2(0, 6);

    }

    public void LosePower()
    {
        //si diminuisce il contatore delle trasformazioni ottenute
        t -= 1;
        SetCurrentTransformation(baseSlime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //la forma attuale viene disattivata e vengono raccolti tutti i dati utili
        var isGrounded = player.GetComponent<movement>().isGrounded;
        var actualPosition = player.transform.position;

        player.SetActive(false);

        //se la trasformazione era dello shop si resetta la cella di trassformazione
        if (IsFromShop(c))
        {
            SetFromShop(false);
        }

        //si sostituisce la forma attuale con quella di base
       
        bool found= false;
        GameObject newForm = baseSlime;

        UpdateUI(c);

        if (transformationsInScene.Count != 0)
        {
            foreach (GameObject t in transformationsInScene)
            {
                if (t.name == baseSlime.name)
                {
                    newForm = t;
                    found = true;
                    break;

                }
            }
        }

        if (!found)
        {
            GameObject instance = Instantiate(newForm, actualPosition, newForm.transform.rotation);
            instance.name = newForm.name;
            newForm = instance;
            transformationsInScene.Add(newForm);
        }


        newForm.SetActive(true);

        newForm.GetComponent<movement>().isSwinging = false;
        newForm.transform.position = actualPosition;
        newForm.GetComponent<movement>().isGrounded = isGrounded;

        //si applica la forza di salto dopo aver lasciato la trasformazione
        var rb = newForm.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 6f);
    }


    public void ChangeForm(GameObject newTransformation)
    {
        //controlla prima che la nuova forma non sia nella scena
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var isGrounded = player.GetComponent<movement>().isGrounded;
        var velocity = player.GetComponent<movement>().rb.velocity;
        var isSlamming = player.GetComponent<movement>().isSlamming;
        var canSlam = player.GetComponent<movement>().canSlam;
        var slamTimer = player.GetComponent<movement>().slamTimer;
        var canBeHit = player.GetComponent<movement>().canBeHit;
        var invulnerabilityTimer = player.GetComponent<movement>().invulnerabilityTimer;
        var actualPosition = player.transform.position;

        player.SetActive(false);
        bool found = false;

        //si controlla se la trasformazione è fra quelle presenti nella scena
        foreach(GameObject t in transformationsInScene)
        {//se si la si associa la nuova trasformazione con quella nella lista
            if (t.name == newTransformation.name)
            {
                newTransformation = t;
                found = true;
                break;
            }
        }
        //se la trasformazione non è presente nella scena allora viene aggiunta
        // alla scena e alla lista
        if (!found)
        {
            GameObject instance = Instantiate(newTransformation, actualPosition, newTransformation.transform.rotation);
            instance.name = newTransformation.name;
            newTransformation = instance;
            transformationsInScene.Add(newTransformation);
        }

        newTransformation.SetActive(true);

        //per ristabilire la grandezza originale dopo il cambio forma
        newTransformation.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        newTransformation.transform.position = actualPosition;
        newTransformation.GetComponent<movement>().canBeHit = canBeHit;
        newTransformation.GetComponent<movement>().invulnerabilityTimer = invulnerabilityTimer;
        newTransformation.GetComponent<movement>().isGrounded = isGrounded;
        newTransformation.GetComponent<movement>().isSlamming = isSlamming;
        newTransformation.GetComponent<movement>().rb.velocity = velocity;
        newTransformation.GetComponent<movement>().canSlam = canSlam;
        newTransformation.GetComponent<movement>().slamTimer = slamTimer;
        newTransformation.GetComponent<movement>().isGrounded = isGrounded;
        

        //si ha comunque una modifica della selezione delle trasformazioni
        UpdateUI(c);
    }

    public void ResetTransformation()
    {
        
        //stessa cosa succede per le altre trasformazioni
        for (int i = 0; i < 3; i++)
        {
            if (!IsFromShop(i))
            {
                transformations[i].transformation = baseSlime;
                t -= 1;
                UpdateUI(i);
            }
        }
        c = 0;
        //si ripulisce la lista degli oggetti presenti in scena
        transformationsInScene.Clear();
        ChangeForm(GetCurrentTransformation());
    }

    //salvataggio delle trasformazioni
    public void SaveTransformations()
    {
        for (int i = 0; i < transformations.Length; i++)
        {
            if (transformations[i].fromShop)
            {
                PlayerPrefs.SetString("transformation" + i, transformations[i].transformation.name);
            }
            else
            {   
                //se lo slot trasformazione è occupato da una trasformazione non
                //proveniente dallo shop, allora si svuota
                if (transformations[i] != null)
                {
                    PlayerPrefs.SetString("transformation" + i,null);

                }
            }
        }
    }
    //funzione utile per caricare i dati delle trasformazioni
    public void LoadTransformations()
    {   
        GameObject form = null;

        //scorrimento nel vettore delle trasformazioni 
        for (int i = 0; i < transformations.Length; i++)
        { 
            if ((PlayerPrefs.GetString("transformation" + i)!=null))
            {
                //si ricerca il nome della trasformazione salvata all'interno
                //della lista di tutte le trasformazioni
                for (int c = 0; c < slimeForms.transform.childCount; c++)
                {   
                    form = slimeForms.transform.GetChild(c).gameObject;
                    //trovato il nome corrispondente alla trasformazioni
                    //si avvalora il vettore con il prefabbricato della trasformazione
                    if ((PlayerPrefs.GetString("transformation" + i)== form.name)){
                        transformations[i].transformation = form;
                        transformations[i].fromShop = true;
                        UpdateUI(i);
                        break;
                    }
                }
            }
        }
    }



}
