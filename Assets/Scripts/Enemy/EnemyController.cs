using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed; //Controlamos la velocidad del enemigo
    private Rigidbody2D enemyRigidbody;
    [SerializeField] float playerChaseRange; //A partir de que rango el enemigo nos persigue
    [SerializeField] float playereStopRange; //A partir de que rango el enemigo se aburre

    private Vector3 directionToMove; //Direccion a la que se va a mover el moñeco
    private bool isChasing;
    private Transform playerToChase;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<PlayerController>().transform; //Encuentra el player y guarda el transform
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange) //Si la pos del jugador esta dentro del rango de busqueda
        {
            directionToMove = playerToChase.position-transform.position; //Formula similar a lo que usamos para apuntar
            isChasing = true;
            Debug.Log("Jugador en rango");
        }
        else if (Vector3.Distance(transform.position, playerToChase.position) < playereStopRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position; //Formula similar a lo que usamos para apuntar

        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
            Debug.Log("Jugador fuera del rango");
        }
        directionToMove.Normalize();
        enemyRigidbody.velocity = directionToMove * enemySpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange); //Asi vemos el rango en el que nos ve el enemigo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playereStopRange);
    }
}
