using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthSlider.maxValue = maxHealth;
        UpdatePlayerHealth();
    }

    public void DamagePlayer(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        UpdatePlayerHealth();

        if (currentHealth <= 0)
        {
            //Muelto
            Debug.Log("Muelto");
            gameObject.SetActive(false);
        }
    }
    private void UpdatePlayerHealth()
    {
        UIManager.instance.healthText.text = currentHealth + "/" + maxHealth; //Cambiamos el valor del texto de la vida
        UIManager.instance.healthSlider.value = currentHealth;//Movemos el slider
    }
}
