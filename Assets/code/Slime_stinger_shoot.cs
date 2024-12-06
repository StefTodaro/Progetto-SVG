using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Slime_stinger_shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proiettile
    public Transform firePoint; // Punto di uscita del proiettile
    public float bulletSpeed = 10f; // Velocità del proiettile
    private float nextShot=0.15f;
    private float fireDelay = 1f;
    private float liveTimer;

    public AudioClip shotAudio;

    void Update()
    {   
       
        // Verifica se è stato premuto il tasto sinistro del mouse
        if (Input.GetMouseButtonDown(0) && Time.time >nextShot 
            && !GameManager_logic.Instance.GetInactive())
        {
            // Calcola la direzione del mouse rispetto al giocatore
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 fireDirection = (mousePosition - (Vector2)transform.position).normalized;

            // Sparare il proiettile nella direzione del mouse
            Shoot(fireDirection);
            gameObject.GetComponent<Animator>().SetBool("shoot", true);
            nextShot = Time.time + fireDelay;
        }
    }

    void Shoot(Vector2 direction)
    {

        SoundEffectManager.Instance.PlaySoundEffect(shotAudio, transform, 0.5f);
        // Crea un nuovo proiettile dalla prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Ottieniamo l'angolo di rotazione in radianti
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ruotiamo lo sprite del proiettile di 90 gradi in senso orario
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

        // Ottieniamo il componente Rigidbody2D del proiettile
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Applica la velocità al proiettile nella direzione desiderata
        rb.velocity = direction * bulletSpeed;

        liveTimer+= Time.deltaTime;
    }

    public void EndShoot()
    {
        gameObject.GetComponent<Animator>().SetBool("shoot", false);
    }
}