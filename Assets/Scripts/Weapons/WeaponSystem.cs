using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private float shotCounter;
    [SerializeField] float timeBetweenShots; //Añadimos cadencia
    [SerializeField] GameObject bullet; //Para spawnear la bala
    [SerializeField] Transform firePos; //Desde este punto
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerShooting();
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
}
