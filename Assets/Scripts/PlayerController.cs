using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed; //Podemos acceder a el desde el inspector, pero otros scripts no

    private Vector2 movementInput; //Vector de 2 dimensiones [X,Y] para el movimiento
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); //El eje X se guia por el input llamado "Horizontal"
        movementInput.y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(movementInput.x, movementInput.y, 0f)*Time.deltaTime*movementSpeed;
    }
}
