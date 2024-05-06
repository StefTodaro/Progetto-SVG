using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tonguePivotLogic : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public pivotTOngueLogic grappleRope;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;
    public DistanceJoint2D distanceJoint;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;


    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }


    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;


    public Transform pivot;
    public float pendulumForce = 2f;
    public GameObject player;
    public Vector2 currentVelocity;
    public bool isSwinging;
    public bool hasSwang;
    float distance = 0;


    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        distanceJoint.enabled = false;

        distanceJoint.distance = Vector2.Distance(transform.position, pivot.position);


    }

    private void Update()
    {
        
        if (pivot != null)
        {   
            distance = Vector2.Distance(transform.position, pivot.position);
            distanceJoint.connectedBody = pivot.GetComponent<Rigidbody2D>();
        }
        else
        {
            distanceJoint.connectedBody = null;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) &&  pivot!=null)
        {
            SetGrapplePoint();
            if (distance >= 1.5f)
            {
                distanceJoint.distance = distance;
            }
            else
            {
                distanceJoint.distance = 1.5f;
            }
            distanceJoint.enabled = true;
        }
        float swingDirection=0; 





        if (isSwinging)
        {
            swingDirection = Input.GetAxisRaw("Horizontal");
            

            if (!hasSwang )
            {
                // Applica una forza per far oscillare il giocatore
                if (swingDirection != 0)
                {
                    // Applica una forza per far oscillare il giocatore al punto di attacco del pendolo (pivot)
                    m_rigidbody.AddForce(new Vector2(pendulumForce * swingDirection, 0), ForceMode2D.Force);
                }

                Vector2 lookDirection = pivot.position - transform.position;

                // Calcola l'angolo di rotazione
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

                // Applica la rotazione al personaggio limitandola all'asse Z
               
                if (!player.GetComponent<movement>().facingRight )
                {
                    player.transform.rotation = Quaternion.Euler(180f, 180f, angle);
                }
                else
                {
                    player.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                currentVelocity = m_rigidbody.velocity;
            }

        }

        //Quando il giocatore supera l'altezza del pivot si dà uno slancip verso il basso
        if (pivot != null)
        {
            if (transform.position.y >= pivot.position.y && isSwinging && !hasSwang)
            {
                m_rigidbody.AddForce(Vector2.down * 8f);
            }
        }
        
         if (isSwinging &&  Input.GetKeyUp(KeyCode.Mouse0))
        {
            UndoGrapple();
            
        }

         //condizione che permette al giocatore di mantenere la velocià del dondolio a meno che non si muova in direzione opposta
        if ((currentVelocity.x > 0 && swingDirection < 0) || (currentVelocity.x < 0 && swingDirection > 0))
        {
            if (hasSwang )
            {
                isSwinging = false;
                hasSwang = false;
            }
        }

        if(isSwinging && hasSwang && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hasSwang = false;
        }

        //controllo per far sì che il giocatore non "tocchi terra" mentre dondola
        if(!hasSwang && isSwinging)
        {
            player.GetComponent<movement>().isGrounded = false;
        }

        //una volta a terra torna in una condizione normale
        if (player.GetComponent<movement>().isGrounded)
        {
            isSwinging = false;
            hasSwang = false;
        }
         

        
    }


    
    void SetGrapplePoint()
    {
        Vector2 distanceVector = pivot.transform.position - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
           
                    grapplePoint = pivot.transform.position;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;

            
        }
    }

    public void Grapple()
    {
       
        m_springJoint2D.distance = targetDistance;
        m_springJoint2D.frequency = targetFrequncy;
        m_springJoint2D.connectedAnchor = grapplePoint;
        m_springJoint2D.enabled = true;

       
    }

    public void UndoGrapple()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        distanceJoint.enabled = false;
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_rigidbody.gravityScale = 1;
        //indica che il giocatore ha smesso di dondolare 
        hasSwang = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}
