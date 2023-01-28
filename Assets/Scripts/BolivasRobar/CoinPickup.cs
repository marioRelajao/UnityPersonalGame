using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] int bolivarAmount = 10;
    [SerializeField] int sfxToPlay = 3;

    private bool pickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!pickedUp)
        {
            pickedUp = true;
            GameManager.instance.GetBolivares(bolivarAmount);
            AudioManager.instance.PlaySFX(sfxToPlay);
            Destroy(gameObject);
        }
    }
}
