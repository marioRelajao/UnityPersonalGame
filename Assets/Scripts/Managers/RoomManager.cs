using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose; //Array con las puertas a abrir
    [SerializeField] bool closeDoorOnPlayerEnter, openDoorsNoEnemies;  //Bool que necesito para las condiciones
    [SerializeField] List<Collider2D> enemies = new List<Collider2D>(); //Lista de enemigos a comprobar 

    private Collider2D roomCollider;  //Collider para controlar cantidad enemigos/abir puertas
    private ContactFilter2D contactFilter2D; //Filtrar para poder hacer el Overlap

    // Start is called before the first frame update
    void Start()
    {
        //Get del collider, creamos la mascara y obtenemos array de enemigos
        roomCollider = GetComponent<Collider2D>();
        contactFilter2D.SetLayerMask(LayerMask.GetMask("Enemy"));
        //Mirar la documentacion de OverlapCollider, dada una mascara te da los elementos en una array
        roomCollider.OverlapCollider(contactFilter2D, enemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (closeDoorOnPlayerEnter)
            {
                foreach(GameObject door in doorsToClose)
                {
                    door.SetActive(true);
                }
            }
        }
    }
}
