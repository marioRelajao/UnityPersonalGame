using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed; //Controlamos la velocidad del enemigo
    [SerializeField] float playerChaseRange; //A partir de que rango el enemigo nos persigue
    [SerializeField] float playerShootRange; //A partir de que rango el enemigo nos dispara
    [SerializeField] float playereStopRange; //A partir de que rango el enemigo se aburre
    [SerializeField] int enemyHealth = 100; //Vida, queremos recogerla de BBDD
    [SerializeField] bool esMelee; //Es a melee o pega a distancia?
    [SerializeField] GameObject enemyProjectile; //Que objeto instanciamos cuando el moñeco ataque a distancia
    [SerializeField] Transform firePosition; //Punto de donde instanciamos el proyectil
    [SerializeField] float timeBetweenShots; //Como en player, cadencia
    [SerializeField] float wanderLength, pauseLength;

    private float wanderCounter=2, pauseCounter=2;
    private Vector3 wanderDirection;
    private bool readyToShoot; //El enemigos es capaz de disparar
    private Rigidbody2D enemyRigidbody;
    private Animator enemyAnimator;
    private Vector3 directionToMove; //Direccion a la que se va a mover el moñeco
    private bool isChasing; //Si hemos entrado en los circulos de deteccion
    private Transform playerToChase; //Object al que hacemos chase si entra en rango
    private SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        EnemySetUp();
        pauseCounter = Random.Range(pauseCounter * 0.5f, pauseCounter * 1.25f);

    }

    private void EnemySetUp()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        //Encuentra el player y guarda el transform
        playerToChase = FindObjectOfType<PlayerController>().transform;
        //Como el script esta en Enemy, pero el animator en Body(children) lo tenemos que buscar asi
        enemyAnimator = GetComponentInChildren<Animator>();
        readyToShoot = true; //Cuando aparece puede disparar
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMoving();

        EnemyAnimator();

        EnemyRotate();

        EnemyShoot();
    }

    private void EnemyShoot()
    {
        //----------COROUTINE--------------------
        if (!esMelee &&
            readyToShoot &&
            Vector3.Distance(transform.position, playerToChase.position) < playerShootRange) //Si no somos enemigos a melee y podemos disparar
        {
            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile()); //En lugar de usar un counter, le pasamos el control a la coroutine y asi no la ejecutamso cada frame
        }
    }

    private void EnemyRotate()
    {
        //Rotar el sprite, similar al player pero ahora comparamos con la posicion del jugador
        if (playerToChase.position.x < transform.position.x)
        {
            //renderer.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            //renderer.flipX = false;
            transform.localScale = Vector3.one;
        }
    }

    private void EnemyAnimator()
    {
        //Para controlar el isWalking bool
        if (directionToMove != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }
    }

    private void EnemyMoving()
    {
        //Si la pos del jugador esta dentro del rango de busqueda
        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            //Formula similar a lo que usamos para apuntar
            directionToMove = playerToChase.position - transform.position;
            isChasing = true;
        }
        else if (Vector3.Distance(transform.position, playerToChase.position) < playereStopRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position;

        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
        }
       // Debug.Log(pauseCounter);
        if (wanderCounter > 0 && !isChasing)
        {
            //Debug.Log("Deberia de patrullar");
            wanderCounter -= Time.deltaTime;

            directionToMove = wanderDirection;

            if (wanderCounter <= 0)
            {
                pauseCounter = Random.Range(pauseLength * 0.5f, pauseLength * 1.25f);
            }
        }

        if (pauseCounter > 0)
        {
            pauseCounter -= Time.deltaTime;
           // Debug.Log("Dentro pause counter");

            if (pauseCounter <= 0)
            {
                wanderCounter = Random.Range(wanderLength * 0.5f, wanderLength * 1.25f);
                wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            }
        }
        directionToMove.Normalize();
        enemyRigidbody.velocity = directionToMove * enemySpeed;
    }

    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        if (playerToChase.gameObject.activeInHierarchy) //Que no nos disparen si ya estamos muertos
        {
            Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
            readyToShoot = true;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerShootRange);
    }
}
