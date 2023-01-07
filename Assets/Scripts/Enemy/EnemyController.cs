using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed; //Controlamos la velocidad del enemigo
    private Rigidbody2D enemyRigidbody;
    [SerializeField] float playerChaseRange; //A partir de que rango el enemigo nos persigue
    private Vector3 directionToMove; //Direccion a la que se va a mover el moñeco

    private Transform playerToChase;
    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform; //Encuentra el player y guarda el transform
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange) //Si la pos del jugador esta dentro del rango de busqueda
        {
            Debug.Log("Jugador en rango");
        }
        else
        {
            Debug.Log("Jugador fuera del rango");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange); //Asi vemos el rango en el que nos ve el enemigo
    }
}
