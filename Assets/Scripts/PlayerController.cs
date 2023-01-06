using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed; //Podemos acceder a el desde el inspector, pero otros scripts no

    [SerializeField] Rigidbody2D playerRigidbody; //Para poder controlar el movimiento del pana

    [SerializeField] Transform weaponArm; //Apuntar arma

    private Camera mainCamera;
    private Vector2 movementInput; //Vector de 2 dimensiones [X,Y] para el movimiento
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; //Referenciamos a la camara principal
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); //El eje X se guia por el input llamado "Horizontal"
        movementInput.y = Input.GetAxisRaw("Vertical");
        playerRigidbody.velocity = movementInput * movementSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition); //Punto de la componente Transform de ese objeto

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angulo = Mathf.Atan2(offset.y,offset.x)*Mathf.Rad2Deg; //Retorna rad, necesitamos grados
        weaponArm.rotation = Quaternion.Euler(0, 0, angulo);
    }
}
