using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed; //Podemos acceder a el desde el inspector, pero otros scripts no
    [SerializeField] GameObject bullet; //Para spawnear la bala
    [SerializeField] Transform firePos; //Desde este punto
    [SerializeField] Rigidbody2D playerRigidbody; //Para poder controlar el movimiento del pana
    [SerializeField] float timeBetweenShots; //Añadimos cadencia
    [SerializeField] Transform weaponArm; //Apuntar arma

    private Animator playerAnimator;
    private Camera mainCamera;
    private Vector2 movementInput; //Vector de 2 dimensiones [X,Y] para el movimiento
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; //Referenciamos a la camara principal
        playerAnimator = GetComponent<Animator>(); //Ahora tenemos referencia al Animator del Player
        shotCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------------MOVIMIENTO DEL JUGADOR----------------------------------
        PlayerMoving();

        //----------------------------------ROTAR ARMA----------------------------------
        PointingGunAt();

        //----------------------------------BOOL ANIM----------------------------------
        PlayerAnimator();

        //----------------------------------Disparar----------------------------------
        PlayerShooting();

    }

    private void PlayerAnimator()
    {
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }
    }

    private void PointingGunAt()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition); //Punto de la componente Transform de ese objeto

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angulo = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; //Retorna rad, necesitamos grados
        weaponArm.rotation = Quaternion.Euler(0, 0, angulo);

        //----------------------------------ROTAR SPRITE----------------------------------
        if (mousePos.x < screenPoint.x)//Miramos a la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else//Reiniciamos la orientacion
        {
            transform.localScale = Vector3.one;//Seteamos todo a 1
            weaponArm.localScale = Vector3.one;
        }
    }

    private void PlayerShooting()
    {
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bullet, firePos.position, firePos.rotation);//Instanciamos la bala con la pos y rotacion correctas
                shotCounter = timeBetweenShots;
            }
        }
        shotCounter -= Time.deltaTime;
    }

    private void PlayerMoving()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); //El eje X se guia por el input llamado "Horizontal"
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();//Normalizamos movimiento diagonal
        playerRigidbody.velocity = movementInput * movementSpeed;
    }
}
