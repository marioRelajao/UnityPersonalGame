using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletImpactEffect; //Efecto de la espada rompiendo


    private Vector3 playerDirection; //Para ubicar la posicion del jugador

    // Start is called before the first frame update
    void Start()
    {
        //Lo hacemos en el start pq no queremos que la bala le siga
        playerDirection = FindObjectOfType<PlayerController>().transform.position - transform.position;
        playerDirection.Normalize(); //No queremos que vaya mas rapido si va diagonalmente
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Si la colision es con un elemento con tag Player
        {
            //Instanciamos la anim
            Debug.Log("Player hit");
        }
        Instantiate(bulletImpactEffect.transform, transform.position, transform.localRotation);

        Destroy(gameObject); //Una vez hecho todo, limpiamos
    }
}
