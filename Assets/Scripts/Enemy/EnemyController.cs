using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed; //Controlamos la velocidad del enemigo
    [SerializeField] float playerChaseRange; //A partir de que rango el enemigo nos persigue
    [SerializeField] float playereStopRange; //A partir de que rango el enemigo se aburre
    [SerializeField] int enemyHealth = 100;
    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator;
    private Vector3 directionToMove; //Direccion a la que se va a mover el moñeco
    private bool isChasing;
    private Transform playerToChase;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        //Encuentra el player y guarda el transform
        playerToChase = FindObjectOfType<PlayerController>().transform; 
        //Como el script esta en Enemy, pero el animator en Body(children) lo tenemos que buscar asi
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si la pos del jugador esta dentro del rango de busqueda
        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange) 
        {
            //Formula similar a lo que usamos para apuntar
            directionToMove = playerToChase.position-transform.position; 
            isChasing = true;
            Debug.Log("Jugador en rango");
        }
        else if (Vector3.Distance(transform.position, playerToChase.position) < playereStopRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position; 

        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
            Debug.Log("Jugador fuera del rango");
        }
        directionToMove.Normalize();
        enemyRigidbody.velocity = directionToMove * enemySpeed;

        //Para controlar el isWalking bool
        if(directionToMove != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }

        //Rotar el sprite, similar al player pero ahora comparamos con la posicion del jugador
        if (playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public void DamageEnemy(int dmgTaken)
    {
        enemyHealth -= dmgTaken;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange); //Asi vemos el rango en el que nos ve el enemigo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playereStopRange);
    }
}
