using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    [SerializeField] Canvas canvasMessage;
    private bool inBuyZone = false;

    enum ItemType { healthRestore, healthUpgrade}
    [SerializeField] ItemType itemType;
    [SerializeField] int itemCost;

    private void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(GameManager.instance.GetCurrentBolivar() >= itemCost)
                {
                    GameManager.instance.SpendBolivares(itemCost);
                    switch (itemType)
                    {
                        case ItemType.healthRestore:
                            FindObjectOfType<PlayerHealthHandler>().AddHpToPlayer(10);
                            break;

                        case ItemType.healthUpgrade:
                            FindObjectOfType<PlayerHealthHandler>().IncreaseMaxHealth(20);
                            break;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(true);
        inBuyZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasMessage.gameObject.SetActive(false);
        inBuyZone = false;
    }
}
