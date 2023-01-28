using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{

    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] float itemDropChance = 2;

    public void DropItem()
    {
        float i = Random.value;
        Debug.Log("Valor del Random: " + i);

        if ( i< itemDropChance)
        {
            Debug.Log("Deberia soltar moneda");
            int randomItemNumber = Random.Range(0, itemsToDrop.Length);
            Instantiate(itemsToDrop[0], transform.position, transform.rotation);
        }
    }
}
